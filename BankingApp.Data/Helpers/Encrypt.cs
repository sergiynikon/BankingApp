using System.Security.Cryptography;
using System.Text;

namespace BankingApp.Data.Helpers
{
    public class Encrypt
    {
        public static string GetHash(string input)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hash)
                {
                    //format to hexadecimal string with length at least 2
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}