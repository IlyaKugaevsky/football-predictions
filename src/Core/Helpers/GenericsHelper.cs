using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public static class GenericsHelper
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) return true;
            var collection = enumerable as ICollection<T>;
            if (collection != null) return collection.Count < 1;
            return !enumerable.Any();
        }

        public static IEnumerable<TResult> TripleZip<TFirst, TSecond, TThird, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            IEnumerable<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
            using (var enum3 = third.GetEnumerator())
            {
                while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext())
                {
                    yield return resultSelector(
                        enum1.Current,
                        enum2.Current,
                        enum3.Current);
                }
            }
        }
    }
}
