using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Person
    {
        // Person's first name
        public string? FirstName { get; set; }

        // Person's last name
        public string? LastName { get; set; }

        // Person's username to login
        public string? Username { get; set; }

        // Password Hash of the person's password
        public byte[]? PasswordHash { get; set; }
        
        // Password salt 
        public byte[]? PasswordSalt { get; set; }

        // Person's date of birth
        public DateTime DateOfBirth { get; set; }

        // Person's email address
        public string? Email { get; set; }

        // Person's phone number
        public string? PhoneNumber { get; set; }

        // Person's address information
        public string? Address { get; set; }
    }
}