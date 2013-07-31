using System;
using System.Security.Cryptography;
using System.Text;

namespace ThursdayAfternoon.Infrastructure.Services.Security
{
    public class EncryptionService : IEncryptionService
    {
        public string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            string saltAndPassword = string.Concat(password, saltkey);
            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name", "hashName");
            }
            byte[] hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));

            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }
    }
}