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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> AsSpan(this Texture2D texture)
        {
            return texture.GetRawTextureData<byte>().AsSpan();
        }

        static Span<T> AsSpan<T>(this in NativeArray<T> array) where T : unmanaged
        {
            return MemoryMarshal.CreateSpan(ref array.GetUnsafeRef(), array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(this in NativeArray<T> array, int start, int length) where T : unmanaged
        {
            return array.AsSpan().Slice(start, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<byte> AsReadOnlySpan(this in NativeArray<byte> array)
        {
            return MemoryMarshal.CreateReadOnlySpan(ref array.GetUnsafeRef(), array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> AsSpan<T>(this List<T> list) where T : unmanaged
        {
            return new Span<T>(list.ExtractArray(), 0, list.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list) where T : unmanaged
        {
            return new ReadOnlySpan<T>(list.ExtractArray(), 0, list.Count);
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
            var dstBytes = dst.AsSpan();
            Unsafe.CopyBlock(
                ref dstBytes.GetReference(),
                ref srcBytes.GetReference(),
                (uint)srcBytes.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BlockCopyTo(this ReadOnlySpan<byte> src, Span<byte> dst)
        {
            Unsafe.CopyBlock(
                ref src.GetReference(),
                ref dst.GetReference(),
                (uint)src.Length);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte GetReference(this Span<byte> span) => ref span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref uint GetReference(this Span<uint> span) => ref span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref byte GetReference(this ReadOnlySpan<byte> span) => ref *(byte*)span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref sbyte GetReference(this ReadOnlySpan<sbyte> span) => ref *(sbyte*)span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref uint GetReference(this ReadOnlySpan<uint> span) => ref *(uint*)span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref ushort GetReference(this ReadOnlySpan<ushort> span) => ref *(ushort*)span[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref char GetReference(this ReadOnlySpan<char> span)
        {
            fixed (char* ptr = &span[0]) return ref *ptr;
        }

        public static ref T GetReference<T>(this ReadOnlySpan<T> span)
        {
            return ref MemoryMarshal.GetReference(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref byte Plus(this ref byte ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ushort Plus(this ref ushort ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref uint Plus(this ref uint ptr, int i) => ref Unsafe.Add(ref ptr, i);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref int Plus(this ref int ptr, int i) => ref Unsafe.Add(ref ptr, i);

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