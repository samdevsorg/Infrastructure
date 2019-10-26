using System.Text.RegularExpressions;

namespace SamDevs.InfrastructureCore.Utilities
{
    public class PasswordUtility
    {
        public static bool IsComplex(string password)
        {
            return Regex.IsMatch(password, @"[A-Z]") &&
                Regex.IsMatch(password, @"[a-z]") &&
                Regex.IsMatch(password, @"[0-9]") &&
                Regex.IsMatch(password, @"[^A-Za-z0-9 ]");
        }
    }
}
