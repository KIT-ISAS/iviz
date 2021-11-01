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
        readonly int maxPacketSize;

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

            var (remoteHostname, remotePort, _, newMaxPacketSize, header) = response;

            RemoteEndpoint = new Endpoint(remoteHostname, remotePort);
            RemoteUri = remoteUri;
            this.udpClient = udpClient;
            this.topicInfo = topicInfo;
            maxPacketSize = newMaxPacketSize;

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

            task = TaskUtils.StartLongTask(async () => await StartSession().AwaitNoThrow(this));
        }

        public SubscriberReceiverState State => new UdpReceiverState(RemoteUri)
        {
            Status = Status,
            EndPoint = Endpoint,
            RemoteEndpoint = RemoteEndpoint,
            NumReceived = numReceived,
            BytesReceived = bytesReceived,
            NumDropped = numDropped,
            ErrorDescription = ErrorDescription,
            MaxPacketSize = maxPacketSize
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
            using var resizableBuffer = new ByteBufferRent();

            int expectedBlockNr = 0;
            int totalBlocks = 0;
            int offset = 0;

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

                int blockNr = readBuffer[6] + (readBuffer[7] << 8);
                switch (opCode)
                {
                    case UdpRosParams.OpCodeErr:
                        Logger.LogFormat("{0}: Partner sent UDPROS error code. Disconnecting.", this);
                        return;
                    case UdpRosParams.OpCodePing:
                        continue;
                    case UdpRosParams.OpCodeData0 when blockNr <= 1:
                        if (totalBlocks != 0)
                        {
                            Logger.LogDebugFormat(
                                "{0}: Partner started new UDPROS packet, but I was expecting {1}/{2}." +
                                " Dropping old packet.", this, expectedBlockNr, totalBlocks);
                            MarkDropped();
                        }

                        ProcessMessage(generator, readBuffer.Array, received, UdpRosParams.HeaderLength);
                        continue;
                    case UdpRosParams.OpCodeData0:
                        if (totalBlocks != 0)
                        {
                            Logger.LogDebugFormat(
                                "{0}: Partner started new UDPROS packet, but I was expecting {1}/{2}." +
                                " Dropping packet.", this, expectedBlockNr, totalBlocks);
                            MarkDropped();
                        }

                        totalBlocks = blockNr;
                        resizableBuffer.EnsureCapability(maxPacketSize * totalBlocks);

                        blockNr = 0;
                        goto case UdpRosParams.OpCodeDataN;
                    case UdpRosParams.OpCodeDataN when totalBlocks == 0:
                        continue; // error previously marked, skip this
                    case UdpRosParams.OpCodeDataN:
                        if (blockNr == expectedBlockNr)
                        {
                            int payload = received - UdpRosParams.HeaderLength;
                            Buffer.BlockCopy(readBuffer.Array, UdpRosParams.HeaderLength,
                                resizableBuffer.Array, offset, payload);
                            offset += payload;
                            expectedBlockNr++;
                        }
                        else
                        {
                            Logger.LogDebugFormat(
                                "{0}: Partner sent UDPROS packet {1}/{2}, but I was expecting {3}/{2}." +
                                " Dropping packet.", this, blockNr, totalBlocks, expectedBlockNr);
                            MarkDropped();
                            continue;
                        }

                        if (expectedBlockNr == totalBlocks)
                        {
                            ProcessMessage(generator, resizableBuffer.Array, offset, 0);
                            totalBlocks = 0;
                            expectedBlockNr = 0;
                            offset = 0;
                        }

                        break;
                }

                void MarkDropped()
                {
                    numDropped++;
                    totalBlocks = 0;
                    expectedBlockNr = 0;
                    offset = 0;
                }
            }
        }

        void ProcessMessage(IDeserializable<T> generator, byte[] array, int length, int offset)
        {
            if (IsPaused)
            {
                return;
            }

            int packageSize = BitConverter.ToInt32(array, offset);
            if (offset + 4 + packageSize > length)
            {
                // incomplete packet
                numDropped++;
                return;
            }

            T message = generator.DeserializeFromArray(array, offset: offset + 4);
            manager.MessageCallback(message, this);
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