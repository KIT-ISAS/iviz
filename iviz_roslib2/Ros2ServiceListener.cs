using Iviz.Msgs;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;

namespace Iviz.Roslib2;

internal class Ros2ServiceListener : ISignalizable
{
    readonly Func<IService> generator;
    readonly SemaphoreSlim signal = new(0);
    readonly CancellationTokenSource runningTs = new();
    readonly Ros2Client client;
    readonly Func<IService, ValueTask> callback;

    RclServiceServer? serviceServer;
    Task? task;
    bool disposed;

    public string Service => ServiceServer.Service;
    public string ServiceType => ServiceServer.ServiceType;
    
    public RclServiceServer ServiceServer
    {
        private get => serviceServer ?? throw new NullReferenceException("Service server has not been initialized!");
        set => serviceServer = value;
    }
    
    public Ros2ServiceListener(Ros2Client client, Func<IService> generator, Func<IService, ValueTask> callback)
    {
        this.client = client;
        this.generator = generator;
        this.callback = callback;
    }

    public void Start()
    {
        task = TaskUtils.Run(() => Run().AwaitNoThrow(this));
    }

    async ValueTask Run()
    {
        var cancellationToken = runningTs.Token;
        using var serializedBuffer = new RclSerializedBuffer();

        while (true)
        {
            await signal.WaitAsync(cancellationToken);
            ProcessRequests(serializedBuffer);
        }
    }

    void ProcessRequests(RclSerializedBuffer serializedBuffer)
    {
        while (ServiceServer.TryTakeRequest(serializedBuffer, out var span, out var requestId))
        {
            var service = generator();
            var request = (IDeserializable<IRequest>)service.Request;
            service.Request = ReadBuffer2.Deserialize(request, span);

            _ = TaskUtils.Run(() => ProcessRequest(service, requestId));
        }
    }

    async Task ProcessRequest(IService service, RmwRequestId requestId)
    {
        try
        {
            await callback(service);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Inner exception in service callback: {1}", this, e);
            return;
        }

        try
        {
            service.Response.RosValidate();
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Exception validating service callback response: {1}", this, e);
            return;
        }

        try
        {
            ServiceServer.SendResponse(service.Response, requestId);
        }
        catch (Exception e)
        {
            Logger.LogErrorFormat("{0}: Exception in {1}: {2}", this, nameof(ServiceServer.SendResponse), e);
        }
    }
    
    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return;
        disposed = true;

        await client.Rcl.UnadvertiseServiceAsync(ServiceServer, default).AwaitNoThrow(this);

        runningTs.Cancel();
        await task.AwaitNoThrow(2000, this, token);

        client.RemoveServiceListener(this);
    }

    void ISignalizable.Signal()
    {
        signal.Release();
    }    
    
    public override string ToString()
    {
        return $"[{nameof(Ros2ServiceListener)} {Service} [{ServiceType}] ]";
    }    
}