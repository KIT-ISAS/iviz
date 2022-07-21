using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib.XmlRpc;
using Iviz.Tools;
using Iviz.XmlRpc;

namespace Iviz.Roslib;

/// <summary>
/// Class in charge of trying to connect and reconnect to a ROS publisher 
/// </summary>
internal class ReceiverConnector
{
    const int MaxConnectionRetries = 120;
    const int DisposeTimeoutInMs = 2000;

    readonly RosNodeClient client;
    readonly string topic;
    readonly CancellationTokenSource tokenSource = new();
    readonly Task task;
    readonly Action<ReceiverConnector, ReceiverConnectorResponse> onConnectionSucceeded;
    readonly RpcUdpTopicRequest? udpRequest;
    readonly UdpClient? udpClient;
    readonly RosTransportHint transportHint;

    ReceiverStatus status;
    bool disposed;

    CancellationToken Token => tokenSource.Token;
    public Uri RemoteUri => client.Uri;
    public ErrorMessage? ErrorDescription { get; private set; }
    public bool IsAlive => !task.IsCompleted;

    public ReceiverConnector(RosNodeClient client, string topic, RosTransportHint transportHint,
        RpcUdpTopicRequest? udpRequest, Action<ReceiverConnector, ReceiverConnectorResponse> onConnectionSucceeded)
    {
        this.topic = topic;
        this.client = client;
        this.onConnectionSucceeded = onConnectionSucceeded;
        this.transportHint = transportHint;

        if (udpRequest != null)
        {
            udpClient = new UdpClient(AddressFamily.InterNetworkV6) { Client = { DualMode = true } };
            udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, 0));

            int port = ((IPEndPoint)udpClient.Client.LocalEndPoint!).Port;
            this.udpRequest = udpRequest.WithPort(port);
        }

        status = ReceiverStatus.ConnectingRpc;
        task = TaskUtils.Run(async () => await KeepReconnecting().AwaitNoThrow(this));
    }

    async ValueTask KeepReconnecting()
    {
        int numTry;
        for (numTry = 0; numTry < MaxConnectionRetries; numTry++)
        {
            Logger.LogDebugFormat("{0}: Attempting connection...", this);
            var response = await RequestTopic();

            if (response != null)
            {
                if (!response.IsValid)
                {
                    ErrorDescription = new ErrorMessage(response.StatusMessage);
                    break;
                }

                if (response.IsFailure)
                {
                    ErrorDescription = new ErrorMessage(response.StatusMessage);
                    break;
                }

                if (response.TcpResponse is { Port: <= 0 or > ushort.MaxValue })
                {
                    ErrorDescription = new ErrorMessage(
                        $"Received invalid TCP port {response.TcpResponse.Value.Port.ToString()}");
                    break;
                }

                if (response.UdpResponse is { Port: <= 0 or > ushort.MaxValue })
                {
                    ErrorDescription = new ErrorMessage(
                        $"Received invalid UDP port {response.UdpResponse.Port.ToString()}");
                    break;
                }

                if (response.UdpResponse is { MaxPacketSize: <= UdpRosParams.HeaderLength or > ushort.MaxValue })
                {
                    ErrorDescription = new ErrorMessage(
                        $"Received invalid max packet size {response.UdpResponse.MaxPacketSize.ToString()}");
                    break;
                }

                ErrorDescription = null;
                status = ReceiverStatus.Connected;
                Logger.LogDebugFormat("{0}: Connection succeeded.", this);
                onConnectionSucceeded(this,
                    new ReceiverConnectorResponse(response.TcpResponse, response.UdpResponse, udpClient));
                return;
            }

            Logger.LogDebugFormat("{0}: Connection failed! Reason: {1}", this, ErrorDescription);

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

        if (numTry == MaxConnectionRetries)
        {
            status = ReceiverStatus.OutOfRetries;
            ErrorDescription = new ErrorMessage($"Giving up after {MaxConnectionRetries.ToString()} tries");
        }
        else
        {
            status = ReceiverStatus.Dead;
        }

        Logger.LogDebugFormat("{0}: Removing connection - {1}", this, ErrorDescription);
        udpClient?.Dispose();
    }

    async ValueTask<RequestTopicResponse?> RequestTopic()
    {
        try
        {
            return await client.RequestTopicAsync(topic, transportHint, udpRequest, Token);
        }
        catch (Exception e)
        {
            ErrorDescription = e switch
            {
                OperationCanceledException => null,
                ObjectDisposedException => null,
                XmlRpcException => new ErrorMessage(e.InnerException ?? e),
                _ => new ErrorMessage(e)
            };
        }

        return null;
    }

    public async ValueTask DisposeAsync(CancellationToken token)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        tokenSource.Cancel();
        await task.AwaitNoThrow(DisposeTimeoutInMs, this, token);
        Logger.LogDebugFormat("{0}: Disposing!", this);
    }

    public Ros1ReceiverState State => new UninitializedReceiverState(RemoteUri)
    {
        Status = status,
        ErrorDescription = ErrorDescription
    };

    public override string ToString() => $"[{nameof(ReceiverConnector)} '{topic}' Uri='{RemoteUri}']";

    static int WaitTimeInMsFromTry(int numTry) => BaseUtils.Random.Next(0, 1000) +
                                                  numTry switch
                                                  {
                                                      < 5 => 3000,
                                                      < 50 => 5000,
                                                      _ => 10000
                                                  };
}

public sealed class ReceiverConnectorResponse
{
    public Endpoint? TcpResponse { get; }
    public RpcUdpTopicResponse? UdpResponse { get; }
    public UdpClient? UdpClient { get; }

    public ReceiverConnectorResponse(Endpoint? tcpResponse, RpcUdpTopicResponse? udpResponse, UdpClient? udpClient) =>
        (TcpResponse, UdpResponse, UdpClient) = (tcpResponse, udpResponse, udpClient);

    public void Deconstruct(out Endpoint? tcpResponse, out RpcUdpTopicResponse? udpResponse,
        out UdpClient? udpClient) =>
        (tcpResponse, udpResponse, udpClient) = (TcpResponse, UdpResponse, UdpClient);
}