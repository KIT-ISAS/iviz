using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Iviz.Tools;

public static class EnumeratorUtils
{
    public static ZipEnumerable<TA, TB> Zip<TA, TB>(this IReadOnlyList<TA> a, IReadOnlyList<TB> b)
    {
        if (a == null)
        {
            ThrowArgumentNull(nameof(a));
        }

        if (b == null)
        {
            ThrowArgumentNull(nameof(b));
        }

        return new ZipEnumerable<TA, TB>(a, b);
    }

    public static SelectEnumerable<IReadOnlyList<TA>, TA, TB> Select<TA, TB>(this IReadOnlyList<TA> a,
        Func<TA, TB> f)
    {
        if (a == null)
        {
            ThrowArgumentNull(nameof(a));
        }

        if (f == null)
        {
            ThrowArgumentNull(nameof(f));
        }

        return new SelectEnumerable<IReadOnlyList<TA>, TA, TB>(a, f);
    }

    public static SelectEnumerable<TA[], TA, TB> Select<TA, TB>(this TA[] a, Func<TA, TB> f)
    {
        if (a == null)
        {
            ThrowArgumentNull(nameof(a));
        }

        if (f == null)
        {
            ThrowArgumentNull(nameof(f));
        }

        return new SelectEnumerable<TA[], TA, TB>(a, f);
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

    /// <summary>
    /// Returns an enumerable for a <see cref="Range"/> that can be used in a foreach.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexRangeEnumerator GetEnumerator(this Range range) =>
        // GetHashCode returns the value without additional validation
        new(range.Start.GetHashCode(), range.End.GetHashCode());

    public static Span<T> Slice<T>(this T[] t, Range range)
    {
        return t.AsSpan(range);
    }
    
    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentNull(string arg) => throw new ArgumentNullException(arg);

}