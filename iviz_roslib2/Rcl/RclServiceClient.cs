using System.Runtime.CompilerServices;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

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

        int ret = Rcl.CreateClientHandle(out clientHandle, nodeHandle, service, serviceType, in profile.Profile);
        switch (ret)
        {
            case -1:
                throw new RosUnsupportedMessageException(serviceType);
            case Rcl.Ok:
                break;
            default:
                throw new RosRclException($"Creating service client '{service}' [{serviceType}] failed!", ret);
        }
    }

    public void SendRequest(ISerializable request, out long sequenceNumber)
    {
        using var messageBuffer = new RclBuffer();
        const int headerSize = 4;

        int messageLength = request.Ros2MessageLength;
        int serializedLength = messageLength + headerSize;

        var span = messageBuffer.Resize(serializedLength);

        /*
        const byte cdrLittleEndian0 = 0x00;
        const byte cdrLittleEndian1 = 0x01;
        const byte serializationOptions0 = 0x00;
        const byte serializationOptions1 = 0x00;

        span[0] = cdrLittleEndian0;
        span[1] = cdrLittleEndian1;
        span[2] = serializationOptions0;
        span[3] = serializationOptions1;
        */

        const int header = 0x00000100;
        Unsafe.WriteUnaligned(ref span[0], header);

        WriteBuffer2.Serialize(request, span[headerSize..]);

        Check(Rcl.SendRequest(Handle, messageBuffer.Handle, out sequenceNumber));
    }

    public bool TryTakeResponse(RclBuffer messageBuffer, out Span<byte> span, out long sequenceNumber)
    {
        int ret = Rcl.TakeResponse(Handle, messageBuffer.Handle, out var requestHeader, out var ptr,
            out int length);

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                const int headerSize = 4;
                span = Rcl.CreateByteSpan(ptr + headerSize, length - 4);
                sequenceNumber = requestHeader.requestId.sequenceNumber;
                return true;
            case RclRet.SubscriptionTakeFailed:
                span = default;
                sequenceNumber = 0;
                return false;
            default:
                Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.TakeResponse));
                span = default;
                sequenceNumber = 0;
                return false;
        }
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public bool IsServiceServerAvailable()
    {
        Check(Rcl.IsServiceServerAvailable(clientHandle, nodeHandle, out byte isAvailable));
        return isAvailable != 0;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyClientHandle(clientHandle, nodeHandle);
    }

    ~RclServiceClient() => Logger.LogErrorFormat("{0} has not been disposed!", this);

    public override string ToString()
    {
        return $"[{nameof(RclServiceClient)} {Service} [{ServiceType}] ]";
    }
}