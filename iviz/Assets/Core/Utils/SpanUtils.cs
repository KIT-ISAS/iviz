#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

namespace Iviz.Core
{
    public static class SpanUtils
    {
        public static Span<byte> AsSpan(this Texture2D texture)
        {
            return texture.GetRawTextureData<byte>().AsSpan();
        }

        static Span<T> AsSpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return MemoryMarshal.CreateSpan(ref array.GetUnsafeRef(), array.Length);
        }

        public static Span<T> AsSpan<T>(this in NativeArray<T> array, int start, int length) where T : unmanaged
        {
            return array.AsSpan().Slice(start, length);
        }

        public static Span<byte> CreateSpan(this IntPtr ptr, int length)
        {
            ref byte nullPtr = ref MemoryMarshal.GetReference(default(Span<byte>));
            ref byte refPtr = ref Unsafe.Add(ref nullPtr, ptr);
            return MemoryMarshal.CreateSpan(ref refPtr, length);
        }

        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return MemoryMarshal.CreateReadOnlySpan(ref array.GetUnsafeRef(), array.Length);
        }

        public static Span<T> AsSpan<T>(this List<T> list) where T : unmanaged
        {
            return new Span<T>(list.ExtractArray(), 0, list.Count);
        }

        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list) where T : unmanaged
        {
            return new ReadOnlySpan<T>(list.ExtractArray(), 0, list.Count);
        }

        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array, Range range)
        {
            return array.AsSpan(range);
        }

        /// <summary>
        /// Convenience function to obtain spans from <see cref="Memory{T}"/> the same way as with arrays. 
        /// </summary>
        public static Span<T> AsSpan<T>(this Memory<T> memory) where T : unmanaged => memory.Span;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyFrom<T>(this Texture2D dst, ReadOnlySpan<T> srcSpan) where T : unmanaged
        {
            var srcBytes = MemoryMarshal.AsBytes(srcSpan);
            var dstBytes = dst.AsSpan();
            Unsafe.CopyBlock(
                ref MemoryMarshal.GetReference(dstBytes),
                ref MemoryMarshal.GetReference(srcBytes),
                (uint)srcBytes.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo<T>(this ReadOnlySpan<T> src, Span<T> dst) where T : unmanaged
        {
            var srcBytes = MemoryMarshal.AsBytes(src);
            var dstBytes = MemoryMarshal.AsBytes(dst);
            Unsafe.CopyBlock(
                ref MemoryMarshal.GetReference(dstBytes),
                ref MemoryMarshal.GetReference(srcBytes),
                (uint)srcBytes.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo<T>(this Span<T> src, Span<T> dst) where T : unmanaged
        {
            BlockCopyTo((ReadOnlySpan<T>)src, dst);
        }

        public static ReadOnlySpan<T> Cast<T>(this ReadOnlySpan<byte> src) where T : unmanaged
        {
            return MemoryMarshal.Cast<byte, T>(src);
        }

        public static Span<T> Cast<T>(this Span<byte> src) where T : unmanaged
        {
            return MemoryMarshal.Cast<byte, T>(src);
        }        
        
        static Func<object, Array>? extractArrayFromListTypeFn;

        static Func<object, Array> ExtractArrayFromList
        {
            get
            {
                if (extractArrayFromListTypeFn != null)
                {
                    return extractArrayFromListTypeFn;
                }

                var assembly = typeof(MonoBehaviour /* any type in UnityEngine.CoreModule */).Assembly;
                var type = assembly?.GetType("UnityEngine.NoAllocHelpers");
                var methodInfo = type?.GetMethod("ExtractArrayFromList", BindingFlags.Static | BindingFlags.Public);
                if (methodInfo == null)
                {
                    throw new InvalidOperationException("Failed to retrieve function ExtractArrayFromList");
                }

                extractArrayFromListTypeFn =
                    (Func<object, Array>)methodInfo.CreateDelegate(typeof(Func<object, Array>));

                return extractArrayFromListTypeFn;
            }
        }

        static T[] ExtractArray<T>(this List<T> list) => (T[])ExtractArrayFromList(list);
    }
}