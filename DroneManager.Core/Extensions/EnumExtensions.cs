using System;
using System.Linq;
using System.Runtime.Serialization;

namespace DroneManager.Core.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute? GetAttributeOfType<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var enumType = value.GetType();
            if (!enumType.IsEnum)
                throw new ArgumentException("The specified type is not an Enum.");

            var name = Enum.GetName(enumType, value);

            return enumType.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static string? ToEnumValueString(this Enum value)
        {
            var attribute = value.GetAttributeOfType<EnumMemberAttribute>();
            return attribute?.Value ?? value.ToString();
        }
    }
}


