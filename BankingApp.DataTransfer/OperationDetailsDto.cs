using System;

namespace BankingApp.DataTransfer
{
    public class OperationDetailsDto
    {
        public OperationDetailsDto(double amount, Guid? senderUserId = null, Guid? receiverUserId = null)
        {
            Amount = amount;
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
        }

        public double? Amount { get; }
        public Guid? SenderUserId { get; }
        public Guid? ReceiverUserId { get; }
    }
}
