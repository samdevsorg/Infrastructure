﻿using System;
using System.ComponentModel.DataAnnotations;
using SamDevs.Infrastructure.Enums;
using SamDevs.Infrastructure.Extensions;

namespace SamDevs.Infrastructure.Attributes
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
