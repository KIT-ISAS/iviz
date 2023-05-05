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
            // When calling `AcceptWebSocketAsync` the negotiated subprotocol must be specified.
            // This sample assumes that no subprotocol was requested. 
            webSocketContext = await listenerContext.AcceptWebSocketAsync(subProtocol: null);
        }
        catch
        {
            // The upgrade process failed somehow.
            // For simplicity lets assume it was a failure on the part of the server and indicate this using 500.
            listenerContext.Response.StatusCode = 500;
            listenerContext.Response.Close();
            return;
        }

        using var webSocket = webSocketContext.WebSocket;
        using var receiveBuffer = new ResizableRent(2048);
        await using var context = new SocketConnection(client, webSocket, endpoint);

        try
        {
            //### Receiving
            // Define a receive buffer to hold data received on the WebSocket connection. The buffer will be reused as we only need to hold on to the data
            // long enough to send it back to the sender.

            // While the WebSocket connection remains open run a simple loop that receives data and sends it back.
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
            // Just log any exceptions to the console.
            // Pretty much any exception that occurs when calling `SendAsync`/`ReceiveAsync`/`CloseAsync` is
            // unrecoverable in that it will abort the connection and leave the `WebSocket` instance in an unusable state.
            Console.WriteLine("Exception: {0}", e);
        }
    }
}