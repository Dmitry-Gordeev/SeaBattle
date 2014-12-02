using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SeaBattle.Utils
{
    public class HashHelper
    {
        /// <summary>
        /// выдаёт последовательность из 32 шестнадцатеричных цифр (md5 хеш от аргумента)
        /// </summary>
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (byte elem in data)
            {
                sBuilder.Append(elem.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
