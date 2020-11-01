using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.RosMaster;
using JetBrains.Annotations;

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

        // ---

        readonly SemaphoreSlim signal1 = new SemaphoreSlim(0, 1);
        readonly SemaphoreSlim signal2 = new SemaphoreSlim(0, 1);

        Task task;
        RosMasterServer server;

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

            task = Task.Run(async () => await TryCreateAsync(masterUri, masterId));

            // wait for TryCreateAsync()
            signal1.Wait();

            return server != null;
        }

        async Task TryCreateAsync(Uri masterUri, string masterId)
        {
            Task serverTask = null;
            try
            {
                server = new RosMasterServer(masterUri, masterId);

                foreach (var (key, value) in DefaultKeys)
                {
                    server.AddKey(key, value);
                }

                // start in background
                serverTask = server.Start();
            }
            catch (Exception e)
            {
                Logger.Warn(e);
                server = null;
            }

            // tell TryCreate() to continue
            signal1.Release();

            if (server == null || serverTask == null)
            {
                return;
            }

            // wait for Reset()
            await signal2.WaitAsync();

            server.Dispose();
            await serverTask;

            server = null;
        }

        void Reset()
        {
            if (server == null)
            {
                return;
            }

            // tell TryCreate() to stop waiting
            signal2.Release();

            task.Wait();
        }
    }
}