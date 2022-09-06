using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Iviz.Tools;

/// <summary>
/// Wrapper around renting and returning an array from an <see cref="ArrayPool{T}"/>.
/// Creating this value will rent an array of (at least) the given size, and disposing it will return it.
/// This class is meant only for short-lived rents.
/// </summary>
/// <typeparam name="T">
/// The array type. Must be unmanaged. This is to prevent object references from remaining in the array
/// after returning it to the array pool, which keeps them from being garbage collected.
/// For a more generic version that clears the array after disposing, use <see cref="RentAndClear{T}"/>.
/// </typeparam>
public readonly struct Rent<T> : IDisposable where T : unmanaged
{
    public readonly T[] Array;
    public readonly int Length;

    Rent(T[] array, int length) => (Array, Length) = (array, length);

    public Rent(int length)
    {
        switch (length)
        {
            case < 0:
                Rent.ThrowArgumentNegative();
                goto case 0; // unreachable
            case 0:
                Array = System.Array.Empty<T>();
                Length = 0;
                break;
            default:
                Array = ArrayPool<T>.Shared.Rent(length);
                Length = length;
                break;
        }
    }

    public Rent(ReadOnlySpan<T> span) : this(span.Length)
    {
        span.CopyTo(AsSpan());
    }

    public void Dispose()
    {
        if (Length > 0)
        {
            ArrayPool<T>.Shared.Return(Array);
        }
    }

    public override string ToString()
    {
        return $"[{nameof(Rent<T>)} Type={typeof(T).Name} Length={Length.ToString()} " +
               $"RealSize={(Array != null ? Array.Length : 0).ToString()}]";
    }

    public ref T this[int index] => ref Array[index];

    public Span<T> AsSpan() => new(Array, 0, Length);
    public ReadOnlySpan<T> AsReadOnlySpan() => new(Array, 0, Length);
    public Span<T> Slice(int start, int count) => new(Array, start, count);
    public Memory<T> AsMemory() => new(Array, 0, Length);
    public RentEnumerator<T> GetEnumerator() => new(Array, Length);
    public Span<T> this[Range range] => AsSpan()[range];
    public Rent<T> Resize(int newLength) => new(Array, newLength);
    public static implicit operator Span<T>(Rent<T> rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<T>(Rent<T> rent) => rent.AsReadOnlySpan();
    public static implicit operator Memory<T>(Rent<T> rent) => rent.AsMemory();
    public static implicit operator ReadOnlyMemory<T>(Rent<T> rent) => new(rent.Array, 0, rent.Length);
}

public readonly struct Rent : IDisposable
{
    public readonly byte[] Array;
    public readonly int Length;

    Rent(byte[] array, int length) => (Array, Length) = (array, length);

    public Rent(int length)
    {
        switch (length)
        {
            case < 0:
                ThrowArgumentNegative();
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
    }

    public Rent(ReadOnlySpan<byte> span) : this(span.Length)
    {
        span.CopyTo(AsSpan());
    }

    public void Dispose()
    {
        if (Length > 0)
        {
            ArrayPool<byte>.Shared.Return(Array);
        }
    }

    public override string ToString()
    {
        return $"[{nameof(Rent)} Type=byte Length={Length.ToString()} " +
               $"RealSize={(Array != null ? Array.Length : 0).ToString()}]";
    }
    
    public byte this[int index] => Array[index];
    
    public Span<byte> AsSpan() => new(Array, 0, Length);
    public ReadOnlySpan<byte> AsReadOnlySpan() => new(Array, 0, Length);
    public Span<byte> Slice(int start, int count) => new(Array, start, count);
    public Memory<byte> AsMemory() => new(Array, 0, Length);
    public RentEnumerator<byte> GetEnumerator() => new(Array, Length);
    public Span<byte> this[Range range] => AsSpan()[range];
    public Rent Resize(int newLength) => new(Array, newLength);
    public static implicit operator Span<byte>(Rent rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<byte>(Rent rent) => rent.AsReadOnlySpan();
    public static implicit operator Memory<byte>(Rent rent) => rent.AsMemory();
    public static implicit operator ReadOnlyMemory<byte>(Rent rent) => new(rent.Array, 0, rent.Length);

    public static Rent<T> Empty<T>() where T : unmanaged => default;
    public static Rent Empty() => default;

    [DoesNotReturn]
    internal static void ThrowArgumentNegative() => throw new ArgumentException("Rent size cannot be negative");

    [DoesNotReturn]
    internal static void ThrowOutOfRange() => throw new IndexOutOfRangeException();
}