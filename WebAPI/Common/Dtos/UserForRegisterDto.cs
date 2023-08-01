using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        //Validate password to be between 4 to 8 charachters
        // [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8")]
        public string password { get; set; }

    }
}