using System;
using System.IO;

namespace Iviz.Rosbag;

internal static class StreamUtils
{
    public static void ReadAll(this Stream stream, Span<byte> span)
    {
        int read = 0;
        while (read != span.Length)
        {
            read += stream.Read(span[read..]);
        }
    }
}