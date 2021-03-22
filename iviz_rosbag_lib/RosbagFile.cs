using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Iviz.Msgs;

namespace Iviz.Rosbag
{
    public sealed class RosbagFile : IDisposable
    {
        internal const string RosbagMagic = "#ROSBAG V2.0\n";

        readonly Stream reader;
        readonly bool leaveOpen;

        /// <summary>
        /// Opens a rosbag from a given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="leaveOpen">Whether to leave the stream open when this object is disposed.</param>
        /// <exception cref="ArgumentException">Thrown if the stream does not allow reading and seeking.</exception>
        /// <exception cref="InvalidDataException">Thrown if the data is not a rosbag file.</exception>
        public RosbagFile(Stream stream, bool leaveOpen = false)
        {
            reader = stream ?? throw new ArgumentNullException(nameof(stream));
            this.leaveOpen = leaveOpen;

            if (!stream.CanRead || !stream.CanSeek)
            {
                throw new ArgumentException("Stream needs to be readable and be able to seek", nameof(stream));
            }

            ValidateMagic();
        }

        /// <summary>
        /// Opens a rosbag from a given file.
        /// </summary>
        /// <param name="path">The file to open.</param>
        /// <exception cref="InvalidDataException">Thrown if the data is not a rosbag file.</exception>
        public RosbagFile(string path) : this(File.Open(path, FileMode.Open, FileAccess.Read))
        {
        }

        void ValidateMagic()
        {
            using var renter = new Rent<byte>(RosbagMagic.Length);
            reader.Read(renter.Array, 0, RosbagMagic.Length);

            for (int i = 0; i < RosbagMagic.Length; i++)
            {
                if (renter[i] != RosbagMagic[i])
                {
                    throw new InvalidDataException("File does not appear to be a Rosbag v2 file!");
                }
            }
        }


        /// <summary>
        /// Closes the underlying stream.
        /// </summary>
        public void Dispose()
        {
            if (!leaveOpen)
            {
                reader.Dispose();
            }
        }

        /// <summary>
        /// Gets all records in the file.
        /// </summary>
        /// <returns>An enumerable that iterates through the records.</returns>
        public RecordEnumerable Records => new(new Record(reader));

        /// <summary>
        /// Gets all records in the file, while unwrapping the inner records of a Chunk record.
        /// </summary>
        /// <returns>An enumerable that iterates through the records.</returns>
        public IEnumerable<Record> GetAllRecords()
        {
            foreach (var record in Records)
            {
                if (record.OpCode == OpCode.Chunk)
                {
                    foreach (var chunk in record.Chunk.Records)
                    {
                        yield return chunk;
                    }
                }
                else
                {
                    yield return record;
                }
            }
        }

        /// <summary>
        /// Get all messages in the file, and associates them with their originating connection.
        /// </summary>
        /// <returns>An enumerable that iterates through the messages.</returns>
        public IEnumerable<MessageData> GetAllMessages()
        {
            var connections = new Dictionary<int, Connection>();

            foreach (var record in GetAllRecords())
            {
                switch (record.OpCode)
                {
                    case OpCode.Connection:
                        var connection = record.Connection;
                        connections[connection.ConnectionId] = connection;
                        break;
                    case OpCode.MessageData:
                        if (connections.TryGetValue(record.ConnectionId, out var msgConnection))
                        {
                            var message = record.MessageData;
                            message.Connection = msgConnection;
                            yield return message;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Get all messages in the file whose originating connection fulfills the given condition.
        /// Pretty much the same as using a LINQ Where() on <see cref="GetAllMessages"/>,
        /// except it doesn't create a MessageData object if not needed, so it generates a little less garbage.
        /// </summary>
        /// <param name="predicate">The condition that the message connection must fulfill</param>
        /// <returns>An enumerable that iterates through the messages</returns>
        public IEnumerable<MessageData> GetAllMessagesWhere(Predicate<Connection> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            var connections = new Dictionary<int, Connection>();

            foreach (var record in GetAllRecords())
            {
                switch (record.OpCode)
                {
                    case OpCode.Connection:
                        var connection = record.Connection;
                        if (predicate(connection))
                        {
                            connections[connection.ConnectionId] = connection;
                        }

                        break;
                    case OpCode.MessageData:
                        if (connections.TryGetValue(record.ConnectionId, out var msgConnection))
                        {
                            var message = record.MessageData;
                            message.Connection = msgConnection;
                            yield return message;
                        }

                        break;
                }
            }
        }
    }
}