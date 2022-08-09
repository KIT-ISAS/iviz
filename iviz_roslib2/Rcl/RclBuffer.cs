using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclBuffer : IDisposable
{
    readonly IntPtr handle;
    bool disposed;

    internal IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : handle;

    public RclBuffer()
    {
        Rcl.Check(Rcl.Impl.CreateSerializedMessage(out handle));
    }

    public Span<byte> Resize(int size)
    {
        if (size < 0) BuiltIns.ThrowArgumentOutOfRange();
        
        int sizeAlign4 = (size + 3) & ~3;
        
        ref var serializedMessage = ref GetSerializedMessage();
        if (sizeAlign4 <= serializedMessage.bufferCapacity)
        {
            serializedMessage.bufferLength = sizeAlign4;
            return Rcl.CreateByteSpan(serializedMessage.buffer, sizeAlign4);
        }

        Rcl.Impl.EnsureSerializedMessageSize(Handle, sizeAlign4, out IntPtr ptr); // allocate new memory
        return Rcl.CreateByteSpan(ptr, sizeAlign4);
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.Impl.DestroySerializedMessage(handle);
    }

    ~RclBuffer() => Logger.LogErrorFormat("{0} has not been disposed!", this);
    
    unsafe ref RclSerializedMessage GetSerializedMessage() => ref *(RclSerializedMessage*)Handle.ToPointer();
}
