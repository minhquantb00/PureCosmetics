using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Encrypts
{
    public class EncryptionExtensions
    {
        public static string Encryption(string prefix, string password, out string saltKey)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            saltKey = Convert.ToBase64String(salt);
            string input = $"{prefix}---{password}";
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        
        public static string Encryption(string prefix, string password, string saltKey)
        {
            byte[] salt = Convert.FromBase64String(saltKey);
            string input = $"{prefix}---{password}";
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public static string MD5Hash(string input)
        {
            using var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Encoding.ASCII.GetString(result);
        }   

        public static string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(codeVerifier);   
            byte[] hash = sha256.ComputeHash(bytes);
            string challenge = Base64UrlEncode(hash);
            return challenge;
        }

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace("+", "-")
                .Replace("/", "_");
        }   
    }
}
