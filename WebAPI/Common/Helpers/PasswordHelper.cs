using System.Security.Cryptography;
using API.API.Data;
using API.Models;

namespace API.Authorization
{
    public static class PasswordHelper
    {
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void CreatePasswordHashWrapper(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
        }

        public static bool VerifyPasswordHashWrapper(string password, byte[] storedHash, byte[] storedSalt)
        {
            return VerifyPasswordHash(password, storedHash, storedSalt);
        }
    }
}