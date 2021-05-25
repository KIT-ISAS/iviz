using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Rosbag.Writer
{
    public class RosbagFileWriter : IDisposable
    {
        const string RosbagMagic = "#ROSBAG V2.0\n";

        readonly Stream writer;
        readonly bool leaveOpen;
        readonly Dictionary<IRosConnection, int> connections = new();
        readonly Dictionary<int, List<(time time, long offset)>> chunkIndices = new();
        readonly List<ChunkInfoRecord> chunkInfos = new();

        long? chunkStart;
        long chunkDataStart;
        time chunkStartTime;
        time chunkEndTime;

        public long Length => writer.Length;
        
        public RosbagFileWriter(Stream stream, bool leaveOpen = false)
        {
            writer = stream ?? throw new ArgumentNullException(nameof(stream));
            this.leaveOpen = leaveOpen;

            if (!stream.CanWrite || !stream.CanSeek)
            {
                throw new ArgumentException("Stream needs to be writable and be able to seek", nameof(stream));
            }

            WriteMagic();
            WriteHeaderRecord(0, 0, 0);
        }

        public RosbagFileWriter(string path, bool enableAsync = false) : this(new FileStream(path, FileMode.Create,
            FileAccess.Write, FileShare.None, 4096, enableAsync))
        {
        }

        void WriteMagic()
        {
            writer.WriteValue(RosbagMagic);
        }

        void WriteHeaderRecord(int numConnections, int numChunks, long connectionIndexPosition)
        {
            var op = new OpCodeHeaderEntry(OpCode.BagHeader);
            var chunkCount = new IntHeaderEntry("chunk_count", numChunks);
            var connCount = new IntHeaderEntry("conn_count", numConnections);
            var indexPosEntry = new LongHeaderEntry("index_pos", connectionIndexPosition);

            //int headerLength = OpCodeHeaderEntry.Length + chunkCount.Length + connCount.Length + indexPosEntry.Length;
            const int headerLength = 69;

            //Console.WriteLine("** start " + writer.Position);
            //Console.WriteLine("** header_len " + headerLength);

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(chunkCount)
                .WriteValue(connCount)
                .WriteValue(indexPosEntry);

            const int dataLength = 4096 - headerLength;
            using var padding = new Rent<byte>(dataLength);
            writer.WriteValue(dataLength)
                .WriteValue(padding);
        }

        void WriteConnectionRecord(int connectionId, string topicName, string[] headerData)
        {
            var op = new OpCodeHeaderEntry(OpCode.Connection);
            var conn = new IntHeaderEntry("conn", connectionId);
            var topic = new StringHeaderEntry("topic", topicName);

            int headerLength = 12 + OpCodeHeaderEntry.Length + conn.Length + topic.Length;

            //Console.WriteLine("** Connection");
            //Console.WriteLine("** start " + writer.Position);
            //Console.WriteLine("** header_len " + headerLength);

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(conn)
                .WriteValue(topic);

            int dataLength = 4 * headerData.Length + headerData.Sum(entry => BuiltIns.UTF8.GetByteCount(entry));
            //Console.WriteLine("** dataLength " + dataLength + " at " + writer.Position);
            writer.WriteValue(dataLength);
            foreach (var entry in headerData)
            {
                int length = BuiltIns.UTF8.GetByteCount(entry);
                writer.WriteValue(length).WriteValueUtf8(entry);
            }
        }

        async Task WriteConnectionRecordAsync(int connectionId, string topicName, string[] headerData)
        {
            var op = new OpCodeHeaderEntry(OpCode.Connection);
            var conn = new IntHeaderEntry("conn", connectionId);
            var topic = new StringHeaderEntry("topic", topicName);

            int headerLength = 12 + OpCodeHeaderEntry.Length + conn.Length + topic.Length;

            //Console.WriteLine("** Connection");
            //Console.WriteLine("** start " + writer.Position);
            //Console.WriteLine("** header_len " + headerLength);

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(conn);
            await writer.WriteValueAsync(topic);

            int dataLength = 4 * headerData.Length + headerData.Sum(entry => BuiltIns.UTF8.GetByteCount(entry));
            //Console.WriteLine("** dataLength " + dataLength + " at " + writer.Position);
            await writer.WriteValueAsync(dataLength);
            foreach (var entry in headerData)
            {
                int length = BuiltIns.UTF8.GetByteCount(entry);
                await writer.WriteValueAsync(length);
                await writer.WriteValueUtf8Async(entry);
            }
        }

        void WriteMessageRecord(int connectionId, in time timestamp, IMessage message)
        {
            var op = new OpCodeHeaderEntry(OpCode.MessageData);
            var conn = new IntHeaderEntry("conn", connectionId);
            var time = new TimeHeaderEntry("time", timestamp);

            int headerLength = 12 + OpCodeHeaderEntry.Length + conn.Length + time.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(conn)
                .WriteValue(time);

            int dataLength = message.RosMessageLength;

            using Rent<byte> bytes = new Rent<byte>(dataLength);
            message.SerializeToArray(bytes.Array);

            writer.WriteValue(dataLength)
                .WriteValue(bytes);
        }

        async Task WriteMessageRecordAsync(int connectionId, time timestamp, IMessage message)
        {
            var op = new OpCodeHeaderEntry(OpCode.MessageData);
            var conn = new IntHeaderEntry("conn", connectionId);
            var time = new TimeHeaderEntry("time", timestamp);

            int headerLength = 12 + OpCodeHeaderEntry.Length + conn.Length + time.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(conn)
                .WriteValue(time);

            int dataLength = message.RosMessageLength;

            using Rent<byte> bytes = new Rent<byte>(dataLength);
            message.SerializeToArray(bytes.Array);

            await writer.WriteValueAsync(dataLength);
            await writer.WriteValueAsync(bytes);
        }

        void WritePartialChunkRecord(int sizeInBytes)
        {
            //Console.WriteLine("** Chunk start " + writer.Position);
            var op = new OpCodeHeaderEntry(OpCode.Chunk);
            var compression = new StringHeaderEntry("compression", "none");
            var size = new IntHeaderEntry("size", sizeInBytes);

            int headerLength = 12 + OpCodeHeaderEntry.Length + compression.Length + size.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(compression)
                .WriteValue(size);

            writer.WriteValue(sizeInBytes);
            //Console.WriteLine("** Chunk end " + writer.Position + " size " + sizeInBytes);
            // data is written 
        }

        async Task WritePartialChunkRecordAsync(int sizeInBytes)
        {
            //Console.WriteLine("** Chunk start " + writer.Position);
            var op = new OpCodeHeaderEntry(OpCode.Chunk);
            var compression = new StringHeaderEntry("compression", "none");
            var size = new IntHeaderEntry("size", sizeInBytes);

            int headerLength = 12 + OpCodeHeaderEntry.Length + compression.Length + size.Length;

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(compression);
            await writer.WriteValueAsync(size);

            await writer.WriteValueAsync(sizeInBytes);
            //Console.WriteLine("** Chunk end " + writer.Position + " size " + sizeInBytes);
            // data is written 
        }

        void WriteIndexRecord(int connectionId)
        {
            var list = chunkIndices[connectionId];
            if (list.Count == 0)
            {
                return;
            }

            Console.WriteLine("** Index start " + writer.Position);

            var op = new OpCodeHeaderEntry(OpCode.IndexData);
            var ver = new IntHeaderEntry("ver", 1);
            var conn = new IntHeaderEntry("conn", connectionId);
            var count = new IntHeaderEntry("count", list.Count);

            int headerLength = 16 + OpCodeHeaderEntry.Length + ver.Length + conn.Length + count.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(ver)
                .WriteValue(conn)
                .WriteValue(count);

            int dataLength = 12 * list.Count;
            writer.WriteValue(dataLength);
            foreach ((time time, long offset) in list)
            {
                writer.WriteValue(time.Secs)
                    .WriteValue(time.Nsecs)
                    .WriteValue((int) offset);
            }

            Console.WriteLine("** End position: " + writer.Position);
        }

        void WriteChunkInfoRecord(in ChunkInfoRecord record)
        {
            Console.WriteLine("** Chunk start " + writer.Position);

            var op = new OpCodeHeaderEntry(OpCode.ChunkInfo);
            var ver = new IntHeaderEntry("ver", 1);
            var chunkPos = new LongHeaderEntry("chunk_pos", record.ChunkPos);
            var startTime = new TimeHeaderEntry("start_time", record.StartTime);
            var endTime = new TimeHeaderEntry("end_time", record.EndTime);
            var count = new IntHeaderEntry("count", record.MessagesByConnection.Length);

            int headerLength = 24 + OpCodeHeaderEntry.Length
                                  + ver.Length + chunkPos.Length + startTime.Length
                                  + endTime.Length + count.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(ver)
                .WriteValue(chunkPos)
                .WriteValue(startTime)
                .WriteValue(endTime)
                .WriteValue(count);

            int dataLength = 8 * record.MessagesByConnection.Length;
            writer.WriteValue(dataLength);
            foreach ((int connId, int connCount) in record.MessagesByConnection)
            {
                writer.WriteValue(connId)
                    .WriteValue(connCount);
            }

            Console.WriteLine("** End position: " + writer.Position);
        }

        void TryOpenChunk(in time timestamp)
        {
            if (chunkStart != null)
            {
                return;
            }

            chunkStart = writer.Position;
            WritePartialChunkRecord(0);
            chunkDataStart = writer.Position;
            chunkStartTime = timestamp;
        }

        async Task TryOpenChunkAsync(time timestamp)
        {
            if (chunkStart != null)
            {
                return;
            }

            chunkStart = writer.Position;
            await WritePartialChunkRecordAsync(0);
            chunkDataStart = writer.Position;
            chunkStartTime = timestamp;
        }

        void TryCloseChunk()
        {
            if (chunkStart == null)
            {
                return;
            }

            long position = writer.Position;
            int size = (int) (position - chunkDataStart);

            writer.Seek(chunkStart.Value, SeekOrigin.Begin);
            WritePartialChunkRecord(size);
            writer.Seek(position, SeekOrigin.Begin);

            foreach (int connectionId in chunkIndices.Keys)
            {
                WriteIndexRecord(connectionId);
            }

            var messagesByConnection = chunkIndices.Select(pair => (pair.Key, pair.Value.Count)).ToArray();
            chunkInfos.Add(new ChunkInfoRecord(chunkStart.Value, chunkStartTime, chunkEndTime, messagesByConnection));

            chunkStart = null;
            chunkDataStart = 0;
            chunkStartTime = default;
            chunkEndTime = default;

            foreach (var list in chunkIndices.Values)
            {
                list.Clear();
            }
        }

        public void Write(IMessage message, IRosConnection connection, in time timestamp)
        {
            TryOpenChunk(timestamp);

            int connectionId;
            if (connections.TryGetValue(connection, out int tmpId))
            {
                connectionId = tmpId;
            }
            else
            {
                Console.WriteLine("**  Adding connection " + connections.Count);
                connectionId = connections[connection] = connections.Count;
                WriteConnectionRecord(connectionId, connection.Topic, connection.TcpHeader);
            }

            var messageList =
                chunkIndices.TryGetValue(connectionId, out var tmpList)
                    ? tmpList
                    : (chunkIndices[connectionId] = new List<(time time, long offset)>());

            messageList.Add((timestamp, writer.Position - chunkDataStart));

            WriteMessageRecord(connectionId, timestamp, message);
            chunkEndTime = timestamp;
        }

        public async Task WriteAsync(IMessage message, IRosConnection connection, time timestamp)
        {
            await TryOpenChunkAsync(timestamp);

            int connectionId;
            if (connections.TryGetValue(connection, out int tmpId))
            {
                connectionId = tmpId;
            }
            else
            {
                Console.WriteLine("**  Adding connection " + connections.Count);
                connectionId = connections[connection] = connections.Count;
                await WriteConnectionRecordAsync(connectionId, connection.Topic, connection.TcpHeader);
            }

            var messageList =
                chunkIndices.TryGetValue(connectionId, out var tmpList)
                    ? tmpList
                    : (chunkIndices[connectionId] = new List<(time time, long offset)>());

            messageList.Add((timestamp, writer.Position - chunkDataStart));

            await WriteMessageRecordAsync(connectionId, timestamp, message);
            chunkEndTime = timestamp;
        }

        public void Dispose()
        {
            TryCloseChunk();

            if (connections.Count != 0 && chunkInfos.Count != 0)
            {
                long connectionStart = writer.Position;

                Console.WriteLine("** Writing connections");
                foreach (var pair in connections)
                {
                    WriteConnectionRecord(pair.Value, pair.Key.Topic, pair.Key.TcpHeader);
                }

                Console.WriteLine("** Writing chunk infos");
                foreach (var info in chunkInfos)
                {
                    WriteChunkInfoRecord(info);
                }

                Console.WriteLine("** Updating header");
                writer.Seek(RosbagMagic.Length, SeekOrigin.Begin);
                WriteHeaderRecord(connections.Count, chunkInfos.Count, connectionStart);

                //Console.WriteLine("**  Closing rosbag with " + connections.Count + " connections and " +
                //                  chunkInfos.Count +
                //                  " chunks and " + connectionStart + " first index ");
            }

            if (!leaveOpen)
            {
                writer.Dispose();
            }
        }
    }
}