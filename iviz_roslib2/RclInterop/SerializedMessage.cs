using System.Runtime.CompilerServices;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class SerializedMessage : IDisposable
{
    readonly IntPtr handle;
    bool disposed;

    ref RclSerializedMessage GetSerializedMessage() =>
        ref Unsafe.As<byte, RclSerializedMessage>(ref Rcl.GetReference(Handle));

    internal IntPtr Handle => disposed
        ? BuiltIns.ThrowPointerDisposed(nameof(SerializedMessage)) 
        : handle;

    public SerializedMessage()
    {
        Rcl.Check(Rcl.Impl.CreateSerializedMessage(out handle));
    }

    public Span<byte> Resize(int size)
    {
        if (size < 0) BuiltIns.ThrowArgumentOutOfRange();

        ref var serializedMessage = ref GetSerializedMessage();
        if (size <= serializedMessage.bufferCapacity)
        {
            serializedMessage.bufferLength = size;
            return Rcl.CreateByteSpan(serializedMessage.buffer, size);
        }

        Rcl.Impl.EnsureSerializedMessageSize(Handle, size, out IntPtr ptr); // allocate new memory
        return Rcl.CreateByteSpan(ptr, size);
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.Impl.DestroySerializedMessage(handle);
    }

    ~SerializedMessage() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}