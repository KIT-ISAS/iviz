using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using Iviz.Tools;

namespace Iviz.Bridge;

static class Program
{
    /*
    static async Task Main(string[] args)
    {
        //try
        {
            await Run();
        }
        ////catch (Exception e)
        {
            //Console.WriteLine("EE Fatal error:" + Logger.ExceptionToString(e));
        }
    }
    */
    static async Task Main()
    {
        await Run();
    }

    static async ValueTask Run()
    {
        //const string masterUri = "http://141.3.59.5:11311";
        const Uri? masterUri = null;

        await using var rosClient = await RosClient.CreateAsync(
            masterUri: masterUri,
            ownId: "/iviz_rosbridge");

        Logger.LogCallback = Console.WriteLine;

        Logger.Log($"** Starting with ROS master uri '{rosClient.MasterUri}'");

        string hostname = rosClient.CallerUri.Host;
        const int port = 8080;

        string myWebsocketUrl = $"http://{hostname}:{port}/";

        Logger.Log($"** Starting with websocket uri '{myWebsocketUrl}'");
        await Start(myWebsocketUrl, rosClient);
    }

    static async ValueTask Start(string listenerPrefix, IRosClient client)
    {
        var listener = new HttpListener();
        listener.Prefixes.Add(listenerPrefix);
        listener.Start();

        Logger.Log("** Listening...");

        while (true)
        {
            var listenerContext = await listener.GetContextAsync();
            if (listenerContext.Request.IsWebSocketRequest)
            {
                string endpoint = listenerContext.Request.RemoteEndPoint.ToString();
                _ = ProcessRequest(listenerContext, client, endpoint);
            }
            else
            {
                listenerContext.Response.StatusCode = 400;
                listenerContext.Response.Close();
            }
        }
    }

    static async ValueTask ProcessRequest(HttpListenerContext listenerContext, IRosClient client, string endpoint)
    {
        WebSocketContext? webSocketContext;
        try
        {
            webSocketContext = await listenerContext.AcceptWebSocketAsync(subProtocol: null);
        }
        catch
        {
            listenerContext.Response.StatusCode = 500;
            listenerContext.Response.Close();
            return;
        }

        using var webSocket = webSocketContext.WebSocket;
        using var receiveBuffer = new ResizableRent(2048);
        await using var context = new SocketConnection(client, webSocket, endpoint);

        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                ValueWebSocketReceiveResult receiveResult;

                int offset = 0;
                while (true)
                {
                    receiveResult =
                        await webSocket.ReceiveAsync(receiveBuffer.Array.AsMemory(offset), default);
                    
                    if (receiveResult.EndOfMessage) break;

                    int length = receiveBuffer.Array.Length;
                    offset = length;
                    receiveBuffer.EnsureCapacity(2 * length, true);
                }

                switch (receiveResult.MessageType)
                {
                    case WebSocketMessageType.Close:
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                        break;
                    case WebSocketMessageType.Text:
                        await context.OnMessage(receiveBuffer.Array);
                        break;
                    case WebSocketMessageType.Binary:
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }
    }
}