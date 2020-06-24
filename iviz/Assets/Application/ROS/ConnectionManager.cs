using Iviz.Msgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.RoslibSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public class ConnectionManager : MonoBehaviour
    {
        public static ConnectionManager Instance { get; private set; }
        public static RosConnection Connection { get; private set; }
        RosSender<Log> sender;

        int collectedUp, collectedDown;

        void Awake()
        {
            Instance = this;
            Connection = new RoslibConnection();
            Logger.Log += LogMessage;

            sender = new RosSender<Log>("/rosout");
        }

        void OnDestroy()
        {
            Connection?.Stop();
            Connection = null;
        }

        uint logSeq = 0;
        void LogMessage(in LogMessage msg)
        {
            if (msg.Level == LogLevel.Debug)
            {
                return;
            }

            sender.Publish(new Log()
            {
                Header = RosUtils.CreateHeader(logSeq++),
                Level = (byte)msg.Level,
                Name = Connection.MyId,
                Msg = (msg.Message is Exception ex) ? ex.Message : msg.Message.ToString(),
                File = msg.File,
                Line = (uint)msg.Line
            });
        }

        public static string MyId => Connection?.MyId;
        public static Uri MyUri => Connection?.MyUri;
        public static Uri MasterUri => Connection?.MasterUri;

        public static ConnectionState ConnectionState => Connection?.ConnectionState ?? ConnectionState.Disconnected;
        public static bool Connected => ConnectionState == ConnectionState.Connected;

        public static void Subscribe<T>(RosListener<T> listener) where T : IMessage, new()
            => Connection.Subscribe(listener);
        public static void Unsubscribe(RosListener subscriber) => Connection.Unsubscribe(subscriber);

        public static void Advertise<T>(RosSender<T> advertiser) where T : IMessage
            => Connection.Advertise(advertiser);
        public static void Unadvertise(RosSender advertiser) => Connection.Unadvertise(advertiser);
        public static void Publish(RosSender advertiser, IMessage msg) => Connection.Publish(advertiser, msg);

        public static void AdvertiseService<T>(string service, Action<T> callback) where T : IService, new()
            => Connection.AdvertiseService(service, callback);

        public static ReadOnlyCollection<BriefTopicInfo>  GetSystemPublishedTopics() => Connection.GetSystemPublishedTopics();

        public static void ReportUp(int size)
        {
            Instance.collectedUp += size;
        }

        public static void ReportDown(int size)
        {
            Instance.collectedDown += size;
        }

        public static (int, int) CollectReported()
        {
            (int, int) result = (Instance.collectedDown, Instance.collectedUp);
            Instance.collectedDown = 0;
            Instance.collectedUp = 0;
            return result;
        }
    }

    public abstract class RosConnection
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

        public virtual Uri MasterUri { get; set; }
        public virtual Uri MyUri { get; set; }
        public virtual string MyId { get; set; }

        public bool KeepReconnecting { get; set; }

        protected static readonly ReadOnlyCollection<BriefTopicInfo> EmptyTopics =
            new ReadOnlyCollection<BriefTopicInfo>(new List<BriefTopicInfo>());

        public ReadOnlyCollection<BriefTopicInfo> PublishedTopics { get; protected set; } = EmptyTopics;

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
            try
            {
                while (keepRunning)
                {
                    if (KeepReconnecting && ConnectionState != ConnectionState.Connected)
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
            catch (Exception e)
            {
                Debug.Log("Left connection thread: " + e);
            }
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

        public virtual void Disconnect()
        {
            SetConnectionState(ConnectionState.Disconnected);
        }

        public abstract void Subscribe<T>(RosListener<T> listener) where T : IMessage, new();
        public abstract void Unsubscribe(RosListener subscriber);
        public abstract void Advertise<T>(RosSender<T> advertiser) where T : IMessage;
        public abstract void Unadvertise(RosSender advertiser);
        public abstract void Publish(RosSender advertiser, IMessage msg);
        public abstract void AdvertiseService<T>(string service, Action<T> callback) where T : IService, new();
        public abstract ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics();
        public abstract int GetNumPublishers(string topic);
        public abstract int GetNumSubscribers(string topic);

        protected virtual void Update()
        {
            // do nothing
        }
    }
}