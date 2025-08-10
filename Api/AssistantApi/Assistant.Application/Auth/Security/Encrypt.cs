using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Assistant.Application.Auth.Security
{
    public class Encrypt : IEncrypt
    {
        public string HashPassword(string password, string salt)
        {
            var iterationCount = 5000;
            var numBytesRequested = 512 / 8;

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
                Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA512,
                iterationCount,
                numBytesRequested));
        }
    }
}
