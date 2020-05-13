using System;
using System.Text.RegularExpressions;
using SamDevs.InfrastructureCore.Enums;

namespace SamDevs.InfrastructureCore.Extensions
{
    public static class StringExtensions
    {
        public static string GetDirection(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            input = input.StripHtml();
            var firstChar = Regex.Match(input, @"[\p{L}]").Value;
            return firstChar.Length > 0 && firstChar[0].IsRtlLetter() ? "rtl" : "ltr";
        }

        public static string StripHtml(this string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString)) return htmlString;
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
            str = Regex.Replace(str, @"[\p{Mn}]", "");
            str = Regex.Replace(str, @"[^\p{L}\p{Nd}]", "-");
            str = Regex.Replace(str, "-{2,}", "-");
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

        public static string InputFormat(this string input, InputFormats flags)
        {
            if ((flags & InputFormats.IgnoreCase) != InputFormats.IgnoreCase)
                if ((flags & InputFormats.UpperCase) == InputFormats.UpperCase)
                    input = input.ToUpper();
                else if ((flags & InputFormats.LowerCase) == InputFormats.LowerCase)
                    input = input.ToLower();

            if ((flags & InputFormats.Trim) == InputFormats.Trim)
                input = input.Trim();
            else if ((flags & InputFormats.TrimStart) == InputFormats.TrimStart)
                input = input.TrimStart();
            else if ((flags & InputFormats.TrimEnd) == InputFormats.TrimEnd)
                input = input.TrimEnd();

            if ((flags & InputFormats.EnglishNumber) == InputFormats.EnglishNumber)
                input = input.ToEnglishNumber();
            if ((flags & InputFormats.Uniform) == InputFormats.Uniform)
                input = input.Uniform();

            return input;
        }
    }
}
