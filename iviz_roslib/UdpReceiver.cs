using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.MsgsGen.Dynamic;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;

namespace Iviz.Roslib
{
    internal static class UdpOpCode
    {
        public const byte Data0 = 0;
        public const byte DataN = 1;
        public const byte Ping = 2;
        public const byte Err = 3;
    }
    
    internal sealed class UdpReceiver<T> : IProtocolReceiver, ILoopbackReceiver<T> where T : IMessage
    {
        const int DisposeTimeoutInMs = 2000;
        const int MaxPacketSize = 1500;

        readonly Endpoint remoteEndpoint;
        readonly UdpClient udpClient;

        readonly string[] rosHeader = Array.Empty<string>();
        readonly TopicInfo<T> topicInfo;
        readonly CancellationTokenSource runningTs = new();
        readonly Task task = Task.CompletedTask;
        readonly ReceiverManager<T> manager;
        readonly Endpoint endpoint;

        long numReceived;
        long numDropped;
        long bytesReceived;
        string? errorDescription;

        bool disposed;

        IRosReceiverInfo? cachedConnectionInfo;

        bool KeepRunning => !runningTs.IsCancellationRequested;

        public bool IsAlive => true;
        public bool IsPaused { get; set; }
        public bool IsConnected => true;
        public Endpoint? Endpoint => endpoint;
        public Uri RemoteUri { get; }
        public ReceiverStatus Status { get; private set; }

        public UdpReceiver(ReceiverManager<T> manager, RpcUdpTopicResponse response, UdpClient udpClient, Uri remoteUri,
            TopicInfo<T> topicInfo)
        {
            this.manager = manager;

            var (remoteHostname, remotePort, _, _, header) = response;
            remoteEndpoint = new Endpoint(remoteHostname, remotePort);
            RemoteUri = remoteUri;
            this.udpClient = udpClient;
            this.topicInfo = topicInfo;

            var udpEndpoint = (IPEndPoint?)udpClient.Client.LocalEndPoint;
            if (udpEndpoint == null)
            {
                errorDescription = "Failed to bind to local socket";
                Status = ReceiverStatus.Dead;
                udpClient.Dispose();
                return;
            }

            endpoint = new Endpoint(udpEndpoint);

            List<string> responseHeader;
            try
            {
                responseHeader = RosUtils.ParseHeader(header);
            }
            catch (RosInvalidHeaderException e)
            {
                errorDescription = e.Message;
                Status = ReceiverStatus.Dead;
                udpClient.Dispose();
                return;
            }

            rosHeader = responseHeader.ToArray();

            var dictionary = RosUtils.CreateHeaderDictionary(responseHeader);
            if (dictionary.TryGetValue("error", out string? message)) // TODO: improve error handling here
            {
                errorDescription = $"Partner sent error message: [{message}]";
                Status = ReceiverStatus.Dead;
                udpClient.Dispose();
                return;
            }

            if (DynamicMessage.IsDynamic<T>() || DynamicMessage.IsGenericMessage<T>())
            {
                try
                {
                    this.topicInfo =
                        RosUtils.GenerateDynamicTopicInfo<T>(topicInfo.CallerId, topicInfo.Topic, rosHeader);
                }
                catch (RosHandshakeException e)
                {
                    errorDescription = e.Message;
                    Status = ReceiverStatus.Dead;
                    udpClient.Dispose();
                    return;
                }
            }

            task = Task.Run(StartSession);
        }

        public SubscriberReceiverState State => new(RemoteUri)
        {
            Status = Status,
            TransportType = TransportType.Udp,
            RequestNoDelay = false,
            EndPoint = Endpoint,
            RemoteEndpoint = remoteEndpoint,
            NumReceived = numReceived,
            BytesReceived = bytesReceived,
            NumDropped = numDropped,
            ErrorDescription = errorDescription
        };

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            udpClient.Dispose();

            await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
            runningTs.Dispose();
        }

        async Task StartSession()
        {
            try
            {
                await ProcessLoop();
            }
            catch (Exception e) when (e is ObjectDisposedException or OperationCanceledException)
            {
            }
            catch (Exception e) when (e is IOException or SocketException or TimeoutException)
            {
                Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
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

            Status = ReceiverStatus.Dead;
            udpClient.Dispose();

            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }
            
            Logger.LogDebugFormat("{0}: Stopped!", this);
        }

        async Task ProcessLoop()
        {
            const int udpRosHeaderLength = 12;
            
            Status = ReceiverStatus.Running;
            cachedConnectionInfo = CreateConnectionInfo();

            using var readBuffer = new Rent<byte>(MaxPacketSize);

            while (KeepRunning)
            {
                int received = await udpClient.ReadChunkAsync(readBuffer.Array, runningTs.Token);

                numReceived++;
                bytesReceived += received;

                byte opCode = readBuffer[4];
                if (opCode != UdpOpCode.Data0)
                {
                    continue;
                }
                
                if (received < udpRosHeaderLength)
                {
                    // status packet
                    continue;
                }

                int packageSize = BitConverter.ToInt32(readBuffer.Array, 8);
                if (packageSize + udpRosHeaderLength > received)
                {
                    // incomplete
                    numDropped++;
                    continue;
                }

                if (!IsPaused)
                {
                    T message = topicInfo.Generator.DeserializeFromArray(readBuffer.Array, packageSize, udpRosHeaderLength);
                    manager.MessageCallback(message, cachedConnectionInfo);
                }
            }
        }
        
        public static RpcUdpTopicRequest CreateRequest(string hostname, TopicInfo<T> topicInfo)
        {
            string[] contents =
            {
                $"message_definition={topicInfo.MessageDependencies}",
                $"callerid={topicInfo.CallerId}",
                $"topic={topicInfo.Topic}",
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
            };

            byte[] header = StreamUtils.WriteHeaderToArray(contents);
            return new RpcUdpTopicRequest(header, hostname, 0 /* will be set later */, MaxPacketSize);
        }

        void ILoopbackReceiver<T>.Post(in T message, int rcvLength)
        {
            if (!IsAlive)
            {
                return;
            }

            cachedConnectionInfo ??= CreateConnectionInfo();

            numReceived++;
            bytesReceived += rcvLength + 12;

            if (!IsPaused)
            {
                manager.MessageCallback(message, cachedConnectionInfo);
            }
        }

        IRosReceiverInfo CreateConnectionInfo() => new RosReceiverInfo(
            RemoteUri,
            remoteEndpoint,
            endpoint,
            topicInfo.Topic,
            topicInfo.Type,
            rosHeader
        );
        
        public override string ToString()
        {
            return $"[UdpReceiver for '{topicInfo.Topic}' PartnerUri={RemoteUri} " +
                   $"PartnerSocket={remoteEndpoint.Hostname}:{remoteEndpoint.Port.ToString()}]";
        }
        
    }
}