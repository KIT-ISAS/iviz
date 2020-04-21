using System;
using System.Collections.Generic;
using System.Threading;
using Iviz.RoslibSharp;
using WebSocketSharp.Server;

namespace Iviz.Bridge
{
    class RosBridge
    {
        public readonly RosClient rosClient;
        public readonly TypeDictionary types = new TypeDictionary();
        readonly WebSocketServer server;
        readonly HashSet<SocketConnection> connections = new HashSet<SocketConnection>();
        volatile bool keepGoing;

        public RosBridge(RosClient rosClient, string websocketUrl)
        {
            SocketConnection.client = this;
            this.rosClient = rosClient;
            
            Logger.Log("Bridge: Starting with url '" + websocketUrl + "'");

            server = new WebSocketServer(websocketUrl);
            //server.Log.Output = (_, __) => { };
            server.AddWebSocketService<SocketConnection>("/");
            server.Start();

            Logger.Log("Bridge: Started!");
        }

        void OnShutdownActionCall(string callerId, string reason, out RpcStatusCode status, out string response)
        {
            status = RpcStatusCode.Success;
            response = "ok";
            keepGoing = false;
        }

        void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            keepGoing = false;
            e.Cancel = true;
        }

        public void Run()
        {
            rosClient.ShutdownAction = OnShutdownActionCall;
            Console.CancelKeyPress += Console_CancelKeyPress;

            keepGoing = true;
            while (keepGoing)
            {
                Thread.Sleep(1000);
            }

            server.Stop();
            rosClient.Close();
        }

        public void AddConnection(SocketConnection connection)
        {
            lock (connections)
            {
                connections.Add(connection);
            }
        }

        public void RemoveConnection(SocketConnection connection)
        {
            lock (connections)
            {
                connections.Remove(connection);
            }

        }
    }
}
