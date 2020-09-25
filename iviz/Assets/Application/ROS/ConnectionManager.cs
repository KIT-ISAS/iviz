//#define PUBLISH_LOG

using Iviz.Msgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.Controllers
{
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
    }

    public class ConnectionManager : MonoBehaviour
    {
        static ConnectionManager Instance;

        public static RosConnection Connection { get; private set; }

#if PUBLISH_LOG
        RosSender<Log> sender;
#endif

        int collectedUp, collectedDown;

        void Awake()
        {
            Instance = this;
            Connection = new RoslibConnection();

#if PUBLISH_LOG
            Logger.Log += LogMessage;
            sender = new RosSender<Log>("/rosout");
#endif
        }

        void OnDestroy()
        {
            Connection?.Stop();
            Connection = null;

            RosServerManager.Dispose();
        }

#if PUBLISH_LOG
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
                Level = (byte) msg.Level,
                Name = Connection.MyId,
                Msg = (msg.Message is Exception ex) ? ex.Message : msg.Message.ToString(),
                File = msg.File,
                Line = (uint) msg.Line
            });
        }
#endif

        public static string MyId => Connection?.MyId;
        public static Uri MyUri => Connection?.MyUri;
        public static Uri MasterUri => Connection?.MasterUri;

        public static ConnectionState ConnectionState => Connection?.ConnectionState ?? ConnectionState.Disconnected;
        public static bool IsConnected => ConnectionState == ConnectionState.Connected;

        public static void Subscribe<T>(RosListener<T> listener) where T : IMessage, new()
            => Connection.Subscribe(listener);

        public static void Unsubscribe(RosListener subscriber) => Connection.Unsubscribe(subscriber);

        public static void Advertise<T>(RosSender<T> advertiser) where T : IMessage
            => Connection.Advertise(advertiser);

        public static void Unadvertise(RosSender advertiser) => Connection.Unadvertise(advertiser);
        public static void Publish(RosSender advertiser, IMessage msg) => Connection.Publish(advertiser, msg);

        public static void AdvertiseService<T>(string service, Action<T> callback) where T : IService, new()
            => Connection.AdvertiseService(service, callback);

        public static ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics() =>
            Connection.GetSystemPublishedTopics();

        public static ReadOnlyCollection<string> GetSystemParameterList() => Connection.GetSystemParameterList();

        public static void ReportBandwidthUp(int size)
        {
            Instance.collectedUp += size;
        }

        public static void ReportBandwidthDown(int size)
        {
            Instance.collectedDown += size;
        }

        public static (int, int) CollectBandwidthReport()
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

        readonly Queue<Func<Task>> toDos = new Queue<Func<Task>>();
        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 100);
        readonly Task task;

        volatile bool keepRunning;

        public event Action<ConnectionState> ConnectionStateChanged;

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;

        public virtual Uri MasterUri { get; set; }
        public virtual Uri MyUri { get; set; }
        public virtual string MyId { get; set; }

        public bool KeepReconnecting { get; set; }

        protected static readonly ReadOnlyCollection<BriefTopicInfo> EmptyTopics =
            Array.Empty<BriefTopicInfo>().AsReadOnly();

        public ReadOnlyCollection<BriefTopicInfo> PublishedTopics { get; protected set; } = EmptyTopics;

        protected RosConnection()
        {
            keepRunning = true;
            task = Task.Run(async () => await Run());
            GameThread.EverySecond += Update;
        }

        public virtual void Stop()
        {
            keepRunning = false;
            signal.Release();
            task?.Wait();
            GameThread.EverySecond -= Update;
        }

        void SetConnectionState(ConnectionState newState)
        {
            if (ConnectionState != newState)
            {
                ConnectionState = newState;
                GameThread.RunOnce(() => ConnectionStateChanged?.Invoke(newState));
            }
        }

        protected void AddTask(Func<Task> a)
        {
            lock (toDos)
            {
                toDos.Enqueue(a);
            }

            signal.Release();
        }

        protected void AddTask(Action a)
        {
            lock (toDos)
            {
                toDos.Enqueue(async () => a());
            }

            signal.Release();
        }

        async Task Run()
        {
            try
            {
                while (keepRunning)
                {
                    if (KeepReconnecting && ConnectionState != ConnectionState.Connected)
                    {
                        SetConnectionState(ConnectionState.Connecting);

                        bool connectionResult;
                        try
                        {
                            connectionResult = await Connect();
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Left connection:" + e);
                            connectionResult = false;
                        }

                        SetConnectionState(connectionResult ? ConnectionState.Connected : ConnectionState.Disconnected);
                    }

                    await signal.WaitAsync(TaskWaitTime);
                    await ExecuteTasks();
                }

                SetConnectionState(ConnectionState.Disconnected);
            }
            catch (Exception e)
            {
                // shouldn't happen
                Logger.Internal("Left connection thread!");
                Debug.LogError("XXX Left connection thread: " + e);
            }
        }

        async Task ExecuteTasks()
        {
            while (true)
            {
                Func<Task> action;
                lock (toDos)
                {
                    if (toDos.Count == 0)
                    {
                        break;
                    }

                    action = toDos.Dequeue();
                }

                try
                {
                    await action();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        protected abstract Task<bool> Connect();

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
        //public abstract void CallServiceAsync<T>(string service, T srv, Action<T> callback) where T : IService;
        public abstract bool CallService<T>(string service, T srv) where T : IService;
        public abstract ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics();
        public abstract ReadOnlyCollection<string> GetSystemParameterList();
        public abstract int GetNumPublishers(string topic);
        public abstract int GetNumSubscribers(string topic);

        public abstract object GetParameter(string parameter);

        protected virtual void Update()
        {
            // do nothing
        }
    }
}