#nullable enable

using System;
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
        }

        public void Add(in T t)
        {
            int nextLength = length + 1;
            EnsureCapacity(nextLength);
            UnsafeGet(length) = t;
            length = nextLength;
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
            get
            {
                if (index >= length)
                {
                    throw new IndexOutOfRangeException();
                }

                return ref UnsafeGet(index);
            }
        }

        public unsafe Span<T> AsSpan() => new(array.GetUnsafePtr(), length);

        public unsafe ReadOnlySpan<T> AsReadOnlySpan() => new(array.GetUnsafeReadOnlyPtr(), length);

        unsafe ref T UnsafeGet(int index) =>
            ref *((T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(array) + index);

        public void Clear()
        {
            length = 0;
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
}