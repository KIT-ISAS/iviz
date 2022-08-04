using System.Runtime.CompilerServices;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclServiceServer : IDisposable, IHasHandle
{
    readonly IntPtr contextHandle;
    readonly IntPtr serviceHandle;
    readonly IntPtr nodeHandle;
    bool disposed;

    public string Service { get; }
    public string ServiceType { get; }

    public IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : serviceHandle;

    public RclServiceServer(IntPtr contextHandle, IntPtr nodeHandle, string service, string serviceType,
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

        int ret = Rcl.CreateServiceHandle(out serviceHandle, nodeHandle, service, serviceType, in profile.Profile);
        switch (ret)
        {
            case -1:
                throw new RosUnsupportedMessageException(serviceType);
            case Rcl.Ok:
                break;
            default:
                throw new RosRclException($"Creating service server '{service}' [{serviceType}] failed!", ret);
        }
    }

    public bool TryTakeRequest(RclBuffer messageBuffer, out Span<byte> span, out RmwRequestId requestId)
    {
        int ret = Rcl.TakeRequest(Handle, messageBuffer.Handle, out var serviceInfo, out var ptr,
            out int length);

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                const int headerSize = 4;
                span = Rcl.CreateByteSpan(ptr + headerSize, length - 4);
                requestId = serviceInfo.requestId;
                return true;
            case RclRet.SubscriptionTakeFailed:
                span = default;
                requestId = default;
                return false;
            default:
                Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.TakeResponse));
                goto case RclRet.SubscriptionTakeFailed;
        }
    }

    public void SendResponse(ISerializable response, in RmwRequestId requestId)
    {
        using var messageBuffer = new RclBuffer();
        const int headerSize = 4;

        int messageLength = response.Ros2MessageLength;
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

        WriteBuffer2.Serialize(response, span[headerSize..]);

        Check(Rcl.SendResponse(Handle, messageBuffer.Handle, in requestId));
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyServiceHandle(serviceHandle, nodeHandle);
    }

    ~RclServiceServer() => Logger.LogErrorFormat("{0} has not been disposed!", this);

    public override string ToString()
    {
        return $"[{nameof(RclServiceServer)} {Service} [{ServiceType}] ]";
    }
}