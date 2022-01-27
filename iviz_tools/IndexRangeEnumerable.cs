using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Tools;

public struct IndexRangeEnumerator
{
    int index;
    readonly int end;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexRangeEnumerator(int start, int end)
    {
        index = start;
        this.end = end;
    }

    public int Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => index++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() => index < end;
}