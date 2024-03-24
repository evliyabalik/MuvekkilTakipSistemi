using System.Security.Cryptography;
using System.Text;

namespace MuvekkilTakipSistemi.Helper
{
    public class HashHelper
    {
        public static string GetMd5Hash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
