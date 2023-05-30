using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public struct RangeEnumerable<TA>
{
    readonly List<TA> a;
    readonly int end;
    int index;

    internal RangeEnumerable(List<TA> a, int start, int end) =>
        (this.a, index, this.end) = (a, start - 1, end);

    //public Enumerator GetEnumerator() => new(a, start, end);
    public readonly RangeEnumerable<TA> GetEnumerator() => this;

    public bool MoveNext() => ++index < end;
    public readonly TA Current => a[index];

    /*

    readonly int start;
    internal RangeEnumerable(List<TA> a, int start, int end) =>
        (this.a, this.start, this.end) = (a, start, end);

    public struct Enumerator 
    {
        readonly List<TA> a;
        readonly int end;
        int index;

        public Enumerator(List<TA> na, int start, int end) => (a, index, this.end) = (na, start - 1, end);

        public bool MoveNext() => ++index < end;
        public TA Current => a[index];
    }
    */
}