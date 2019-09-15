using System.Text.RegularExpressions;
using SamDevs.InfrastructureCore.Extensions;

namespace SamDevs.InfrastructureCore.Validations
{
    public static class MobileNumberValidations
    {
        /// <summary>
        /// Check if the mobile number is a valid iranian mobile number
        /// </summary>
        /// <param name="mobileNumber">The mobile number to be checked</param>
        /// <returns></returns>
        public static bool IsIranMobileNumberValid(this string mobileNumber)
        {
            return Regex.IsMatch(mobileNumber.ToEnglishNumber(), @"((\+|00)?98)?0?9\d{9}");
        }
    }
}
