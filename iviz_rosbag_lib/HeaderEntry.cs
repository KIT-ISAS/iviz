using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Iviz.Msgs;

namespace Iviz.Rosbag
{
    public readonly struct HeaderEntry
    {
        readonly Stream reader;
        readonly long nameStart;
        readonly long valueStart;
        readonly long nextStart;
        readonly long end;

        public string FieldName
        {
            get
            {
                using var name = new Rent<byte>((int) (valueStart - nameStart - 1));
                reader.Seek(nameStart, SeekOrigin.Begin);
                reader.Read(name.Array, 0, name.Length);
                return Encoding.ASCII.GetString(name.Array, 0, name.Length);
            }
        }

        Rent<byte> Value
        {
            get
            {
                var value = new Rent<byte>((int) (nextStart - valueStart));
                reader.Seek(valueStart, SeekOrigin.Begin);
                reader.Read(value.Array, 0, value.Length);
                return value;
            }
        }

        public byte ValueAsByte
        {
            get
            {
                reader.Seek(valueStart, SeekOrigin.Begin);
                return (byte) reader.ReadByte();
            }
        }

        public int ValueAsInt
        {
            get
            {
                using var value = Value;
                return value.ToInt();
            }
        }

        public long ValueAsLong
        {
            get
            {
                using var value = Value;
                return value[0] + (value[1] << 8) + (value[2] << 16) + (value[3] << 24) +
                       ((long) (value[4] + (value[5] << 8) + (value[6] << 16) + (value[7] << 24)) << 32);
            }
        }
        
        public time ValueAsTime
        {
            get
            {
                using var value = Value;
                return new time(
                    (uint)(value[0] + (value[1] << 8) + (value[2] << 16) + (value[3] << 24)),
                    (uint)(value[4] + (value[5] << 8) + (value[6] << 16) + (value[7] << 24))
                    );
            }
        }        

        public string ValueAsString
        {
            get
            {
                using var value = Value;
                return Encoding.ASCII.GetString(value.Array, 0, value.Length);
            }
        }

        internal HeaderEntry(Stream reader, long start, long end)
        {
            this.reader = reader;
            nameStart = 0;
            valueStart = 0;
            nextStart = start;
            this.end = end;
        }

        HeaderEntry(long start, long end, Stream reader)
        {
            this.reader = reader;
            this.end = end;

            reader.Seek(start, SeekOrigin.Begin);

            int entrySize;
            using (var intBytes = new Rent<byte>(4))
            {
                reader.Read(intBytes.Array, 0, 4);
                entrySize = intBytes.ToInt();
            }

            nameStart = start + 4;
            nextStart = nameStart + entrySize;

            long equalsPosition = nameStart;
            while (equalsPosition < nextStart)
            {
                if (reader.ReadByte() == '=')
                {
                    valueStart = equalsPosition + 1;
                    return;
                }

                equalsPosition++;
            }

            valueStart = nextStart;
        }

        internal bool TryMoveNext(out HeaderEntry next)
        {
            if (nextStart < end)
            {
                next = new HeaderEntry(nextStart, end, reader);
                return true;
            }

            next = default;
            return false;
        }

        public bool NameEquals(string name)
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
        
        public bool ValueEquals(string value)
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

    public struct HeaderEntryEnumerator : IEnumerator<HeaderEntry>
    {
        HeaderEntry current;

        internal HeaderEntryEnumerator(HeaderEntry start) => current = start;

        public bool MoveNext() => current.TryMoveNext(out current);

        void IEnumerator.Reset() => throw new NotSupportedException();

        public HeaderEntry Current => current;

        HeaderEntry IEnumerator<HeaderEntry>.Current => current;

        object IEnumerator.Current => current;

        void IDisposable.Dispose()
        {
        }
    }

    public readonly struct HeaderEntryEnumerable : IEnumerable<HeaderEntry>
    {
        readonly HeaderEntry start;

        internal HeaderEntryEnumerable(HeaderEntry start) => this.start = start;

        public HeaderEntryEnumerator GetEnumerator() => new(start);

        IEnumerator<HeaderEntry> IEnumerable<HeaderEntry>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}