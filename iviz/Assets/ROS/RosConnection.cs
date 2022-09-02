#nullable enable

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Tools;

namespace Iviz.Ros
{
    /// <summary>
    /// Partial implementation of a ROS connection. The rest is in <see cref="RoslibConnection"/>.
    /// Here we only handle initializing connections and task queues.
    /// </summary>
    internal class RosConnection
    {
        const int TaskWaitTimeInMs = 2000;
        const int ConnectionRetryTimeInMs = TaskWaitTimeInMs;

        bool disposed;

        public const bool IsRos2VersionSupported = Settings.IsAndroid || Settings.IsIPhone || Settings.IsMacOS;

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

            IRosProvider.ClearEvents();
        }

        void SetConnectionState(ConnectionState newState)
        {
            if (connectionState == newState)
            {
                return;
            }

            connectionState = newState;
            GameThread.Post(() => IRosProvider.RaiseConnectionStateChanged(newState));
        }

        protected static void SetConnectionWarningState(bool value)
        {
            GameThread.Post(() => IRosProvider.RaiseConnectionWarningStateChanged(value));
        }

        /// <summary>
        /// Runs the task in the connection thread. 
        /// </summary>
        protected void Post(Func<ValueTask> a)
        {
            if (disposed)
            {
                return;
            }

            toDos.Enqueue(a);
            Signal();
        }

        /// <summary>
        /// Notifies the connection thread that it has a new task. 
        /// </summary>
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
                connectionResult =
                    await ((RoslibConnection)this).ConnectAsync(); // hack! but faster than virtual dispatch
            }
            catch (Exception e)
            {
                RosLogger.Error($"{ToString()}: Unexpected error in {nameof(TryToConnect)}", e);
                return;
            }
            finally
            {
                tryConnectOnce = false;
            }

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

        public virtual void Disconnect()
        {
            SetConnectionState(ConnectionState.Disconnected);
            lastConnectionTry = DateTime.MinValue;
        }

        public sealed override string ToString() => $"[{nameof(RosConnection)}]";
    }
}