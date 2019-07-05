namespace BankingApp.DataTransfer
{
    public class AuthenticationDetailsDto
    {
        private const string DefaultErrorMessage = "Something went wrong";
        private static readonly string SuccessMessage = "Success";

        private AuthenticationDetailsDto(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        public string Message { get; }

        public static AuthenticationDetailsDto Error(string errorMessage = DefaultErrorMessage)
        {
            return new AuthenticationDetailsDto(
                isSuccess: false,
                message: errorMessage);
        }

        public static AuthenticationDetailsDto Success()
        {
            return new AuthenticationDetailsDto(
                isSuccess: true,
                message: SuccessMessage);
        }
        
    }
}