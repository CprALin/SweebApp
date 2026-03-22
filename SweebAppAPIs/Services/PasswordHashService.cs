

using Konscious.Security.Cryptography;
using SweebAppAPIs.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SweebAppAPIs.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(32);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = 1,
                Iterations = 2,
                MemorySize = 64 * 1024 
            };

            byte[] hash = argon2.GetBytes(32);

   
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                DegreeOfParallelism = 1,
                Iterations = 2,
                MemorySize = 64 * 1024
            };

            byte[] actualHash = argon2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
