using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public readonly struct RangeEnumerable<TA>
{
    readonly List<TA> a;
    readonly int start;
    readonly int end;

    internal RangeEnumerable(List<TA> a, int start, int end) =>
        (this.a, this.start, this.end) = (a, start, end);

    public Enumerator GetEnumerator() => new(a, start, end);
    public TA this[int index] => a[start + index];

    public struct Enumerator 
    {
        readonly List<TA> a;
        readonly int end;
        int index;

        public Enumerator(List<TA> na, int nStart, int nEnd) => (a, index, end) = (na, nStart - 1, nEnd);

        public bool MoveNext() => ++index < end;
        public TA Current => a[index];
    }
}