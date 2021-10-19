using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Roslib
{
    internal sealed class TcpReceiver<T> : IProtocolReceiver, ILoopbackReceiver<T> where T : IMessage
    {
        const int DisposeTimeoutInMs = 2000;

        readonly ReceiverManager<T> manager;
        readonly bool requestNoDelay;
        readonly CancellationTokenSource runningTs = new();
        readonly Task task;
        readonly int connectionTimeoutInMs;

        TopicInfo<T> topicInfo;
        TcpClient? tcpClient;
        bool disposed;
        long numReceived;
        long bytesReceived;
        string? errorDescription;

        readonly Endpoint remoteEndpoint;
        public Endpoint? Endpoint { get; private set; }
        string[]? rosHeader;

        bool KeepRunning => !runningTs.IsCancellationRequested;
        string Topic => topicInfo.Topic;

        public bool IsPaused { get; set; }
        public Uri RemoteUri { get; }
        public bool IsAlive => !task.IsCompleted;
        public bool IsConnected => tcpClient is { Connected: true };
        public ReceiverStatus Status { get; private set; }

        IRosReceiverInfo? cachedConnectionInfo;

        public TcpReceiver(ReceiverManager<T> manager,
            Uri remoteUri, Endpoint remoteEndpoint, TopicInfo<T> topicInfo,
            bool requestNoDelay, int timeoutInMs)
        {
            RemoteUri = remoteUri;
            this.remoteEndpoint = remoteEndpoint;
            this.topicInfo = topicInfo;
            this.manager = manager;
            this.requestNoDelay = requestNoDelay;
            connectionTimeoutInMs = timeoutInMs;
            Status = ReceiverStatus.ConnectingTcp;
            task = TaskUtils.StartLongTask(StartSession, runningTs.Token);
        }

        public SubscriberReceiverState State => new(RemoteUri)
        {
            Status = Status,
            TransportType = TransportType.Tcp,
            RequestNoDelay = requestNoDelay,
            EndPoint = Endpoint,
            RemoteEndpoint = remoteEndpoint,
            NumReceived = numReceived,
            BytesReceived = bytesReceived,
            ErrorDescription = errorDescription
        };

        async Task StartSession()
        {
            bool shouldRetry = false;

            var newTcpClient = await StartTcpConnection();
            if (newTcpClient != null)
            {
                Logger.LogDebugFormat("{0}: Connected!", this);
                var maybeEndpoint = (IPEndPoint?)newTcpClient.Client.LocalEndPoint;
                Endpoint = maybeEndpoint != null ? new Endpoint(maybeEndpoint) : null;
                tcpClient = newTcpClient;

                try
                {
                    await ProcessLoop(newTcpClient);
                }
                catch (Exception e) when (e is ObjectDisposedException or OperationCanceledException)
                {
                }
                catch (Exception e) when (e is IOException or SocketException or TimeoutException)
                {
                    Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                    shouldRetry = true;
                }
                catch (Exception e) when (e is RoslibException)
                {
                    errorDescription = e.Message;
                    Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
                }
                catch (Exception e)
                {
                    Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                }
            }
            else
            {
                Logger.LogDebugFormat("{0}: Connection to the TCP listener failed!", this);
                errorDescription = "Could not connect to the TCP listener";
                shouldRetry = true;
            }

            Status = ReceiverStatus.Dead;
            tcpClient?.Dispose();
            tcpClient = null;
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            Logger.LogDebugFormat("{0}: Stopped!", this);

            if (shouldRetry)
            {
                try
                {
                    await Task.Delay(2000, runningTs.Token);
                }
                catch (OperationCanceledException)
                {
                }

                manager.RetryConnection(RemoteUri);
            }
        }

        async Task<TcpClient?> StartTcpConnection()
        {
            Logger.LogDebugFormat("{0}: Trying to connect!", this);

            TcpClient newTcpClient = new(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
            (string hostname, int port) = remoteEndpoint;

            try
            {
                await newTcpClient.TryConnectAsync(hostname, port, runningTs.Token, connectionTimeoutInMs);
            }
            catch (Exception e)
            {
                if (e is not (IOException or SocketException or OperationCanceledException))
                {
                    Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                }

                newTcpClient.Dispose();
                errorDescription = "Could not connect to the TCP listener";
                return null;
            }

            if (newTcpClient.Client?.RemoteEndPoint == null || newTcpClient.Client?.LocalEndPoint == null)
            {
                newTcpClient.Dispose();
                errorDescription = "Could not connect to the TCP listener";
                return null;
            }

            return newTcpClient;
        }

        async ValueTask<int> ReceivePacket(TcpClient client, ResizableRent<byte> readBuffer)
        {
            if (!await client.ReadChunkAsync(readBuffer.Array, 4, runningTs.Token))
            {
                return -1;
            }

            int length = BitConverter.ToInt32(readBuffer.Array, 0);
            if (length == 0)
            {
                return 0;
            }

            const int maxMessageLength = 64 * 1024 * 1024;
            if (length is < 0 or > maxMessageLength)
            {
                throw new RosInvalidPackageSizeException($"Invalid packet size {length}. Disconnecting.");
            }

            readBuffer.EnsureCapability(length);
            if (!await client.ReadChunkAsync(readBuffer.Array, length, runningTs.Token))
            {
                return -1;
            }

            return length;
        }

        async ValueTask<int> ReceiveAndIgnore(TcpClient client, byte[] tmpBuffer)
        {
            if (!await client.ReadChunkAsync(tmpBuffer, 4, runningTs.Token))
            {
                return -1;
            }

            int length = BitConverter.ToInt32(tmpBuffer, 0);
            if (length == 0)
            {
                return 0;
            }

            if (!await client.ReadAndIgnoreAsync(length, runningTs.Token))
            {
                return -1;
            }

            return length;
        }


        Task SendHeader(TcpClient client)
        {
            string[] contents =
            {
                $"message_definition={topicInfo.MessageDependencies}",
                $"callerid={topicInfo.CallerId}",
                $"topic={topicInfo.Topic}",
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                requestNoDelay ? "tcp_nodelay=1" : "tcp_nodelay=0"
            };

            return client.WriteHeaderAsync(contents, runningTs.Token);
        }

        async Task ProcessHandshake(TcpClient client)
        {
            await SendHeader(client);

            using ResizableRent<byte> readBuffer = new(4);

            int receivedLength = await ReceivePacket(client, readBuffer);
            if (receivedLength == -1)
            {
                throw new IOException("Connection closed during handshake");
            }


            List<string> responseHeader = RosUtils.ParseHeader(readBuffer.Array, receivedLength);
            var dictionary = RosUtils.CreateHeaderDictionary(responseHeader);
            if (dictionary.TryGetValue("error", out string? message)) // TODO: improve error handling here
            {
                throw new RosHandshakeException($"Partner sent error message: [{message}]");
            }

            rosHeader = responseHeader.ToArray();

            if (DynamicMessage.IsDynamic<T>() || DynamicMessage.IsGenericMessage<T>())
            {
                topicInfo = RosUtils.GenerateDynamicTopicInfo<T>(topicInfo.CallerId, topicInfo.Topic, rosHeader);
            }
        }

        async Task ProcessLoop(TcpClient client)
        {
            await ProcessHandshake(client);

            Status = ReceiverStatus.Running;

            bool hasFixedSize = BuiltIns.TryGetFixedSize<T>(out int fixedSize);
            await (hasFixedSize
                ? ProcessLoopFixed(client, fixedSize)
                : ProcessLoopVariable(client));
        }

        async Task ProcessLoopFixed(TcpClient client, int fixedSize)
        {
            cachedConnectionInfo = CreateConnectionInfo();

            int fixedSizeWithHeader = 4 + fixedSize;
            using var readBuffer = new Rent<byte>(fixedSizeWithHeader);

            while (KeepRunning)
            {
                bool success = await client.ReadChunkAsync(readBuffer.Array, fixedSizeWithHeader, runningTs.Token);
                if (!success)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                int receivedSize = BitConverter.ToInt32(readBuffer.Array, 0);
                if (receivedSize != fixedSize)
                {
                    throw new RosInvalidPackageSizeException(
                        $"Receiver expected packet with fixed size of {fixedSize} bytes, " +
                        $"but got a packet of size {receivedSize}!");
                }

                numReceived++;
                bytesReceived += fixedSizeWithHeader;

                if (!IsPaused)
                {
                    T message = topicInfo.Generator.DeserializeFromArray(readBuffer.Array, fixedSizeWithHeader, 4);
                    manager.MessageCallback(message, cachedConnectionInfo);
                }
            }
        }

        async Task ProcessLoopVariable(TcpClient client)
        {
            cachedConnectionInfo = CreateConnectionInfo();

            using ResizableRent<byte> readBuffer = new(4);
            while (KeepRunning)
            {
                bool isPaused = IsPaused;

                int rcvLength = isPaused
                    ? await ReceiveAndIgnore(client, readBuffer.Array)
                    : await ReceivePacket(client, readBuffer);

                if (rcvLength == -1)
                {
                    Logger.LogDebugFormat("{0}: Partner closed connection", this);
                    return;
                }

                numReceived++;
                bytesReceived += rcvLength + 4;

                if (!isPaused)
                {
                    T message = topicInfo.Generator.DeserializeFromArray(readBuffer.Array, rcvLength);
                    manager.MessageCallback(message, cachedConnectionInfo);
                }
            }
        }

        void ILoopbackReceiver<T>.Post(in T message, int rcvLength)
        {
            if (!IsAlive)
            {
                return;
            }

            cachedConnectionInfo ??= CreateConnectionInfo();

            numReceived++;
            bytesReceived += rcvLength + 4;

            if (!IsPaused)
            {
                manager.MessageCallback(message, cachedConnectionInfo);
            }
        }

        IRosReceiverInfo CreateConnectionInfo() => new RosReceiverInfo(
            RemoteUri,
            remoteEndpoint,
            Endpoint ?? default,
            Topic,
            topicInfo.Type,
            rosHeader ?? Array.Empty<string>() // none of these are null by creation time
        );

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            tcpClient?.Dispose();

            await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
            runningTs.Dispose();
        }

        public override string ToString()
        {
            return $"[TcpReceiver for '{Topic}' PartnerUri={RemoteUri} " +
                   $"PartnerSocket={remoteEndpoint.Hostname}:{remoteEndpoint.Port.ToString()}]";
        }
    }
}