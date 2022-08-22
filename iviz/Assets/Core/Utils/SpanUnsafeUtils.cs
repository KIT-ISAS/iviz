using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs.GeometryMsgs;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    // TODO: get rid of this
    public static class SpanUnsafeUtils
    {
        [Obsolete]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T GetReference<T>(this ReadOnlySpan<T> span) where T : unmanaged
        {
            fixed (T* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte Plus(this ref byte ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref float Plus(this ref float ptr, int i) => ref Unsafe.Add(ref ptr, i);
    }
}