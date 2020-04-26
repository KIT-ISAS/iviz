using Iviz.Msgs;
using Iviz.Msgs.rosgraph_msgs;
using Iviz.RoslibSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Iviz.App
{
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
    }

    public interface IRosConnection
    {
        event Action<ConnectionState> ConnectionStateChanged;

        ConnectionState ConnectionState { get; }
        Uri Uri { get; }
        string Id { get; }
        BriefTopicInfo[] PublishedTopics { get; }

        bool TrySetUri(string uri);

        void Subscribe<T>(RosListener<T> listener) where T : IMessage, new();
        void Unsubscribe(RosListener subscriber);

        void Advertise<T>(RosSender<T> advertiser) where T : IMessage;
        void Unadvertise(RosSender advertiser);
        void Publish(RosSender advertiser, IMessage msg);
        void Stop();


        BriefTopicInfo[] GetSystemPublishedTopics();
    }


    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance { get; private set; }
        public static IRosConnection Connection { get; private set; }
        RosSender<Log> sender;

        [Flags]
        public enum LogLevel
        {
            Debug = Log.DEBUG,
            Info = Log.INFO,
            Warn = Log.WARN,
            Error = Log.ERROR,
            Fatal = Log.FATAL,
        }

        void Awake()
        {
            Instance = this;
            Connection = new RoslibConnection();

            sender = new RosSender<Log>("/rosout");
        }

        void OnDestroy()
        {
            Connection.Stop();
            Connection = null;
        }

        public void LogMessage(LogLevel logLevel, string message)
        {
            sender.Publish(new Log()
            {
                header = Utils.CreateHeader(),
                level = (byte)logLevel,
                name = Connection.Id,
                msg = message,
            });
        }


        public static ConnectionState ConnectionState => Connection?.ConnectionState ?? ConnectionState.Disconnected;
        public static bool Connected => ConnectionState == ConnectionState.Connected;

        public static void Subscribe<T>(RosListener<T> listener) where T : IMessage, new()
            => Connection.Subscribe(listener);
        public static void Unsubscribe(RosListener subscriber) => Connection.Unsubscribe(subscriber);

        public static void Advertise<T>(RosSender<T> advertiser) where T : IMessage
            => Connection.Advertise(advertiser);
        public static void Unadvertise(RosSender advertiser) => Connection.Unadvertise(advertiser);
        public static void Publish(RosSender advertiser, IMessage msg) => Connection.Publish(advertiser, msg);

        public static BriefTopicInfo[] GetSystemPublishedTopics() => Connection.GetSystemPublishedTopics();
    }

    public abstract class RosConnection : IRosConnection
    {
        static readonly TimeSpan TaskWaitTime = TimeSpan.FromMilliseconds(2000);

        readonly Queue<Action> ToDos = new Queue<Action>();
        readonly object condVar = new object();
        readonly Task task;

        protected readonly Dictionary<string, HashSet<RosSender>> senders = new Dictionary<string, HashSet<RosSender>>();
        protected readonly Dictionary<string, HashSet<RosListener>> listeners = new Dictionary<string, HashSet<RosListener>>();
        protected volatile bool keepRunning;

        public event Action<ConnectionState> ConnectionStateChanged;

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;
        public virtual Uri Uri { get; protected set; }
        public abstract string Id { get; }
        public BriefTopicInfo[] PublishedTopics { get; protected set; }

        public RosConnection()
        {
            keepRunning = true;
            task = Task.Run(Run);
            GameThread.EverySecond += Update;
        }

        public virtual void Stop()
        {
            keepRunning = false;
            lock (condVar)
            {
                Monitor.Pulse(condVar);
            }
            task?.Wait();
            GameThread.EverySecond -= Update;
        }

        protected void SetConnectionState(ConnectionState newState)
        {
            if (ConnectionState != newState)
            {
                ConnectionState = newState;
                GameThread.RunOnce(() => ConnectionStateChanged?.Invoke(newState));
            }
        }

        public bool TrySetUri(string uristr)
        {
            if (!Uri.TryCreate(uristr, UriKind.Absolute, out Uri uri) ||
                uri.Scheme != "http" || (uri.AbsolutePath != "/"))
            {
                Uri = null;
                return false;
            }
            Uri = uri;
            return true;
        }

        protected void AddTask(Action a)
        {
            lock (condVar)
            {
                ToDos.Enqueue(a);
                Monitor.Pulse(condVar);
            }
        }

        void Run()
        {
            while (keepRunning)
            {
                if (ConnectionState != ConnectionState.Connected)
                {
                    SetConnectionState(ConnectionState.Connecting);

                    if (Connect())
                    {
                        SetConnectionState(ConnectionState.Connected);
                    }
                    else
                    {
                        SetConnectionState(ConnectionState.Disconnected);
                    }
                }

                lock (condVar)
                {
                    Monitor.Wait(condVar, TaskWaitTime);
                }
                ExecuteTasks();
            }

            SetConnectionState(ConnectionState.Disconnected);
        }

        void ExecuteTasks()
        {
            while (true)
            {
                Action action;
                lock (condVar)
                {
                    if (ToDos.Count == 0)
                    {
                        break;
                    }
                    action = ToDos.Dequeue();
                }
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        protected abstract bool Connect();

        protected virtual void Disconnect()
        {
            SetConnectionState(ConnectionState.Disconnected);
        }

        public abstract void Subscribe<T>(RosListener<T> listener) where T : IMessage, new();
        public abstract void Unsubscribe(RosListener subscriber);
        public abstract void Advertise<T>(RosSender<T> advertiser) where T : IMessage;
        public abstract void Unadvertise(RosSender advertiser);
        public abstract void Publish(RosSender advertiser, IMessage msg);
        public abstract BriefTopicInfo[] GetSystemPublishedTopics();

        protected virtual void Update()
        {
            // do nothing
        }
    }
}