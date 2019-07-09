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
                var strNumber = number.ToString(CultureInfo.InvariantCulture);

                if (!strNumber.Contains("."))
                {
                    return true;
                }

                if (strNumber.Substring(strNumber.IndexOf('.') + 1).Length <= 2)
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