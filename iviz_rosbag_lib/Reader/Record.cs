using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Iviz.Msgs;
using Iviz.Tools;
using Newtonsoft.Json;

namespace Iviz.Rosbag.Reader
{
    [DataContract]
    public readonly struct Record
    {
        readonly Stream reader;

        [DataMember] public OpCode OpCode { get; }

        readonly long headerStart;
        readonly long dataStart;
        readonly long nextStart;
        readonly long end;


        /// <summary>
        /// Gets all the headers of the record.
        /// </summary>
        public HeaderEntryEnumerable RecordHeaders => new(new RecordHeaderEntry(reader, headerStart, dataStart - 4));

        /// <summary>
        /// If this is a Chunk record, generates an object containing the corresponding fields.
        /// </summary>
        public Chunk Chunk => OpCode == OpCode.Chunk
            ? new Chunk(reader, dataStart, nextStart, IsCompressed)
            : throw new InvalidOperationException("Operation only allowed in Chunk types");

        /// <summary>
        /// If this is a MessageData record, generates an object containing the corresponding fields.
        /// </summary>
        public MessageData GetMessageData(Connection connection) => OpCode == OpCode.MessageData
            ? new MessageData(reader, dataStart, nextStart, Time, connection)
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

        internal Record(Stream reader) : this(reader, RosbagFileReader.RosbagMagicLength, reader.Length)
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

            //Console.WriteLine("** start " + start);

            //using var intBytes = new Rent<byte>(4);
            Span<byte> intBytes = stackalloc byte[4];
            reader.Seek(start, SeekOrigin.Begin);
            reader.Read(intBytes);

            //Console.WriteLine("** Start " + start);

            headerStart = start + 4;
            int headerLength = intBytes.Read<int>();

            //Console.WriteLine("** header_len " + headerLength);

            reader.Seek(headerStart + headerLength, SeekOrigin.Begin);
            //Console.WriteLine("** data_len pos " + reader.Position);

            reader.Read(intBytes);
            int dataLength = intBytes.Read<int>();

            //Console.WriteLine("** data_len " + dataLength);

            dataStart = headerStart + headerLength + 4;
            nextStart = dataStart + dataLength;

            this.end = end;
            //Console.WriteLine("** end " + end);

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

        bool TryGetHeaderEntry(string name, out RecordHeaderEntry result)
        {
            foreach (var entry in RecordHeaders)
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

        public override string ToString() => BuiltIns.ToJsonString(this);
    }

    public struct RecordEnumerator
    {
        Record current;

        internal RecordEnumerator(Record start) => current = start;

        public bool MoveNext() => current.TryMoveNext(out current);

        public Record Current => current;
    }

    public readonly struct RecordEnumerable
    {
        readonly Record start;

        internal RecordEnumerable(Record start) => this.start = start;

        public RecordEnumerator GetEnumerator() => new(start);
    }
}