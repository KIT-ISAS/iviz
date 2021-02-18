using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Msgs
{
    public struct RentEnumerator<T> : IEnumerator<T>
    {
        readonly T[] a;
        readonly int length;
        int currentIndex;

        public RentEnumerator(T[] a, int length)
        {
            this.a = a;
            this.length = length;
            currentIndex = -1;
        }

        public bool MoveNext()
        {
            if (currentIndex == length - 1)
            {
                return false;
            }

            currentIndex++;
            return true;
        }

        public void Reset() => throw new NotImplementedException();
        object? IEnumerator.Current => Current;
        public T Current => a[currentIndex];

        public void Dispose()
        {
        }
    }
}