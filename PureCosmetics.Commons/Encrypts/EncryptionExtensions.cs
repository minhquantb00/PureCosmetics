using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Encrypts
{
    /// <summary>
    /// Provides extension methods for generating encrypted hashes, code challenges, and MD5 hashes for authentication
    /// and security-related operations.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>This class includes static methods for password hashing with salt, MD5 hashing, and
    /// generating code challenges suitable for OAuth 2.0 PKCE flows. All methods are stateless and thread-safe. The
    /// class is intended for use in scenarios where secure password storage, verification, or code challenge generation
    /// is required.</remarks>
    public class EncryptionExtensions
    {
        /// <summary>
        /// Generates a hashed representation of the specified password using a randomly generated salt and a prefix
        /// value.
        /// </summary>
        /// <remarks>The method uses PBKDF2 with HMACSHA256 and 10,000 iterations to derive the hash. The
        /// returned hash and salt should be stored together to allow password verification. The salt is unique for each
        /// invocation, ensuring that identical passwords produce different hashes.</remarks>
        /// <param name="prefix">A string value to prepend to the password before hashing. Used to add contextual information or namespace to
        /// the password.</param>
        /// <param name="password">The password to be hashed. This value is combined with the prefix and processed using a key derivation
        /// function.</param>
        /// <param name="saltKey">When this method returns, contains the randomly generated salt used for hashing, encoded as a Base64 string.</param>
        /// <returns>A Base64-encoded string containing the hashed result of the prefixed password using the generated salt.</returns>
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
        
        /// <summary>
        /// Generates a hashed representation of the specified password using a prefix and a salt key.
        /// </summary>
        /// <remarks>This method uses PBKDF2 with HMACSHA256 and 10,000 iterations to derive the hash. The
        /// same combination of prefix, password, and saltKey will always produce the same output. The saltKey must be
        /// securely generated and stored to ensure password security.</remarks>
        /// <param name="prefix">A string value to prepend to the password before hashing. Used to distinguish different password contexts or
        /// scopes.</param>
        /// <param name="password">The password to be hashed. This value is combined with the prefix and used as input for the key derivation
        /// function.</param>
        /// <param name="saltKey">A base64-encoded string representing the cryptographic salt to use in the hashing process. Must be a valid
        /// base64 string.</param>
        /// <returns>A base64-encoded string containing the hashed result of the combined prefix and password using the provided
        /// salt key.</returns>
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

        /// <summary>
        /// Computes the MD5 hash of the specified input string and returns the result as an ASCII-encoded string.
        /// </summary>
        /// <remarks>The returned string is the raw ASCII representation of the hash bytes, not a
        /// hexadecimal string. For interoperability or display purposes, consider converting the hash to a hexadecimal
        /// format.</remarks>
        /// <param name="input">The input string to be hashed using the MD5 algorithm. Cannot be null.</param>
        /// <returns>An ASCII-encoded string representing the MD5 hash of the input.</returns>
        public static string MD5Hash(string input)
        {
            using var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Encoding.ASCII.GetString(result);
        }   

        /// <summary>
        /// Generates a code challenge string from the specified code verifier using the SHA-256 hash algorithm and
        /// Base64 URL encoding, as required by the OAuth 2.0 PKCE protocol.
        /// </summary>
        /// <remarks>This method implements the transformation defined by the OAuth 2.0 Proof Key for Code
        /// Exchange (PKCE) standard. The returned code challenge can be used in authorization requests to enhance
        /// security for public clients. The input should conform to PKCE requirements for code verifiers.</remarks>
        /// <param name="codeVerifier">The code verifier to be transformed into a code challenge. Must be a non-empty string consisting of valid
        /// characters as defined by the PKCE specification.</param>
        /// <returns>A Base64 URL-encoded string representing the code challenge derived from the input code verifier.</returns>
        public static string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(codeVerifier);   
            byte[] hash = sha256.ComputeHash(bytes);
            string challenge = Base64UrlEncode(hash);
            return challenge;
        }

        /// <summary>
        /// Encodes the specified byte array to a Base64 URL-safe string without padding.
        /// </summary>
        /// <remarks>This method produces a string suitable for use in URLs and web tokens by replacing
        /// '+' with '-', '/' with '_', and omitting '=' padding characters. The caller is responsible for ensuring the
        /// input is not null.</remarks>
        /// <param name="input">The byte array to encode. Cannot be null.</param>
        /// <returns>A Base64 URL-safe string representation of the input data, with padding removed and characters replaced to
        /// be safe for URLs.</returns>
        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace("+", "-")
                .Replace("/", "_");
        }   
    }
}
