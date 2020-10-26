using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Roslib;

namespace Iviz.Bridge
{
    abstract class Listener
    {
        protected string topic;
        protected RosClient client;
        TcpListener listener;
        bool keepRunning;
        //Task task;

        public int Port { get; private set; }

        public abstract bool IsAlive { get; }

        public void Start(RosClient client, string topic, TypeInfo type)
        {
            Console.WriteLine("++ Starting Listener " + topic);

            keepRunning = true;
            this.client = client;
            this.topic = topic;
            listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            Port = ((IPEndPoint)listener.LocalEndpoint).Port;
            //task = Task.Run(Run);
            Run();
        }

        async void Run()
        {
            try
            {
                while (keepRunning)
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    Process(tcpClient);
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
            //task.Wait();
        }

        protected abstract void Process(TcpClient client);

        public abstract void Cleanup();

        public static Listener Instantiate(Type msgType)
        {
            Type listenerType = typeof(ListenerImpl<>).MakeGenericType(msgType);
            return (Listener)Activator.CreateInstance(listenerType);
        }
    }

    class ListenerImpl<T> : Listener where T : IMessage, IDeserializable<T>, new()
    {
        RosSubscriber<T> subscriber;
        
        readonly List<ListenerSocket<T>> subscribers = new List<ListenerSocket<T>>();

        DateTime emptyTime = DateTime.Now;
        public override bool IsAlive => subscribers.Count != 0 || (DateTime.Now - emptyTime).TotalSeconds < 5;

        protected override void Process(TcpClient tcpClient)
        {
            ListenerSocket<T> socket;
            if (subscriber == null)
            {
                string tmpId = client.Subscribe(topic, x => { }, out subscriber);
                socket = new ListenerSocket<T>(subscriber, tcpClient);
                client.Unsubscribe(tmpId);
            }
            else
            {
                socket = new ListenerSocket<T>(subscriber, tcpClient);
            }
            lock (subscribers)
            {
                subscribers.Add(socket);
            }
        }

        public override void Cleanup()
        {
            //subscriber?.Cleanup();
            lock (subscribers)
            {
                var deadSockets = subscribers.Where(x => !x.IsAlive).ToArray();
                if (!deadSockets.Any())
                {
                    return;
                }

                foreach (var socket in deadSockets)
                {
                    socket.Stop();
                    subscribers.Remove(socket);
                }
                if (subscribers.Count == 0)
                {
                    emptyTime = DateTime.Now;
                }
            }
        }

        public override void Stop()
        {
            base.Stop();
            lock (subscribers)
            {
                foreach (var socket in subscribers)
                {
                    socket.Stop();
                    subscribers.Remove(socket);
                }
                subscribers.Clear();
            }
            Console.WriteLine("-- Stopping Listener " + topic);
        }
    }

    class ListenerSocket<T> where T : IMessage, new()
    {
        readonly RosSubscriber<T> subscriber;
        readonly string id;
        readonly TcpClient client;
        readonly BinaryWriter writer;
        byte[] buffer = Array.Empty<byte>();

        readonly ParallelQueue<T> queue;

        public bool IsAlive => client.Connected;

        public ListenerSocket(RosSubscriber<T> subscriber, TcpClient client)
        {
            //Console.WriteLine("++ Starting ListenerSocket " + id);

            this.subscriber = subscriber;
            id = subscriber.Subscribe(Callback);
            queue = new ParallelQueue<T>(Process);
            this.client = client;
            writer = new BinaryWriter(client.GetStream());
        }

        public void Stop()
        {
            subscriber.Unsubscribe(id);
            queue.Stop();
            client.Close();
            writer.Close();
            //Console.WriteLine("-- Stopping ListenerSocket " + id);
        }

        void Callback(T msg)
        {
            queue.Enqueue(msg);
        }

        bool Process(T msg)
        {
            int length = msg.RosMessageLength;
            if (length > buffer.Length)
            {
                buffer = new byte[length + length / 10];
            }
            Msgs.Buffer.Serialize(msg, buffer);
            try
            {
                writer.Write(length);
                writer.Write(buffer, 0, length);
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
