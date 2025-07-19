using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVC
{
    public static class Extension
    {
        public static string TextAfter(this string value, string search)
        {
            return value.Substring(value.IndexOf(search) + search.Length);
        }
        public static string GetUntilOrEmpty(this string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

    }
}
