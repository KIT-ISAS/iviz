using System;
using System.Threading;

namespace Iviz.Msgs
{
    /// <summary>
    /// A shared reference to a rented array. Similar to <see cref="UniqueRef{T}"/>,
    /// but multiple <see cref="SharedRef{T}"/> instances can be created that point to the same array.
    /// Ownership of the rented array is collective, and the rented array will only be
    /// returned once all the shared references are disposed. 
    /// </summary>
    /// <typeparam name="T">The type of the rented array.</typeparam>
    public sealed class SharedRef<T> : IDisposable
    {
        public T[] Array => reference.Array;
        public int Length => reference.Length;
        readonly UniqueRef<T> reference;
        readonly CountdownEvent cd;
        bool disposed;

        /// <summary>
        /// Creates a new shared reference by releasing the array from the given <see cref="UniqueRef{T}"/>.
        /// </summary>
        /// <param name="t">The unique ref that owns the array. Ownership will be removed from this value.</param>
        public SharedRef(UniqueRef<T> t)
        {
            reference = t.Release();
            cd = new CountdownEvent(1);
        }

        SharedRef(SharedRef<T> other)
        {
            reference = other.reference;
            cd = other.cd;
            cd.AddCount();
        }

        /// <summary>
        /// Creates a new <see cref="SharedRef{T}"/> that points to the same rented array as this ref.
        /// The array will not be returned unless all instances created by this function are disposed.
        /// </summary>
        /// <returns>A new shared ref.</returns>
        public SharedRef<T> Share()
        {
            return new(this);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            if (!cd.Signal())
            {
                return;
            }

            reference.Dispose();
            cd.Dispose();
        }
        
        public RentEnumerator<T> GetEnumerator() => reference.GetEnumerator();

        public override string ToString()
        {
            return $"[SharedRef Ref={reference} Count={cd.CurrentCount.ToString()}]";
        }
    }
}