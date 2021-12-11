using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Tools;

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

    public static SelectEnumerable<TA[], TA, TB> Select<TA, TB>(this TA[] a, Func<TA, TB> f)
    {
        if (a == null)
        {
            throw new ArgumentNullException(nameof(a));
        }

        if (f == null)
        {
            throw new ArgumentNullException(nameof(f));
        }

        return new SelectEnumerable<TA[], TA, TB>(a, f);
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

    public static void CopyFrom<TT, TU>(this Span<TT> span, TU list) where TU : IReadOnlyList<TT>
    {
        foreach (int i in ..span.Length)
        {
            span[i] = list[i];
        }
    }
    public static RangeEnumerable<TA> Take<TA>(this IReadOnlyList<TA> a, int count) => new(a, 0, count);

    public static RangeEnumerable<TA> Skip<TA>(this IReadOnlyList<TA> a, int start) => new(a, start, a.Count);

    public static Rent<T> ToRent<T>(this IReadOnlyCollection<T> ts) where T : unmanaged
    {
        var rent = new Rent<T>(ts.Count);
        int i = 0;
        foreach (var t in ts)
        {
            rent[i++] = t;
        }

        return rent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static IndexRangeEnumerable GetEnumerable(this Range range) => new(range);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexRangeEnumerable.Enumerator GetEnumerator(this Range range)
    {
        return range.GetEnumerable().GetEnumerator();
    }

    public static SelectEnumerable<IndexRangeEnumerable, int, T> Select<T>(this Range range, Func<int, T> f)
    {
        return range.GetEnumerable().Select(f);
    }

    public static SelectEnumerable<IndexRangeEnumerable, int, T> Select<T>(this Range range, Func<T> a)
    {
        return range.GetEnumerable().Select(_ => a());
    }

    public static Span<T> Slice<T>(this T[] t, Range range)
    {
        return new Span<T>(t)[range];
    }
}