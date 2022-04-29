#nullable enable

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Tools;
using UnityEngine;
using IServiceProvider = Iviz.Displays.IServiceProvider;

namespace Iviz.Ros
{
    /// <summary>
    /// Partial implementation of a ROS connection. The rest is in <see cref="RoslibConnection"/>.
    /// Here we only handle initializing connections and task queues.
    /// </summary>
    public abstract class RosConnection
    {
        const int TaskWaitTimeInMs = 2000;
        const int ConnectionRetryTimeInMs = TaskWaitTimeInMs;

        bool disposed;

        public static event Action<ConnectionState>? ConnectionStateChanged;
        public static event Action<bool>? ConnectionWarningStateChanged;

        readonly SemaphoreSlim signal = new(0);
        readonly Task task;
        readonly ConcurrentQueue<Func<ValueTask>> toDos = new();
        readonly CancellationTokenSource connectionTs = new();

        ConnectionState connectionState = ConnectionState.Disconnected;
        DateTime lastConnectionTry = DateTime.MinValue;
        bool tryConnectOnce;
        
        public bool IsConnected => connectionState == ConnectionState.Connected;
        public bool KeepReconnecting { get; set; }

        protected RosConnection()
        {
            task = TaskUtils.Run(() => Run().AwaitNoThrow(this));
        }

        internal virtual void Dispose()
        {
            connectionTs.Cancel();
            Signal();
            task.WaitNoThrow(this);
            disposed = true;

            ConnectionStateChanged = null;
            ConnectionWarningStateChanged = null;
        }

        void SetConnectionState(ConnectionState newState)
        {
            if (connectionState == newState)
            {
                return;
            }

            connectionState = newState;
            GameThread.Post(() => ConnectionStateChanged?.Invoke(newState));
        }

        protected static void SetConnectionWarningState(bool value)
        {
            GameThread.Post(() => ConnectionWarningStateChanged?.Invoke(value));
        }

        protected void AddTask(Func<ValueTask> a)
        {
            if (disposed)
            {
                //Debug.Log($"{nameof(RosConnection)}: Ignoring enqueued task. Reason: Already disposed.");
                return;
            }
            
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
                && connectionState != ConnectionState.Connected
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
            if (connectionState != ConnectionState.Disconnected)
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