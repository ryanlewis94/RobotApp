using System.Collections.Generic;
using System.Linq;

namespace RobotApp.Extensions
{
    internal static class CollectionExtensions
    {
        internal static IEnumerable<IEnumerable<T>> ToSubLists<T>(this IEnumerable<T> source, int subListSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / subListSize)
                .Select(x => x.Select(v => v.Value));
        }
    }
}
