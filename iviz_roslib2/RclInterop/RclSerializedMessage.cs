using System.Runtime.InteropServices;

namespace Iviz.Roslib2.RclInterop;

[StructLayout(LayoutKind.Sequential)]
internal struct RclSerializedMessage
{
    public readonly IntPtr buffer;
    public long bufferLength;
    public readonly long bufferCapacity;
    readonly RcUtilsAllocator allocator;
}

[StructLayout(LayoutKind.Sequential)]
internal struct RcUtilsAllocator
{
    readonly IntPtr allocate;
    readonly IntPtr deallocate;
    readonly IntPtr reallocate;
    readonly IntPtr zeroAllocate;
    readonly IntPtr state;
}