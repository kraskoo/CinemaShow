namespace CinemaShow.Common
{
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static string AsString<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }
    }
}