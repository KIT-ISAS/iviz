using System.Collections.Concurrent;
using Iviz.Msgs;
using Iviz.Roslib2.RclInterop;
using Iviz.Tools;

namespace Iviz.Roslib2;

internal sealed class Ros2ServiceCaller : Signalizable
{
    readonly CancellationTokenSource runningTs = new();
    readonly IDeserializableRos2<IResponse> generator;
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

    public Ros2ServiceCaller(Ros2Client client, IDeserializableRos2<IResponse> generator)
    {
        this.client = client;
        this.generator = generator;
    }

    public void Start()
    {
        task = TaskUtils.Run(() => Run().AwaitNoThrow(this));
    }

    public Task<IResponse> ExecuteAsync(IRequest request, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var tcs = TaskUtils.CreateCompletionSource<IResponse>();
        ServiceClient.SendRequest(request, out long sequenceNumber);
        queue.TryAdd(sequenceNumber, tcs);
        token.Register(() => tcs.TrySetCanceled(token));
        return tcs.Task;
    }

    async ValueTask Run()
    {
        var cancellationToken = runningTs.Token;
        using var serializedBuffer = new SerializedMessage();

        while (true)
        {
            await signal.WaitAsync(cancellationToken);
            ProcessResponses(serializedBuffer);
        }
    }

    void ProcessResponses(SerializedMessage serializedBuffer)
    {
        var rclServiceClient = ServiceClient;
        if (!rclServiceClient.TryTakeResponse(serializedBuffer, out var span, out long sequenceNumber))
        {
            return;
        }

        var response = ReadBuffer2.Deserialize(generator, span);
        if (!queue.TryRemove(sequenceNumber, out var responseTcs))
        {
            Logger.LogErrorFormat("{0}: Could not find request with sequence number {1}!", this, sequenceNumber);
            return;
        }

        responseTcs.TrySetResult(response);
    }

    public async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed) return;
        disposed = true;

        await client.Rcl.DisposeServiceClientAsync(ServiceClient, default).AwaitNoThrow(this);

        runningTs.Cancel();
        await task.AwaitNoThrow(2000, this, token);

        client.RemoveServiceCaller(this);
    }

    public override string ToString()
    {
        return $"[{nameof(Ros2ServiceCaller)} {Service} [{ServiceType}] ]";
    }
}