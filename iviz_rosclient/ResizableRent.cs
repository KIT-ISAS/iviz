using System;
using System.Buffers;
using Iviz.Msgs;

namespace Iviz.Roslib;

/// <summary>
/// Class used by the senders and listeners. Basically keeps a rented array,
/// and if the rent is too small, releases it and rents a larger one.
/// </summary>
public sealed class ResizableRent : IDisposable
{
    static ArrayPool<byte> Pool => ArrayPool<byte>.Shared;

    bool disposed;
    public byte[] Array;

    public Span<byte> AsSpan() => new(Array);
    public ReadOnlySpan<byte> AsReadOnlySpan() => new(Array);
    public Span<byte> this[Range range] => Array.AsSpan(range);
    public Span<byte> Slice(int start, int count) => AsSpan().Slice(start, count);

    public ResizableRent(int startSize = 16)
    {
        Array = Pool.Rent(startSize);
    }

    public void EnsureCapacity(int size, bool retain = false)
    {
        if (disposed)
        {
            BuiltIns.ThrowObjectDisposed(nameof(ResizableRent));
        }

        if (Array.Length >= size)
        {
            return;
        }

        byte[] newArray = Pool.Rent(size);
        if (retain)
        {
            Buffer.BlockCopy(Array, 0, newArray, 0, Array.Length);
        }

        Pool.Return(Array);
        Array = newArray;
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