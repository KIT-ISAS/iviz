using System;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib.Utils
{
    /// <summary>
    /// Class used by the senders and listeners. Basically keeps a rented array,
    /// and if the rent is too small, releases it and rents a larger one.
    /// </summary>
    /// <typeparam name="T">The size of the rent. Must be unmanaged.</typeparam>
    internal sealed class ResizableRent<T> : IDisposable where T : unmanaged
    {
        bool disposed;
        Rent<T> buffer;

        public T[] Array => buffer.Array;

        public ResizableRent(int size)
        {
            buffer = new Rent<T>(size);
        }

        public void EnsureCapability(int size)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("this", "Dispose() has already been called on this object.");
            }

            if (buffer.Array.Length >= size)
            {
                return;
            }

            buffer.Dispose();
            buffer = new Rent<T>(size);
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            buffer.Dispose();
        }
    }
}