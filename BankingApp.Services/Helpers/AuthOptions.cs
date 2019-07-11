using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.Services.Helpers
{
    public static class AuthOptions
    {
        private static IConfiguration ConstantsConfig { get; } = SetConfiguration()
            .GetSection("Constants")
            .GetSection("ForAuthOptions");

        private static IConfiguration SetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        private static readonly string Key = ConstantsConfig["Key"];

        public static string Issuer = ConstantsConfig["Issuer"];
        public static string Audience = ConstantsConfig["Audience"];

        public static int Lifetime = int.Parse(ConstantsConfig["LifeTime"]);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
