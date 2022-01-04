#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Ros
{
    public abstract class RosConnection : IExternalServiceProvider
    {
        const int TaskWaitTimeInMs = 2000;
        const int ConnectionRetryTimeInMs = TaskWaitTimeInMs;

        readonly SemaphoreSlim signal = new(0);
        readonly Task task;
        readonly ConcurrentQueue<Func<ValueTask>> toDos = new();
        readonly CancellationTokenSource connectionTs = new();

        DateTime lastConnectionTry = DateTime.MinValue;

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;
        public bool KeepReconnecting { get; set; }

        public event Action<ConnectionState>? ConnectionStateChanged;
        public event Action<bool>? ConnectionWarningStateChanged;

        protected RosConnection()
        {
            task = TaskUtils.StartLongTask(async () => await Run().AwaitNoThrow(this));
        }

        public abstract ValueTask<bool> CallServiceAsync<T>(string service, T srv, int timeoutInMs,
            CancellationToken token)
            where T : IService;

        internal virtual void Dispose()
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

        protected void AddTask(Func<ValueTask> a)
        {
            toDos.Enqueue(a);
            Signal();
        }

        protected void Signal()
        {
            signal.Release();
        }

        async ValueTask Run()
        {
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
                            connectionResult = await ConnectAsync();
                        }
                        catch (Exception e)
                        {
                            RosLogger.Error("Unexpected error in RosConnection.Connect", e);
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
                RosLogger.Internal("Left connection thread!");
                RosLogger.Internal("Error:", e);
                RosLogger.Error("XXX Left connection thread: ", e);
            }

            connectionTs.Cancel();
        }

        async ValueTask ExecuteTasks()
        {
            while (toDos.TryDequeue(out var action))
            {
                await action().AwaitNoThrow(this);
            }
        }

        protected abstract ValueTask<bool> ConnectAsync();

        public virtual void Disconnect()
        {
            SetConnectionState(ConnectionState.Disconnected);
            lastConnectionTry = DateTime.MinValue;
        }

        public override string ToString() => "[RosConnection]";
    }

    public enum RequestType
    {
        CachedOnly,
        CachedButRequestInBackground,
    }
}