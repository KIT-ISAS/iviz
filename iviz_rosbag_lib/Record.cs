using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Iviz.Msgs;

namespace Iviz.Rosbag
{
    public readonly struct Record
    {
        readonly Stream reader;
        readonly long headerStart;
        readonly long dataStart;
        readonly long nextStart;
        readonly long end;

        public OpCode OpCode { get; }

        /// <summary>
        /// Gets all the headers of the record.
        /// </summary>
        public HeaderEntryEnumerable Headers => new(new HeaderEntry(reader, headerStart, dataStart));

        /// <summary>
        /// If this is a Chunk record, generates an object containing the corresponding fields.
        /// </summary>
        public Chunk Chunk => OpCode == OpCode.Chunk
            ? new Chunk(reader, dataStart, nextStart, IsCompressed)
            : throw new InvalidOperationException("Operation only allowed in Chunk types");

        /// <summary>
        /// If this is a MessageData record, generates an object containing the corresponding fields.
        /// </summary>
        public MessageData MessageData => OpCode == OpCode.MessageData
            ? new MessageData(reader, dataStart, nextStart, ConnectionId, Time)
            : throw new InvalidOperationException("Operation only allowed in MessageData types");

        /// <summary>
        /// If this is a Connection record, generates an object containing the corresponding fields.
        /// </summary>
        public Connection Connection => OpCode == OpCode.Connection
            ? new Connection(reader, dataStart, nextStart, ConnectionId, Topic)
            : throw new InvalidOperationException("Operation only allowed in Connection types");

        bool IsCompressed => TryGetHeaderEntry("compression", out var entry) && entry.ValueEquals("bz2");
        
        internal int ConnectionId => TryGetHeaderEntry("conn", out var entry) ? entry.ValueAsInt : -1;
        
        time Time => TryGetHeaderEntry("time", out var entry) ? entry.ValueAsTime : default;

        string? Topic => TryGetHeaderEntry("topic", out var entry) ? entry.ValueAsString : null;
        
        internal Record(Stream reader) : this(reader, RosbagFile.RosbagMagic.Length, reader.Length)
        {
        }

        internal Record(Stream reader, long start, long end)
        {
            this.reader = reader;
            headerStart = 0;
            dataStart = 0;
            nextStart = start;
            this.end = end;
            OpCode = OpCode.Unknown;
        }

        Record(long start, long end, Stream reader)
        {
            this.reader = reader;

            using var intBytes = new Rent<byte>(4);
            reader.Seek(start, SeekOrigin.Begin);
            reader.Read(intBytes.Array, 0, 4);

            headerStart = start + 4;
            int headerLength = intBytes.ToInt();

            reader.Seek(headerStart + headerLength, SeekOrigin.Begin);
            reader.Read(intBytes.Array, 0, 4);
            int dataLength = intBytes.ToInt();

            dataStart = headerStart + headerLength + 4;
            nextStart = dataStart + dataLength;

            this.end = end;

            OpCode = OpCode.Unknown;
            OpCode = TryGetHeaderEntry("op", out var entry)
                ? (OpCode) entry.ValueAsByte
                : OpCode.Unknown;
        }

        internal bool TryMoveNext(out Record next)
        {
            if (nextStart < end)
            {
                next = new Record(nextStart, end, reader);
                return true;
            }

            next = default;
            return false;
        }

        bool TryGetHeaderEntry(string name, out HeaderEntry result)
        {
            foreach (var entry in Headers)
            {
                if (entry.NameEquals(name))
                {
                    result = entry;
                    return true;
                }
            }

            result = default;
            return false;
        }
    }

    public struct RecordEnumerator : IEnumerator<Record>
    {
        Record current;

        internal RecordEnumerator(Record start) => current = start;

        public bool MoveNext() => current.TryMoveNext(out current);

        void IEnumerator.Reset() => throw new NotSupportedException();

        public Record Current => current;

        Record IEnumerator<Record>.Current => current;

        object IEnumerator.Current => current;

        void IDisposable.Dispose()
        {
        }
    }

    public readonly struct RecordEnumerable : IEnumerable<Record>
    {
        readonly Record start;

        internal RecordEnumerable(Record start) => this.start = start;

        public RecordEnumerator GetEnumerator() => new(start);

        IEnumerator<Record> IEnumerable<Record>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}