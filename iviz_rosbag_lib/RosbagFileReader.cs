using System;
using System.Collections.Generic;
using System.IO;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Rosbag.Reader;

public sealed class RosbagFileReader : IDisposable
{
    const string RosbagMagic = "#ROSBAG V2.0\n";
    const int RosbagMagicLength = 13;

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
            BuiltIns.ThrowArgument(nameof(stream), "Stream needs to be readable and be able to seek");
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
            : throw new FileNotFoundException("Failed to initialize reader: Bag path not found", path)
    )
    {
    }

    void ValidateMagic()
    {
        Span<byte> renter = stackalloc byte[RosbagMagicLength];
        reader.ReadAll(renter);

        foreach (int i in ..RosbagMagicLength)
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
    public RecordEnumerable Records => new(new Record(reader, RosbagMagicLength), reader.Length);

    /// <summary>
    /// Gets all records in the file, while unwrapping the inner records of a Chunk record.
    /// </summary>
    /// <returns>An enumerable that iterates through the records.</returns>
    public IEnumerable<Record> ReadAllRecords()
    {
        foreach (var record in Records)
        {
            if (record.OpCode == OpCode.Chunk && record.ChunkRecords is { } chunkRecords)
            {
                foreach (var chunkRecord in chunkRecords)
                {
                    yield return chunkRecord;
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
    /// You can use <see cref="LinqUtils.SelectMessage{T}"/> on the result to enumerate on the messages.
    /// </summary>
    /// <returns>An enumerable that iterates through the messages.</returns>
    public IEnumerable<MessageData> ReadAllMessages()
    {
        if (!TryGetNumConnections(out int numConnections))
        {
            yield break;
        }

        using var connections = new RentAndClear<Connection>(numConnections);

        foreach (var record in ReadAllRecords())
        {
            switch (record.OpCode)
            {
                case OpCode.Connection:
                    if (record.Connection is { } newConnection
                        && newConnection.ConnectionId < numConnections)
                    {
                        connections[newConnection.ConnectionId] = newConnection;
                    }

                    break;
                case OpCode.MessageData:
                    if (record.ConnectionId is { } connectionId
                        && connectionId < numConnections
                        && connections[connectionId] is { } connection)
                    {
                        yield return record.GetMessageData(connection);
                    }

                    break;
            }
        }
    }

    bool TryGetNumConnections(out int numConnections)
    {
        var enumerator = Records.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            numConnections = default;
            return false;
        }

        var record = enumerator.Current;
        if (record.OpCode == OpCode.BagHeader
            && record.TryGetHeaderEntry("conn_count", out var entry))
        {
            numConnections = entry.ValueAsInt;
            return true;
        }

        numConnections = default;
        return false;
    }

    public IEnumerable<Connection> GetAllConnections()
    {
        var enumerator = Records.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            return Array.Empty<Connection>();
        }

        var firstRecord = enumerator.Current;
        if (firstRecord.OpCode != OpCode.BagHeader
            || !firstRecord.TryGetHeaderEntry("conn_count", out var connEntry)
            || !firstRecord.TryGetHeaderEntry("index_pos", out var indexEntry))
        {
            return Array.Empty<Connection>();
        }

        int numConnections = connEntry.ValueAsInt;
        int indexPos = indexEntry.ValueAsInt;

        var connections = new List<Connection>(numConnections);
        var connectionRecords = new RecordEnumerable(new Record(reader, indexPos), reader.Length);
        foreach (var record in connectionRecords)
        {
            if (record.Connection is { } connection)
            {
                connections.Add(connection);
            }
        }

        return connections;
    }
}