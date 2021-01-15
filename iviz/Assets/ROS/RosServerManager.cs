using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.RosMaster;
using Iviz.XmlRpc;
using JetBrains.Annotations;

namespace Iviz.Ros
{
    public class RosServerManager
    {
        public const int DefaultPort = RosMasterServer.DefaultPort;
        const int DisposeTimeoutInMs = 2000;

        static readonly List<(string key, string value)> DefaultKeys = new List<(string, string)>
        {
            ("/rosdistro", "noetic"),
            ("/rosversion", "1.15.8"),
            ("/ivizversion", "1.0.0")
        };

        static RosServerManager instance;

        RosMasterServer server;
        Task serverTask;
        
        [NotNull] static RosServerManager Instance => instance ?? (instance = new RosServerManager());
        public static bool IsActive => instance?.server != null;
        [CanBeNull] public static Uri MasterUri => instance?.server?.MasterUri;

        public static bool Create([NotNull] Uri masterUri, [NotNull] string masterId)
        {
            return Instance.TryCreate(
                masterUri ?? throw new ArgumentNullException(nameof(masterUri)),
                masterId ?? throw new ArgumentNullException(nameof(masterId)));
        }

        public static void Dispose()
        {
            instance?.DisposeImpl();
            instance = null;
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
                serverTask = Task.Run(() => server.StartAsync());
            }
            catch (Exception e)
            {
                Logger.Internal("<b>Error:</b> Failed to start ROS master", e);
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
            serverTask.WaitForWithTimeout(DisposeTimeoutInMs).WaitNoThrow("RosServerManager");
            server = null;
        }
    }
}