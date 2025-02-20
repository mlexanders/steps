using System.ComponentModel.DataAnnotations;

namespace Steps.Shared.Utils;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = fieldInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

        if (attributes != null && attributes.Length > 0) return attributes.First()!.ShortName!;

        return value.ToString();
    }
}