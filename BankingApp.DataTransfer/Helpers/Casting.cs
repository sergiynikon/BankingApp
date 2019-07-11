using System;

namespace BankingApp.DataTransfer.Helpers
{
    public static class Casting
    {
        private static readonly int CentValue = 100;

        public static long DoubleToLong(double value)
        {
            //for resolving rounding issues
            var res = Math.Floor(Math.Round(value * CentValue, 12));
            return (long) res;
        }

        public static double LongToDouble(long value)
        {
            return value / (double)CentValue;
        }
    }
}