using System;
using System.Buffers;
using Iviz.Tools;

namespace Iviz.Roslib.Utils;

/// <summary>
/// Class used by the senders and listeners. Basically keeps a rented array,
/// and if the rent is too small, releases it and rents a larger one.
/// </summary>
internal sealed class ResizableRent : IDisposable
{
    static readonly ArrayPool<byte> Pool = ArrayPool<byte>.Shared;

    bool disposed;
    byte[] buffer;

    public byte[] Array => buffer;

    public Span<byte> AsSpan() => buffer;
    public ReadOnlySpan<byte> AsReadOnlySpan() => buffer;
    public Span<byte> this[Range range] => buffer.AsSpan(range);
        
    public ResizableRent()
    {
        buffer = Pool.Rent(16);
    }

    public void EnsureCapacity(int size)
    {
        if (disposed)
        {
            throw new ObjectDisposedException("this", "Dispose() has already been called on this object.");
        }

        if (buffer.Length >= size)
        {
            return;
        }

        Pool.Return(buffer);
        buffer = Pool.Rent(size);
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        Pool.Return(buffer);
    }

    public static implicit operator Span<byte>(ResizableRent rent)
    {
        return rent.AsSpan();
    }

    public static implicit operator ReadOnlySpan<byte>(ResizableRent rent)
    {
        return rent.AsReadOnlySpan();
    }
}