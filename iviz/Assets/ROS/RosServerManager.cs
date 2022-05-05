#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Roslib;
using Iviz.Roslib.XmlRpc;
using Iviz.RosMaster;
using Iviz.Tools;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Ros
{
    public sealed class RosServerManager
    {
        const int DisposeTimeoutInMs = 2000;

        static readonly List<(string key, string value)> DefaultKeys = new()
        {
            ("/rosdistro", "noetic"),
            ("/rosversion", "1.15.8"),
            ("/ivizversion", "1.0.1")
        };

        RosMasterServer? server;
        Task? serverTask;

        public bool IsActive => server != null;

        public ValueTask<bool> StartAsync(Uri masterUri, string masterId)
        {
            ThrowHelper.ThrowIfNull(masterUri, nameof(masterUri));
            ThrowHelper.ThrowIfNull(masterId, nameof(masterId));
            return TryStartAsync(masterUri, masterId);
        }

        async ValueTask<bool> TryStartAsync(Uri masterUri, string masterId)
        {
            if (server != null)
            {
                if (server.MasterUri == masterUri)
                {
                    return true;
                }

                await DisposeAsync();
                // pass through
            }

            server = new RosMasterServer(masterUri, masterId);

            foreach ((string key, string value) in DefaultKeys)
            {
                server.AddKey(key, value);
            }

            var initCompletedSignal = TaskUtils.CreateCompletionSource();

            // start in background
            serverTask = TaskUtils.Run(() => server.StartAsync(initCompletedSignal).AwaitNoThrow(this));

            try
            {
                await initCompletedSignal.Task;
            }
            catch
            {
                await DisposeAsync();
                return false; 
            }

            await Task.Delay(100);

            if (await PingMaster(server.MasterUri))
            {
                return true;
            }

            await DisposeAsync();
            return false;
        }

        public void Dispose()
        {
            if (server == null)
            {
                return;
            }

            RosLogger.Info($"{this}: Disposing!");
            server.Dispose();

            // should return immediately, the waiting happened in server.Dispose()
            _ = serverTask.AwaitNoThrow(DisposeTimeoutInMs, this);

            server = null;
        }

        public async ValueTask DisposeAsync()
        {
            if (server == null)
            {
                return;
            }

            RosLogger.Info($"{this}: Disposing!");
            await server.DisposeAsync();
            await serverTask.AwaitNoThrow(DisposeTimeoutInMs, this);
            server = null;
        }

        public override string ToString() => $"[{nameof(RosServerManager)}]";

        static async ValueTask<bool> PingMaster(Uri masterUri)
        {
            using var tokenSource = new CancellationTokenSource(200);
            var connection = new XmlRpcConnection("pingConnection", masterUri);
            var callerUri = masterUri;
            const string callerId = "iviz_ping";

            try
            {
                await connection.MethodCallAsync(callerUri, "getUri", new XmlRpcArg[] { callerId }, tokenSource.Token);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }
}