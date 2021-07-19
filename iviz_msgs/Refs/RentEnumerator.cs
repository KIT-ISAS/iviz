using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Msgs
{
    public struct RentEnumerator<T> : IEnumerator<T>
    {
        readonly T[] a;
        readonly int length;
        int pos;

        public RentEnumerator(T[] a, int length) => (this.a, this.length, pos) = (a, length, -1);
        public bool MoveNext() => ++pos < length;
        public void Reset() => throw new NotImplementedException();
        object? IEnumerator.Current => Current;
        public T Current => a[pos];

        public void Dispose()
        {
        }
    }
}