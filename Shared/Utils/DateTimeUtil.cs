using System.Globalization;

namespace BlazorApp2.Shared.Utils;

public static class DateTimeUtil
{
    private static readonly string[] _dateFormats = { "yyyy", "MM", "dd", "yyyyMM", "yyyyMMdd", "MMddyyyy", "yyyy-MM-dd"};
    
    public static DateTime? ConvertDate(string dateString)
    {
        if (string.IsNullOrEmpty(dateString)) return null;
        
        if (DateTime.TryParseExact(dateString, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        return null;
    }
}