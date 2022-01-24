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

        static RosServerManager? instance;

        RosMasterServer? server;
        Task? serverTask;

        static RosServerManager Instance => instance ??= new RosServerManager();
        public static bool IsActive => instance?.server != null;
        public static Uri? MasterUri => instance?.server?.MasterUri;

        public static bool Create(Uri masterUri, string masterId)
        {
            return Instance.TryCreate(
                masterUri ?? throw new ArgumentNullException(nameof(masterUri)),
                masterId ?? throw new ArgumentNullException(nameof(masterId)));
        }

        bool TryCreate(Uri masterUri, string masterId)
        {
            if (server != null)
            {
                if (server.MasterUri == masterUri)
                {
                    return true;
                }

                DisposeImpl();
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
                    var task = server.StartAsync().AwaitNoThrow(this);
                    await Task.Delay(100);
                    ConnectionManager.Connection.TryOnceToConnect();
                    await task;
                });
            }
            catch (Exception e)
            {
                RosLogger.Internal("<b>Error:</b> Failed to start ROS master", e);
                server = null;
            }

            return server != null;
        }

        void DisposeImpl()
        {
            if (server == null)
            {
                return;
            }

            server.Dispose();
            _ = serverTask.AwaitNoThrow(DisposeTimeoutInMs, this); // should return immediately
            server = null;
        }

        public static void Dispose()
        {
            if (instance != null)
            {
                RosLogger.Info("RosServerManager: Disposing!");
                instance.DisposeImpl();
            }

            instance = null;
        }

        async ValueTask DisposeImplAsync()
        {
            if (server == null)
            {
                return;
            }

            await server.DisposeAsync();
            await serverTask.AwaitNoThrow(DisposeTimeoutInMs, this);
            server = null;
        }

        public static async ValueTask DisposeAsync()
        {
            if (instance != null)
            {
                RosLogger.Info("RosServerManager: Disposing!");
                await instance.DisposeImplAsync();
            }

            instance = null;
        }
    }
}