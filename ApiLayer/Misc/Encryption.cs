using System;
using System.Text;
using System.Security.Cryptography;
using Olahrago.ApiLayer.Misc.Interface;

namespace Olahrago.ApiLayer.Misc
{
    public class Encryption : IEncryption
    {
        public string EncryptPassword(string username, string password)
        {
            string result = string.Empty;

            result = GetMd5Hash(string.Concat(username, password));

            return result;
        }

        public bool VerifyEncryption(string username, string password)
        {
            bool result = false;

            string key = string.Concat(username, password);

            string hash = GetMd5Hash(key);

            result = VerifyMd5Hash(key, hash);

            return result;
        }

        private static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private static bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
