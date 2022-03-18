using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iviz.Tools;

public readonly struct SharedRent<T> : IDisposable  where T : unmanaged
{
    readonly byte[] array;
    readonly int byteLength;

    public readonly int Length;
    public Span<byte> ByteSpan => array.AsSpan(0, byteLength);
    public Span<T> AsSpan() => MemoryMarshal.Cast<byte, T>(ByteSpan);
    public ReadOnlySpan<T> AsReadOnlySpan() => AsSpan();
    public Span<T> Slice(int start, int count) => AsSpan().Slice(start, count);

    public SharedRent(int length)
    {
        switch (length)
        {
            case < 0:
                throw new ArgumentException("Count cannot be negative", nameof(length));
            case 0:
                array = Array.Empty<byte>();
                byteLength = 0;
                Length = 0;
                break;
            default:
                byteLength = length * Unsafe.SizeOf<T>();
                array = ArrayPool<byte>.Shared.Rent(byteLength);
                Length = length;
                break;
        }
    }
    public void Dispose()
    {
        if (array.Length != 0)
        {
            ArrayPool<byte>.Shared.Return(array);
        }
    }

    public override string ToString()
    {
        return $"[SharedRent Type={typeof(T).Name} Length={Length.ToString()}]";
    }

    public Span<T> this[Range range] => AsSpan()[range];

    public static implicit operator Span<T>(SharedRent<T> rent)
    {
        return rent.AsSpan();
    }
        
    public static implicit operator ReadOnlySpan<T>(SharedRent<T> rent)
    {
        return rent.AsReadOnlySpan();
    }
}