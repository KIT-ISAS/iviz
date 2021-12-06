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
using Buffer = System.Buffer;

namespace Iviz.Roslib
{
    internal sealed class UdpReceiver<T> : IProtocolReceiver, ILoopbackReceiver<T>, IUdpReceiver where T : IMessage
    {
        const int DisposeTimeoutInMs = 2000;

        readonly TopicInfo<T> topicInfo;
        readonly CancellationTokenSource runningTs = new();
        readonly Task task;
        readonly ReceiverManager<T> manager;

        int receiveBufferSize = 8192;

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
        public int MaxPacketSize { get; }
        public UdpClient UdpClient { get; }
        public string? RemoteId { get; }

        public UdpReceiver(ReceiverManager<T> manager, RpcUdpTopicResponse response, UdpClient client, Uri remoteUri,
            TopicInfo<T> topicInfo)
        {
            this.manager = manager;

            var (remoteHostname, remotePort, _, newMaxPacketSize, header) = response;

            RemoteEndpoint = new Endpoint(remoteHostname, remotePort);
            RemoteUri = remoteUri;
            UdpClient = client;
            this.topicInfo = topicInfo;
            MaxPacketSize = newMaxPacketSize;
            task = Task.CompletedTask;

            client.Client.ReceiveBufferSize = 32768 * 32;

            var udpEndpoint = (IPEndPoint?)client.Client.LocalEndPoint;
            if (udpEndpoint == null)
            {
                ErrorDescription = new ErrorMessage("Failed to bind to local socket");
                Status = ReceiverStatus.Dead;
                client.Dispose();
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
                ErrorDescription = new ErrorMessage(e);
                Status = ReceiverStatus.Dead;
                client.Dispose();
                return;
            }

            RosHeader = responseHeader.ToArray();

            var dictionary = RosUtils.CreateHeaderDictionary(responseHeader);
            if (dictionary.TryGetValue("callerid", out string? remoteCallerId))
            {
                RemoteId = remoteCallerId;
            }

            if (dictionary.TryGetValue("error", out string? message)) // TODO: improve error handling here
            {
                ErrorDescription = new ErrorMessage($"Partner sent error message: [{message}]");
                Status = ReceiverStatus.Dead;
                client.Dispose();
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
                    ErrorDescription = new ErrorMessage(e);
                    Status = ReceiverStatus.Dead;
                    client.Dispose();
                    return;
                }
            }

            task = TaskUtils.StartLongTask(async () => await StartSession().AwaitNoThrow(this));
        }

        public SubscriberReceiverState State => new UdpReceiverState(RemoteUri)
        {
            RemoteId = RemoteId,
            Status = Status,
            EndPoint = Endpoint,
            RemoteEndpoint = RemoteEndpoint,
            NumReceived = numReceived,
            BytesReceived = bytesReceived,
            NumDropped = numDropped,
            ErrorDescription = ErrorDescription,
            MaxPacketSize = MaxPacketSize
        };

        public async ValueTask DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            runningTs.Cancel();
            UdpClient.Dispose();

            await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
            runningTs.Dispose();
        }

        async ValueTask StartSession()
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
                        ErrorDescription = new ErrorMessage(e);
                        Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                    case RoslibException:
                        ErrorDescription = new ErrorMessage(e);
                        Logger.LogErrorFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                    default:
                        Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                }
            }

            Status = ReceiverStatus.Dead;
            UdpClient.Dispose();

            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            Logger.LogDebugFormat("{0}: Stopped!", this);
        }

        async ValueTask ProcessLoop()
        {
            var generator = topicInfo.Generator ?? throw new InvalidOperationException("Invalid generator!");

            Status = ReceiverStatus.Running;
            using var readBuffer = new Rent<byte>(MaxPacketSize);
            using var resizableBuffer = new ResizableRent();

            int expectedBlockNr = 0;
            int expectedMsgId = 0;
            int totalBlocks = 0;
            int offset = 0;

            while (KeepRunning)
            {
                int received = await UdpClient.ReadChunkAsync(readBuffer.Array, runningTs.Token);
                if (received > MaxPacketSize)
                {
                    throw new RosConnectionException("Udp socket received " + received +
                                                     " bytes, but the agreed maximum was " + MaxPacketSize +
                                                     ". Dropping connection.");
                }

                byte opCode = readBuffer[4];
                if (opCode == UdpRosParams.OpCodePing)
                {
                    continue;
                }

                numReceived++;
                bytesReceived += received;

                int msgId = readBuffer[5];
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
                                "{0}: Partner started new UDPROS packet, but I was expecting {1}/{2} (id {3})." +
                                " Dropping old packet.", this, expectedBlockNr, totalBlocks, expectedMsgId);
                            MarkDropped();
                        }

                        ProcessMessage(generator, readBuffer[UdpRosParams.HeaderLength..],
                            received - UdpRosParams.HeaderLength);
                        continue;
                    case UdpRosParams.OpCodeData0:
                        if (totalBlocks != 0)
                        {
                            Logger.LogDebugFormat(
                                "{0}: Partner started new UDPROS packet, but I was expecting {1}/{2} (id {3})." +
                                " Dropping packet.", this, expectedBlockNr, totalBlocks, expectedMsgId);
                            MarkDropped();
                        }

                        totalBlocks = blockNr;
                        expectedMsgId = msgId;
                        resizableBuffer.EnsureCapacity(MaxPacketSize * totalBlocks);

                        blockNr = 0;
                        goto case UdpRosParams.OpCodeDataN;
                    case UdpRosParams.OpCodeDataN when totalBlocks == 0:
                        continue; // error previously marked, skip this
                    case UdpRosParams.OpCodeDataN:
                        if (blockNr == expectedBlockNr && msgId == expectedMsgId)
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
                            ProcessMessage(generator, resizableBuffer, offset);
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
                    expectedMsgId = 0;
                    offset = 0;
                }
            }
        }

        void ProcessMessage(IDeserializable<T> generator, Span<byte> array, int rcvLength)
        {
            if (IsPaused)
            {
                return;
            }

            int msgLength = BitConverter.ToInt32(array);
            if (4 + msgLength > rcvLength)
            {
                // incomplete packet
                numDropped++;
                return;
            }

            var message = generator.DeserializeFrom(array[4..]);
            manager.MessageCallback(message, this);

            CheckBufferSize(rcvLength);
        }

        void CheckBufferSize(int rcvLength)
        {
            if (receiveBufferSize >= rcvLength)
            {
                return;
            }

            int recommendedSize = RosUtils.GetRecommendedBufferSize(rcvLength, receiveBufferSize);
            if (recommendedSize == receiveBufferSize)
            {
                return;
            }

            receiveBufferSize = recommendedSize;
            UdpClient.Client.ReceiveBufferSize = recommendedSize;
            Logger.LogDebugFormat("{0}: Large message received. Changing buffer size to {1} kB.", this,
                recommendedSize / 1024);
        }

        public static RpcUdpTopicRequest CreateRequest(string ownHostname, string remoteHostname,
            TopicInfo<T> topicInfo)
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

            int maxPacketSize = RosUtils.TryGetMaxPacketSizeForAddress(remoteHostname) ??
                                (UdpRosParams.DefaultMTU - UdpRosParams.Ip4UdpHeadersLength);
            return new RpcUdpTopicRequest(header, ownHostname, maxPacketSize);
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
            return $"[UdpReceiver for '{topicInfo.Topic}' :{Endpoint.Port.ToString()} PartnerUri={RemoteUri} " +
                   $"PartnerSocket={RemoteEndpoint.Hostname}:{RemoteEndpoint.Port.ToString()}]";
        }
    }
}