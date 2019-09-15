using System.Text.RegularExpressions;

namespace SamDevs.InfrastructureCore.Utilities
{
    public class PasswordUtil
    {
        public static bool IsComplex(string password)
        {
            return Regex.IsMatch(password, @"[A-Z]") &&
                Regex.IsMatch(password, @"[a-z]") &&
                Regex.IsMatch(password, @"[0-9]") &&
                Regex.IsMatch(password, @"[^A-Za-Z0-9 ]");
        }
    }
}
