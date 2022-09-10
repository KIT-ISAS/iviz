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
    static ArrayPool<byte> Pool => ArrayPool<byte>.Shared;

    bool disposed;
    public byte[] Array;

    public Span<byte> AsSpan() => new(Array);
    public ReadOnlySpan<byte> AsReadOnlySpan() => new(Array);
    public Span<byte> this[Range range] => Array.AsSpan(range);
    public Span<byte> Slice(int start, int count) => AsSpan().Slice(start, count);

    public ResizableRent()
    {
        Array = Pool.Rent(16);
    }

    public void EnsureCapacity(int size)
    {
        if (disposed)
        {
            throw new ObjectDisposedException("this", "Dispose() has already been called on this object.");
        }

        if (Array.Length >= size)
        {
            return;
        }

        Pool.Return(Array);
        Array = Pool.Rent(size);
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        Pool.Return(Array);
    }

    public static implicit operator Span<byte>(ResizableRent rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<byte>(ResizableRent rent) => rent.AsReadOnlySpan();
}