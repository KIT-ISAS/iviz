#nullable enable

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Iviz.Core
{
    public sealed unsafe class NativeList<T> : IDisposable where T : unmanaged
    {
        static NativeArray<T> emptyArray;

        static NativeArray<T> EmptyArray => emptyArray.IsCreated
            ? emptyArray
            : (emptyArray = new NativeArray<T>(0, Allocator.Persistent));

        NativeArray<T> array;
        T* unsafePtr;
        
        int length;
        int capacity;
        
        bool disposed;

        public int Length => length;
        public int Capacity => capacity;
        public T* GetUnsafePtr() => unsafePtr;
        
        public void EnsureCapacity(int value)
        {
            if (value <= capacity)
            {
                return;
            }

            if (value is < 0 or > NativeList.MaxElements)
            {
                ThrowHelper.ThrowArgumentOutOfRange(nameof(value));
            }

            int newCapacity = Mathf.Max(capacity, 16);
            while (newCapacity < value)
            {
                newCapacity *= 2;
            }

            var newArray =
                new NativeArray<T>(newCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
            if (array.Length != 0)
            {
                NativeArray<T>.Copy(array, 0, newArray, 0, length);
                array.Dispose();
            }

            array = newArray;
            capacity = newCapacity;
            unsafePtr = array.GetUnsafePtr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(in T t)
        {
            int nextLength = length + 1;
            if (nextLength >= capacity)
            {
                EnsureCapacity(nextLength);
            }

            unsafePtr[length] = t;
            length = nextLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddUnsafe(in T t)
        {
            unsafePtr[length++] = t;
        }

        public void Resize(int size)
        {
            if (size < 0) BuiltIns.ThrowArgumentOutOfRange(nameof(size));
            
            EnsureCapacity(size);
            length = size;
        }

        public void AddRange(ReadOnlySpan<T> otherArray)
        {
            int otherLength = otherArray.Length;
            if (otherLength == 0)
            {
                return;
            }

            EnsureCapacity(length + otherLength);

            var destination = new Span<T>(unsafePtr + length, otherLength);
            otherArray.CopyTo(destination);
            
            length += otherLength;
        }

        public NativeArray<T> AsArray() => length == 0 ? EmptyArray : array.GetSubArray(0, length); 

        Span<T> AsSpan() => new(unsafePtr, length);
        ReadOnlySpan<T> AsReadOnlySpan() => new(unsafePtr, length);

        public void Clear()
        {
            length = 0;
        }

        public void Reset()
        {
            length = 0;

            if (capacity <= 16)
            {
                return;
            }

            array.Dispose();
            
            array = new NativeArray<T>(16, Allocator.Persistent);
            capacity = 16;
            unsafePtr = array.GetUnsafePtr();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            unsafePtr = null;
            capacity = 0;

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

        public static implicit operator Span<T>(NativeList<T> list) => list.AsSpan();
        public static implicit operator ReadOnlySpan<T>(NativeList<T> list) => list.AsReadOnlySpan();
        public static implicit operator NativeArray<T>(NativeList<T> list) => list.AsArray();
    }

    public static class NativeList
    {
        public const int MaxElements = 1024 * 1024 * 64;
    }
}