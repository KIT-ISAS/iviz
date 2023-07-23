using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public readonly struct SelectEnumerable<TC, TA, TB> : IReadOnlyList<TB>, ICollection<TB> where TC : IReadOnlyList<TA>
{
    readonly TC a;
    readonly Func<TA, TB> f;
    public readonly int Count;

    public struct Enumerator : IEnumerator<TB>
    {
        readonly TC a;
        readonly Func<TA, TB> f;
        readonly int count;
        int index;

        internal Enumerator(TC a, Func<TA, TB> f, int count)
        {
            this.a = a;
            this.f = f;
            this.count = count;
            index = -1;
        }

        public bool MoveNext() => ++index < count;
        public void Reset() => index = -1;
        public TB Current => f(a[index]);
        object? IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public TB this[int index] => f(a[index]);

    public SelectEnumerable(TC a, Func<TA, TB> f)
    {
        this.a = a;
        this.f = f;
        Count = a.Count;
    }

    public Enumerator GetEnumerator() => new(a, f, Count);

    public SelectEnumerable<SelectEnumerable<TC, TA, TB>, TB, TD> Select<TD>(Func<TB, TD> ff)
    {
        return new SelectEnumerable<SelectEnumerable<TC, TA, TB>, TB, TD>(this, ff);
    }

    public TB[] ToArray()
    {
        if (Count == 0)
        {
            return Array.Empty<TB>();
        }

        TB[] array = new TB[Count];
        for (int i = 0; i < Count; i++)
        {
            array[i] = f(a[i]);
        }

        return array;
    }
    
    public List<TB> ToList()
    {
        if (Count == 0)
        {
            return new List<TB>();
        }

        var list = new List<TB>(Count);
        for (int i = 0; i < Count; i++)
        {
            list.Add(f(a[i]));
        }

        return list;
    }

    public void CopyTo(TB[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
        {
            array[i + arrayIndex] = f(a[i]);
        }
    }

    int IReadOnlyCollection<TB>.Count => Count;
    int ICollection<TB>.Count => Count;
    void ICollection<TB>.Add(TB item) => throw new NotSupportedException();
    void ICollection<TB>.Clear() => throw new NotSupportedException();
    bool ICollection<TB>.Contains(TB item) => throw new NotSupportedException();
    bool ICollection<TB>.Remove(TB item) => throw new NotSupportedException();
    IEnumerator<TB> IEnumerable<TB>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    bool ICollection<TB>.IsReadOnly => true;
}