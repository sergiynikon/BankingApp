using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.DataTransfer
{
    public class OperationModelDto
    { 
        public Guid? ReceiverUserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)] //from 1 cent to double.MaxValue
        public double Amount { get; set; }
    }
}
