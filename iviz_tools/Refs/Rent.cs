using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Tools
{
    /// <summary>
    /// Wrapper around renting and returning an array from an <see cref="ArrayPool{T}"/>.
    /// Creating this value will rent an array of (at least) the given size, and disposing it will return it.
    /// This class is meant only for short-lived rents.
    /// </summary>
    /// <typeparam name="T">
    /// The array type. Must be unmanaged. This is to prevent object references from remaining in the array
    /// after returning it to the array pool, which keeps them from being garbage collected.
    /// For a more generic version that clears the array after disposing, use <see cref="RentAndClear{T}"/>.
    /// </typeparam>
    public readonly struct Rent<T> : IReadOnlyList<T>, IDisposable  where T : unmanaged
    {
        static readonly ArrayPool<T> Pool = ArrayPool<T>.Shared;

        public readonly int Length;
        public readonly T[] Array;

#if !NETSTANDARD2_0
        public Span<T> Span => new(Array, 0, Length);
#endif

        public Rent(int count)
        {
            switch (count)
            {
                case < 0:
                    throw new ArgumentException("Count cannot be negative", nameof(count));
                case 0:
                    Array = System.Array.Empty<T>();
                    Length = 0;
                    break;
                default:
                    Array = Pool.Rent(count);
                    Length = count;
                    break;
            }
        }

        public void Dispose()
        {
            if (Length > 0)
            {
                Pool.Return(Array);
            }
        }

        public override string ToString()
        {
            return $"[Rent Type={typeof(T).Name} Length={Length} RealSize={(Array != null ? Array.Length : 0)}]";
        }

        public RentEnumerator<T> GetEnumerator() => new(Array, Length);

        public RentRefEnumerable<T> Ref() => new(Array, Length);

        public ref T this[int index]
        {
            get
            {
                if ((uint) index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                
                return ref Array[index];                
            }
        }

        public void CopyFrom(IEnumerable<T> src)
        {
            int i = 0;
            foreach (T t in src)
            {
                Array[i++] = t;
            }
        }

        int IReadOnlyCollection<T>.Count => Length;
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        T IReadOnlyList<T>.this[int index] => Array[index];            
    }

    public static class Rent
    {
        public static Rent<T> Empty<T>() where T : unmanaged => new(0);
    }
    
}