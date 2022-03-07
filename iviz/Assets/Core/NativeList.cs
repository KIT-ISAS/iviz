#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Iviz.Core
{
    public sealed class NativeList<T> : IDisposable where T : unmanaged
    {
        static NativeArray<T> emptyArray;

        static NativeArray<T> EmptyArray => emptyArray.IsCreated
            ? emptyArray
            : (emptyArray = new NativeArray<T>(0, Allocator.Persistent));

        NativeArray<T> array;
        int length;
        bool disposed;

        public int Capacity => array.Length;

        public void EnsureCapacity(int value)
        {
            if (value <= Capacity)
            {
                return;
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Capacity cannot be negative");
            }

            if (value > NativeList.MaxElements)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Capacity exceeds maximal size");
            }

            int newCapacity = Math.Max(Capacity, 16);
            while (newCapacity < value)
            {
                newCapacity *= 2;
            }

            var newArray = new NativeArray<T>(newCapacity, Allocator.Persistent);
            if (array.Length != 0)
            {
                NativeArray<T>.Copy(array, newArray, length);
                array.Dispose();
            }

            array = newArray;

            if (array.Length < value)
            {
                // this post-condition ensures that the unsafes later won't crash (as long as we're single-threaded) 
                throw new InvalidOperationException("Array enlargement failed. Something went wrong!");
            }
        }

        public void Add(in T t)
        {
            int nextLength = length + 1;
            EnsureCapacity(nextLength);
            UnsafeGet(length) = t;
            length = nextLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddUnsafe(in T t)
        {
            UnsafeGet(length++) = t;
        }

        public void AddRange(ReadOnlySpan<T> otherArray)
        {
            if (otherArray.Length == 0)
            {
                return;
            }

            EnsureCapacity(length + otherArray.Length);
            otherArray.CopyTo(array.AsSpan(length, otherArray.Length));
            length += otherArray.Length;
        }

        public NativeArray<T> AsArray() => length == 0 ? EmptyArray : array.GetSubArray(0, length);

        public int Length => length;

        public ref T this[int index]
        {
            [UsedImplicitly]
            get => ref AsSpan()[index];
        }

        public Span<T> AsSpan() => MemoryMarshal.CreateSpan(ref array.GetUnsafeRef(), length);

        ReadOnlySpan<T> AsReadOnlySpan() => MemoryMarshal.CreateReadOnlySpan(ref array.GetUnsafeRef(), length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref T UnsafeGet(int index) => ref Unsafe.Add(ref array.GetUnsafeRef(), index);

        public void Clear()
        {
            length = 0;
        }

        public void Trim()
        {
            if (Capacity <= 16)
            {
                return;
            }

            array.Dispose();
            array = new NativeArray<T>(16, Allocator.Persistent);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (array.Length != 0)
            {
                array.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        ~NativeList()
        {
            Dispose();
        }

        public void Resize(int newSize)
        {
            EnsureCapacity(newSize);
            length = newSize;
        }

        public static implicit operator ReadOnlySpan<T>(NativeList<T> list)
        {
            return list.AsReadOnlySpan();
        }
    }

    public static class NativeList
    {
        public const int MaxElements = 1024 * 1024 * 64;
    }
}