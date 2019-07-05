namespace BankingApp.DataTransfer
{
    public class ResultDto
    {
        private const string DefaultErrorMessage = "Something went wrong";
        private static readonly string SuccessMessage = "Success";

        private ResultDto(bool isSuccess, string message, object result = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Result = result;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
        public object Result { get; }

        public static ResultDto Success(object result = null)
        {
            return new ResultDto(
                isSuccess: true,
                message: SuccessMessage,
                result: result);
        }

        public static ResultDto Error(string errorMessage, object result = null)
        {
            return new ResultDto(
                isSuccess: false,
                message: errorMessage,
                result: result);
        }
    }
}