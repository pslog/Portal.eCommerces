using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Libraries.Extensions
{
    public static class Extensions
    {
        public static string ToString(this DateTime? dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if(dateTime == null)
            {
                return string.Empty;
            }

            return ((DateTime)dateTime).ToString(format);
        }

        public static String ToEllipsis(this String str, int startIndex, int length, string suffix = "")
        {
            if (string.IsNullOrEmpty(str) || (startIndex + length) > str.Length)
            {
                return str;
            }

            return string.Format("{0}{1}", str.Substring(startIndex, length), suffix);
        }
    }
}
