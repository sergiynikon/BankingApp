using BankingApp.DataTransfer.Helpers;

namespace BankingApp.DataTransfer
{
    public class AuthenticationDetailsDto
    {
        private AuthenticationDetailsDto(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        public string Message { get; }

        public static AuthenticationDetailsDto Error(string errorMessage = Constants.DefaultErrorMessage)
        {
            return new AuthenticationDetailsDto(
                isSuccess: false,
                message: errorMessage);
        }

        public static AuthenticationDetailsDto Success()
        {
            return new AuthenticationDetailsDto(
                isSuccess: true,
                message: Constants.SuccessMessage);
        }
        
    }
}