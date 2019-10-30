using System;
using System.Security.Cryptography;
using System.Text;

namespace SamDevs.Infrastructure.Utilities
{
    public class TripleDesUtility
    {
        public static string Encrypt(string textToEncrypt, string key = null)
        {
            var iv = new byte[] { 124, 25, 251, 182, 109, 78, 173, 39 };
            if (string.IsNullOrWhiteSpace(key))
                key = "Hr7!l#p;$%&-=TnjW201";
            var buffer = Encoding.UTF8.GetBytes(textToEncrypt);
            using var triple = new TripleDESCryptoServiceProvider();
            var md5 = new MD5CryptoServiceProvider();
            triple.IV = iv;
            triple.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var encodeText = triple.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length);
            var user = Convert.ToBase64String(encodeText);
            return user;
        }
        public static string Decrypt(string encryptedText, string key = null)
        {
            var iv = new byte[] { 124, 25, 251, 182, 109, 78, 173, 39 };
            if (string.IsNullOrWhiteSpace(key))
                key = "Hr7!l#p;$%&-=TnjW201";
            var buffer = Convert.FromBase64String(encryptedText);
            using var triple = new TripleDESCryptoServiceProvider();
            var md5 = new MD5CryptoServiceProvider();
            triple.IV = iv;
            triple.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var decodeText = triple.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(decodeText);
        }
    }
}
