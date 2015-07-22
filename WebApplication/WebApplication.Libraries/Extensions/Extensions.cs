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
    }
}
