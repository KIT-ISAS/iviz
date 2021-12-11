using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Tools;

// Adapted from https://github.com/YairHalberstadt/RangeForeach/blob/master/RangeForeach/RangeExtensions.cs
public readonly struct IndexRangeEnumerable : IReadOnlyList<int>
{
    readonly int start;
    readonly int end;

    public struct Enumerator : IEnumerator<int>
    {
        int index;
        readonly int end;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator(int start, int end) => (index, this.end) = (start - 1, end);
        public int Current => index;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext() => ++index < end;
        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public void Reset()
        {
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexRangeEnumerable(Range range)
    {
        if (range.Start.IsFromEnd || range.End.IsFromEnd)
        {
            throw new ArgumentException("Range start and end must not be from end");
        }

        if (range.Start.Value > range.End.Value)
        {
            throw new ArgumentException("start is greater than end");                
        }

        (start, end) = (range.Start.Value, range.End.Value);
    } 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator GetEnumerator() => new(start, end);
    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public int Count => end - start;
    public int this[int index] => start + index;
    public SelectEnumerable<IndexRangeEnumerable, int, TC> Select<TC>(Func<int, TC> f) => new(this, f);
}