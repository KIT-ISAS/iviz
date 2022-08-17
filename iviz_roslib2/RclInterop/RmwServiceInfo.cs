using System.Runtime.InteropServices;

namespace Iviz.Roslib2.RclInterop;

[StructLayout(LayoutKind.Sequential)]
public readonly struct RmwServiceInfo
{
    readonly long sourceTimestamp;
    readonly long receivedTimestamp;
    public readonly RmwRequestId requestId;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct RmwRequestId
{
    readonly long writerGuidA;
    readonly long writerGuidB;
    public readonly long sequenceNumber;
}