using BankingApp.DataTransfer;

namespace BankingApp.Services.Interfaces
{
    public interface IAuthenticateService
    {
        ResultDto GetIdentityToken(LoginDto identity);
        ResultDto RegisterUser(RegisterDto identity);
    }
}
