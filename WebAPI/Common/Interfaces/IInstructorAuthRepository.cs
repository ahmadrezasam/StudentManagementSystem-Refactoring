using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Authorization
{
    public interface IInstructorAuthRepository
    {
        //User Authorization
        Task<bool> InstructorExist(string username);
        Task<Instructor> InstructorLogin(string username, string password);
        Task<Instructor> InstructorRegister(Instructor instructor, string password);
    }
}