using System.Runtime.InteropServices;

namespace Iviz.Roslib2.Rcl;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct RmwServiceInfo
{
    public readonly long sourceTimestamp;
    public readonly long receivedTimestamp;
    public readonly RmwRequestId requestId;
}

[StructLayout(LayoutKind.Sequential)]
internal readonly struct RmwRequestId
{
    public readonly long writerGuidA;
    public readonly long writerGuidB;
    public readonly long sequenceNumber;
}