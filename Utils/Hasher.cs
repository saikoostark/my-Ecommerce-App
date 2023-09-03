using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace my_Ecommerce_App.Utils
{
    public class Hasher
    {
        public static string GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string password, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}