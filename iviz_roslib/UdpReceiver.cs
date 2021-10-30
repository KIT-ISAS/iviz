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
    internal sealed class UdpReceiver<T> : IProtocolReceiver, ILoopbackReceiver<T>, IRosReceiverInfo where T : IMessage
    {
        const int DisposeTimeoutInMs = 2000;

        readonly UdpClient udpClient;

        readonly TopicInfo<T> topicInfo;
        readonly CancellationTokenSource runningTs = new();
        readonly Task task = Task.CompletedTask;
        readonly ReceiverManager<T> manager;

        long numReceived;
        long numDropped;
        long bytesReceived;

        bool disposed;

        bool KeepRunning => !runningTs.IsCancellationRequested;

        public ErrorMessage? ErrorDescription { get; private set; }
        public bool IsAlive => true;
        public bool IsPaused { get; set; }
        public bool IsConnected => true;
        public Endpoint Endpoint { get; }
        public Endpoint RemoteEndpoint { get; }
        public Uri RemoteUri { get; }
        public ReceiverStatus Status { get; private set; }
        public IReadOnlyCollection<string> RosHeader { get; } = Array.Empty<string>();
        public string Topic => topicInfo.Topic;
        public string Type => topicInfo.Type;

        public UdpReceiver(ReceiverManager<T> manager, RpcUdpTopicResponse response, UdpClient udpClient, Uri remoteUri,
            TopicInfo<T> topicInfo)
        {
            this.manager = manager;

            var (remoteHostname, remotePort, _, _, header) = response;
            RemoteEndpoint = new Endpoint(remoteHostname, remotePort);
            RemoteUri = remoteUri;
            this.udpClient = udpClient;
            this.topicInfo = topicInfo;

            var udpEndpoint = (IPEndPoint?)udpClient.Client.LocalEndPoint;
            if (udpEndpoint == null)
            {
                ErrorDescription = new ErrorMessage("Failed to bind to local socket");
                Status = ReceiverStatus.Dead;
                udpClient.Dispose();
                return;
            }

            Endpoint = new Endpoint(udpEndpoint);

            List<string> responseHeader;
            try
            {
                responseHeader = RosUtils.ParseHeader(header);
            }
            catch (RosInvalidHeaderException e)
            {
                ErrorDescription = new ErrorMessage(e.Message);
                Status = ReceiverStatus.Dead;
                udpClient.Dispose();
                return;
            }

            RosHeader = responseHeader.ToArray();

            var dictionary = RosUtils.CreateHeaderDictionary(responseHeader);
            if (dictionary.TryGetValue("error", out string? message)) // TODO: improve error handling here
            {
                ErrorDescription = new ErrorMessage($"Partner sent error message: [{message}]");
                Status = ReceiverStatus.Dead;
                udpClient.Dispose();
                return;
            }

            if (DynamicMessage.IsDynamic<T>() || DynamicMessage.IsGenericMessage<T>())
            {
                try
                {
                    this.topicInfo =
                        RosUtils.GenerateDynamicTopicInfo<T>(topicInfo.CallerId, topicInfo.Topic, RosHeader);
                }
                catch (RosHandshakeException e)
                {
                    ErrorDescription = new ErrorMessage(e.Message);
                    Status = ReceiverStatus.Dead;
                    udpClient.Dispose();
                    return;
                }
            }

            task = Task.Run(async () => await StartSession().AwaitNoThrow(this));
        }

        public SubscriberReceiverState State => new(RemoteUri)
        {
            Status = Status,
            TransportType = TransportType.Udp,
            RequestNoDelay = false,
            EndPoint = Endpoint,
            RemoteEndpoint = RemoteEndpoint,
            NumReceived = numReceived,
            BytesReceived = bytesReceived,
            NumDropped = numDropped,
            ErrorDescription = ErrorDescription
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
            catch (Exception e)
            {
                switch (e)
                {
                    case ObjectDisposedException:
                    case OperationCanceledException:
                        break;
                    case IOException:
                    case SocketException:
                    case TimeoutException:
                        ErrorDescription = new ErrorMessage(e.CheckMessage());
                        Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                    case RoslibException:
                        ErrorDescription = new ErrorMessage(e.Message);
                        Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                    default:
                        Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                }
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
            var generator = topicInfo.Generator ?? throw new InvalidOperationException("Invalid generator!");

            Status = ReceiverStatus.Running;
            using var readBuffer = new Rent<byte>(UdpRosParams.DefaultMTU);

            while (KeepRunning)
            {
                int received = await udpClient.ReadChunkAsync(readBuffer.Array, runningTs.Token);

                byte opCode = readBuffer[4];
                if (opCode == UdpRosParams.OpCodePing)
                {
                    continue;
                }

                numReceived++;
                bytesReceived += received;

                if (opCode != UdpRosParams.OpCodeData0)
                {
                    continue;
                }

                if (received < UdpRosParams.HeaderLength)
                {
                    // incomplete packet
                    numDropped++;
                    continue;
                }

                int packageSize = BitConverter.ToInt32(readBuffer.Array, 8);
                if (packageSize + UdpRosParams.HeaderLength > received)
                {
                    // incomplete packet
                    numDropped++;
                    continue;
                }

                if (!IsPaused)
                {
                    T message = generator.DeserializeFromArray(readBuffer.Array, packageSize,
                        UdpRosParams.HeaderLength + 4);
                    manager.MessageCallback(message, this);
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
            return new RpcUdpTopicRequest(header, hostname, 0 /* will be set later */, UdpRosParams.DefaultMTU);
        }

        void ILoopbackReceiver<T>.Post(in T message, int rcvLength)
        {
            if (!IsAlive)
            {
                return;
            }

            numReceived++;
            bytesReceived += rcvLength + UdpRosParams.HeaderLength + 4;

            if (!IsPaused)
            {
                manager.MessageCallback(message, this);
            }
        }

        public override string ToString()
        {
            return $"[UdpReceiver for '{topicInfo.Topic}' PartnerUri={RemoteUri} " +
                   $"PartnerSocket={RemoteEndpoint.Hostname}:{RemoteEndpoint.Port.ToString()}]";
        }
    }
}