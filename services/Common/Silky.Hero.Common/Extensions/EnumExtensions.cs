using System.ComponentModel;

namespace Silky.Hero.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this System.Enum enumValue)
    {
        var fi = enumValue.GetType().GetField(enumValue.ToString());
        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;

        return enumValue.ToString();
    }
}