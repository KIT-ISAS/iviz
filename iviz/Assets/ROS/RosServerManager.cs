#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.RosMaster;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    public sealed class RosServerManager
    {
        public const int DefaultPort = RosMasterServer.DefaultPort;
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
        public Uri? MasterUri => server?.MasterUri;

        public bool Start(Uri masterUri, string masterId)
        {
            return TryStart(
                masterUri ?? throw new ArgumentNullException(nameof(masterUri)),
                masterId ?? throw new ArgumentNullException(nameof(masterId)));
        }

        bool TryStart(Uri masterUri, string masterId)
        {
            if (server != null)
            {
                if (server.MasterUri == masterUri)
                {
                    return true;
                }

                Dispose();
                // pass through
            }

            try
            {
                server = new RosMasterServer(masterUri, masterId);

                foreach ((string key, string value) in DefaultKeys)
                {
                    server.AddKey(key, value);
                }

                // start in background
                serverTask = TaskUtils.Run(async () =>
                {
                    var task = server.StartAsync();
                    await Task.Delay(100);
                    RosManager.Connection.TryOnceToConnect();
                    await task.AwaitNoThrow(this);
                });
            }
            catch (Exception e)
            {
                RosLogger.Internal("<b>Error:</b> Failed to start ROS master", e);
                server = null;
            }

            return server != null;
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
    }
}