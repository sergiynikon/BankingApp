using System;
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
        private OperationDetailsDto(bool isSuccess, string message, double amount, string senderUserEmail, string receiverUserEmail)
        {
            IsSuccess = isSuccess;
            Message = message;
            Amount = amount;
            SenderUserEmail = senderUserEmail;
            ReceiverUserEmail = receiverUserEmail;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
        public double Amount { get; }

        public string SenderUserEmail { get; }
        public string ReceiverUserEmail { get; }

        public static OperationDetailsDto Error(string senderUserEmail, string errorMessage = Constants.DefaultErrorMessage)
        {
            return new OperationDetailsDto(
                isSuccess: false,
                message: errorMessage,
                amount: 0);
        }

        public static OperationDetailsDto Success(string senderUserEmail, string receiverUserEmail, double amount)
        {
            return new OperationDetailsDto(
                isSuccess: true,
                message: Constants.SuccessMessage,
                amount: amount,
                senderUserEmail: senderUserEmail,
                receiverUserEmail: receiverUserEmail);
        }

    }
}
