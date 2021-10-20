using System;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib.Utils
{
    /// <summary>
    /// Class used by the senders and listeners. Basically keeps a rented array,
    /// and if the rent is too small, releases it and rents a larger one.
    /// </summary>
    internal sealed class ByteBufferRent : IDisposable 
    {
        bool disposed;
        Rent<byte> buffer;

        public byte[] Array => buffer.Array;

        public ByteBufferRent(int size)
        {
            buffer = new Rent<byte>(size);
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
            buffer = new Rent<byte>(size);
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