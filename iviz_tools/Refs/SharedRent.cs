using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Iviz.Tools;

public class SharedRent<T> : IDisposable where T : unmanaged
{
    public static readonly SharedRent<T> Empty = new(0);

    volatile int refCount;

    public readonly int Length;
    public readonly T[] Array;
    readonly bool isOwn;

    public SharedRent(int length)
    {
        switch (length)
        {
            case < 0:
                throw new ArgumentException($"{nameof(length)} cannot be negative", nameof(length));
            case 0:
                Array = System.Array.Empty<T>();
                Length = 0;
                break;
            default:
                Array = ArrayPool<T>.Shared.Rent(length);
                Length = length;
                break;
        }

        isOwn = true;
        refCount = 1;
    }

    SharedRent(T[] array)
    {
        Array = array;
        Length = array.Length;
        isOwn = false;
    }

    public SharedRent<T> Share()
    {
        Interlocked.Increment(ref refCount);
        return this;
    }

    public void Dispose()
    {
        if (Interlocked.Decrement(ref refCount) != 0)
        {
            return;
        }

        if (isOwn && Array.Length != 0)
        {
            ArrayPool<T>.Shared.Return(Array);
        }
    }

    public Span<T> AsSpan() => Array.AsSpan(0, Length);
    public ReadOnlySpan<T> AsReadOnlySpan() => AsSpan();
    public Memory<T> AsMemory() => new(Array, 0, Length);
    public ArraySegment<T> AsArraySegment() => new(Array, 0, Length);
    public Span<T> Slice(int start, int count) => AsSpan().Slice(start, count);
    public RentEnumerator<T> GetEnumerator() => new(Array, Length);
    public override string ToString() => $"[{nameof(SharedRent<T>)} Length={Length.ToString()}]";
    public Span<T> this[Range range] => AsSpan()[range];
    public static implicit operator Span<T>(SharedRent<T> rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<T>(SharedRent<T> rent) => rent.AsSpan();
    public static implicit operator Memory<T>(SharedRent<T> rent) => rent.AsMemory();
    public static implicit operator ReadOnlyMemory<T>(SharedRent<T> rent) => rent.AsMemory();
    public static implicit operator SharedRent<T>(T[] array) => new(array);
}