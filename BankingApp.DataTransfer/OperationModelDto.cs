using System;
using System.ComponentModel.DataAnnotations;
using BankingApp.DataTransfer.Helpers;
using BankingApp.DataTransfer.Helpers.CustomAttributes;

namespace BankingApp.DataTransfer
{
    public class OperationModelDto
    { 
        public Guid? ReceiverUserId { get; set; }

        [Required]
        [BankAmount]
        [Range(Constants.MinOperationAmount, Constants.MaxOperationAmount)]
        public double Amount { get; set; }
    }
}
