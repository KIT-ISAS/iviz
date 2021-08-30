using System;
using System.Collections;
using System.Collections.Generic;
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

        public NativeList()
        {
        }

        public NativeList(int capacity)
        {
            EnsureCapacity(capacity);
        }

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

        public void AddRange([NotNull] NativeList<T> otherArray)
        {
            if (otherArray.Length == 0)
            {
                return;
            }
            
            EnsureCapacity(length + otherArray.Length);
            NativeArray<T>.Copy(otherArray.array, 0, array, length, otherArray.Length);
            length += otherArray.Length;
        }

        //public NativeArray<T>.Enumerator GetEnumerator() => AsArray().GetEnumerator();

        public NativeArray<T> AsArray() => length == 0 ? EmptyArray : array.GetSubArray(0, length);

        public int Length => length;


        /*
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        int IReadOnlyCollection<T>.Count => length;

        T IReadOnlyList<T>.this[int index] => index < length ? array[index] : throw new IndexOutOfRangeException();
        */

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

        unsafe ref T UnsafeGet(int index) => ref *((T*) array.GetUnsafePtr() + index);

        public void Clear()
        {
            length = 0;
        }

        public unsafe T* GetUnsafePtr() => (T*) array.GetUnsafePtr();

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

        public RefEnumerable Ref() => new RefEnumerable(array, length);

        public readonly struct RefEnumerable
        {
            readonly NativeArray<T> a;
            readonly int length;
            public RefEnumerable(in NativeArray<T> a, int length) => (this.a, this.length) = (a, length);
            public Enumerator GetEnumerator() => new Enumerator(a, length);
            
            public unsafe struct Enumerator
            {
                T* pos;
                readonly T* end;

                public Enumerator(in NativeArray<T> a, int length)
                {
                    if (length == 0)
                    {
                        pos = null;
                        end = null;
                        return;
                    }

                    T* ptr = (T*) a.GetUnsafePtr();
                    pos = ptr - 1;
                    end = ptr + length;
                }

                public bool MoveNext() => ++pos < end;
                public ref T Current => ref *pos;
            }
        }

    }
}