using System;

namespace SamDevs.Infrastructure.Enums
{
    [Flags]
    public enum InputFormats
    {
        UpperCase = 0x1,
        LowerCase = 0x2,
        IgnoreCase = UpperCase | LowerCase,
        TrimStart = 0x4,
        TrimEnd = 0x8,
        /// <summary>
        /// TrimStart | TrimEnd
        /// </summary>
        Trim = TrimStart | TrimEnd,
        EnglishNumber = 0x10,
        Uniform = 0x20,
        /// <summary>
        /// Trim | EnglishNumber | Uniform
        /// </summary>
        Normal = Trim | EnglishNumber | Uniform,


    }
}
