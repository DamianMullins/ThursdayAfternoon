using System;
using System.Collections;
using System.Linq;

namespace ThursdayAfternoon.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToCommaSeperatedString(this object obj)
        {
            var arrValues = ((IEnumerable)obj).Cast<object>()
                     .Select(x => x.ToString())
                     .ToArray();

            return String.Join(",", arrValues);
        }
    }
}
