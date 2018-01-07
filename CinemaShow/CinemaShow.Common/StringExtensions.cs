namespace CinemaShow.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StringExtensions
    {
        public static string FromPublic(this string @string)
        {
            return @string.Substring(@string.IndexOf("/public"));
        }

        public static string TakeMax(this string @string, int count)
        {
            return string.Join(string.Empty, @string.Take(count));
        }

        public static IEnumerable<string> SeparatedFromNewLine(this string @string)
        {
            return @string.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}