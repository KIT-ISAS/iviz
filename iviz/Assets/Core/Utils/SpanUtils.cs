#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Unity.Collections;
using UnityEngine;
using JetBrains.Annotations;

namespace Iviz.Core
{
    public static unsafe class SpanUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> AsSpan(this Texture2D texture)
        {
            return texture.GetRawTextureData<byte>().AsSpan();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte* GetUnsafePtr(this Texture2D texture)
        {
            return texture.GetRawTextureData<byte>().GetUnsafePtr();
        }

        public static Span<T> AsSpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return new Span<T>(array.GetUnsafePtr(), array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(this in NativeArray<T> array, int start, int length) where T : unmanaged
        {
            return array.AsSpan().Slice(start, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return new ReadOnlySpan<T>(array.GetUnsafePtr(), array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(this List<T> list) where T : unmanaged
        {
            return new Span<T>((T[]?)ExtractArray(list), 0, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list) where T : unmanaged
        {
            int count = list.Count;
            return count == 0
                ? default
                : new ReadOnlySpan<T>((T[]?)ExtractArray(list), 0, count);
            return new ReadOnlySpan<T>((T[]?)ExtractArray(list), 0, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFrom(this Texture2D dst, ReadOnlySpan<byte> srcSpan)
        {
            uint length = (uint)srcSpan.Length;
            if (length == 0) return;

            fixed (byte* srcBytesPtr = &srcSpan[0])
            {
                Unsafe.CopyBlock(dst.GetUnsafePtr(), srcBytesPtr, length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFrom(this Texture2D dst, ReadOnlySpan<float> srcSpan)
        {
            uint length = (uint)srcSpan.Length;
            if (length == 0) return;

            fixed (float* srcBytesPtr = &srcSpan[0])
            {
                Unsafe.CopyBlock(dst.GetUnsafePtr(), srcBytesPtr, length * sizeof(float));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this ReadOnlySpan<byte> src, Span<byte> dst)
        {
            int length = src.Length;
            if (length > dst.Length) ThrowHelper.ThrowArgumentOutOfRange();

            fixed (byte* srcPtr = src)
            fixed (byte* dstPtr = dst)
            {
                Unsafe.CopyBlock(dstPtr, srcPtr, (uint)length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this Span<byte> src, Span<byte> dst)
        {
            int length = src.Length;
            if (length == 0) return;
            if (length > dst.Length) ThrowHelper.ThrowArgumentOutOfRange();

            Unsafe.CopyBlock(ref dst[0], ref src[0], (uint)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(this Span<byte> src, byte c)
        {
            int length = src.Length;
            if (length == 0) return;
            
            Unsafe.InitBlock(ref src[0], c, (uint)length);
        }

        public static ReadOnlySpan<T> Cast<T>(this ReadOnlySpan<byte> src) where T : unmanaged =>
            MemoryMarshal.Cast<byte, T>(src);

        public static Span<T> Cast<T>(this Span<byte> src) where T : unmanaged =>
            MemoryMarshal.Cast<byte, T>(src);
        public static Span<T> Cast<T>(this Span<byte> src) where T : unmanaged => MemoryMarshal.Cast<byte, T>(src);

        public static T Read<T>(this ReadOnlySpan<byte> span) where T : unmanaged => MemoryMarshal.Read<T>(span);

        [UsedImplicitly]
        sealed class OpenList
        {
            public readonly Array? items;
        }

        static Array? ExtractArray(IList list)
        {
            return Unsafe.As<IList, OpenList>(ref list).items;
        }

        /// <summary>
        /// Creates a span from the given pointer and size. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> AsSpan(this IntPtr ptr, int size) => new(ptr.ToPointer(), size);
    }
}