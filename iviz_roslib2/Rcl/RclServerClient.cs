using System.Buffers;
using System.Collections.Concurrent;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclServerClient : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr clientHandle;
    readonly IntPtr nodeHandle;
    bool disposed;

    //delegate void SpanAction(ReadOnlySpan<byte> span);
    //readonly ConcurrentDictionary<long, SpanAction> queue = new();

    public string Service { get; }
    public string ServiceType { get; }
    
    internal IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : clientHandle;

    public RclServerClient(IntPtr contextHandle, IntPtr nodeHandle, string service, string serviceType)
    {
        if (contextHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (nodeHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (service == null) BuiltIns.ThrowArgumentNull(nameof(service));
        if (serviceType == null) BuiltIns.ThrowArgumentNull(nameof(serviceType));

        this.contextHandle = contextHandle;
        this.nodeHandle = nodeHandle;
        Service = service;
        ServiceType = serviceType;

        int ret = Rcl.CreateClientHandle(out clientHandle, nodeHandle, service, serviceType);
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

    public void Execute(IRequest request, out long sequenceNumber)
    {
        using var messageBuffer = new RclSerializedBuffer();
        const int headerSize = 4;

        const byte cdrLittleEndian0 = 0x00;
        const byte cdrLittleEndian1 = 0x01;
        const byte serializationOptions0 = 0x00;
        const byte serializationOptions1 = 0x00;

        int messageLength = request.Ros2MessageLength;
        int serializedLength = messageLength + headerSize;

        var span = messageBuffer.EnsureSize(serializedLength);
        span[0] = cdrLittleEndian0;
        span[1] = cdrLittleEndian1;
        span[2] = serializationOptions0;
        span[3] = serializationOptions1;

        WriteBuffer2.Serialize(request, span[headerSize..]);

        Check(Rcl.SendRequest(Handle, messageBuffer.Handle, out sequenceNumber));
    }

    public bool TryTakeResponse(RclSerializedBuffer messageBuffer, out Span<byte> span, out long sequenceNumber)
    {
        int ret = Rcl.TakeResponse(clientHandle, messageBuffer.Handle, out var requestHeader, out var ptr,
            out int length);

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                const int headerSize = 4;
                span = Rcl.CreateSpan(ptr + headerSize, length - 4);
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


    /*
    bool TakeResponse(RclSerializedBuffer messageBuffer)
    {
        int ret = Rcl.TakeResponse(clientHandle, messageBuffer.Handle, out var requestHeader, out var ptr,
            out int length);
        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                long sequenceNumber = requestHeader.requestId.sequenceNumber;
                if (queue.TryRemove(sequenceNumber, out var callback))
                {
                    callback(Rcl.CreateReadOnlySpan(ptr, length));
                }
                else
                {
                    Logger.LogErrorFormat("{0}: Could not find request with sequence number {1}!", this,
                        sequenceNumber);
                }

                return true;
            case RclRet.ClientTakeFailed:
                return false;
            default:
                Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.TakeSerializedMessage));
                return false;
        }
    }


    void ISignalizable.Signal()
    {
        using var messageBuffer = new RclSerializedBuffer();
        
        while (TakeResponse(messageBuffer))
        {
        }
    }
    */

    void Check(int result) => Rcl.Check(contextHandle, result);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyClientHandle(clientHandle, nodeHandle);
    }

    ~RclServerClient() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}