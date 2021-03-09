using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Iviz.Msgs
{
    /// <summary>
    /// Wrapper around renting and returning an array from an <see cref="ArrayPool{T}"/>.
    /// Creating this value will rent an array of the given size, and disposing it will return it.
    /// This class is meant only for short-lived rents. If you want to pass the reference around,
    /// then you should probably use <see cref="UniqueRef{T}"/> or <see cref="SharedRef{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct Rent<T> : IDisposable where T : unmanaged
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

        public T this[int index]
        {
            get
            {
                if ((uint) index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return Array[index];
            }
            set
            {
                if ((uint) index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                Array[index] = value;
            }
        }
    }

    public readonly struct RentAndClear<T> : IDisposable
    {
        static readonly ArrayPool<T?> Pool = ArrayPool<T?>.Shared;

        public readonly int Length;
        public readonly T?[] Array;

#if !NETSTANDARD2_0
        public Span<T> Span => new(Array, 0, Length);
#endif

        public RentAndClear(int count)
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
            if (Length <= 0)
            {
                return;
            }

            for (int i = 0; i < Length; i++)
            {
                Array[i] = default;
            }

            Pool.Return(Array);
        }

        public override string ToString()
        {
            return $"[RentAndClear Type={typeof(T).Name} Length={Length} RealSize={(Array != null ? Array.Length : 0)}]";
        }

        public RentEnumerator<T?> GetEnumerator() => new(Array, Length);

        public T? this[int index]
        {
            get
            {
                if ((uint) index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return Array[index];
            }
            set
            {
                if ((uint) index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }

                Array[index] = value;
            }
        }
    }
}