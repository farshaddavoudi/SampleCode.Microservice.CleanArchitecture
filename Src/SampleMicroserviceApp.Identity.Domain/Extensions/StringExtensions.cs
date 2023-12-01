using DNTPersianUtils.Core;

namespace SampleMicroserviceApp.Identity.Domain.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///Get String Between Two String In a String
    /// </summary>      
    public static string Between(this string value, string str1, string str2)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        var str1BeginIndex = value.IndexOf(str1, StringComparison.Ordinal);
        if (str1BeginIndex == -1)
            return "";
        var str2BeginIndex = value.IndexOf(str2, str1BeginIndex, StringComparison.Ordinal);
        if (str2BeginIndex == -1)
            return "";
        var str1EndIndex = str1BeginIndex + str1.Length;
        return str1EndIndex >= str2BeginIndex ? ""
            : value.Substring(str1EndIndex, str2BeginIndex - str1EndIndex);
    }

    public static string After(this string value, string str)
    {
        var strBeginIndex = value.LastIndexOf(str, StringComparison.Ordinal);
        if (strBeginIndex == -1)
            return "";
        var strEndIndex = strBeginIndex + str.Length;
        return strBeginIndex >= value.Length ? "" : value.Substring(strEndIndex);
    }

    /// <summary>
    /// Check if a string has value or is null / empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNotNullOrEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// = !string.IsNullOrWhitespace(string)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNotNullOrWhitespace(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static int ToInt(this string value)
    {
        return Convert.ToInt32(value.Trim().ToEnglishNumbers());
    }

    public static int? ToInt(this string value, bool flagAllowNull)
    {
        return string.IsNullOrWhiteSpace(value) ? null : Convert.ToInt32(value.Trim().ToEnglishNumbers());
    }

    public static long ToLong(this string value)
    {
        return Convert.ToInt64(value.Trim().ToEnglishNumbers());
    }

    public static decimal ToDecimal(this string value)
    {
        return Convert.ToDecimal(value.Trim().ToEnglishNumbers());
    }

    /// <summary>
    /// 1,000,000 => 1000000
    /// </summary>
    /// <param name="rialDisplayAmount"></param>
    /// <returns></returns>
    public static long ToLongAmountFromRialDisplay(this string rialDisplayAmount)
    {
        return rialDisplayAmount.Replace(",", "").Trim().ToLong();
    }

    /// <summary>
    /// 1,000,000 => 1000000
    /// </summary>
    /// <param name="rialDisplayAmount"></param>
    /// <returns></returns>
    public static int ToIntAmountFromRialDisplay(this string rialDisplayAmount)
    {
        return rialDisplayAmount.Replace(",", "").Trim().ToInt();
    }

    [Obsolete("Please use <.ToPersianNumber> method of DNTPersianUtils")]
    public static string En2FaDigits(this string str)
    {
        return str.Replace("0", "۰")
            .Replace("1", "۱")
            .Replace("2", "۲")
            .Replace("3", "۳")
            .Replace("4", "۴")
            .Replace("5", "۵")
            .Replace("6", "۶")
            .Replace("7", "۷")
            .Replace("8", "۸")
            .Replace("9", "۹");
    }

    [Obsolete("Please use <.ToEnglishNumber> method of DNTPersianUtils")]
    public static string Fa2EnDigits(this string str)
    {
        return str.Replace("۰", "0")
            .Replace("۱", "1")
            .Replace("۲", "2")
            .Replace("۳", "3")
            .Replace("۴", "4")
            .Replace("۵", "5")
            .Replace("۶", "6")
            .Replace("۷", "7")
            .Replace("۸", "8")
            .Replace("۹", "9")
            //iphone numeric
            .Replace("٠", "0")
            .Replace("١", "1")
            .Replace("٢", "2")
            .Replace("٣", "3")
            .Replace("٤", "4")
            .Replace("٥", "5")
            .Replace("٦", "6")
            .Replace("٧", "7")
            .Replace("٨", "8")
            .Replace("٩", "9");
    }

    public static string FixPersianChars(this string str)
    {
        return str.ApplyCorrectYeKe()
            .Replace("ﮎ", "ک")
            .Replace("ﮏ", "ک")
            .Replace("ﮐ", "ک")
            .Replace("ﮑ", "ک")
            .Replace("ك", "ک")
            .Replace("ي", "ی")
            .Replace(" ", " ")
            .Replace("ھ", "ه");
    }

    /// <summary>
    /// Trim + FixPersianChars + ToEnglishNumbers() + NullIfEmpty operations on a string altogether
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string? Clean(this string str)
    {
        return str.Trim().FixPersianChars().ToEnglishNumbers().NullIfEmpty();
    }

    /// <summary>
    /// Set null if string is empty ("")
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string? NullIfEmpty(this string? str)
    {
        return str?.Length == 0 ? null : str;
    }

    /// <summary>
    /// Is string convertable to Int type
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsInt(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentNullException(nameof(str));

        var cleanedStr = str.Trim().ToEnglishNumbers();
        return int.TryParse(cleanedStr, out _);
    }

    /// <summary>
    /// Is string convertable to Long type
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsLong(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentNullException(nameof(str));

        var cleanedStr = str.Trim().ToEnglishNumbers();
        return long.TryParse(cleanedStr, out _);
    }

    /// <summary>
    /// Is string convertable to Decimal type
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>

    public static bool IsDecimal(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentNullException(nameof(str));

        var cleanedStr = str.Trim().ToEnglishNumbers();
        return decimal.TryParse(cleanedStr, out _);
    }

    /// <summary>
    /// Convert BigBoss to bigBoss (PascalCase to camelCase)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToLowerFirstChar(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentNullException(nameof(str));

        return char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}