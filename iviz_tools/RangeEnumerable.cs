using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools
{
    public readonly struct RangeEnumerable<TA> : IReadOnlyList<TA>
    {
        readonly IReadOnlyList<TA> a;
        readonly int start;
        readonly int end;

        internal RangeEnumerable(IReadOnlyList<TA> a, int start, int end) =>
            (this.a, this.start, this.end) =
            (a ?? throw new ArgumentNullException(nameof(a)),
                Math.Max(Math.Min(start, a.Count), 0),
                Math.Max(Math.Min(end, a.Count), 0));

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

            internal Enumerator(IReadOnlyList<TA> na, int nStart, int nEnd) =>
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
    }
}