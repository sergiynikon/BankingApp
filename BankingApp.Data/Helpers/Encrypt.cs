using System.Security.Cryptography;
using System.Text;

namespace BankingApp.Data.Helpers
{
    public class Encrypt
    {
        public static string GetHash(string input)
        {
            using (MD5CryptoServiceProvider mdS = new MD5CryptoServiceProvider())
            {
                byte[] b = Encoding.UTF8.GetBytes(input);
                b = mdS.ComputeHash(b);

                StringBuilder sb = new StringBuilder();

                foreach (byte x in b)
                {
                    sb.Append(x.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}