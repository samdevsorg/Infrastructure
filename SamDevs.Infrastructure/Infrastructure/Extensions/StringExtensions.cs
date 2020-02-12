using System;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string GetDirection(this string input)
        {
            input = input.StripHtml();
            var firstChar = Regex.Match(input, @"[\p{L}]").Value;
            return firstChar.Length > 0 && firstChar[0].IsRtlLetter() ? "rtl" : "ltr";
        }

        public static string StripHtml(this string htmlString)
        {
            if (String.IsNullOrEmpty(htmlString)) return htmlString;
            htmlString = Regex.Replace(htmlString, @"<[^>]*(>|$)", "");
            return Regex.Replace(htmlString, @"[\n\r]+", " ");
        }
        public static string SafeSubstring(this string str, int start, int maxLength, bool useEllipsis = false)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Length <= start + maxLength)
                return str.Substring(start);
            return str.Substring(start, maxLength) + (useEllipsis ? "..." : "");
        }
        public static string ToDashedUrl(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            str = Regex.Replace(str, @"[""\.\?\^\{\}\|\[\]\(\)\*\+\$\\_~:/#@!&',;=%<> ؟]", "-", RegexOptions.IgnoreCase);
            while (str.Contains("--"))
                str = str.Replace("--", "-");
            return str.Trim('-');
        }

        public static string ClearText(this string input)
        {
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

        public static string ToEnglishNumber(this string text)
        {
            return text.Replace("۰", "0").Replace("١", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4")
                .Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");

        }

        public static string ToPersianNumber(this string text)
        {
            return text.Replace("0", "۰").Replace("1", "١").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴")
                .Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
        }
    }
}
