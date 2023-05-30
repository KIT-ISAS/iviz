using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace Iviz.Tools;

public static class BaseUtils
{
    public const string GenericExceptionFormat = "{0}: {1}";

    public static bool HasPrefix(this string check, string prefix)
    {
        if (check is null) ThrowArgumentNull(nameof(check));
        if (prefix is null) ThrowArgumentNull(nameof(prefix));

        int prefixLength = prefix.Length;
        if (check.Length < prefixLength)
        {
            return false;
        }

        for (int i = 0; i < prefixLength; i++)
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
        if (check is null) ThrowArgumentNull(nameof(check));
        if (suffix is null) ThrowArgumentNull(nameof(suffix));

        int suffixLength = suffix.Length;
        int checkLength = check.Length;

        if (checkLength < suffixLength)
        {
            return false;
        }

        int offset = checkLength - suffixLength;
        for (int i = 0; i < suffixLength; i++)
        {
            if (check[offset + i] != suffix[i])
            {
                return false;
            }
        }

        return true;
    }

    public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> t) => new(t);

    /// <summary>
    /// Copies the content of the string into a byte <see cref="Rent"/>.
    /// </summary>
    public static Rent AsRent(this string s)
    {
        var bytes = new Rent(Defaults.UTF8.GetMaxByteCount(s.Length));
        int size = Defaults.UTF8.GetBytes(s, 0, s.Length, bytes.Array, 0);
        return bytes.Resize(size);
    }

    /// <summary>
    ///     A string hash that does not change every run unlike <see cref="string.GetHashCode()"/>
    /// </summary>
    /// <param name="str">String to calculate the hash from</param>
    /// <returns>A hash integer</returns>
    public static int GetDeterministicHashCode(this string str)
    {
        // stolen from https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            int strLength = str.Length;
            for (int i = 0; i < strLength; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == strLength - 1)
                {
                    break;
                }

                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + hash2 * 1566083941;
        }
    }

    public static string GetMd5Hash(byte[] input)
    {
        using var md5Hash = MD5.Create();
        byte[] data = md5Hash.ComputeHash(input);
        var sBuilder = new StringBuilder(data.Length * 2);
        foreach (byte b in data)
        {
            sBuilder.Append(b.ToString("x2"));
        }

        return sBuilder.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ReadInt(this Span<byte> span) => span.Length >= sizeof(int)
        ? Unsafe.ReadUnaligned<int>(ref span[0])
        : ThrowIndexOutOfRange();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ReadInt(this byte[] array) => array.Length >= sizeof(int)
        ? Unsafe.ReadUnaligned<int>(ref array[0])
        : ThrowIndexOutOfRange();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteInt(this byte[] span, int t)
    {
        if (span.Length < sizeof(int)) ThrowIndexOutOfRange();
        Unsafe.WriteUnaligned(ref span[0], t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteInt(this Span<byte> span, int t)
    {
        if (span.Length < sizeof(int)) ThrowIndexOutOfRange();
        Unsafe.WriteUnaligned(ref span[0], t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => 
        str is null || str.Length is 0;

    [DoesNotReturn, AssertionMethod]
    public static void ThrowArgumentNull(string arg) => throw new ArgumentNullException(arg);

    [DoesNotReturn]
    static int ThrowIndexOutOfRange() => throw new IndexOutOfRangeException();
}