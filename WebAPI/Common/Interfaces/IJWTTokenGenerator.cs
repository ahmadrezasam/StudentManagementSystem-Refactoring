using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Common.Interfaces
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(int userId,string username);
    }
}