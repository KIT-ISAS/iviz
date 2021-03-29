using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Nito.AsyncEx;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Permanent connection to an XML-RPC server.
    /// </summary>
    public sealed class XmlRpcConnection : IDisposable, IComparable<XmlRpcConnection>
    {
        const int DelayQueueSize = 5;

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

        public async ValueTask<XmlRpcValue> MethodCallAsync(Uri callerUri, string method, XmlRpcArg[] args,
            CancellationToken token = default)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("this");
            }

            if (callerUri is null)
            {
                throw new ArgumentNullException(nameof(callerUri));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            token.ThrowIfCancellationRequested();

            string outData = XmlRpcService.CreateRequest(method, args);
            string inData;

            Interlocked.Increment(ref queueSize);
            if (queueSize > 3)
            {
                Logger.LogDebugFormat("{0} has reached queue size {1}", this, queueSize);
            }

            using (await mutex.LockAsync(token))
            {
                Interlocked.Decrement(ref queueSize);

                if (request != null && !request.IsAlive)
                {
                    request.Dispose();
                    request = null;
                }

                DateTime start = DateTime.Now;
                try
                {
                    if (request == null)
                    {
                        var newRequest = await EnsureValidRequester(callerUri, token);
                        token.ThrowIfCancellationRequested();
                        request = newRequest;
                    }

                    int deltaSent = await request.SendRequestAsync(outData, true, true, token);

                    token.ThrowIfCancellationRequested();

                    int deltaReceived;
                    bool shouldDispose;
                    (inData, deltaReceived, shouldDispose) = await request.GetResponseAsync(token);
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

                    if (e is OperationCanceledException || e is ObjectDisposedException)
                    {
                        throw;
                    }

                    throw new RpcConnectionException($"Error while calling RPC method '{method}' at {remoteUri}", e);
                }
                finally
                {
                    DateTime end = DateTime.Now;
                    int msDiff = (int) (end - start).TotalMilliseconds;
                    if (msDiff > 5000)
                    {
                        // bad stuff. this is way higher than the timeout. it means that the 
                        // task was cancelled a long time ago and it wasn't processed.
                        // most likely ran out of threadpool threads!
                        Logger.LogErrorFormat("{0}: Request FAILED with {1} ms for '{2}' XXXXXXXXXXX", this, msDiff,
                            method);
                    }
                    
                    AddToQueue(msDiff);
                }
            }

            return XmlRpcService.ProcessResponse(inData);
        }

        async Task<HttpRequest> EnsureValidRequester(Uri callerUri, CancellationToken token)
        {
            Logger.LogFormat("{0}: Starting new connection", this);

            while (true)
            {
                token.ThrowIfCancellationRequested();

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

                    switch (e)
                    {
                        case OperationCanceledException:
                            throw;
                        case IOException:
                        case SocketException:
                            Logger.LogDebugFormat("{0} XmlRpc error: {1}", this, e);
                            break;
                        default:
                            Logger.LogFormat("{0} XmlRpc error: {1}", this, e);
                            break;
                    }
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

        public override string ToString()
        {
            return $"[XmlRpcConnection {name} uri={remoteUri}]";
        }

        public int CompareTo(XmlRpcConnection? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return queueSize.CompareTo(other.queueSize);
        }
    }
}