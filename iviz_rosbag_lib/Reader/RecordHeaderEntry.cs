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
        readonly long nameStart;
        readonly long valueStart;
        readonly long nextStart;
        readonly long end;

        T ReadValue<T>() where T : unmanaged
        {
            Span<byte> span = stackalloc byte[Unsafe.SizeOf<T>()];
            if (span.Length > nextStart - valueStart)
            {
                throw new IndexOutOfRangeException();
            }

            //var value = new Rent<byte>((int)(nextStart - valueStart));
            reader.Seek(valueStart, SeekOrigin.Begin);
            //reader.Read(value.Array, 0, value.Length);
            reader.Read(span);
            //return value;
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
                    throw new IndexOutOfRangeException();
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

        internal RecordHeaderEntry(Stream reader, long start, long end)
        {
            this.reader = reader;
            nameStart = 0;
            valueStart = 0;
            nextStart = start;
            this.end = end;
        }

        RecordHeaderEntry(long start, long end, Stream reader)
        {
            this.reader = reader;
            this.end = end;

            reader.Seek(start, SeekOrigin.Begin);

            Span<byte> intBytes = stackalloc byte[4];
            reader.Read(intBytes);
            int entrySize = intBytes.Read<int>();

            /*
            using (var intBytes = new Rent<byte>(4))
            {
                reader.Read(intBytes.Array, 0, 4);
                entrySize = intBytes.Read<int>();
            }
            */

            nameStart = start + 4;
            nextStart = nameStart + entrySize;

            long equalsPosition = nameStart;
            while (equalsPosition < nextStart)
            {
                if (reader.ReadByte() == '=')
                {
                    valueStart = equalsPosition + 1;
                    //Console.WriteLine("  ** Start " + start + " valueSize " + (nextStart - valueStart) +  " next " + nextStart);
                    return;
                }

                equalsPosition++;
            }

            // if no '=' found
            valueStart = nextStart;
        }

        internal bool TryMoveNext(out RecordHeaderEntry next)
        {
            if (nextStart < end)
            {
                next = new RecordHeaderEntry(nextStart, end, reader);
                return true;
            }

            next = default;
            return false;
        }

        internal bool NameEquals(string name)
        {
            if (name.Length != valueStart - nameStart - 1)
            {
                return false;
            }

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

        internal HeaderEntryEnumerator(RecordHeaderEntry start) => current = start;

        public bool MoveNext() => current.TryMoveNext(out current);

        public RecordHeaderEntry Current => current;
    }

    public readonly struct HeaderEntryEnumerable
    {
        readonly RecordHeaderEntry start;

        internal HeaderEntryEnumerable(RecordHeaderEntry start) => this.start = start;

        public HeaderEntryEnumerator GetEnumerator() => new(start);
    }
}