using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.API.Data;
using API.Common.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Authorization
{
    public class InstructorAuthRepository : IInstructorAuthRepository
    {
        private readonly DataContext _context;

        public InstructorAuthRepository(DataContext context)
        {
            _context = context;
        }

        public string GenerateToken(Guid userId, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InstructorExist(string username)
        {
            if (await _context.Instructors.AnyAsync(s => s.Username == username)) return true;
            return false;
        }

        public async Task<Instructor> InstructorLogin(string username, string password)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(s => s.Username == username);

            if (instructor == null)
            {
                return null; // Instructor not found
            }

            if (!PasswordHelper.VerifyPasswordHashWrapper(password, instructor.PasswordHash, instructor.PasswordSalt))
            {
                return null; // Invalid password
            }

            return instructor; // Login successful        }
        }

        public async Task<Instructor> InstructorRegister(Instructor instructor, string password)
        {

            // if (!(person is Instructor instructor))
            // {
            //     throw new ArgumentException("The person must be an instructor.");
            // }

            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHashWrapper(password, out passwordHash, out passwordSalt);

            instructor.PasswordHash = passwordHash;
            instructor.PasswordSalt = passwordSalt;

            await _context.Instructors.AddAsync(instructor);  // Insert the isntructor into DB
            await _context.SaveChangesAsync();

            return instructor;  // Login successful      
        }
    }
}