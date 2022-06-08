using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Core
{
    /// <summary>
    /// Note: These operations are meant for il2cpp.
    /// In normal C# using 'fixed' is really slow and should be avoided in hot paths, but in il2cpp it is a free operation.
    /// We use this variants because <see cref="Unsafe.AsRef{T}"/> is slow in il2cpp and generics have lots of
    /// unnecessary internal checks.
    /// Note that the <see cref="Plus(ref byte,int)"/> pointers will probably break in GCs with compacting (i.e., normal .NET). 
    /// </summary>
    public static class SpanUnsafeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T GetReference<T>(this ReadOnlySpan<T> span) where T : unmanaged
        {
            fixed (T* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T* GetPointer<T>(this Span<T> span) where T : unmanaged
        {
            return (T*)Unsafe.AsPointer(ref span[0]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T* GetPointer<T>(this ReadOnlySpan<T> span) where T : unmanaged
        {
            return (T*)Unsafe.AsPointer(ref span.GetReference());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte Plus(this ref byte ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref sbyte Plus(this ref sbyte ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ushort Plus(this ref ushort ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref short Plus(this ref short ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref uint Plus(this ref uint ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref int Plus(this ref int ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref float Plus(this ref float ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref double Plus(this ref double ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref long Plus(this ref long ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ulong Plus(this ref ulong ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref char Plus(this ref char ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Vector2 Plus(this ref Vector2 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Vector3 Plus(this ref Vector3 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Color32 Plus(this ref Color32 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref float4x2 Plus(this ref float4x2 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref float4 Plus(this ref float4 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref uint4 Plus(this ref uint4 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Point Plus(this ref Point ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref R16 Plus(this ref R16 ptr, int i) => ref Unsafe.Add(ref ptr, i);

        /// <summary>
        /// Creates a span from the given pointer and size. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<byte> AsSpan(this IntPtr ptr, int size) => new(ptr.ToPointer(), size);
    }
}