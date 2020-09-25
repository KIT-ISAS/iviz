using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.RosMaster;
using UnityEngine;

namespace Iviz.Controllers
{
    public class RosServerManager
    {
        public const int DefaultPort = Server.DefaultPort;

        static readonly List<(string key, string value)> DefaultKeys = new List<(string, string)>
        {
            ("/rosdistro", "noetic"),
            ("/rosversion", "1.15.8"),
            ("/ivizversion", "1.0.0")
        };

        static RosServerManager instance;
        static RosServerManager Instance => instance ?? (instance = new RosServerManager());
        public static bool IsActive => instance?.server != null;
        public static Uri MasterUri => instance?.server?.MasterUri;

        public static bool Create(Uri masterUri) =>
            Instance.TryCreate(masterUri ?? throw new ArgumentNullException(nameof(masterUri)));

        public static void Dispose() => instance?.Reset();

        // ---

        readonly SemaphoreSlim signal1 = new SemaphoreSlim(0, 1);
        readonly SemaphoreSlim signal2 = new SemaphoreSlim(0, 1);

        Task task;
        Server server;

        bool TryCreate(Uri masterUri)
        {
            if (server != null)
            {
                if (server.MasterUri == masterUri)
                {
                    return true;
                }

                Reset();
            }

            task = Task.Run(async () => await TryCreateAsync(masterUri));
            
            // wait for TryCreateAsync()
            signal1.Wait();

            return server != null;
        }

        async Task TryCreateAsync(Uri masterUri)
        {
            Task serverTask = null;
            try
            {
                server = new Server(masterUri);

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