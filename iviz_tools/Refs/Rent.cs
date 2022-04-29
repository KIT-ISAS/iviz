using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;

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
public readonly struct Rent<T> : IDisposable  where T : unmanaged
{
    public readonly int Length;
    public readonly T[] Array;

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
    
    public ref T this[int index]
    {
        get
        {
            if ((uint) index >= Length)
            {
                throw new IndexOutOfRangeException();
            }
                
            return ref Array[index];                
        }
    }

    public Span<T> AsSpan() => new(Array, 0, Length);
    public ReadOnlySpan<T> AsReadOnlySpan() => AsSpan();
    public Span<T> Slice(int start, int count) => AsSpan().Slice(start, count);
    public Memory<T> AsMemory() => new(Array, 0, Length);
    public RentEnumerator<T> GetEnumerator() => new(Array, Length);
    public Span<T> this[Range range] => AsSpan()[range];
    public Rent<T> Resize(int newLength) => new(Array, newLength);
    public static implicit operator Span<T>(Rent<T> rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<T>(Rent<T> rent) => rent.AsSpan();
    public static implicit operator Memory<T>(Rent<T> rent) => rent.AsMemory();
    public static implicit operator ReadOnlyMemory<T>(Rent<T> rent) => rent.AsMemory();
}

public static class Rent
{
    public static Rent<T> Empty<T>() where T : unmanaged => new(0);
    
    internal static void ThrowArgumentNegative() => throw new ArgumentException("Rent size cannot be negative");
}
