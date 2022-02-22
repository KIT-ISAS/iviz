using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Rosbag.Reader
{
    /// <summary>
    /// Record header entry. The enclosed type depends on the specification.
    /// </summary>
    public readonly struct RecordHeaderEntry
    {
        readonly Stream reader;
        readonly long valueStart;
        readonly long nextStart;
        readonly int nameSize;
        
        T ReadValue<T>() where T : unmanaged
        {
            Span<byte> span = stackalloc byte[Unsafe.SizeOf<T>()];
            if (span.Length > nextStart - valueStart)
            {
                throw new IndexOutOfRangeException();
            }

            reader.Seek(valueStart, SeekOrigin.Begin);
            reader.Read(span);
            return span.Read<T>();
        }

        public byte ValueAsByte
        {
            get
            {
                if (nextStart == valueStart)
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
                if (nextStart == valueStart)
                {
                    return "";
                }

                int msgSize = (int)(nextStart - valueStart);
                var rent = Rent.Empty<byte>();
                Span<byte> span = msgSize < 256
                    ? stackalloc byte[msgSize]
                    : (rent = new Rent<byte>(msgSize)).AsSpan();

                try
                {
                    reader.Seek(valueStart, SeekOrigin.Begin);
                    reader.Read(span);
                    return Encoding.ASCII.GetString(span);
                }
                finally
                {
                    rent.Dispose();
                }
            }
        }

        internal RecordHeaderEntry(Stream reader, long start)
        {
            this.reader = reader;
            nameSize = 0;
            valueStart = 0;
            nextStart = start;
        }

        RecordHeaderEntry(long start, Stream reader)
        {
            this.reader = reader;

            reader.Seek(start, SeekOrigin.Begin);

            Span<byte> intBytes = stackalloc byte[4];
            reader.Read(intBytes);
            int entrySize = intBytes.Read<int>();

            long nameStart = start + 4;
            nextStart = nameStart + entrySize;

            long equalsPosition = nameStart;
            while (equalsPosition < nextStart)
            {
                if (reader.ReadByte() == '=')
                {
                    valueStart = equalsPosition + 1;
                    nameSize = (int)(valueStart - nameStart - 1);
                    //Console.WriteLine("  ** Start " + start + " valueSize " + (nextStart - valueStart) +  " next " + nextStart);
                    return;
                }

                equalsPosition++;
            }

            // if no '=' found
            valueStart = nextStart;
            nameSize = (int)(valueStart - nameStart - 1);
        }

        internal bool TryMoveNext(long end, out RecordHeaderEntry next)
        {
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

            long nameStart = valueStart - nameSize + 1;
            reader.Seek(nameStart, SeekOrigin.Begin);
            
            foreach (char c in name)
            {
                if (c != reader.ReadByte())
                {
                    return false;
                }
            }

            return true;
        }

        internal bool ValueEquals(string value)
        {
            if (value.Length != nextStart - valueStart)
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
}