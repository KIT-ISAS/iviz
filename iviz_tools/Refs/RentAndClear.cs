using System;
using System.Buffers;

namespace Iviz.Tools;

/// <summary>
/// Wrapper around renting and returning an array from an <see cref="ArrayPool{T}"/>.
/// Creating this value will rent an array of the given size, and disposing it will return it.
/// The functionality is the same as <see cref="Rent{T}"/>, but this variant clears the array before
/// releasing it (sets all values from 0 to Length to null/default).
/// </summary>
/// <typeparam name="T">
/// The array type. Can be any type.
/// </typeparam>
public readonly struct RentAndClear<T> : IDisposable
{
    static ArrayPool<T?> Pool => ArrayPool<T?>.Shared;

    public readonly T?[] Array;
    public readonly int Length;
        
    public Span<T?> AsSpan() => new(Array, 0, Length);
    public ReadOnlySpan<T?> AsReadOnlySpan() => new(Array, 0, Length);
    
    public RentAndClear(int count)
    {
        switch (count)
        {
            case < 0:
                Rent.ThrowArgumentNegative();
                this = default; // unreachable
                break;
            case 0:
                Array = System.Array.Empty<T>();
                Length = 0;
                break;
            default:
                Array = Pool.Rent(count);
                Length = count;
                break;
        }
    }

    public void Dispose()
    {
        if (Length <= 0)
        {
            return;
        }

        for (int i = 0; i < Length; i++)
        {
            Array[i] = default;
        }

        Pool.Return(Array);
    }

    public override string ToString()
    {
        return $"[{nameof(RentAndClear<T>)} Type={typeof(T).Name} Length={Length.ToString()}]";
    }

    public RentEnumerator<T?> GetEnumerator() => new(Array, Length);

    public ref T? this[int index]
    {
        get
        {
            if ((uint) index >= Length)
            {
                Rent.ThrowOutOfRange();
            }

            return ref Array[index];
        }
    }
    
    public static implicit operator Span<T?>(RentAndClear<T?> rent) => rent.AsSpan();
    public static implicit operator ReadOnlySpan<T?>(RentAndClear<T?> rent) => rent.AsReadOnlySpan();    
}