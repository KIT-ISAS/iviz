using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib.Utils;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib
{
    internal class ReceiverConnector
    {
        const int MaxConnectionRetries = 120;
        const int DisposeTimeoutInMs = 2000;

        readonly RosNodeClient client;
        readonly string topic;
        readonly CancellationTokenSource tokenSource = new();
        readonly Task task;

        readonly Action<ReceiverConnector, Response> onConnectionSucceeded;

        readonly RpcUdpTopicRequest? udpRequest;
        readonly UdpClient? udpClient;
        readonly RosTransportHint transportHint;

        ReceiverStatus status;
        string? errorDescription;

        CancellationToken Token => tokenSource.Token;
        public Uri RemoteUri => client.Uri;
        public bool IsAlive => !task.IsCompleted;

        public sealed class Response
        {
            public Endpoint? TcpResponse { get; }
            public RpcUdpTopicResponse? UdpResponse { get; }
            public UdpClient? UdpClient { get; }

            public Response(Endpoint? tcpResponse, RpcUdpTopicResponse? udpResponse, UdpClient? udpClient) =>
                (TcpResponse, UdpResponse, UdpClient) = (tcpResponse, udpResponse, udpClient);

            public void Deconstruct(out Endpoint? tcpResponse, out RpcUdpTopicResponse? udpResponse,
                out UdpClient? udpClient) =>
                (tcpResponse, udpResponse, udpClient) = (TcpResponse, UdpResponse, UdpClient);
        }

        public ReceiverConnector(RosNodeClient client, string topic, RosTransportHint transportHint,
            RpcUdpTopicRequest? udpRequest, Action<ReceiverConnector, Response> onConnectionSucceeded)
        {
            this.topic = topic;
            this.client = client;
            this.onConnectionSucceeded = onConnectionSucceeded;
            this.transportHint = transportHint;
            status = ReceiverStatus.ConnectingRpc;

            if (udpRequest != null)
            {
                udpClient = new UdpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
                udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, 0));

                int port = ((IPEndPoint)udpClient.Client.LocalEndPoint!).Port;
                this.udpRequest = udpRequest.WithPort(port);
            }

            task = Task.Run(KeepReconnecting);
        }

        async Task KeepReconnecting()
        {
            for (int numTry = 0; numTry < MaxConnectionRetries; numTry++)
            {
                var response = await RequestTopic();
                if (response != null)
                {
                    if (!response.IsValid)
                    {
                        errorDescription = response.StatusMessage;
                        break;
                    }

                    if (response.IsFailure)
                    {
                        errorDescription = response.StatusMessage;
                        break;
                    }

                    if (response.TcpResponse is { Port: <= 0 or > ushort.MaxValue })
                    {
                        errorDescription = $"Received invalid TCP port {response.TcpResponse.Value.Port.ToString()}";
                        break;
                    }

                    if (response.UdpResponse is { Port: <= 0 or > ushort.MaxValue })
                    {
                        errorDescription = $"Received invalid UDP port {response.UdpResponse.Port.ToString()}";
                        break;
                    }

                    if (response.UdpResponse is { MaxPacketSize: <= 12 or > ushort.MaxValue })
                    {
                        errorDescription =
                            $"Received invalid max packet size {response.UdpResponse.MaxPacketSize.ToString()}";
                        break;
                    }

                    errorDescription = "";
                    status = ReceiverStatus.Connected;
                    onConnectionSucceeded(this, new Response(response.TcpResponse, response.UdpResponse, udpClient));
                    return;
                }

                try
                {
                    await Task.Delay(WaitTimeInMsFromTry(numTry), Token);
                }
                catch (OperationCanceledException)
                {
                    status = ReceiverStatus.Canceled;
                    udpClient?.Dispose();
                    return;
                }
            }

            if (errorDescription == null)
            {
                status = ReceiverStatus.OutOfRetries;
                errorDescription = $"Giving up after {MaxConnectionRetries.ToString()} tries";
            }

            Logger.Log($"{this}: Removing connection with '{RemoteUri}' - {errorDescription}");
            udpClient?.Dispose();
            status = ReceiverStatus.Dead;
        }

        async ValueTask<RequestTopicResponse?> RequestTopic()
        {
            try
            {
                return await client.RequestTopicAsync(topic, transportHint, udpRequest, Token);
            }
            catch (Exception e) when (e is OperationCanceledException)
            {
            }
            catch (Exception e) when (e is TimeoutException or XmlRpcException or SocketException or IOException)
            {
                Logger.LogDebugFormat("{0}: Connection request to publisher {1} failed: {2}", this, RemoteUri, e);
                errorDescription = e.Message;
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Connection request to publisher {1} failed: {2}", this, RemoteUri, e);
                errorDescription = e.Message;
            }

            return null;
        }

        public async Task DisposeAsync(CancellationToken token)
        {
            tokenSource.Cancel();
            await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
            tokenSource.Dispose();
        }

        public SubscriberReceiverState State => new(RemoteUri)
        {
            Status = status,
            TransportType = null,
            ErrorDescription = errorDescription
        };

        public override string ToString() => $"[ReceiverConnector Uri='{RemoteUri}']";

        static int WaitTimeInMsFromTry(int numTry) => BaseUtils.Random.Next(0, 1000) +
                                                      numTry switch
                                                      {
                                                          < 5 => 3000,
                                                          < 50 => 5000,
                                                          _ => 10000
                                                      };
    }
}