using System;
using System.Buffers;
using System.Runtime.Serialization;
using System.Threading;

namespace Iviz.Tools;

public sealed class SharedRent : IDisposable
{
    static SharedRent? empty;
    public static SharedRent Empty => empty ??= new SharedRent(0);

    volatile int refCount;

    [IgnoreDataMember] public readonly int Length;
    [IgnoreDataMember] public readonly byte[] Array;
    readonly bool isOwn;

    public SharedRent(int length)
    {
        switch (length)
        {
            case < 0:
                Rent.ThrowArgumentNegative();
                goto case 0; // unreachable
            case 0:
                Array = System.Array.Empty<byte>();
                Length = 0;
                break;
            default:
                Array = ArrayPool<byte>.Shared.Rent(length);
                Length = length;
                break;
        }

        isOwn = true;
        refCount = 1;
    }

    SharedRent(byte[] array)
    {
        Array = array;
        Length = array.Length;
        isOwn = false;
    }

    public SharedRent Share()
    {
        Interlocked.Increment(ref refCount);
        return this;
    }

    public void Dispose()
    {
        if (!isOwn || Array.Length == 0) return;

        if (Interlocked.Decrement(ref refCount) != 0) return;

        ArrayPool<byte>.Shared.Return(Array);
    }

    public override string ToString() => $"[{nameof(SharedRent)} Type=byte Length={Length.ToString()}]";

    public Span<byte> AsSpan() => new(Array, 0, Length);
    public ReadOnlySpan<byte> AsReadOnlySpan() => new(Array, 0, Length);
    public Memory<byte> AsMemory() => new(Array, 0, Length);
    public ArraySegment<byte> AsArraySegment() => new(Array, 0, Length);
    public Span<byte> Slice(int start, int count) => new(Array, start, count);
    public RentEnumerator<byte> GetEnumerator() => new(Array, Length);
    public Span<byte> this[Range range] => AsSpan()[range];
    public static implicit operator Span<byte>(SharedRent rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<byte>(SharedRent rent) => rent.AsReadOnlySpan();
    public static implicit operator Memory<byte>(SharedRent rent) => rent.AsMemory();
    public static implicit operator ReadOnlyMemory<byte>(SharedRent rent) => new(rent.Array, 0, rent.Length);
    public static implicit operator SharedRent(byte[] array) => new(array);
}