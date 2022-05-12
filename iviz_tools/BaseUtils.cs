using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace Iviz.Tools;

public static class BaseUtils
{
    public static Random Random => Defaults.Random;

    public const string GenericExceptionFormat = "{0}: {1}";

    public static bool HasPrefix(this string check, string prefix)
    {
        if (check is null)
        {
            throw new ArgumentNullException(nameof(check));
        }

        if (prefix is null)
        {
            throw new ArgumentNullException(nameof(prefix));
        }

        if (check.Length < prefix.Length)
        {
            return false;
        }

        for (int i = 0; i < prefix.Length; i++)
        {
            if (check[i] != prefix[i])
            {
                return false;
            }
        }

        return true;
    }

    public static bool HasSuffix(this string check, string suffix)
    {
        if (check is null)
        {
            throw new ArgumentNullException(nameof(check));
        }

        if (suffix is null)
        {
            throw new ArgumentNullException(nameof(suffix));
        }

        if (check.Length < suffix.Length)
        {
            return false;
        }

        int offset = check.Length - suffix.Length;
        for (int i = 0; i < suffix.Length; i++)
        {
            if (check[offset + i] != suffix[i])
            {
                return false;
            }
        }

        return true;
    }

    public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> t) => new(t);

    public static int Sum<T>(this ReadOnlySpan<T> ts, Func<T, int> selector)
    {
        int sum = 0;
        foreach (T t in ts)
        {
            sum += selector(t);
        }

        return sum;
    }

    public static int Sum<T>(this T[] ts, Func<T, int> selector)
    {
        int sum = 0;
        foreach (T t in ts)
        {
            sum += selector(t);
        }

        return sum;
    }

    /// <summary>
    /// Copies the content of the string into a byte <see cref="Rent{T}"/>.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Rent<byte> AsRent(this string s)
    {
        var bytes = new Rent<byte>(Defaults.UTF8.GetMaxByteCount(s.Length));
        int size = Defaults.UTF8.GetBytes(s, 0, s.Length, bytes.Array, 0);
        return bytes.Resize(size);
    }

    internal static Rent<byte> AsRent(this StringBuilder str, in ReadOnlyMemory<char> mainChunk)
    {
        var bytes = new Rent<byte>(Defaults.UTF8.GetMaxByteCount(str.Length));
        int size;

        if (mainChunk.Length >= str.Length)
        {
            size = Defaults.UTF8.GetBytes(mainChunk[..str.Length].Span, bytes);
        }
        else
        {
            // slow path
            using var chars = new Rent<char>(str.Length);
            var array = chars.AsSpan();
            for (int i = 0; i < str.Length; i++)
            {
                array[i] = str[i];
            }

            size = Defaults.UTF8.GetBytes(chars, bytes);
        }

        return bytes.Resize(size);
    }

#if NETSTANDARD2_1
    static FieldInfo? chunkField;

    internal static ReadOnlyMemory<char> GetMainChunk(this StringBuilder str)
    {
        if (chunkField == null)
        {
            chunkField =
                typeof(StringBuilder).GetField("m_ChunkChars", BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new InvalidOperationException("Failed to find StringBuilder chunk field!");
        }

        return (char[])chunkField.GetValue(str)!;
    }
#else
    internal static ReadOnlyMemory<char> GetMainChunk(this StringBuilder str)
    {
        var e = str.GetChunks();
        return !e.MoveNext() ? ReadOnlyMemory<char>.Empty : e.Current;
    }
#endif

    /// <summary>
    ///     A string hash that does not change every run unlike <see cref="string.GetHashCode()"/>
    /// </summary>
    /// <param name="str">String to calculate the hash from</param>
    /// <returns>A hash integer</returns>
    public static int GetDeterministicHashCode(this string str)
    {
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                {
                    break;
                }

                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + hash2 * 1566083941;
        }
    }

    public static string GetMd5Hash(byte[] input, MD5? md5Hash = null)
    {
        MD5? ownMd5Hash = null;
        MD5 validatedMd5Hash = md5Hash ?? (ownMd5Hash = MD5.Create());
        byte[] data = validatedMd5Hash.ComputeHash(input);
        var sBuilder = new StringBuilder(data.Length * 2);
        foreach (byte b in data)
        {
            sBuilder.Append(b.ToString("x2"));
        }

        ownMd5Hash?.Dispose();
        return sBuilder.ToString();
    }

    public static int ReadInt(this Span<byte> span) => span.Length >= sizeof(int)
        ? Unsafe.ReadUnaligned<int>(ref span[0])
        : ThrowIndexOutOfRange();

    public static int ReadInt(this byte[] array) => array.Length >= sizeof(int)
        ? Unsafe.ReadUnaligned<int>(ref array[0])
        : ThrowIndexOutOfRange();

    public static T Read<T>(this ReadOnlySpan<byte> span) where T : unmanaged => MemoryMarshal.Read<T>(span);

    public static void WriteInt(this Span<byte> span, int t)
    {
        if (span.Length < sizeof(int))
        {
            ThrowIndexOutOfRange();
        } 

        Unsafe.WriteUnaligned(ref span[0], t);
    }

    public static void Write<T>(this Span<byte> span, T t) where T : unmanaged => MemoryMarshal.Write(span, ref t);

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentNull(string arg) => throw new ArgumentNullException(arg);
    
    [DoesNotReturn]
    static int ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();
}

public sealed class ConcurrentSet<T> : IReadOnlyCollection<T> where T : notnull
{
    readonly ConcurrentDictionary<T, object?> backend = new();
    public IEnumerator<T> GetEnumerator() => backend.Keys.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public void Add(T s) => backend[s] = null;
    public bool Remove(T s) => backend.TryRemove(s, out _);
    public int Count => backend.Count;
    public void Clear() => backend.Clear();
    public T[] ToArray() => backend.Keys.ToArray();
}