using System;
using System.ComponentModel.DataAnnotations;
using BankingApp.DataTransfer.Helpers.CustomAttributes;

namespace BankingApp.DataTransfer
{
    public class OperationModelDto
    { 
        public Guid? ReceiverUserId { get; set; }

        [Required]
        [BankAmount]
        [Range(0.01, double.MaxValue)] //from 1 cent to double.MaxValue
        public double Amount { get; set; }
    }
}
