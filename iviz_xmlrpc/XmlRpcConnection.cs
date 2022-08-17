using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Tools;
using Nito.AsyncEx;

namespace Iviz.XmlRpc;

/// <summary>
/// Permanent connection to an XML-RPC server.
/// </summary>
public sealed class XmlRpcConnection : IDisposable, IComparable<XmlRpcConnection>
{
    const int DelayQueueSize = 5;

    /// <summary> Socket error: connection refused </summary>
    const int ErrConnRef = (int)SocketError.ConnectionRefused;

    /// <summary> Socket error: connection refused, workaround for mono bug in mobile </summary>
    const int ErrConnRefAlt = ErrConnRef - 10000;

    /// <summary> Socket error: host down </summary>
    const int ErrHostDown = (int)SocketError.HostDown;

    /// <summary> Socket error: host down, workaround for mono bug in mobile </summary>
    const int ErrHostDownAlt = ErrHostDown - 10000;

    readonly Queue<int> delays = new(DelayQueueSize);
    readonly Uri remoteUri;
    readonly AsyncLock mutex = new();
    readonly string name;
    HttpRequest? request;
    bool disposed;

    int queueSize;

    public int QueueSize => queueSize;
    public long BytesSent { get; private set; }
    public long BytesReceived { get; private set; }
    public int TotalRequests { get; private set; }
    public int AvgTimeInQueueInMs => delays.Count == 0 ? 0 : delays.Sum() / delays.Count;

    public XmlRpcConnection(string name, Uri remoteUri)
    {
        this.name = name;
        this.remoteUri = remoteUri ?? throw new ArgumentNullException(nameof(remoteUri));
    }

    public async ValueTask<RosValue> MethodCallAsync(Uri callerUri, string method, XmlRpcArg[] args,
        CancellationToken token = default)
    {
        if (disposed)
        {
            throw new ObjectDisposedException("this");
        }

        if (callerUri is null)
        {
            BaseUtils.ThrowArgumentNull(nameof(callerUri));
        }

        if (args is null)
        {
            BaseUtils.ThrowArgumentNull(nameof(args));
        }

        token.ThrowIfCancellationRequested();

        Interlocked.Increment(ref queueSize);
        if (queueSize > 3)
        {
            Logger.LogDebugFormat("{0} has reached queue size {1}", this, queueSize);
        }

        string inData;
        using (await mutex.LockAsync(token))
        {
            Interlocked.Decrement(ref queueSize);

            if (request is { IsAlive: false })
            {
                request.Dispose();
                request = null;
            }

            var start = DateTime.Now;
            try
            {
                request ??= await EnsureValidRequester(callerUri, token);

                int deltaSent;
                using (var outData = XmlRpcService.CreateRequest(method, args))
                {
                    deltaSent = await request.SendRequestAsync(outData, true, true, token);
                }

                token.ThrowIfCancellationRequested();

                (inData, int deltaReceived, bool shouldDispose) = await request.GetResponseAsync(token);
                if (shouldDispose)
                {
                    request.Dispose();
                }

                TotalRequests++;
                BytesSent += deltaSent;
                BytesReceived += deltaReceived;
            }
            catch (Exception e)
            {
                request?.Dispose();
                request = null;

                if (e is OperationCanceledException or ObjectDisposedException)
                {
                    throw;
                }

                throw new RpcConnectionException($"Error while calling RPC method '{method}' at {remoteUri}", e);
            }
            finally
            {
                var end = DateTime.Now;
                int msDiff = (int)(end - start).TotalMilliseconds;
                if (msDiff > 5000)
                {
                    Logger.LogErrorFormat("{0}: Request FAILED after {1} ms for '{2}'. " +
                                          "Did the device come back from suspension?",
                        this, msDiff, method);
                }

                AddToQueue(msDiff);
            }
        }

        return XmlRpcService.ProcessResponseOfMethodCall(inData);
    }

    async ValueTask<HttpRequest> EnsureValidRequester(Uri callerUri, CancellationToken token)
    {
        const int waitTimeInMs = 750;

        Logger.LogDebugFormat("{0}: Starting new connection", this);

        while (true) // keep retrying until we're connected or token expires
        {
            token.ThrowIfCancellationRequested();

            DateTime start = DateTime.Now;
            HttpRequest? newRequest = null;
            try
            {
                newRequest = new HttpRequest(callerUri, remoteUri);
                await newRequest.StartAsync(token);
                return newRequest;
            }
            catch (Exception e)
            {
                newRequest?.Dispose();

                if (e is OperationCanceledException or
                    SocketException { ErrorCode: ErrConnRef or ErrConnRefAlt or ErrHostDown or ErrHostDownAlt })
                {
                    throw; // stop retrying
                }

                Logger.LogDebugFormat("{0} XmlRpc error: {1}", this, e);
            }

            var end = DateTime.Now;
            int msDiff = (int)(end - start).TotalMilliseconds;
            if (msDiff < waitTimeInMs)
            {
                await Task.Delay(waitTimeInMs - msDiff, token);
            }
        }
    }

    void AddToQueue(int value)
    {
        delays.Enqueue(value);
        if (delays.Count > DelayQueueSize)
        {
            delays.Dequeue();
        }
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        request?.Dispose();
    }

    public override string ToString() => $"[{nameof(XmlRpcConnection)} {name} uri={remoteUri}]";

    public int CompareTo(XmlRpcConnection? other) => other is null ? 1 : queueSize - other.queueSize;
}