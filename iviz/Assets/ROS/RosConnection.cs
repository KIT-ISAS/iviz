#nullable enable

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Ros
{
    public abstract class RosConnection : IExternalServiceProvider
    {
        const int TaskWaitTimeInMs = 2000;
        const int ConnectionRetryTimeInMs = TaskWaitTimeInMs;

        public static event Action<ConnectionState>? ConnectionStateChanged;
        public static event Action<bool>? ConnectionWarningStateChanged;

        readonly SemaphoreSlim signal = new(0);
        readonly Task task;
        readonly ConcurrentQueue<Func<ValueTask>> toDos = new();
        readonly CancellationTokenSource connectionTs = new();

        DateTime lastConnectionTry = DateTime.MinValue;
        bool tryConnectOnce;

        public ConnectionState ConnectionState { get; private set; } = ConnectionState.Disconnected;
        public bool KeepReconnecting { get; set; }

        protected RosConnection()
        {
            task = TaskUtils.Run(() => Run().AwaitNoThrow(this));
        }

        public abstract ValueTask<bool> CallServiceAsync<T>(string service, T srv, int timeoutInMs,
            CancellationToken token)
            where T : IService;

        internal virtual void Dispose()
        {
            connectionTs.Cancel();
            Signal();
            task.WaitNoThrow(this);

            ConnectionStateChanged = null;
            ConnectionWarningStateChanged = null;
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

        protected static void SetConnectionWarningState(bool value)
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
                    await RunTasks();
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

        async ValueTask RunTasks()
        {
            var now = GameThread.Now;
            if ((KeepReconnecting || tryConnectOnce)
                && ConnectionState != ConnectionState.Connected
                && (now - lastConnectionTry).TotalMilliseconds > ConnectionRetryTimeInMs)
            {
                await TryToConnect(now);
            }

            await signal.WaitAsync(TaskWaitTimeInMs);
            await ExecuteTasks().AwaitNoThrow(this);
        }

        async ValueTask TryToConnect(DateTime now)
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
                RosLogger.Error($"{this}: Unexpected error in Connect", e);
                return;
            }
            finally
            {
                tryConnectOnce = false;
            }

            SetConnectionState(connectionResult ? ConnectionState.Connected : ConnectionState.Disconnected);
            if (connectionResult)
            {
                KeepReconnecting = true;
                SetConnectionState(ConnectionState.Connected);
            }
            else
            {
                SetConnectionState(ConnectionState.Disconnected);
            }            
        }

        async ValueTask ExecuteTasks()
        {
            while (toDos.TryDequeue(out var action))
            {
                await action().AwaitNoThrow(this);
            }
        }

        public void TryOnceToConnect()
        {
            if (ConnectionState != ConnectionState.Disconnected)
            {
                return;
            } 
            
            tryConnectOnce = true;
            Signal();
        }
        
        protected abstract ValueTask<bool> ConnectAsync();

        public virtual void Disconnect()
        {
            SetConnectionState(ConnectionState.Disconnected);
            lastConnectionTry = DateTime.MinValue;
        }

        public sealed override string ToString() => "[RosConnection]";
    }

    public enum RequestType
    {
        CachedOnly,
        CachedButRequestInBackground,
    }
}