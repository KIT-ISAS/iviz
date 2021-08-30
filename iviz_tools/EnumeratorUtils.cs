using System;
using System.Collections.Generic;

namespace Iviz.Tools
{
    public static class EnumeratorUtils
    {
        public static ZipEnumerable<TA, TB> Zip<TA, TB>(this IReadOnlyList<TA> a, IReadOnlyList<TB> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            return new ZipEnumerable<TA, TB>(a, b);
        }

        public static SelectEnumerable<IReadOnlyList<TA>, TA, TB> Select<TA, TB>(this IReadOnlyList<TA> a,
            Func<TA, TB> f)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            return new SelectEnumerable<IReadOnlyList<TA>, TA, TB>(a, f);
        }

        public static void AddRange<TC, TA, TB>(this List<TB> list, in SelectEnumerable<TC, TA, TB> tb)
            where TC : IReadOnlyList<TA>
        {
            list.Capacity = list.Count + tb.Count;
            foreach (TB b in tb)
            {
                list.Add(b);
            }
        }

        public static RefEnumerable<T> Ref<T>(this T[] a) =>
            new(a ?? throw new ArgumentNullException(nameof(a)));

        public static RefEnumerable<T>.Enumerator RefEnumerator<T>(this T[] a) =>
            new(a ?? throw new ArgumentNullException(nameof(a)));

        public delegate void RefAction<T>(ref T t);

        public static T[] ForEach<T>(this T[] a, RefAction<T> action)
        {
            for (int i = 0; i < a.Length; i++)
            {
                action(ref a[i]);
            }

            return a;
        }

        public static RangeEnumerable<TA> Take<TA>(this IReadOnlyList<TA> a, int count) => new(a, 0, count);

        public static RangeEnumerable<TA> Skip<TA>(this IReadOnlyList<TA> a, int start) => new(a, start, a.Count);
    }
}