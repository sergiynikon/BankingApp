using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.Services.Helpers
{
    public class AuthOptions
    {
        //TODO: move all configurations to appsettings.json and add it using configuration

        public const string Issuer = "BankingApp";
        public const string Audience = "https://localhost:4000";

        private const string Key = "ethuasb-ythu2-3uhs4ithukl3sth2--elk";

        public const int Lifetime = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

    }
}
