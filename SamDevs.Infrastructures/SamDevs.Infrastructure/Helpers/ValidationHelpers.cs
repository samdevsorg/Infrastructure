using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SamDevs.Infrastructure.Helpers
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// تعیین معتبر بودن کد ملی
        /// </summary>
        /// <param name="nationalCode">کد ملی وارد شده</param>
        /// <returns>
        /// در صورتی که کد ملی صحیح باشد خروجی <c>true</c> و در صورتی که کد ملی اشتباه باشد خروجی <c>false</c> خواهد بود
        /// </returns>
        public static bool IsNationalCodeValid(this string nationalCode)
        {
            try
            {
                if (string.IsNullOrEmpty(nationalCode))
                    return false;


                var input = nationalCode.PadLeft(10, '0');

                if (!Regex.IsMatch(input, @"^\d{10}$"))
                    return false;

                var check = Convert.ToInt32(input.Substring(9, 1));
                var sum = Enumerable.Range(0, 9).Select(x => Convert.ToInt32(input.Substring(x, 1)) * (10 - x)).Sum() % 11;

                return sum < 2 && check == sum || sum >= 2 && check + sum == 11;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDigit(this string inputText)
        {
            return Regex.IsMatch(inputText, @"^\d+$");
        }

        public static bool IsMobileNumberValid(this string mobileNumber)
        {
            return Regex.IsMatch(mobileNumber, @"((\+|00)?98)?0?9\d{9}");
        }
    }
}
