using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Helpers
{
    public static class EnumHelpers
    {
        public static string Description(this Enum eValue)
        {
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (nAttributes.Any())
                return ((DescriptionAttribute)nAttributes.FirstOrDefault())?.Description;
            return eValue.ToString();
        }

        public static Dictionary<Enum, string> GetAllValuesAndDescriptions(Type t)
        {
            if (!t.IsEnum)
                throw new ArgumentException("t must be an enum type");
            var enumDictionary = new Dictionary<Enum, string>();
            var enumArray = Enum.GetValues(t);
            Array.Sort(enumArray);
            foreach (var value in enumArray)
            {
                enumDictionary.Add((Enum)value, ((Enum)value).Description());
            }
            return enumDictionary;
            //Enum.GetValues(t).Cast<Enum>().Select(x =>new Dictionary<int,string>((int)x,x.Description())) /*new ValueDescription() { Value = e, Description = e.Description() }*/).ToList();
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
