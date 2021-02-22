using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Msgs
{
    /// <summary>
    /// A reference to a rented array. Similar to <see cref="Rent{T}"/>, but meant to be passed around.
    /// Disposing this object will return the array to the pool, but only if it still owns the array.
    /// Ownership can be transferred to another <see cref="UniqueRef{T}"/> or a <see cref="SharedRef{T}"/>.
    /// Once ownership has been transferred, this object will point to an empty array.
    /// For backwards compatibility, this object can also point to an existing non-rented array, in which case
    /// disposing it will do nothing.
    /// </summary>
    /// <typeparam name="T">The type of the rented array.</typeparam>
    public sealed class UniqueRef<T> : IDisposable, IReadOnlyList<T>
    {
        static readonly ArrayPool<T> Pool = ArrayPool<T>.Shared;
        static readonly bool TypeNeedsClear = !typeof(T).IsValueType;

        public static readonly UniqueRef<T> Empty = new();

        int length;
        T[]? array;
        bool ownArray;
        readonly bool clearArray;

        /// <summary>
        /// The length of the array. It is less or equal to the size of <see cref="Array"/>.
        /// </summary>
        public int Length => length;

        /// <summary>
        /// A reference to the array. 
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Thrown if the object has been disposed or the array has been released.
        /// </exception>
        public T[] Array =>
            array ?? throw new ObjectDisposedException($"{this}: Rented array has been disposed or released.");

        public ArraySegment<T> Segment => new(array!, 0, length);

        UniqueRef()
        {
            length = 0;
            ownArray = false;
            array = System.Array.Empty<T>();
        }

        UniqueRef(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            length = array.Length;
            this.array = array;
            ownArray = false;
        }

        UniqueRef(UniqueRef<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            length = other.Length;
            array = other.array;
            ownArray = other.ownArray;
            other.ownArray = false;
            other.array = null;
            other.length = 0;
        }

        /// <summary>
        /// Rents a new array.
        /// </summary>
        /// <param name="length">The size of the rented array.</param>
        /// <param name="clearArray">Whether the array pool should clear the array after disposing.</param>
        public UniqueRef(uint length, bool clearArray = false)
        {
            switch (length)
            {
                case 0:
                    array = System.Array.Empty<T>();
                    this.length = 0;
                    ownArray = false;
                    break;
                default:
                    array = Pool.Rent((int) length);
                    this.length = (int) length;
                    ownArray = true;
                    this.clearArray = clearArray | TypeNeedsClear;
                    break;
            }
        }

        public UniqueRef(int length, bool clearArray = false) : this((uint) length, clearArray)
        {
        }

        /// <summary>
        /// Rents a new array.
        /// </summary>
        /// <param name="length">The size of the rented array.</param>
        public UniqueRef(int length) : this((uint) length)
        {
        }

        /// <summary>
        /// If this object owns the array, returns it to the array pool.
        /// </summary>
        public void Dispose()
        {
            if (!ownArray || array == null)
            {
                return;
            }

            Pool.Return(array, clearArray);
            array = null;
            ownArray = false;
            length = 0;
        }

        /// <summary>
        /// Creates a new <see cref="UniqueRef{T}"/> that will own the rented array of this object.
        /// After this call, this ref will point to an empty array.
        /// </summary>
        /// <returns>A new ref that owns the array of this object.</returns>
        public UniqueRef<T> Release()
        {
            return new(this);
        }

        public override string ToString()
        {
            return
                $"[UniqueRef Type={typeof(T).Name} Count={length.ToString()}" +
                $" RealSize={array?.Length.ToString() ?? "[disposed]"}" +
                (ownArray ? "" : " (wrap)") +
                "]";
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public RentEnumerator<T> GetEnumerator() => new(array!, length);
        public T this[int index]
        {
            get
            {
                if ((uint) index >= length)
                {
                    throw new IndexOutOfRangeException();
                }

                return array![index];
            }
            set
            {
                if ((uint) index >= length)
                {
                    throw new IndexOutOfRangeException();
                }

                array![index] = value;
            }
        }

        /// <summary>
        /// Creates a new ref that points to the given array, but does not own it.
        /// Disposing this object will do nothing.
        /// </summary>
        /// <param name="t">The array to be wrapped.</param>
        /// <returns>A new ref wrapping the given array.</returns>
        /// <exception cref="ArgumentNullException">Thrown if t is null.</exception>
        public static implicit operator UniqueRef<T>(T[] t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            return new UniqueRef<T>(t);
        }

        int IReadOnlyCollection<T>.Count => Length;
    }

    public sealed class DisposableRef<T> : IDisposable, IReadOnlyList<T> where T : IDisposable
    {
        readonly UniqueRef<T> uRef;
        public DisposableRef(UniqueRef<T> uRef)
        {
            this.uRef = uRef;
        }
        
        public T this[int index]
        {
            get => uRef[index];
            set => uRef[index] = value;
        }

        public int Length => uRef.Length;
        int IReadOnlyCollection<T>.Count => uRef.Length;

        public void Dispose()
        {
            foreach (var child in uRef)
            {
                child.Dispose();
            }
            
            uRef.Dispose();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public RentEnumerator<T> GetEnumerator()
        {
            return uRef.GetEnumerator();
        }
    }
}