using System.Runtime.CompilerServices;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class RclServiceClient : IDisposable, IHasHandle
{
    readonly IntPtr contextHandle;
    readonly IntPtr clientHandle;
    readonly IntPtr nodeHandle;
    bool disposed;

    public string Service { get; }
    public string ServiceType { get; }

    public IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : clientHandle;

    public RclServiceClient(IntPtr contextHandle, IntPtr nodeHandle, string service, string serviceType,
        QosProfile profile)
    {
        if (contextHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (nodeHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (service == null) BuiltIns.ThrowArgumentNull(nameof(service));
        if (serviceType == null) BuiltIns.ThrowArgumentNull(nameof(serviceType));

        this.contextHandle = contextHandle;
        this.nodeHandle = nodeHandle;
        Service = service;
        ServiceType = serviceType;

        int ret = Rcl.Impl.CreateClientHandle(out clientHandle, nodeHandle, service, serviceType, in profile.Profile);
        switch ((RclRet)ret)
        {
            case RclRet.InvalidMsgType:
                throw new RosUnsupportedMessageException(serviceType);
            case RclRet.Ok:
                break;
            default:
                throw new RosRclException($"Creating service client '{service}' [{serviceType}] failed!", ret);
        }
    }

    public void SendRequest(IRequest request, out long sequenceNumber)
    {
        using var messageBuffer = new SerializedMessage();
        const int headerSize = 4;

        int messageLength = request.GetRos2MessageLength();
        int serializedLength = messageLength + headerSize;

        var span = messageBuffer.Resize(serializedLength);

        const int header = 0x00000100; // CDR_LE { 0x00, 0x01}, serialization options { 0x00, 0x00 } 
        Unsafe.WriteUnaligned(ref span[0], header);

        WriteBuffer2.Serialize(request, span[headerSize..]);

        Check(Rcl.Impl.SendRequest(Handle, messageBuffer.Handle, out sequenceNumber));
    }

    public bool TryTakeResponse(SerializedMessage messageBuffer, out ReadOnlySpan<byte> span, out long sequenceNumber)
    {
        int ret = Rcl.Impl.TakeResponse(Handle, messageBuffer.Handle, out var requestHeader, out var ptr,
            out int length);

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                const int headerSize = 4;
                span = Rcl.CreateReadOnlyByteSpan(ptr + headerSize, length - headerSize);
                sequenceNumber = requestHeader.requestId.sequenceNumber;
                return true;
            case RclRet.SubscriptionTakeFailed:
                span = default;
                sequenceNumber = 0;
                return false;
            default:
                Logger.LogErrorFormat("{0}: " + nameof(Rcl.Impl.TakeResponse) + " failed!", this);
                span = default;
                sequenceNumber = 0;
                return false;
        }
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public bool IsServiceServerAvailable()
    {
        Check(Rcl.Impl.IsServiceServerAvailable(clientHandle, nodeHandle, out byte isAvailable));
        return isAvailable != 0;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.Impl.DestroyClientHandle(clientHandle, nodeHandle);
    }

    ~RclServiceClient() => Logger.LogErrorFormat("{0} has not been disposed!", this);

    public override string ToString()
    {
        return $"[{nameof(RclServiceClient)} {Service} [{ServiceType}] ]";
    }
}