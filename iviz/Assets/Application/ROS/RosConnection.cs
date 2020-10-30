using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public abstract class RosConnection : IExternalServiceProvider
    {
        const int TaskWaitTimeInMs = 2000;

        [ItemNotNull] protected static readonly ReadOnlyCollection<BriefTopicInfo> EmptyTopics =
            Array.Empty<BriefTopicInfo>().AsReadOnly();

        readonly SemaphoreSlim signal = new SemaphoreSlim(0, 1);
        readonly Task task;
        readonly ConcurrentQueue<Func<Task>> toDos = new ConcurrentQueue<Func<Task>>();

        volatile bool keepRunning;

        protected RosConnection()
        {
            keepRunning = true;
            task = Task.Run(Run);
        }

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;
        [CanBeNull] public virtual Uri MasterUri { get; set; }
        [CanBeNull] public virtual Uri MyUri { get; set; }
        [CanBeNull] public virtual string MyId { get; set; }
        public bool KeepReconnecting { get; set; }
        protected ReadOnlyCollection<BriefTopicInfo> PublishedTopics { get; set; } = EmptyTopics;

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

                        bool connectionResult = await Connect();

                        SetConnectionState(connectionResult ? ConnectionState.Connected : ConnectionState.Disconnected);
                    }

                    await signal.WaitAsync(TaskWaitTimeInMs);
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
            while (toDos.TryDequeue(out Func<Task> action))
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

        public enum RequestType
        {
            CachedOnly,
            CachedButRequestInBackground,
            WaitForRequest
        }

        public abstract void Subscribe<T>([NotNull] RosListener<T> listener)
            where T : IMessage, IDeserializable<T>, new();

        public abstract void Unsubscribe([NotNull] IRosListener subscriber);
        public abstract void Advertise<T>([NotNull] RosSender<T> advertiser) where T : IMessage;
        public abstract void Unadvertise([NotNull] IRosSender advertiser);
        public abstract void Publish<T>([NotNull] RosSender<T> advertiser, [NotNull] T msg) where T : IMessage;

        public abstract void AdvertiseService<T>([NotNull] string service, [NotNull] Action<T> callback)
            where T : IService, new();

        public abstract bool CallService<T>(string service, T srv) where T : IService;

        [ItemNotNull]
        [NotNull]
        public abstract ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics(
            RequestType type = RequestType.CachedButRequestInBackground);

        [NotNull]
        [ItemNotNull]
        public abstract ReadOnlyCollection<string> GetSystemParameterList();

        public abstract int GetNumPublishers([NotNull] string topic);
        public abstract int GetNumSubscribers([NotNull] string topic);
        public abstract object GetParameter([NotNull] string parameter);
    }
}