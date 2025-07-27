using System;

namespace SVC.Core.Extensions
{
    public static class StringExtension
    {
        public static string TextAfter(this string value, string search)
        {
            return value.Substring(value.IndexOf(search) + search.Length);
        }
        public static string GetUntilOrEmpty(this string text, string stopAt)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return string.Empty;
        }

    }
}
