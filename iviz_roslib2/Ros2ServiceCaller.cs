using System.Buffers;
using System.Collections.Concurrent;
using Iviz.Msgs;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;

namespace Iviz.Roslib2;

internal class Ros2ServiceCaller : ISignalizable
{
    readonly SemaphoreSlim signal = new(0);
    readonly CancellationTokenSource runningTs = new();
    readonly ConcurrentDictionary<long, TaskCompletionSource<IResponse>> queue = new();
    readonly Ros2Client client;

    RclServiceClient? serverClient;
    Task? task;
    bool disposed;

    public RclServiceClient ServiceClient
    {
        private get => serverClient ?? throw new NullReferenceException("Service client has not been initialized!");
        set => serverClient = value;
    }

    public string Service => ServiceClient.Service;
    public string ServiceType => ServiceClient.ServiceType;

    public Ros2ServiceCaller(Ros2Client client)
    {
        this.client = client;
    }

    public void Start(IDeserializableRos2<IResponse> generator)
    {
        task = TaskUtils.Run(() => Run(generator).AwaitNoThrow(this));
    }

    public Task<IResponse> ExecuteAsync(IRequest request)
    {
        var tcs = TaskUtils.CreateCompletionSource<IResponse>();
        ServiceClient.SendRequest(request, out long sequenceNumber);
        queue.TryAdd(sequenceNumber, tcs);
        return tcs.Task;
    }

    async ValueTask Run(IDeserializableRos2<IResponse> generator)
    {
        var rclServiceClient = ServiceClient;
        var cancellationToken = runningTs.Token;
        using var serializedBuffer = new RclSerializedBuffer();

        while (true)
        {
            await signal.WaitAsync(cancellationToken);
            ProcessResponses();
        }

        void ProcessResponses()
        {
            while (rclServiceClient.TryTakeResponse(serializedBuffer, out var span, out long sequenceNumber))
            {
                var response = ReadBuffer2.Deserialize(generator, span);
                if (!queue.TryRemove(sequenceNumber, out var responseTcs))
                {
                    Logger.LogErrorFormat("{0}: Could not find request with sequence number {1}!", this,
                        sequenceNumber);
                    continue;
                }

                responseTcs.TrySetResult(response);
            }
        }
    }

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return;
        disposed = true;

        await client.Rcl.DisposeServiceClientAsync(ServiceClient, default).AwaitNoThrow(this);

        runningTs.Cancel();
        await task.AwaitNoThrow(2000, this, token);

        client.RemoveServiceCaller(this);
    }

    void ISignalizable.Signal()
    {
        signal.Release();
    }
    
    public override string ToString()
    {
        return $"[{nameof(Ros2ServiceCaller)} {Service} [{ServiceType}] ]";
    }    
}