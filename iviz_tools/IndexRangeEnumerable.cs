using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Tools;

/// <summary>
/// Simple enumerator for a <see cref="Range"/> to use it in a foreach.
/// Example code:
/// <code>
/// foreach (int i in 0..10) { /* iterates i from 0 to 9 inclusive */ }
/// </code>
/// </summary>
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
        // this assumes that Current is called only once per cycle
        get => index++;   
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() => index < end;
}