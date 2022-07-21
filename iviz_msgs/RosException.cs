using System;

namespace Iviz.Msgs;

public class RosException : Exception
{
    protected RosException(string msg) : base(msg)
    {
    }

    protected RosException(string msg, Exception e) : base(msg, e)
    {
    }
}

public class RosInvalidMessageException : RosException
{
    public RosInvalidMessageException(string msg) : base(msg)
    {
    }
}

public class RosInvalidSizeForFixedArrayException : RosInvalidMessageException
{
    public RosInvalidSizeForFixedArrayException() : base(
        "Array size does not match the fixed size of the message definition")
    {
    }

    public RosInvalidSizeForFixedArrayException(string name, int size, int expected) : base(
        $"Array '{name}' with size {size.ToString()} does not match the fixed size " +
        $"{expected.ToString()} of the message definition")
    {
    }
    
    public RosInvalidSizeForFixedArrayException(int size, int expected) : base(
        $"Array field with size {size.ToString()} does not match the fixed size " +
        $"{expected.ToString()} of the message definition")
    {
    }
}
    
public sealed class RosBufferException : RosException
{
    public RosBufferException(string message) : base(message)
    {
    }
}

public sealed class RosInvalidMessageForVersion : RosException
{
    public RosInvalidMessageForVersion() : base(
        "This message cannot be used in this version of ROS")
    {
    }
}
