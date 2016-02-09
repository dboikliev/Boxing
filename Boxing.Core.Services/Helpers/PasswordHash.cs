using System;
using System.Security.Cryptography;
using System.Text;

namespace Boxing.Core.Services.Helpers
{
    internal static class PasswordHash
    {
        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private static readonly SHA512 _sha512 = SHA512.Create();

        internal static byte[] GenerateSalt()
        {
            byte[] saltBuffer = new byte[1024];
            _rng.GetBytes(saltBuffer);
            return saltBuffer;
        }

        internal static byte[] GenerateSaltedHash(string password, byte[] salt)
        {
            byte[] passwordBuffer = Encoding.UTF8.GetBytes(password);
            byte[] saltedBuffer = new byte[passwordBuffer.Length + salt.Length];

            Buffer.BlockCopy(passwordBuffer, 0, saltedBuffer, 0, passwordBuffer.Length);
            Buffer.BlockCopy(salt, 0, saltedBuffer, passwordBuffer.Length, salt.Length);

            byte[] hashed = _sha512.ComputeHash(saltedBuffer);
            return hashed;
        }
    }
}
