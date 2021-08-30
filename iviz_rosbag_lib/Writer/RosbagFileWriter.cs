using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Rosbag.Writer
{
    /// <summary>
    /// Writer for Rosbag files.
    /// Usage: Construct, call <see cref="Write"/> to add messages, and close it with <see cref="Dispose"/>.
    /// Don't forget to dispose, the file cannot be read without it.
    /// </summary>
    public sealed class RosbagFileWriter : IDisposable
#if !NETSTANDARD2_0
        , IAsyncDisposable
#endif
    {
        const int MaxChunkSize = int.MaxValue / 2; // 1 GB
        
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
        bool disposed;

        /// <summary>
        /// The current size of the rosbag file.
        /// </summary>
        public long Length => writer.Length;

        /// <summary>
        /// The size of the current chunk.
        /// </summary>
        long CurrentChunkLength => chunkStart == null ? 0 : writer.Length - chunkStart.Value;

        /// <summary>
        /// Creates a rosbag file that writes on the given stream.
        /// </summary>
        /// <param name="stream">A writable and seekable stream</param>
        /// <param name="leaveOpen">Whether the stream should be left open after disposing this file</param>
        /// <exception cref="ArgumentNullException">Thrown if stream is null</exception>
        /// <exception cref="ArgumentException">Thrown if the stream is not writable</exception>
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

        /// <summary>
        /// Creates a rosbag file on the given path.
        /// Use <see cref="CreateAsync"/> instead if you want to use async operations.
        /// </summary>
        /// <param name="path">The path where the file should be written</param>
        public RosbagFileWriter(string path) : this(new FileStream(path, FileMode.Create), false)
        {
        }

        RosbagFileWriter(Stream writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Creates a rosbag file using a stream that allows async operations.
        /// </summary>
        /// <param name="path">The path where the file should be written</param>
        /// <returns>An awaitable task</returns>
        public static async ValueTask<RosbagFileWriter> CreateAsync(string path)
        {
            var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            var writer = new RosbagFileWriter(stream);

            await writer.WriteMagicAsync();
            await writer.WriteHeaderRecordAsync(0, 0, 0);
            return writer;
        }

        void WriteMagic() => writer.WriteValue(RosbagMagic);

        Task WriteMagicAsync() => writer.WriteValueAsync(RosbagMagic);

        void WriteHeaderRecord(int numConnections, int numChunks, long connectionIndexPosition)
        {
            var op = new OpCodeHeaderEntry(OpCode.BagHeader);
            var chunkCount = new IntHeaderEntry("chunk_count", numChunks);
            var connCount = new IntHeaderEntry("conn_count", numConnections);
            var indexPosEntry = new LongHeaderEntry("index_pos", connectionIndexPosition);

            const int headerLength = 69;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(chunkCount)
                .WriteValue(connCount)
                .WriteValue(indexPosEntry);

            const int dataLength = 4096 - headerLength;
            using var padding = new Rent<byte>(dataLength);
#if NETSTANDARD2_0
            for (int i = 0; i < dataLength; i++)
            {
                padding.Array[i] = 0x20;
            }
#else
            new Span<byte>(padding.Array, 0, dataLength).Fill(0x20);
#endif

            writer.WriteValue(dataLength)
                .WriteValue(padding);
        }

        async Task WriteHeaderRecordAsync(int numConnections, int numChunks, long connectionIndexPosition)
        {
            var op = new OpCodeHeaderEntry(OpCode.BagHeader);
            var chunkCount = new IntHeaderEntry("chunk_count", numChunks);
            var connCount = new IntHeaderEntry("conn_count", numConnections);
            var indexPosEntry = new LongHeaderEntry("index_pos", connectionIndexPosition);

            const int headerLength = 69;

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(chunkCount);
            await writer.WriteValueAsync(connCount);
            await writer.WriteValueAsync(indexPosEntry);

            const int dataLength = 4096 - headerLength;
            using var padding = new Rent<byte>(dataLength);
#if NETSTANDARD2_0
            for (int i = 0; i < dataLength; i++)
            {
                padding.Array[i] = 0x20;
            }
#else
            new Span<byte>(padding.Array, 0, dataLength).Fill(0x20);
#endif

            await writer.WriteValueAsync(dataLength);
            await writer.WriteValueAsync(padding);
        }

        void WriteConnectionRecord(int connectionId, string topicName, IReadOnlyCollection<string> headerData)
        {
            var op = new OpCodeHeaderEntry(OpCode.Connection);
            var conn = new IntHeaderEntry("conn", connectionId);
            var topic = new StringHeaderEntry("topic", topicName);

            int headerLength = 12 + OpCodeHeaderEntry.Length + conn.Length + topic.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(conn)
                .WriteValue(topic);

            int dataLength = 4 * headerData.Count + headerData.Sum(entry => BuiltIns.UTF8.GetByteCount(entry));

            writer.WriteValue(dataLength);
            foreach (var entry in headerData)
            {
                int length = BuiltIns.UTF8.GetByteCount(entry);
                writer.WriteValue(length).WriteValueUtf8(entry);
            }
        }

        async Task WriteConnectionRecordAsync(int connectionId, string topicName,
            IReadOnlyCollection<string> headerData)
        {
            var op = new OpCodeHeaderEntry(OpCode.Connection);
            var conn = new IntHeaderEntry("conn", connectionId);
            var topic = new StringHeaderEntry("topic", topicName);

            int headerLength = 12 + OpCodeHeaderEntry.Length + conn.Length + topic.Length;

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(conn);
            await writer.WriteValueAsync(topic);

            int dataLength = 4 * headerData.Count + headerData.Sum(entry => BuiltIns.UTF8.GetByteCount(entry));
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

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(conn);
            await writer.WriteValueAsync(time);

            int dataLength = message.RosMessageLength;

            using Rent<byte> bytes = new Rent<byte>(dataLength);
            message.SerializeToArray(bytes.Array);

            await writer.WriteValueAsync(dataLength);
            await writer.WriteValueAsync(bytes);
        }

        void WritePartialChunkRecord(int sizeInBytes)
        {
            var op = new OpCodeHeaderEntry(OpCode.Chunk);
            var compression = new StringHeaderEntry("compression", "none");
            var size = new IntHeaderEntry("size", sizeInBytes);

            int headerLength = 12 + OpCodeHeaderEntry.Length + compression.Length + size.Length;

            writer.WriteValue(headerLength)
                .WriteValue(op)
                .WriteValue(compression)
                .WriteValue(size);

            writer.WriteValue(sizeInBytes);
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
        }

        void WriteIndexRecord(int connectionId)
        {
            var list = chunkIndices[connectionId];
            if (list.Count == 0)
            {
                return;
            }

            //Console.WriteLine("** Index start " + writer.Position);

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

            //Console.WriteLine("** End position: " + writer.Position);
        }

        async Task WriteIndexRecordAsync(int connectionId)
        {
            var list = chunkIndices[connectionId];
            if (list.Count == 0)
            {
                return;
            }

            //Console.WriteLine("** Index start " + writer.Position);

            var op = new OpCodeHeaderEntry(OpCode.IndexData);
            var ver = new IntHeaderEntry("ver", 1);
            var conn = new IntHeaderEntry("conn", connectionId);
            var count = new IntHeaderEntry("count", list.Count);

            int headerLength = 16 + OpCodeHeaderEntry.Length + ver.Length + conn.Length + count.Length;

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(ver);
            await writer.WriteValueAsync(conn);
            await writer.WriteValueAsync(count);

            int dataLength = 12 * list.Count;
            await writer.WriteValueAsync(dataLength);
            foreach ((time time, long offset) in list)
            {
                await writer.WriteValueAsync(time.Secs);
                await writer.WriteValueAsync(time.Nsecs);
                await writer.WriteValueAsync((int) offset);
            }

            //Console.WriteLine("** End position: " + writer.Position);
        }

        void WriteChunkInfoRecord(ChunkInfoRecord record)
        {
            //Console.WriteLine("** Chunk start " + writer.Position);

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

            //Console.WriteLine("** End position: " + writer.Position);
        }

        async Task WriteChunkInfoRecordAsync(ChunkInfoRecord record)
        {
            //Console.WriteLine("** Chunk start " + writer.Position);

            var op = new OpCodeHeaderEntry(OpCode.ChunkInfo);
            var ver = new IntHeaderEntry("ver", 1);
            var chunkPos = new LongHeaderEntry("chunk_pos", record.ChunkPos);
            var startTime = new TimeHeaderEntry("start_time", record.StartTime);
            var endTime = new TimeHeaderEntry("end_time", record.EndTime);
            var count = new IntHeaderEntry("count", record.MessagesByConnection.Length);

            int headerLength = 24 + OpCodeHeaderEntry.Length
                                  + ver.Length + chunkPos.Length + startTime.Length
                                  + endTime.Length + count.Length;

            await writer.WriteValueAsync(headerLength);
            await writer.WriteValueAsync(op);
            await writer.WriteValueAsync(ver);
            await writer.WriteValueAsync(chunkPos);
            await writer.WriteValueAsync(startTime);
            await writer.WriteValueAsync(endTime);
            await writer.WriteValueAsync(count);

            int dataLength = 8 * record.MessagesByConnection.Length;
            await writer.WriteValueAsync(dataLength);
            foreach ((int connId, int connCount) in record.MessagesByConnection)
            {
                await writer.WriteValueAsync(connId);
                await writer.WriteValueAsync(connCount);
            }

            //Console.WriteLine("** End position: " + writer.Position);
        }

        void TryOpenChunk(in time timestamp)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("RosbagFileWriter",
                    "Cannot write in a rosbag file that has already been disposed.");
            }

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

        /// <summary>
        /// Closes the current chunk. The next message will be written in a new chunk.
        /// A chunk is a container for messages. Splitting the rosbag into multiple chunks will make it easier to
        /// recover the closed chunks if for some reason you cannot close the file properly.
        /// In most cases you do not need to call this.
        /// </summary>
        /// <returns>True if the current chunk was closed. False if there is no current chunk.</returns>
        public bool TryCloseChunk()
        {
            if (chunkStart == null)
            {
                return false;
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

            return true;
        }

        
        /// <summary>
        /// Closes the current chunk. The next message will be written in a new chunk.
        /// A chunk is a container for messages. Splitting the rosbag into multiple chunks will make it easier to
        /// recover the closed chunks if for some reason you cannot close the file properly.
        /// In most cases you do not need to call this.
        /// </summary>
        /// <returns>True if the current chunk was closed. False if there is no current chunk.</returns>
        async ValueTask<bool> TryCloseChunkAsync()
        {
            if (chunkStart == null)
            {
                return false;
            }

            long position = writer.Position;
            int size = (int) (position - chunkDataStart);

            writer.Seek(chunkStart.Value, SeekOrigin.Begin);
            await WritePartialChunkRecordAsync(size);
            writer.Seek(position, SeekOrigin.Begin);

            foreach (int connectionId in chunkIndices.Keys)
            {
                await WriteIndexRecordAsync(connectionId);
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

            return true;
        }

        
        /// <summary>
        /// Adds a message to the rosbag file.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="connection">
        /// The connection where the message originated.
        /// See for example RosSubscriber.Subscribe with the IRosTcpReceiver callback.
        /// </param>
        /// <param name="timestamp">The time at which the message arrived</param>
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
                //Console.WriteLine("**  Adding connection " + connections.Count);
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

            if (CurrentChunkLength > MaxChunkSize)
            {
                TryCloseChunk();
            }
        }

        /// <summary>
        /// Adds a message to the rosbag file.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="connection">
        /// The connection where the message originated.
        /// See for example RosSubscriber.Subscribe with the IRosTcpReceiver callback.
        /// </param>
        /// <param name="timestamp">The time at which the message arrived</param>
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
                //Console.WriteLine("**  Adding connection " + connections.Count);
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
            
            if (CurrentChunkLength > MaxChunkSize)
            {
                await TryCloseChunkAsync();
            }            
        }

        /// <summary>
        /// Writes the index and closes the file.
        /// </summary>
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            TryCloseChunk();

            if (connections.Count != 0 && chunkInfos.Count != 0)
            {
                long connectionStart = writer.Position;

                //Console.WriteLine("** Writing connections");
                foreach (var pair in connections)
                {
                    WriteConnectionRecord(pair.Value, pair.Key.Topic, pair.Key.TcpHeader);
                }

                //Console.WriteLine("** Writing chunk infos");
                foreach (var info in chunkInfos)
                {
                    WriteChunkInfoRecord(info);
                }

                //Console.WriteLine("** Updating header");
                writer.Seek(RosbagMagic.Length, SeekOrigin.Begin);
                WriteHeaderRecord(connections.Count, chunkInfos.Count, connectionStart);
            }

            if (!leaveOpen)
            {
                writer.Dispose();
            }
        }

#if !NETSTANDARD2_0
        ValueTask IAsyncDisposable.DisposeAsync() => new(DisposeAsync());
#endif

        /// <summary>
        /// Writes the connection index and closes the file.
        /// </summary>
        public async Task DisposeAsync()
        {
            await TryCloseChunkAsync();

            if (connections.Count != 0 && chunkInfos.Count != 0)
            {
                long connectionStart = writer.Position;

                //Console.WriteLine("** Writing connections");
                foreach (var pair in connections)
                {
                    await WriteConnectionRecordAsync(pair.Value, pair.Key.Topic, pair.Key.TcpHeader);
                }

                //Console.WriteLine("** Writing chunk infos");
                foreach (var info in chunkInfos)
                {
                    await WriteChunkInfoRecordAsync(info);
                }

                //Console.WriteLine("** Updating header");
                writer.Seek(RosbagMagic.Length, SeekOrigin.Begin);
                await WriteHeaderRecordAsync(connections.Count, chunkInfos.Count, connectionStart);
            }

            if (!leaveOpen)
            {
                writer.Dispose();
            }
        }
    }
}