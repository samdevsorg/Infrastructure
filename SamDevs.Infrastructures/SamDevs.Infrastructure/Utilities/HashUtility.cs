using System;
using System.Security.Cryptography;
using System.Text;

namespace SamDevs.Infrastructure.Utilities
{
    public class HashUtility
    {
        public static string ComputeHash(string text, byte[] salt = null)
        {
            if (salt == null)
            {
                var rand = new Random();
                var saltSize = rand.Next(16, 24);
                salt = new byte[saltSize];
                var rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(salt);
            }

            var textBytes = Encoding.UTF8.GetBytes(text);
            var rfc = new Rfc2898DeriveBytes(textBytes, salt, 10000);
            var hash = rfc.GetBytes(32);
            var hashWithSalt = new byte[hash.Length + salt.Length];
            Array.Copy(salt, 0, hashWithSalt, 0, salt.Length);
            Array.Copy(hash, 0, hashWithSalt, salt.Length, hash.Length);

            return Convert.ToBase64String(hashWithSalt);

        }

        public static bool VerifyHash(string text, string hashValue)
        {
            var hashWithSalt = Convert.FromBase64String(hashValue);
            if (hashWithSalt.Length < 48 || hashWithSalt.Length > 56)
                return false;
            var salt = new byte[hashWithSalt.Length - 32];
            Array.Copy(hashWithSalt, 0, salt, 0, salt.Length);
            var hash = ComputeHash(text, salt);
            return hash == hashValue;
        }

        public static string GeneratePassword(int length)
        {
            var valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
