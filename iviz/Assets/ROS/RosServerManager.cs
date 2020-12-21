using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Roslib;
using Iviz.RosMaster;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    public class RosServerManager
    {
        public const int DefaultPort = RosMasterServer.DefaultPort;

        static readonly List<(string key, string value)> DefaultKeys = new List<(string, string)>
        {
            ("/rosdistro", "noetic"),
            ("/rosversion", "1.15.8"),
            ("/ivizversion", "1.0.0")
        };

        static RosServerManager instance;
        [NotNull] static RosServerManager Instance => instance ?? (instance = new RosServerManager());
        public static bool IsActive => instance?.server != null;
        [CanBeNull] public static Uri MasterUri => instance?.server?.MasterUri;

        public static bool Create([NotNull] Uri masterUri, [NotNull] string masterId) =>
            Instance.TryCreate(
                masterUri ?? throw new ArgumentNullException(nameof(masterUri)),
                masterId ?? throw new ArgumentNullException(nameof(masterId)));

        public static void Dispose() => instance?.Reset();

        RosMasterServer server;
        Task serverTask;

        bool TryCreate(Uri masterUri, string masterId)
        {
            if (server != null)
            {
                if (server.MasterUri == masterUri)
                {
                    return true;
                }

                Reset();
            }

            try
            {
                server = new RosMasterServer(masterUri, masterId);

                foreach (var (key, value) in DefaultKeys)
                {
                    server.AddKey(key, value);
                }

                // start in background
                serverTask = Task.Run(async () =>
                {
                    await server.Start();
                });
            }
            catch (Exception e)
            {
                Logger.Warn(e);
                server = null;
            }
            
            return server != null;
        }

        void Reset()
        {
            if (server == null)
            {
                return;
            }

            server.Dispose();
            serverTask.WaitForWithTimeout(2000).WaitNoThrow("RosServerManager");
            server = null;
        }
    }
}