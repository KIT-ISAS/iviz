#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;
using Iviz.Tools;
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
            return new Span<T>(ExtractArray(list), 0, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list) where T : unmanaged
        {
            return new ReadOnlySpan<T>(ExtractArray(list), 0, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array, Range range)
        {
            return array.AsSpan(range);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFrom<T>(this Texture2D dst, ReadOnlySpan<T> srcSpan) where T : unmanaged
        {
            var srcBytes = MemoryMarshal.AsBytes(srcSpan);
            fixed (byte* srcBytesPtr = srcBytes)
            {
                Unsafe.CopyBlock(dst.GetUnsafePtr(), srcBytesPtr, (uint)srcBytes.Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this ReadOnlySpan<byte> src, Span<byte> dst)
        {
            fixed (byte* srcPtr = src)
            fixed (byte* dstPtr = dst)
            {
                Unsafe.CopyBlock(dstPtr, srcPtr, (uint)src.Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this Span<byte> src, Span<byte> dst)
        {
            BlockCopyTo((ReadOnlySpan<byte>)src, dst);
        }

        public static ReadOnlySpan<T> Cast<T>(this ReadOnlySpan<byte> src) where T : unmanaged
        {
            return MemoryMarshal.Cast<byte, T>(src);
        }

        public static Span<T> Cast<T>(this Span<byte> src) where T : unmanaged
        {
            return MemoryMarshal.Cast<byte, T>(src);
        }

        [StructLayout(LayoutKind.Explicit)]
        struct ListConverter
        {
            [UsedImplicitly]
            class OpenList
            {
                public Array? items;
            }

            [FieldOffset(0)] public IList list;
            [FieldOffset(0)] readonly OpenList openList;

            public Array? ExtractArray() => openList.items;
        }

        static T[] ExtractArray<T>(List<T> list)
        {
            return (T[]?)new ListConverter { list = list }.ExtractArray() ?? Array.Empty<T>();
        }
        
        /// <summary>
        /// Creates a span from the given pointer and size. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> AsSpan(this IntPtr ptr, int size) => new(ptr.ToPointer(), size);
    }
}