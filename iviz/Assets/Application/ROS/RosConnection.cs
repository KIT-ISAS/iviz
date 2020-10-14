using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using UnityEngine;
using Logger = Iviz.Logger;

namespace Iviz.Controllers
{
    public abstract class RosConnection : IExternalServiceProvider
    {
        static readonly TimeSpan TaskWaitTime = TimeSpan.FromMilliseconds(2000);

        protected static readonly ReadOnlyCollection<BriefTopicInfo> EmptyTopics =
            Array.Empty<BriefTopicInfo>().AsReadOnly();

        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        readonly ConcurrentQueue<Func<Task>> toDos = new ConcurrentQueue<Func<Task>>();
        readonly Task task;

        volatile bool keepRunning;

        protected RosConnection()
        {
            keepRunning = true;
            task = Task.Run(Run);
        }

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;
        public virtual Uri MasterUri { get; set; }
        public virtual Uri MyUri { get; set; }
        public virtual string MyId { get; set; }
        public bool KeepReconnecting { get; set; }
        public ReadOnlyCollection<BriefTopicInfo> PublishedTopics { get; protected set; } = EmptyTopics;
        public abstract bool CallService<T>(string service, T srv) where T : IService;

        public event Action<ConnectionState> ConnectionStateChanged;

        public virtual void Stop()
        {
            keepRunning = false;
            Signal();

            task?.Wait();
        }

        void SetConnectionState(ConnectionState newState)
        {
            if (ConnectionState == newState)
            {
                return;
            }

            ConnectionState = newState;
            GameThread.Post(() => ConnectionStateChanged?.Invoke(newState));
        }

        protected void AddTask(Func<Task> a)
        {
            toDos.Enqueue(a);
            Signal();
        }

        protected void Signal()
        {
            try { signal.Release(); }
            catch (SemaphoreFullException) { }
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

                        var connectionResult = await Connect();

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
            while (toDos.TryDequeue(out var action))
            {
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
        public abstract ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics();
        public abstract ReadOnlyCollection<string> GetSystemParameterList();
        public abstract int GetNumPublishers(string topic);
        public abstract int GetNumSubscribers(string topic);
        public abstract object GetParameter(string parameter);
    }
}