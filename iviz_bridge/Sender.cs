using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Bridge
{
    abstract class Sender
    {
        protected TcpListener listener;
        protected string topic;
        protected TypeInfo type;
        protected bool keepRunning;
        protected RosClient client;
        protected RosPublisher publisher;
        Task task;

        public int Port { get; private set; }

        public abstract bool IsAlive { get; }

        public void Start(RosClient client, string topic, TypeInfo type)
        {
            Console.WriteLine("++ Starting Sender " + topic);

            keepRunning = true;
            this.client = client;
            this.topic = topic;
            this.type = type;
            listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            Port = ((IPEndPoint)listener.LocalEndpoint).Port;
            task = Task.Run(Run);
        }

        void Run()
        {
            try
            {
                while (keepRunning)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Process(client);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        public virtual void Stop()
        {
            keepRunning = false;
            listener?.Stop();
            listener = null;
            task.Wait();
        }

        protected abstract void Process(TcpClient client);

        public abstract void Cleanup();

        public static Sender Instantiate(Type msgType)
        {
            Type listenerType = typeof(SenderImpl<>).MakeGenericType(msgType);
            return (Sender)Activator.CreateInstance(listenerType);
        }
    }

    class SenderImpl<T> : Sender where T : IMessage, new()
    {
        readonly List<SenderSocket<T>> publishers = new List<SenderSocket<T>>();

        DateTime emptyTime = DateTime.Now;
        public override bool IsAlive => publishers.Count != 0 || (DateTime.Now - emptyTime).TotalSeconds < 5;

        protected override void Process(TcpClient tcpClient)
        {
            SenderSocket<T> socket;
            if (publisher == null)
            {
                string tmpId = client.Advertise<T>(topic, out publisher);
                socket = new SenderSocket<T>(publisher, tcpClient);
                client.Unadvertise(tmpId);
            }
            else
            {
                socket = new SenderSocket<T>(publisher, tcpClient);
            }
            lock (publishers)
            {
                publishers.Add(socket);
            }
        }

        public override void Cleanup()
        {
            //publisher?.Cleanup();
            lock (publishers)
            {
                var deadSockets = publishers.Where(x => !x.IsAlive);
                if (deadSockets.Any())
                {
                    var deadSocketsArray = deadSockets.ToArray();
                    foreach (var socket in deadSockets)
                    {
                        socket.Stop();
                        publishers.Remove(socket);
                    }
                    if (publishers.Count == 0)
                    {
                        emptyTime = DateTime.Now;
                    }
                }
            }
        }

        public override void Stop()
        {
            base.Stop();
            lock (publishers)
            {
                foreach (var socket in publishers)
                {
                    socket.Stop();
                    publishers.Remove(socket);
                }
                publishers.Clear();
            }
            Console.WriteLine("-- Stopping Sender " + topic);
        }
    }

    class SenderSocket<T> where T : IMessage, new()
    {
        readonly RosPublisher publisher;
        readonly string id;
        readonly TcpClient client;
        readonly BinaryReader reader;
        byte[] buffer = new byte[0];
        readonly T generator;

        bool keepRunning;
        readonly Task task;

        public bool IsAlive => client.Connected;

        public SenderSocket(RosPublisher publisher, TcpClient client)
        {
            //Console.WriteLine("-- Starting SenderSocket " + id);

            keepRunning = true;
            this.publisher = publisher;
            id = publisher.Advertise();
            this.client = client;
            reader = new BinaryReader(client.GetStream());
            task = Task.Run(Run);
            generator = new T();
        }

        public void Stop()
        {
            keepRunning = false;
            publisher.Unadvertise(id);
            client.Close();
            reader.Close();
            task.Wait();
            //Console.WriteLine("-- Stopping SenderSocket " + id);
        }

        void Run()
        {
            try
            {
                while (keepRunning)
                {
                    int length;
                    length = reader.ReadInt32();
                    if (length > buffer.Length)
                    {
                        buffer = new byte[length + length / 10];
                    }
                    reader.Read(buffer, 0, length);

                    IMessage msg = Msgs.Buffer.Deserialize(generator, buffer, length);
                    publisher.Publish(msg);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
