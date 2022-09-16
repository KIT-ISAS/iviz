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
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFrom<T>(this Texture2D dst, ReadOnlySpan<T> srcSpan) where T : unmanaged
        {
            var srcBytes = MemoryMarshal.AsBytes(srcSpan);
            fixed (byte* srcBytesPtr = &srcBytes[0])
            {
                Unsafe.CopyBlock(dst.GetUnsafePtr(), srcBytesPtr, (uint)srcBytes.Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this ReadOnlySpan<byte> src, Span<byte> dst)
        {
            fixed (byte* srcPtr = &src[0])
            fixed (byte* dstPtr = &dst[0])
            {
                Unsafe.CopyBlock(dstPtr, srcPtr, (uint)src.Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this Span<byte> src, Span<byte> dst)
        {
            Unsafe.CopyBlock(ref dst[0], ref src[0], (uint)src.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(this Span<byte> src, byte c)
        {
            Unsafe.InitBlock(ref src[0], c, (uint)src.Length);
        }

        public static ReadOnlySpan<T> Cast<T>(this ReadOnlySpan<byte> src) where T : unmanaged
        {
            return MemoryMarshal.Cast<byte, T>(src);
        }

        public static Span<T> Cast<T>(this Span<byte> src) where T : unmanaged =>  MemoryMarshal.Cast<byte, T>(src);

        public static T Read<T>(this ReadOnlySpan<byte> span) where T : unmanaged => MemoryMarshal.Read<T>(span);

        public static float ReadFloat(this ReadOnlySpan<byte> span)
        {
            if (span.Length < sizeof(float)) ThrowHelper.ThrowArgumentOutOfRange();
            fixed (byte* spanPtr = &span[0]) return *(float*)spanPtr;
        }

        
        [StructLayout(LayoutKind.Explicit)]
        struct ListConverter
        {
            [UsedImplicitly]
            sealed class OpenList
            {
                public readonly Array? items;
            }

            [FieldOffset(0)] public IList list;
            [FieldOffset(0)] readonly OpenList openList;

            public Array? ExtractArray() => openList.items;
        }

        static Array? ExtractArray(IList list)
        {
            return new ListConverter { list = list }.ExtractArray();
        }

        /// <summary>
        /// Creates a span from the given pointer and size. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> AsSpan(this IntPtr ptr, int size) => new(ptr.ToPointer(), size);


        /*
        ref struct GetLengthHelper
        {
            public Span<byte> span;
            public int length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), Preserve, SkipLocalsInit]
        public static int GetLength(this Span<byte> span)
        {
            GetLengthHelper h;
            h.span = span;
            h.length = 0;
            return Unsafe.Subtract(ref h.length, 1);
        }
        */
    }
}