using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BankingApp.DataTransfer.Helpers.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    [TypeConverter(typeof(double))]
    public class BankAmountAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is double number)
            {
                string strNumber = number.ToString(CultureInfo.InvariantCulture);
                if (strNumber.Length < 2 || strNumber == string.Format($"{number:0.00}"))
                {
                    return true;
                }
            }
            else
            {
                throw new NotSupportedException("Attribute should specify double property");
            }

            return false;
        }
    }
}