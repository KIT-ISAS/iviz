using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public readonly struct SelectEnumerable<TC, TA, TB> : IReadOnlyList<TB> where TC : IReadOnlyList<TA>
{
    readonly TC a;
    readonly Func<TA, TB> f;

    public struct Enumerator : IEnumerator<TB>
    {
        readonly TC a;
        readonly Func<TA, TB> f;
        int index;

        internal Enumerator(TC na, Func<TA, TB> nf) => (a, f, index) = (na, nf, -1);
        public bool MoveNext() => ++index < a.Count;
        public void Reset() => index = -1;
        public TB Current => f(a[index]);
        object? IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public SelectEnumerable(TC a, Func<TA, TB> f) => (this.a, this.f) = (a, f);
    public Enumerator GetEnumerator() => new(a, f);
    IEnumerator<TB> IEnumerable<TB>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public TB[] ToArray()
    {
        if (a.Count == 0)
        {
            return Array.Empty<TB>();
        }

        TB[] array = new TB[a.Count];
        foreach (int i in ..a.Count)
        {
            array[i] = f(a[i]);
        }

        return array;
    }

    public List<TB> ToList()
    {
        List<TB> array = new(a.Count);
        foreach (int i in ..a.Count)
        {
            array.Add(f(a[i]));
        }

        return array;
    }

    public bool All(Func<TB, bool> predicate)
    {
        foreach (int i in ..a.Count)
        {
            if (!predicate(f(a[i])))
            {
                return false;
            }
        }

        return true;
    }

    public bool Any(Func<TB, bool> predicate)
    {
        foreach (int i in ..a.Count)
        {
            if (predicate(f(a[i])))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(Span<TB> span)
    {
        foreach (int i in ..a.Count)
        {
            span[i] = f(a[i]);
        }
    }


    public int Count => a.Count;

    public TB this[int index] => f(a[index]);
}