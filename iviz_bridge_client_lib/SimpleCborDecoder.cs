using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using Iviz.Msgs;

namespace Iviz.Bridge.Client;

public struct SimpleCborDecoder
{
    const int MajorTypePositiveInteger = 0;
    const int MajorTypeNegativeInteger = 1;
    const int MajorTypeByteArray = 2;
    const int MajorTypeString = 3;
    const int MajorTypeMap = 5;

    const int ShortCountInt8 = 24;
    const int ShortCountInt16 = 25;
    const int ShortCountInt32 = 26;
    const int ShortCountInt64 = 27;

    readonly byte[] array;
    int o;

    public SimpleCborDecoder(byte[] array)
    {
        this.array = array;
    }

    public string? DecodeNextString()
    {
        return GetNextStringSegment() is not var (offset, count)
            ? null
            : BuiltIns.UTF8.GetString(array, offset, count);
    }

    public bool CompareNextString(string s)
    {
        if (GetNextStringSegment() is not var (offset, count)) return false;
        if (s.Length != count) return false;

        for (int i = 0; i < count; i++)
        {
            if (array[offset + i] != s[i]) return false;
        }

        return true;
    }

    (int offset, int count)? GetNextStringSegment()
    {
        int majorType = array[o] / 32;
        if (majorType != MajorTypeString || ReadCount() is not { } count) return null;
        int offset = o;
        o += count;
        return (offset, count);
    }

    public int? GetNextMapCount()
    {
        int majorType = array[o] / 32;
        return majorType != MajorTypeMap || ReadCount() is not { } count
            ? null
            : count;
    }

    public int? GetNextInt()
    {
        int majorType = array[o] / 32;
        if (majorType is not (MajorTypePositiveInteger or MajorTypeNegativeInteger) || ReadCount() is not { } count)
            return null;
        return majorType is MajorTypePositiveInteger 
            ? count 
            : -count;
    }

    public (int offset, int count)? GetNextByteArraySegment()
    {
        int majorType = array[o] / 32;
        if (majorType != MajorTypeByteArray || ReadCount() is not { } count) return null;
        int offset = o;
        o += count;
        return (offset, count);
    }

    int? ReadCount()
    {
        int count = array[o++] & 31;
        return count switch
        {
            < ShortCountInt8 => count,
            ShortCountInt8 => array[o++],
            ShortCountInt16 => BinaryPrimitives.ReverseEndianness(Read16()),
            ShortCountInt32 => BinaryPrimitives.ReverseEndianness(Read32()),
            ShortCountInt64 => (int)BinaryPrimitives.ReverseEndianness(Read64()),
            _ => null
        };
    }
    
    ushort Read16()
    {
        if (o + sizeof(ushort) >= array.Length) throw new IndexOutOfRangeException();
        ushort t = Unsafe.ReadUnaligned<ushort>(ref array[o]);
        o += sizeof(ushort);
        return t;
    }
    
    int Read32()
    {
        if (o + sizeof(int) >= array.Length) throw new IndexOutOfRangeException();
        int t = Unsafe.ReadUnaligned<int>(ref array[o]);
        o += sizeof(int);
        return t;
    }
    
    long Read64()
    {
        if (o + sizeof(long) >= array.Length) throw new IndexOutOfRangeException(); 
        long t = Unsafe.ReadUnaligned<long>(ref array[o]);
        o += sizeof(long);
        return t;
    }
}