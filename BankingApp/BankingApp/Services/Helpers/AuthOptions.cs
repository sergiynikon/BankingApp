using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IO;

namespace Services.Helpers
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
