using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Rosbag.Reader;

/// <summary>
/// Record header entry. The enclosed type depends on the specification.
/// </summary>
public readonly struct RecordHeaderEntry
{
    readonly Stream reader;
    readonly long valueStart;
    readonly int valueSize;
    readonly int nameSize;

    T ReadValue<T>() where T : unmanaged
    {
        Span<byte> span = stackalloc byte[Unsafe.SizeOf<T>()];
        if (span.Length > valueSize)
        {
            throw new IndexOutOfRangeException();
        }

        reader.Seek(valueStart, SeekOrigin.Begin);
        reader.ReadAll(span);
        return Unsafe.ReadUnaligned<T>(ref span[0]);
    }

    public byte ValueAsByte
    {
        get
        {
            if (valueSize == 0)
            {
                throw new IndexOutOfRangeException();
            }

            reader.Seek(valueStart, SeekOrigin.Begin);
            return (byte)reader.ReadByte();
        }
    }

    public int ValueAsInt => ReadValue<int>();

    public long ValueAsLong => ReadValue<long>();

    public time ValueAsTime => ReadValue<time>();

    public string ValueAsString
    {
        get
        {
            if (valueSize == 0)
            {
                return "";
            }

            reader.Seek(valueStart, SeekOrigin.Begin);

            int msgSize = valueSize;
            var rent = Rent.Empty();
            Span<byte> span = msgSize < 256
                ? stackalloc byte[msgSize]
                : (rent = new Rent(msgSize)).AsSpan();

            using (rent)
            {
                reader.ReadAll(span);
                return Encoding.ASCII.GetString(span);
            }
        }
    }

    internal RecordHeaderEntry(Stream reader, long start)
    {
        this.reader = reader;
        nameSize = 0;
        valueStart = start;
        valueSize = 0;
    }

    RecordHeaderEntry(long start, Stream reader)
    {
        this.reader = reader;

        reader.Seek(start, SeekOrigin.Begin);

        Span<byte> intBytes = stackalloc byte[4];
        reader.ReadAll(intBytes);
        int entrySize = intBytes.ReadInt();

        long nameStart = start + 4;
        long nextStart = nameStart + entrySize;

        long equalsPosition = nameStart;
        while (equalsPosition < nextStart)
        {
            int b = reader.ReadByte();
            if (b == '=')
            {
                valueStart = equalsPosition + 1;
                valueSize = (int)(nextStart - valueStart);
                nameSize = (int)(equalsPosition - nameStart);
                return;
            }

            equalsPosition++;
        }

        // if no '=' found
        valueStart = nextStart;
        valueSize = 0;
        nameSize = (int)(valueStart - nameStart - 1);
    }

    internal bool TryMoveNext(long end, out RecordHeaderEntry next)
    {
        long nextStart = valueStart + valueSize;
        if (nextStart < end)
        {
            next = new RecordHeaderEntry(nextStart, reader);
            return true;
        }

        next = default;
        return false;
    }

    internal bool NameEquals(string name)
    {
        if (name.Length != nameSize)
        {
            return false;
        }

        long nameStart = valueStart - nameSize - 1;
        reader.Seek(nameStart, SeekOrigin.Begin);

        foreach (char c in name)
        {
            int oc = reader.ReadByte();
            if (c != oc)
            {
                return false;
            }
        }

        return true;
    }

    internal bool ValueEquals(string value)
    {
        if (value.Length != valueSize)
        {
            return false;
        }

        reader.Seek(valueStart, SeekOrigin.Begin);
        foreach (char c in value)
        {
            if (c != reader.ReadByte())
            {
                return false;
            }
        }

        return true;
    }
}

public struct HeaderEntryEnumerator
{
    RecordHeaderEntry current;
    readonly long dataEnd;

    internal HeaderEntryEnumerator(in RecordHeaderEntry start, long dataEnd)
    {
        current = start;
        this.dataEnd = dataEnd;
    }

    public bool MoveNext() => current.TryMoveNext(dataEnd, out current);

    public RecordHeaderEntry Current => current;
}

public readonly struct HeaderEntryEnumerable
{
    readonly HeaderEntryEnumerator enumerator;

    internal HeaderEntryEnumerable(in RecordHeaderEntry start, long dataEnd) =>
        enumerator = new HeaderEntryEnumerator(start, dataEnd);

    public HeaderEntryEnumerator GetEnumerator() => enumerator;
}