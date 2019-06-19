using System;
using System.Linq;
using System.Reflection;

namespace SamDevs.Infrastructure.Helpers {
    public static class EnumHelper {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }
    }
}
