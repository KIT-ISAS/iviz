using System.Buffers;
using System.Collections.Concurrent;
using Iviz.Msgs;
using Iviz.Roslib2.Rcl;
using Iviz.Tools;

namespace Iviz.Roslib2;

public class Ros2ServiceCaller : ISignalizable
{
    readonly IDeserializable<IResponse> generator;
    readonly SemaphoreSlim signal = new(0);
    readonly CancellationTokenSource runningTs = new();
    readonly ConcurrentDictionary<long, TaskCompletionSource<IResponse>> queue = new();
    readonly Ros2Client client;

    RclServerClient? serverClient;
    Task? task;
    bool disposed;

    internal Ros2ServiceCaller(Ros2Client client, IDeserializable<IResponse> generator)
    {
        this.client = client;
        this.generator = generator;
    }

    internal RclServerClient ServerClient
    {
        private get => serverClient ?? throw new NullReferenceException("Service client has not been initialized!");
        set => serverClient = value;
    }

    internal void Start()
    {
        task = TaskUtils.Run(() => Run().AwaitNoThrow(this));
    }

    public Task<IResponse> Execute(IRequest request)
    {
        var ts = TaskUtils.CreateCompletionSource<IResponse>();
        ServerClient.Execute(request, out long sequenceNumber);
        queue.TryAdd(sequenceNumber, ts);
        return ts.Task;
    }

    async ValueTask Run()
    {
        var rclCaller = ServerClient;
        var cancellationToken = runningTs.Token;
        using var serializedBuffer = new RclSerializedBuffer();

        while (true)
        {
            await signal.WaitAsync(cancellationToken);
            ProcessMessages();
        }

        void ProcessMessages()
        {
            while (rclCaller.TryTakeResponse(serializedBuffer, out var span, out long sequenceNumber))
            {
                var response = ReadBuffer2.Deserialize(generator, span);
                if (queue.TryRemove(sequenceNumber, out var responseTs))
                {
                    responseTs.TrySetResult(response);
                }
                else
                {
                    Logger.LogErrorFormat("{0}: Could not find request with sequence number {1}!", this,
                        sequenceNumber);
                }
            }
        }
    }

    public void Dispose()
    {
        TaskUtils.RunSync(DisposeAsync, default);
    }

    public async ValueTask DisposeAsync(CancellationToken token = default)
    {
        if (disposed) return;
        disposed = true;

        await client.Rcl.DisposeServerClientAsync(ServerClient, default).AwaitNoThrow(this);

        runningTs.Cancel();
        await task.AwaitNoThrow(2000, this, token);

        //client.RemoveSubscriber(this);
    }

    void ISignalizable.Signal()
    {
        signal.Release();
    }
}