using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools
{
    public readonly struct ZipEnumerable<TA, TB> : IReadOnlyList<(TA First, TB Second)>
    {
        readonly IReadOnlyList<TA> a;
        readonly IReadOnlyList<TB> b;

        public struct Enumerator : IEnumerator<(TA First, TB Second)>
        {
            readonly IReadOnlyList<TA> a;
            readonly IReadOnlyList<TB> b;
            int index;

            internal Enumerator(IReadOnlyList<TA> na, IReadOnlyList<TB> nb) => (a, b, index) = (na, nb, -1);
            public bool MoveNext() => ++index < Math.Min(a.Count, b.Count);
            public void Reset() => index = -1;
            public (TA, TB) Current => (a[index], b[index]);
            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }

        public ZipEnumerable(IReadOnlyList<TA> a, IReadOnlyList<TB> b) => (this.a, this.b) = (a, b);

        public Enumerator GetEnumerator() => new(a, b);

        IEnumerator<(TA, TB)> IEnumerable<(TA First, TB Second)>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Math.Min(a.Count, b.Count);

        public (TA First, TB Second) this[int index] => (a[index], b[index]);

        public (TA First, TB Second)[] ToArray()
        {
            if (Count == 0)
            {
                return Array.Empty<(TA, TB)>();
            }

            (TA, TB)[] array = new (TA, TB)[Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = this[i];
            }

            return array;
        }

        public SelectEnumerable<ZipEnumerable<TA, TB>, (TA, TB), TC> Select<TC>(Func<(TA, TB), TC> f) => new(this, f);
    }
}