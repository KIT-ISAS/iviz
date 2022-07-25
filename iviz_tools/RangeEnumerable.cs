using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public readonly struct RangeEnumerable<TA> : IReadOnlyList<TA>, ICollection<TA>
{
    readonly IReadOnlyList<TA> a;
    readonly int start;
    readonly int end;

    internal RangeEnumerable(IReadOnlyList<TA> a, int start, int end) =>
        (this.a, this.start, this.end) = (a, start, end);

    public Enumerator GetEnumerator() => new(a, start, end);
    IEnumerator<TA> IEnumerable<TA>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => end - start;
    public TA this[int index] => a[start + index];
    public RangeEnumerable<TA> Take(int count) => new(a, start, start + count);
    public RangeEnumerable<TA> Skip(int toSkip) => new(a, start + toSkip, end);
    public SelectEnumerable<RangeEnumerable<TA>, TA, TB> Select<TB>(Func<TA, TB> f) => new(this, f);

    void ICollection<TA>.Add(TA item) => throw new NotSupportedException();
    void ICollection<TA>.Clear() => throw new NotSupportedException();
    bool ICollection<TA>.Contains(TA item) => throw new NotSupportedException();

    bool ICollection<TA>.Remove(TA item) => throw new NotSupportedException();
    bool ICollection<TA>.IsReadOnly => true;

    void ICollection<TA>.CopyTo(TA[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
        {
            array[arrayIndex + i] = this[i];
        }
    }
    
    public struct Enumerator : IEnumerator<TA>
    {
        readonly IReadOnlyList<TA> a;
        readonly int end;
        int index;

        public Enumerator(IReadOnlyList<TA> na, int nStart, int nEnd) => (a, index, end) = (na, nStart - 1, nEnd);

        public bool MoveNext() => ++index < end;
        public void Reset() => index = -1;
        public TA Current => a[index];
        object? IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}