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
        readonly long headerStart;
        readonly int dataOffset;
        readonly int nextOffset;

        long DataStart => headerStart + dataOffset;
        long NextStart => headerStart + nextOffset;
        
        //readonly long dataStart;
        //readonly long nextStart;

        [DataMember] public OpCode OpCode { get; }

        /// <summary>
        /// Gets all the headers of the record.
        /// </summary>
        public HeaderEntryEnumerable RecordHeaders => new(new RecordHeaderEntry(reader, headerStart), DataStart - 4);

        /// <summary>
        /// If this is a Chunk record, generates an wrapper containing the chunk info.
        /// </summary>
        public Chunk Chunk => OpCode == OpCode.Chunk
            ? new Chunk(reader, DataStart, NextStart, IsCompressed)
            : throw new InvalidOperationException("Operation only allowed in Chunk types");

        public RecordEnumerable ChunkRecords => OpCode == OpCode.Chunk
            ? new RecordEnumerable(new Record(reader, DataStart), NextStart)
            : throw new InvalidOperationException("Operation only allowed in Chunk types");

        /// <summary>
        /// If this is a MessageData record, generates a wrapper containing the message.
        /// </summary>
        public MessageData MessageData => GetMessageData(null);

        /// <summary>
        /// If this is a MessageData record, generates a wrapper containing the message and sets the connection info.
        /// </summary>
        public MessageData GetMessageData(Connection? connection) => OpCode == OpCode.MessageData
            ? new MessageData(reader, DataStart, NextStart, Time, connection)
            : throw new InvalidOperationException("Operation only allowed in MessageData types");

        /// <summary>
        /// If this is a Connection record, generates a wrapper containing the connection info.
        /// </summary>
        public Connection Connection => OpCode == OpCode.Connection
            ? new Connection(reader, DataStart, NextStart, ConnectionId, Topic)
            : throw new InvalidOperationException("Operation only allowed in Connection types");

        bool IsCompressed => TryGetHeaderEntry("compression", out var entry) && entry.ValueEquals("bz2");

        internal int ConnectionId => TryGetHeaderEntry("conn", out var entry) ? entry.ValueAsInt : -1;

        time Time => TryGetHeaderEntry("time", out var entry) ? entry.ValueAsTime : default;

        string? Topic => TryGetHeaderEntry("topic", out var entry) ? entry.ValueAsString : null;

        internal Record(Stream reader, long start)
        {
            this.reader = reader;
            headerStart = start;
            dataOffset = 0;
            nextOffset = 0;
            OpCode = OpCode.Unknown;
        }

        Record(long start, Stream reader)
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

            //dataStart = headerStart + headerLength + 4;
            //nextStart = dataStart + dataLength;
            dataOffset = headerLength + 4;
            nextOffset = dataLength + dataOffset;

            //Console.WriteLine("** end " + end);

            OpCode = OpCode.Unknown;
            OpCode = TryGetHeaderEntry("op", out var entry)
                ? (OpCode)entry.ValueAsByte
                : OpCode.Unknown;
        }

        internal bool TryMoveNext(long end, out Record next)
        {
            long nextStart = NextStart;
            if (nextStart < end)
            {
                next = new Record(nextStart, reader);
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
        readonly long end;

        internal RecordEnumerator(in Record start, long end)
        {
            current = start;
            this.end = end;
        } 

        public bool MoveNext() => current.TryMoveNext(end, out current);

        public Record Current => current;
    }

    public readonly struct RecordEnumerable
    {
        readonly RecordEnumerator enumerator;

        internal RecordEnumerable(in Record start, long end) => enumerator = new RecordEnumerator(start, end);

        public RecordEnumerator GetEnumerator() => enumerator;
    }
}