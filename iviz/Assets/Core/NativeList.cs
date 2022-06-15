#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

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

            if (value is < 0 or > NativeList.MaxElements)
            {
                ThrowHelper.ThrowArgumentOutOfRange(nameof(value));
            }

            int newCapacity = Mathf.Max(Capacity, 16);
            while (newCapacity < value)
            {
                newCapacity *= 2;
            }

            var newArray =
                new NativeArray<T>(newCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
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
            SetUnsafe(length, t);
            length = nextLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddUnsafe(in T t)
        {
            SetUnsafe(length++, t);
        }

        unsafe void SetUnsafe(int index, in T t)
        {
            array.GetUnsafePtr()[index] = t;
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

        public unsafe Span<T> AsSpan() => new(array.GetUnsafePtr(), length);

        unsafe ReadOnlySpan<T> AsReadOnlySpan() => new(array.GetUnsafePtr(), length);

        public void Clear()
        {
            length = 0;
        }

        public void Reset()
        {
            length = 0;
            
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

        public static implicit operator ReadOnlySpan<T>(NativeList<T> list) => list.AsReadOnlySpan();
        public static implicit operator NativeArray<T>(NativeList<T> list) => list.AsArray();
    }

    public static class NativeList
    {
        public const int MaxElements = 1024 * 1024 * 64;
    }
}