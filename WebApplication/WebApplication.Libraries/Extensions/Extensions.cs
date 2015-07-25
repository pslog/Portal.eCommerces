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

        public static List<T> GetParents<T>(this T entity, string parentPropertyName)
        {
            try
            {
                if(entity == null)
                {
                    return new List<T>();
                }

                var parents = new List<T> { entity };
                var parent = (T)entity.GetType().GetProperty(parentPropertyName).GetValue(entity);

                if(parent != null)
                {
                    parents.Add(parent);
                    parents.AddRange(parent.GetParents<T>(parentPropertyName));    
                }

                return parents;
            }
            catch(Exception)
            {
                return new List<T>();
            }
        }

        public static List<T> GetChildren<T>(this T entity, string childPropertyName)
        {
            try
            {
                var children = new List<T>();
                var items = (ICollection<T>)entity.GetType().GetProperty(childPropertyName).GetValue(entity);

                if (items.Count == 0)
                {
                    return children;
                }

                children.AddRange(items);

                foreach (var item in items)
                {
                    children.AddRange(((T)item).GetChildren<T>(childPropertyName));
                }

                return children;
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }
    }
}
