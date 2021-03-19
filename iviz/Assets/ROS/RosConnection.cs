using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    public abstract class RosConnection : IExternalServiceProvider
    {
        const int TaskWaitTimeInMs = 2000;
        const int ConnectionRetryTimeInMs = TaskWaitTimeInMs;

        [ItemNotNull] protected static readonly ReadOnlyCollection<BriefTopicInfo> EmptyTopics =
            Array.Empty<BriefTopicInfo>().AsReadOnly();

        readonly SemaphoreSlim signal = new SemaphoreSlim(0);
        readonly Task task;
        readonly ConcurrentQueue<Func<Task>> toDos = new ConcurrentQueue<Func<Task>>();
        readonly CancellationTokenSource connectionTs = new CancellationTokenSource();

        protected RosConnection()
        {
            task = TaskUtils.StartLongTask(Run);
        }

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;
        public bool KeepReconnecting { get; set; }
        protected ReadOnlyCollection<BriefTopicInfo> PublishedTopics { get; set; } = EmptyTopics;

        public abstract ValueTask<bool> CallServiceAsync<T>(string service, T srv, CancellationToken token)
            where T : IService;

        public event Action<ConnectionState> ConnectionStateChanged;
        public event Action<bool> ConnectionWarningStateChanged;

        internal virtual void Stop()
        {
            connectionTs.Cancel();
            Signal();
            task.WaitNoThrow(this);
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

        protected void SetConnectionWarningState(bool value)
        {
            GameThread.Post(() => ConnectionWarningStateChanged?.Invoke(value));
        }

        protected void AddTask(Func<Task> a)
        {
            toDos.Enqueue(a);
            Signal();
        }

        protected void Signal()
        {
            signal.Release();
        }

        async Task Run()
        {
            DateTime lastConnectionTry = DateTime.MinValue;
            try
            {
                while (!connectionTs.IsCancellationRequested)
                {
                    DateTime now = GameThread.Now;
                    if (KeepReconnecting
                        && ConnectionState != ConnectionState.Connected
                        && (now - lastConnectionTry).TotalMilliseconds > ConnectionRetryTimeInMs)
                    {
                        SetConnectionState(ConnectionState.Connecting);

                        bool connectionResult;

                        try
                        {
                            lastConnectionTry = now;
                            connectionResult = await Connect();
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Unexpected error in RosConnection.Connect", e);
                            continue;
                        }

                        SetConnectionState(connectionResult ? ConnectionState.Connected : ConnectionState.Disconnected);
                    }

                    await signal.WaitAsync(TaskWaitTimeInMs);
                    await ExecuteTasks().AwaitNoThrow(this);
                }

                SetConnectionState(ConnectionState.Disconnected);
            }
            catch (Exception e)
            {
                // shouldn't happen
                Logger.Internal("Left connection thread!");
                Logger.Internal("Error:", e);
                Logger.Error("XXX Left connection thread: ", e);
            }

            connectionTs.Cancel();
        }

        async Task ExecuteTasks()
        {
            while (toDos.TryDequeue(out var action))
            {
                await action().AwaitNoThrow(this);
                //Debug.Log("-1: " + toDos.Count);
            }
        }

        protected abstract ValueTask<bool> Connect();

        public virtual void Disconnect()
        {
            SetConnectionState(ConnectionState.Disconnected);
            GC.Collect();
        }

        [NotNull]
        public override string ToString()
        {
            return "[RosConnection]";
        }
    }

    public enum RequestType
    {
        CachedOnly,
        CachedButRequestInBackground,
    }
}