using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Iviz.Tools;

namespace Iviz.Rosbag.Reader
{
    public sealed class RosbagFileReader : IDisposable
    {
        const string RosbagMagic = "#ROSBAG V2.0\n";
        internal const int RosbagMagicLength = 13;

        readonly Stream reader;
        readonly bool leaveOpen;

        /// <summary>
        /// Opens a rosbag from a given stream.
        /// </summary>
        /// <param name="stream">The stream to use.</param>
        /// <param name="leaveOpen">Whether to leave the stream open when this object is disposed.</param>
        /// <exception cref="ArgumentException">Thrown if the stream does not allow reading and seeking.</exception>
        /// <exception cref="InvalidDataException">Thrown if the data is not a rosbag file.</exception>
        public RosbagFileReader(Stream stream, bool leaveOpen = false)
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
        public RosbagFileReader(string path) : this(
            File.Exists(path)
                ? File.Open(path, FileMode.Open, FileAccess.Read)
                : throw new FileNotFoundException("Failed to initialize reader: Source path not found", path)
        )
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
        /// Disposes the reader, and closes the underlying stream if requested.
        /// </summary>
        public void Dispose()
        {
            if (!leaveOpen)
            {
                reader.Dispose();
            }
        }

        /// <summary>
        /// Returns an enumerable that iterates through all records
        /// </summary>
        public RecordEnumerable Records => new(new Record(reader));

        /// <summary>
        /// Gets all records in the file, while unwrapping the inner records of a Chunk record.
        /// </summary>
        /// <returns>An enumerable that iterates through the records.</returns>
        public IEnumerable<Record> ReadAllRecords()
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
        public IEnumerable<MessageData> ReadAllMessages()
        {
            var connections = new Dictionary<int, Connection?>
            {
                [-1] = null // shouldn't happen unless bag is broken  
            };

            foreach (var record in ReadAllRecords())
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
                            yield return record.GetMessageData(msgConnection);
                        }

                        break;
                }
            }
        }
    }
}