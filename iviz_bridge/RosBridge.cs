using System;
using System.Collections.Generic;
using System.Threading;
using Iviz.RoslibSharp;
using Iviz.RoslibSharp.XmlRpc;
using WebSocketSharp.Server;

namespace Iviz.Bridge
{
    class RosBridge
    {
        public RosClient RosClient { get; }
        public TypeDictionary Types { get; } = new TypeDictionary();
        readonly WebSocketServer server;
        readonly HashSet<SocketConnection> connections = new HashSet<SocketConnection>();
        volatile bool keepGoing;

        public RosBridge(RosClient rosClient, string websocketUrl)
        {
            SocketConnection.client = this;
            RosClient = rosClient;
            
            Logger.Log("Bridge: Starting with url '" + websocketUrl + "'");

            server = new WebSocketServer(websocketUrl);
            //server.Log.Output = (_, __) => { };
            server.AddWebSocketService<SocketConnection>("/");
            server.Start();

            Logger.Log("Bridge: Started!");
        }

        void OnShutdownActionCall(string callerId, string reason, out StatusCode status, out string response)
        {
            status = StatusCode.Success;
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
            RosClient.ShutdownAction = OnShutdownActionCall;
            Console.CancelKeyPress += Console_CancelKeyPress;

            keepGoing = true;
            while (keepGoing)
            {
                Thread.Sleep(1000);
                Cleanup();
            }

            server.Stop();
            RosClient.Close();
        }

        void Cleanup()
        {
            lock (connections)
            {
                foreach (SocketConnection connection in connections)
                {
                    connection.Cleanup();
                }
            }
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
