using System;

namespace BankingApp.DataTransfer
{
    public class OperationDetailsDto
    {
        private const string DefaultErrorMessage = "Something went wrong";
        private static readonly string SuccessMessage = "Success";

        private OperationDetailsDto(bool isSuccess, string message, double amount, Guid senderUserId, Guid receiverUserId)
        {
            IsSuccess = isSuccess;
            Message = message;
            Amount = amount;
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
        }

        private OperationDetailsDto(bool isSuccess, string message, double amount, Guid senderUserId)
        {
            IsSuccess = isSuccess;
            Message = message;
            Amount = amount;
            SenderUserId = senderUserId;
        }

        public bool IsSuccess { get; }
        public string Message { get; }
        public double Amount { get; }
        public Guid SenderUserId { get; }
        public Guid ReceiverUserId { get; }

        public static OperationDetailsDto Error(Guid senderUserId, Guid receiverUserId, string errorMessage = DefaultErrorMessage)
        {
            return new OperationDetailsDto(
                isSuccess: false,
                message: errorMessage,
                amount: 0,
                senderUserId: senderUserId,
                receiverUserId: receiverUserId);
        }

        public static OperationDetailsDto Error(Guid senderUserId, string errorMessage = DefaultErrorMessage)
        {
            return new OperationDetailsDto(
                isSuccess: false,
                message: errorMessage,
                amount: 0,
                senderUserId: senderUserId);
        }

        public static OperationDetailsDto Success(Guid senderUserId, Guid receiverUserId, double amount)
        {
            return new OperationDetailsDto(
                isSuccess: true,
                message: SuccessMessage,
                amount: amount,
                senderUserId: senderUserId,
                receiverUserId: receiverUserId);
        }

        public static OperationDetailsDto Success(Guid senderUserId, double amount)
        {
            return new OperationDetailsDto(
                isSuccess: true,
                message: SuccessMessage,
                amount: amount,
                senderUserId: senderUserId);
        }
    }
}
