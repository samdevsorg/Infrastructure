using System;
using System.ComponentModel.DataAnnotations;
using SamDevs.InfrastructureCore.Enums;
using SamDevs.InfrastructureCore.Extensions;

namespace SamDevs.InfrastructureCore.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class InputFormatAttribute : ValidationAttribute
    {
        private InputFormats _flags;

        public InputFormatAttribute()
        {
            _flags = InputFormats.Normal;
        }
        public InputFormatAttribute(InputFormats flags)
        {
            _flags = flags;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            if (property.GetSetMethod() == null || property.PropertyType != typeof(string))
                return null;

            property.SetValue(validationContext.ObjectInstance, value.ToString().InputFormat(_flags));
            return null;
        }
    }
}
