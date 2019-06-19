using System;
using System.Text.RegularExpressions;

namespace SamDevs.Infrastructure.Extensions {
    public static class StringExtensions {
        public static string GetDirection(this string input) {
            input = input.StripHtml();
            var firstChar = Regex.Match(input, @"[\p{L}]").Value;
            return firstChar.Length > 0 && firstChar[0].IsRtlLetter() ? "rtl" : "ltr";
        }

        public static string StripHtml(this string htmlString) {
            if (String.IsNullOrEmpty(htmlString)) return htmlString;
            htmlString = Regex.Replace(htmlString, @"<[^>]*(>|$)", "");
            return Regex.Replace(htmlString, @"[\n\r]+", " ");
        }
        public static string SafeSubstring(this string str, int start, int maxLength, bool useEllipsis = false) {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length <= start + maxLength)
                return str.Substring(start);
            return str.Substring(start, maxLength) + (useEllipsis ? "..." : "");
        }
        public static string ToDashedUrl(this string str) {
            if (string.IsNullOrEmpty(str)) return str;

            str = Regex.Replace(str, @"[""\.\?\^\{\}\|\[\]\(\)\*\+\$\\_~:/#@!&',;=%<> ؟]", "-", RegexOptions.IgnoreCase);
            while (str.Contains("--"))
                str = str.Replace("--", "-");
            return str.Trim('-');
        }

        public static string ClearText(this string input) {
            // yesBeforeNoAfter
            var openArea = @"(\s*)([\[\(\{\<⟨‘“«])(\s*)";
            // noBeforeYesAfter
            var closeArea = @"(\s*)([\]\)\}>⟩’”»;:,.?%!])(\s*)";

            // yesBeforeAfterMatch ' " `
            // yesBeforeAfter &
            // noBeforeAfter -   
            input = Regex.Replace(input, openArea, " $2");
            input = Regex.Replace(input, closeArea, "$2 ");
            return input;
        }
    }
}
