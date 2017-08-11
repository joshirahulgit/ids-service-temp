using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Utils
{
    public static class Helpers
    {
        public static bool ListsDiffer<T>(List<T> aList, List<T> bList)
        {
            if (aList.Count != bList.Count) return true;
            foreach (T b in bList)
            {
                if (!aList.Any(a => a.Equals(b)))
                    return true;
            }
            return false;
        }

        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
