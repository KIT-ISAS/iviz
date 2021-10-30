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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Roslib
{
    internal sealed class TcpSender<T> : IProtocolSender<T>, IRosSenderInfo where T : IMessage
    {
        readonly SenderQueue<T> senderQueue;
        readonly CancellationTokenSource runningTs = new();
        readonly TopicInfo<T> topicInfo;
        readonly Task task;
        readonly byte[] lengthBuffer = new byte[4];
        readonly TcpClient tcpClient;

        long bytesDropped;
        long bytesSent;
        bool disposed;

        long numDropped;
        long numSent;

        public IReadOnlyCollection<string> RosHeader { get; private set; } = Array.Empty<string>();
        public string? RemoteCallerId { get; private set; }
        public Endpoint Endpoint { get; }
        public Endpoint RemoteEndpoint { get; }
        public TransportType TransportType => TransportType.Tcp;
        public ILoopbackReceiver<T>? LoopbackReceiver { private get; set; }

        bool KeepRunning => !runningTs.IsCancellationRequested;

        public bool TcpNoDelay
        {
            get => tcpClient.NoDelay;
            set => tcpClient.NoDelay = value;
        }

        public string Topic => topicInfo.Topic;
        public string Type => topicInfo.Type;
        bool IsRunning => !task.IsCompleted;
        public bool IsAlive => IsRunning && tcpClient.Client.CheckIfAlive();

        public int MaxQueueSizeInBytes
        {
            get => senderQueue.MaxQueueSizeInBytes;
            set => senderQueue.MaxQueueSizeInBytes = value;
        }

        public PublisherSenderState State =>
            new()
            {
                IsAlive = IsAlive,
                Endpoint = Endpoint,
                RemoteId = RemoteCallerId ?? "",
                RemoteEndpoint = RemoteEndpoint,
                CurrentQueueSize = senderQueue.Count,
                MaxQueueSize = MaxQueueSizeInBytes,
                NumSent = numSent,
                BytesSent = bytesSent,
                NumDropped = numDropped,
                BytesDropped = bytesDropped
            };

        public TcpSender(TcpClient client, TopicInfo<T> topicInfo, NullableMessage<T> latchedMsg)
        {
            this.topicInfo = topicInfo;
            senderQueue = new SenderQueue<T>(this);
            tcpClient = client;
            Endpoint = new Endpoint((IPEndPoint)tcpClient.Client.LocalEndPoint!);
            RemoteEndpoint = new Endpoint((IPEndPoint)tcpClient.Client.RemoteEndPoint!);
            task = TaskUtils.StartLongTask(() => StartSession(latchedMsg), runningTs.Token);
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            runningTs.Cancel();
            tcpClient.Dispose();

            await task.AwaitNoThrow(5000, this, token);
            runningTs.Dispose();
        }

        async ValueTask<Rent<byte>> ReceivePacket()
        {
            if (!await tcpClient.ReadChunkAsync(lengthBuffer, 4, runningTs.Token))
            {
                throw new IOException("Connection closed during handshake");
            }

            int length = BitConverter.ToInt32(lengthBuffer, 0);
            if (length == 0)
            {
                return Rent.Empty<byte>();
            }

            var readBuffer = new Rent<byte>(length);
            try
            {
                if (await tcpClient.ReadChunkAsync(readBuffer.Array, length, runningTs.Token))
                {
                    return readBuffer;
                }
            }
            catch (Exception)
            {
                readBuffer.Dispose();
                throw;
            }

            readBuffer.Dispose();
            throw new IOException("Connection closed during handshake");
        }

        Task SendHeader(bool latching)
        {
            string[] contents =
            {
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"callerid={topicInfo.CallerId}",
                latching ? "latching=1" : "latching=0",
                $"message_definition={topicInfo.MessageDependencies}",
            };

            RosHeader = contents;
            return tcpClient.WriteHeaderAsync(contents, runningTs.Token);
        }

        Task SendErrorHeader(string errorMessage)
        {
            string[] contents =
            {
                errorMessage,
                $"md5sum={topicInfo.Md5Sum}",
                $"type={topicInfo.Type}",
                $"callerid={topicInfo.CallerId}"
            };
            return tcpClient.WriteHeaderAsync(contents, runningTs.Token);
        }

        bool ProcessRemoteHeader(List<string> fields, out string errorMessage)
        {
            if (fields.Count < 5)
            {
                errorMessage = "error=Expected at least 5 fields. Closing connection";
                return false;
            }

            var values = RosUtils.CreateHeaderDictionary(fields);
            if (values.TryGetValue("callerid", out string? receivedId))
            {
                RemoteCallerId = receivedId;
            }
            else
            {
                errorMessage = "error=Expected entry 'callerid'";
                return false;
            }

            if (!values.TryGetValue("topic", out string? receivedTopic) || receivedTopic != topicInfo.Topic)
            {
                errorMessage =
                    $"error=Expected topic '{topicInfo.Topic}' but received request for '{receivedTopic}'";
                return false;
            }

            if (!values.TryGetValue("type", out string? receivedType) || receivedType != topicInfo.Type)
            {
                if (receivedType != DynamicMessage.RosMessageType) // "*"
                {
                    errorMessage =
                        $"error=Expected message type [{topicInfo.Type}] but received [{receivedType}]";
                    return false;
                }
            }

            if (!values.TryGetValue("md5sum", out string? receivedMd5Sum) || receivedMd5Sum != topicInfo.Md5Sum)
            {
                if (receivedMd5Sum != DynamicMessage.RosMd5Sum) // "*"
                {
                    errorMessage =
                        $"error=Expected md5 '{topicInfo.Md5Sum}' but received '{receivedMd5Sum}'";
                    return false;
                }
            }

            if (values.TryGetValue("tcp_nodelay", out string? receivedNoDelay) && receivedNoDelay == "1")
            {
                TcpNoDelay = true;
            }

            errorMessage = "";
            return true;
        }

        async Task ProcessHandshake(bool latching)
        {
            List<string> fields;
            using (Rent<byte> readBuffer = await ReceivePacket())
            {
                fields = RosUtils.ParseHeader(readBuffer);
            }

            bool success = ProcessRemoteHeader(fields, out string errorMessage);
            if (success)
            {
                await SendHeader(latching);
            }
            else
            {
                await SendErrorHeader(errorMessage);
                throw new RosHandshakeException(
                    $"Failed to parse header sent by partner. Error message: [{errorMessage}]");
            }
        }

        async Task StartSession(NullableMessage<T> latchedMsg)
        {
            Logger.LogDebugFormat("{0}: Started!", this);

            try
            {
                await ProcessLoop(latchedMsg);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case OperationCanceledException:
                    case ObjectDisposedException:
                        break;
                    case IOException:
                    case TimeoutException:
                    case SocketException:
                        Logger.LogDebugFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                    default:
                        Logger.LogFormat(BaseUtils.GenericExceptionFormat, this, e);
                        break;
                }
            }

            tcpClient.Dispose();
            try
            {
                runningTs.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }

            senderQueue.FlushRemaining();
        }

        async Task ProcessLoop(NullableMessage<T> latchedMsg)
        {
            using ByteBufferRent writeBuffer = new(4);

            await ProcessHandshake(latchedMsg.HasValue);

            if (latchedMsg.HasValue)
            {
                Publish(latchedMsg.Value);
            }

            while (KeepRunning)
            {
                await senderQueue.WaitAsync(runningTs.Token);

                var queue = senderQueue.ReadAll(ref numDropped, ref bytesDropped);

                if (LoopbackReceiver != null)
                {
                    senderQueue.SendToLoopback(queue, LoopbackReceiver, ref numSent, ref bytesSent);
                }
                else
                {
                    await SendWithSocketAsync(queue, writeBuffer);
                }
            }
        }

        async Task SendWithSocketAsync(RangeEnumerable<SenderQueue<T>.Entry?> queue, ByteBufferRent writeBuffer)
        {
            void WriteLengthToBuffer(int i)
            {
                byte[] array = writeBuffer.Array;
                array[3] = (byte)(i >> 0x18);
                array[0] = (byte)i;
                array[1] = (byte)(i >> 8);
                array[2] = (byte)(i >> 0x10);
            }

            try
            {
                foreach (var entry in queue)
                {
                    if (entry is not var (msg, msgLength, msgSignal))
                    {
                        continue;
                    }

                    writeBuffer.EnsureCapability(msgLength + 4);

                    msg.SerializeToArray(writeBuffer.Array, 4);
                    WriteLengthToBuffer(msgLength);
                    await tcpClient.WriteChunkAsync(writeBuffer.Array, msgLength + 4, runningTs.Token);

                    numSent++;
                    bytesSent += msgLength + UdpRosParams.HeaderLength + 4;
                    msgSignal?.TrySetResult(null);
                }
            }
            catch (Exception e)
            {
                senderQueue.FlushFrom(queue, e);
                throw;
            }
        }

        public void Publish(in T message)
        {
            if (!IsRunning)
            {
                numDropped++;
                return;
            }

            senderQueue.Enqueue(message);
        }

        public Task PublishAndWaitAsync(in T message, CancellationToken token)
        {
            if (IsRunning)
            {
                return senderQueue.EnqueueAsync(message, token);
            }
            
            numDropped++;
            return Task.FromException(new InvalidOperationException("Sender has been disposed."));

        }

        public override string ToString() => $"[TcpSender '{Topic}' :{Endpoint.Port.ToString()} >>'{RemoteCallerId}']";
    }
}