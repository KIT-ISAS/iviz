using System;
using System.Collections.Generic;
using System.Threading;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
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

        public RosBridge(RosClient rosClient, int port)
        {
            SocketConnection.client = this;
            RosClient = rosClient;

            Logger.Log("** Starting with port " + port);

            server = new WebSocketServer(port);
            //server.Log.Output = (_, __) => { };
            server.AddWebSocketService<SocketConnection>("/");
            server.Start();

            Logger.Log("** Started!");
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

        public void Close()
        {
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