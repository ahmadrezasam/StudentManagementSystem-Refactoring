using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.API.Data;
using API.Authorization;
using API.Common.Interfaces;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private DataContext _context;
        private IInstructorAuthRepository _repo;
        private IConfiguration _confing;
        private IJWTTokenGenerator _tokenGenerator;

        public AuthController(DataContext context, IInstructorAuthRepository repo, IConfiguration config, IJWTTokenGenerator tokenGenerator)
        {
            _context = context;
            _repo = repo;
            _confing = config;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("instructor/register")]
        public async Task<IActionResult> Register(UserForRegisterDto instructor)
        {
            instructor.Username = instructor.Username.ToLower();

            if (await _repo.InstructorExist(instructor.Username))
            {
                return BadRequest("Username already exist.");
            }

            var instructorToCreate = new Instructor
            {
                InstructorId = instructor.Id,
                Username = instructor.Username
            };

            var createdInstructor = await _repo.InstructorRegister(instructorToCreate, instructor.password);

            return StatusCode(201);
        }

        [HttpPost("instructor/login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var instructorFromRepo = await _repo.InstructorLogin(userForLoginDto.Username, userForLoginDto.password);

            if (instructorFromRepo == null)
                return Unauthorized();

            var token = _tokenGenerator.GenerateToken(instructorFromRepo.InstructorId, instructorFromRepo.Username);
            
            return Ok(new {token});
            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.NameIdentifier, instructorFromRepo.InstructorId.ToString()),
            //     new Claim(ClaimTypes.Name, instructorFromRepo.Username)
            // };

            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confing.GetSection("AppSettings:Token").Value));
            // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // var tokenDscriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(claims),
            //     Expires = DateTime.Now.AddDays(1),
            //     SigningCredentials = creds
            // };

            // var tokenHandler = new JwtSecurityTokenHandler();
            // var token = tokenHandler.CreateToken(tokenDscriptor);

            // return Ok(new
            // {
            //     token = tokenHandler.WriteToken(token)
            // });
        }
    }
}