using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclSerializedBuffer : IDisposable
{
    readonly IntPtr handle;
    bool disposed;

    internal IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : handle;
    
    public RclSerializedBuffer()
    {
        Rcl.Check(Rcl.CreateSerializedMessage(out handle));
    }

    public Span<byte> EnsureSize(int size)
    {
        Rcl.EnsureSerializedMessageSize(Handle, size, out IntPtr ptr);
        return Rcl.CreateSpan<byte>(ptr, size);
    }
    
    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroySerializedMessage(handle);
    }
    
    ~RclSerializedBuffer() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}