using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DragonExApiExamples.Api.Common
{
    class EncryptUtil
    {
        /// <summary>
        /// Encrypt using SHA1
        /// </summary>
        /// <param name="data">data</param>
        /// <returns>Encrypt data</returns>
        public static string EncryptSHA1(string data)
        {
            return SHA1(data, Encoding.UTF8);
        }

        /// <summary>  
        /// SHA1 Encrypt
        /// </summary>  
        /// <param name="content">Content</param>  
        /// <param name="encode">Encoding</param>  
        /// <returns>Encrypted string</returns>  
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1 encrypt error happened：" + ex.Message);
            }
        }
    }
}
