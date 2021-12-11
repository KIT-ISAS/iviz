using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public struct RentEnumerator<T> : IEnumerator<T>
{
    readonly T[] a;
    readonly int length;
    int pos;

    public RentEnumerator(T[] a, int length) => (this.a, this.length, pos) = (a, length, -1);
    public bool MoveNext() => ++pos < length;
    public void Reset() => pos = 0;
    object? IEnumerator.Current => Current;
    public T Current => a[pos];

    public void Dispose()
    {
    }
}

public readonly struct RentRefEnumerable<T>
{
    readonly T[] a;
    readonly int length;

    public RentRefEnumerable(T[] a, int length) => (this.a, this.length) = (a, length); 
    public RentRefEnumerator GetEnumerator() => new (a, length);

    public struct RentRefEnumerator
    {
        readonly T[] a;
        readonly int length;
        int pos;

        public RentRefEnumerator(T[] a, int length) => (this.a, this.length, pos) = (a, length, -1);
        public bool MoveNext() => ++pos < length;
        public ref T Current => ref a[pos];
    }
}