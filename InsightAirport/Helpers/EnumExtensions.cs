using System.ComponentModel;
using System.Reflection;

namespace InsightAirport.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            return field != null
                ? (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description ?? value.ToString()
                : value.ToString();
        }
    }
}
