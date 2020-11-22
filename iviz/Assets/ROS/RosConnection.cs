using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
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
        public bool KeepReconnecting { get; set; }
        protected ReadOnlyCollection<BriefTopicInfo> PublishedTopics { get; set; } = EmptyTopics;
        public abstract bool CallService<T>(string service, T srv) where T : IService;
        public event Action<ConnectionState> ConnectionStateChanged;

        internal virtual void Stop()
        {
            keepRunning = false;
            Signal();
            try
            {
                task?.Wait();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
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
            try
            {
                signal.Release();
            }
            catch (SemaphoreFullException)
            {
            }
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
                            Logger.Error("XXX Error in Connect!: " + e);
                            connectionResult = false;
                        }

                        SetConnectionState(connectionResult ? ConnectionState.Connected : ConnectionState.Disconnected);
                    }

                    await signal.WaitAsync(TaskWaitTimeInMs);

                    try
                    {
                        await ExecuteTasks();
                    }
                    catch (Exception e)
                    {
                        Logger.Error("XXX Error in ExecuteTask!: " + e);
                    }
                }

                SetConnectionState(ConnectionState.Disconnected);
            }
            catch (Exception e)
            {
                // shouldn't happen
                Logger.Internal("Left connection thread!");
                Logger.Internal("Error:", e);
                Logger.Error("XXX Left connection thread: " + e);
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
    }

    public enum RequestType
    {
        CachedOnly,
        CachedButRequestInBackground,
        WaitForRequest
    }
}