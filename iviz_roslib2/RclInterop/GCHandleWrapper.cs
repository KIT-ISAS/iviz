using System.Runtime.InteropServices;

namespace Iviz.Roslib2.RclInterop;

internal struct GCHandleWrapper : IDisposable
{
    GCHandle handle;
    public readonly IntPtr ptr; // not a memory pointer! just an offset

    public GCHandleWrapper(object o)
    {
        handle = GCHandle.Alloc(o);
        ptr = (IntPtr)handle;
    }

    public void Dispose() => handle.Free();
}