using System;
using System.Collections.Generic;
using System.Linq;

namespace WordListGenerator
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> CrossJoin<T1, T2, TResult>(
            this IEnumerable<T1> enumerable1,
            IEnumerable<T2> enumerable2,
            Func<T1, T2, TResult> resultSelector)
        {
            return enumerable1.Join(
                enumerable2,
                outerKeySelector: _ => true,
                innerKeySelector: _ => true,
                resultSelector);
        }
    }
}