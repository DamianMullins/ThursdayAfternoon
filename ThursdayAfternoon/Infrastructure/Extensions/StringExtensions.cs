using System.Text.RegularExpressions;
using Utilities.Core.Text;

namespace ThursdayAfternoon.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex HtmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StripTags(this string source)
        {
            return source.IsNotEmpty() ? HtmlRegex.Replace(source, string.Empty) : string.Empty;
        }

        /// <summary>
        ///  Remove HTML from string with compiled Regex, then truncate.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string StripTagsAndTruncate(this string source, int length = 250)
        {
            string result = source.StripTags();
            return result.Truncate(250);
        }
    }
}