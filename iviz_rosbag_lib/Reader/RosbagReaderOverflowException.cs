using System;

namespace Iviz.Rosbag.Reader;

public class RosbagReaderOverflowException : Exception
{
    public RosbagReaderOverflowException(Exception e) : base(
        "The rosbag reader stream overflowed while reading a value", e)
    {
    }
}