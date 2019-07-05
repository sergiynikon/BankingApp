using BankingApp.DataTransfer.Helpers;

namespace BankingApp.DataTransfer
{
    public class OperationDetailsDto
    {
        private OperationDetailsDto(bool isSuccess, string message, double amount)
        {
            IsSuccess = isSuccess;
            Message = message;
            Amount = amount;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
        public double Amount { get; }

        public static OperationDetailsDto Error(string errorMessage = Constants.DefaultErrorMessage)
        {
            return new OperationDetailsDto(
                isSuccess: false,
                message: errorMessage,
                amount: 0);
        }

        public static OperationDetailsDto Success(double amount)
        {
            return new OperationDetailsDto(
                isSuccess: true,
                message: Constants.SuccessMessage,
                amount: amount);
        }
    }
}
