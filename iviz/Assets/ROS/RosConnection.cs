#nullable enable

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Ros
{
    /// <summary>
    /// Partial implementation of a ROS connection. The rest is in <see cref="RosConnection"/>.
    /// Here we only handle initializing connections and task queues.
    /// </summary>
    internal sealed partial class RosConnection : RosProvider
    {
        const int TaskWaitTimeInMs = 2000;
        const int ConnectionRetryTimeInMs = TaskWaitTimeInMs;

        bool disposed;

        readonly SemaphoreSlim signal = new(0);
        readonly Task task;
        readonly ConcurrentQueue<Func<ValueTask>> toDos = new();
        readonly CancellationTokenSource connectionTs = new();

        DateTime lastConnectionTry = DateTime.MinValue;
        bool tryConnectOnce;

        public RosConnection()
        {
            task = TaskUtils.RunNoThrow(Run, this);
        }

        internal bool TryGetClient([NotNullWhen(true)] out IRosClient? conn)
        {
            if (IsConnected && client != null)
            {
                conn = client;
                return true;
            }

            conn = null;
            return false;
        }


        void DisposeBase()
        {
            connectionTs.CancelNoThrow(this);
            Signal();
            task.WaitNoThrow(this);
            disposed = true;

            ClearEvents();
        }

        void SetConnectionState(ConnectionState newState)
        {
            if (connectionState == newState)
            {
                return;
            }

            connectionState = newState;
            GameThread.Post(() => RaiseConnectionStateChanged(newState));
        }

        static void SetConnectionWarningState(bool value)
        {
            GameThread.Post(() => RaiseConnectionWarningStateChanged(value));
        }

        /// <summary>
        /// Runs the task in the connection thread. 
        /// </summary>
        void Post(Func<ValueTask> a)
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
        void Signal()
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

            connectionTs.CancelNoThrow(this);
        }

        async ValueTask RunTasks()
        {
            var now = GameThread.Now;
            if ((KeepReconnecting || tryConnectOnce)
                && !IsConnected
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

            if (!ValidateCanConnect())
            {
                KeepReconnecting = false;
                SetConnectionState(ConnectionState.Disconnected);
                tryConnectOnce = false;
                return;
            }
            
            try
            {
                lastConnectionTry = now;
                connectionResult = await ConnectAsync(); 
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

        public override void TryOnceToConnect()
        {
            if (connectionState != ConnectionState.Disconnected)
            {
                return;
            }

            tryConnectOnce = true;
            Signal();
        }

        void DisconnectBase()
        {
            SetConnectionState(ConnectionState.Disconnected);
            lastConnectionTry = DateTime.MinValue;
        }

        public override string ToString() => $"[{nameof(RosConnection)}]";
    }
}