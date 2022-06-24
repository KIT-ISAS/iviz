using System;
using System.IO;
using Iviz.Rosbag.Reader;

namespace Iviz.Rosbag;

internal static class StreamUtils
{
    public static void ReadAll(this Stream stream, Span<byte> span)
    {
        int read = 0;
        try
        {
            while (read != span.Length)
            {
                read += stream.Read(span[read..]);
            }
        }
        catch (IOException e)
        {
            throw new RosbagReaderOverflowException(e);
        }
    }
}