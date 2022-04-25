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
    /// They are used in <see cref="GetReference(System.ReadOnlySpan{byte})"/> and variants because
    /// <see cref="Unsafe.AsRef"/> is slow in il2cpp and generics have lots of unnecessary internal checks.
    /// Also the <see cref="Plus(ref byte,int)"/> pointers will probably break in GCs with compacting (i.e., normal .NET). 
    /// </summary>
    public static class SpanUnsafeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref byte GetReference(this ReadOnlySpan<byte> span)
        {
            fixed (byte* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref sbyte GetReference(this ReadOnlySpan<sbyte> span)
        {
            fixed (sbyte* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref ushort GetReference(this ReadOnlySpan<ushort> span)
        {
            fixed (ushort* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref short GetReference(this ReadOnlySpan<short> span)
        {
            fixed (short* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref char GetReference(this ReadOnlySpan<char> span)
        {
            fixed (char* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref uint GetReference(this ReadOnlySpan<uint> span)
        {
            fixed (uint* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref int GetReference(this ReadOnlySpan<int> span)
        {
            fixed (int* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref float GetReference(this ReadOnlySpan<float> span)
        {
            fixed (float* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref double GetReference(this ReadOnlySpan<double> span)
        {
            fixed (double* pPtr = &span[0]) return ref *pPtr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte GetReference(this Span<byte> span) => ref span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref uint GetReference(this Span<uint> span) => ref span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref float4x2 GetReference(this ReadOnlySpan<float4x2> span)
        {
            fixed (float4x2* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref Vector3 GetReference(this ReadOnlySpan<Vector3> span)
        {
            fixed (Vector3* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref Vector4 GetReference(this ReadOnlySpan<Vector4> span)
        {
            fixed (Vector4* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref Color GetReference(this ReadOnlySpan<Color> span)
        {
            fixed (Color* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref Color32 GetReference(this ReadOnlySpan<Color32> span)
        {
            fixed (Color32* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref Vector2 GetReference(this ReadOnlySpan<Vector2> span)
        {
            fixed (Vector2* ptr = &span[0]) return ref *ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref float4 GetReference(this ReadOnlySpan<float4> span)
        {
            fixed (float4* pPtr = &span[0]) return ref *pPtr;
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
        public static ref Point Plus(this ref Point ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<byte> AsSpan(this IntPtr ptr, int size) => new(ptr.ToPointer(), size);
    }
}