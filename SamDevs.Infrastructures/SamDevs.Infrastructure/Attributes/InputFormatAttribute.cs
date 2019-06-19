using System;
using System.ComponentModel.DataAnnotations;
using SamDevs.Infrastructure.Extensions;

namespace SamDevs.Infrastructure.Attributes
{
    public enum InputFormats
    {
        UpperCase,
        LowerCase,
        TrimStart,
        TrimEnd,
        Trim,
        EnglishNumber,
        Uniform
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class InputFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return null;
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            if (property.GetSetMethod() == null) return null;
            if (property.PropertyType != typeof(string)) return null;
            property.SetValue(validationContext.ObjectInstance, value.ToString().Trim().ToEnglishNumber().Uniform());
            return null;
        }
    }
}
