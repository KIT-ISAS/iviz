using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools;

public struct RentEnumerator<T>
{
    readonly T[] a;
    readonly int length;
    int pos;

    public RentEnumerator(T[] a, int length) => (this.a, this.length, pos) = (a, length, -1);
    public bool MoveNext() => ++pos < length;
    public void Reset() => pos = 0;
    public ref T Current => ref a[pos];
}