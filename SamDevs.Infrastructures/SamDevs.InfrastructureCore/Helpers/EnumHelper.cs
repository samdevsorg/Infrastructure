using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SamDevs.InfrastructureCore.Helpers
{
    public static class EnumHelper
    {
        public static string Description(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (nAttributes.Any())
                return ((DescriptionAttribute)nAttributes.FirstOrDefault())?.Description;
            return eValue.ToString();
        }

        public static string DisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            //if (enumValue == null)
            //    return string.Empty;

            var member = enumType.GetMember(enumValue)[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (!attributes.Any())
                return value.ToString();

            var outString = ((DisplayAttribute)attributes[0]).Name;
            if (((DisplayAttribute)attributes[0]).ResourceType != null)
                outString = ((DisplayAttribute)attributes[0]).GetName();

            return outString;
        }

        public static List<KeyValuePair<int, string>> GetCollection(this Type enumValue, int startIndex)
        {
            var enumList = new List<KeyValuePair<int, string>>();

            if (enumValue.IsEnum)
                return enumList;

            var enumArray = Enum.GetValues(enumValue);
            Array.Sort(enumArray);

            foreach (var value in enumArray)
            {
                var key = (int)value;
                if (key >= startIndex)
                    enumList.Add(new KeyValuePair<int, string>(key, ((Enum)value).DisplayName()));
            }

            return enumList;
        }

        public static List<KeyValuePair<int, string>> GetCollection(this Type enumValue)
        {
            var enumList = new List<KeyValuePair<int, string>>();

            if (enumValue.IsEnum)
                return enumList;

            var enumArray = Enum.GetValues(enumValue);
            Array.Sort(enumArray);

            foreach (var value in enumArray)
            {
                var key = (int)value;
                enumList.Add(new KeyValuePair<int, string>(key, ((Enum)value).DisplayName()));
            }

            return enumList;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }

    }
}
