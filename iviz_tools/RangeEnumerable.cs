using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Tools
{
    public readonly struct RangeEnumerable<TA> : IReadOnlyList<TA>
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

        public TA this[int index] => a[index];


        public struct Enumerator : IEnumerator<TA>
        {
            readonly IReadOnlyList<TA> a;
            readonly int end;
            int index;

            public Enumerator(IReadOnlyList<TA> na, int nStart, int nEnd) =>
                (a, index, end) = (na, nStart - 1, nEnd);

            public bool MoveNext() => ++index < end;

            public void Reset() => index = -1;
            public TA Current => a[index];
            object? IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public RangeEnumerable<TA> Take(int count) => new(a, start, start + count);

        public RangeEnumerable<TA> Skip(int start) => new(a, this.start + start, a.Count);

        public SelectEnumerable<RangeEnumerable<TA>, TA, TB> Select<TB>(Func<TA, TB> f) => new (this, f);
    }
}