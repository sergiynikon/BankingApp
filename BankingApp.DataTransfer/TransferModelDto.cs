using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.DataTransfer
{
    public class TransferModelDto
    {
        [Required]
        public Guid ReceiverUserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Amount { get; set; }
    }
}