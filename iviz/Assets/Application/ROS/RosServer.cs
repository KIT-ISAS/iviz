using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.RosMaster;
using UnityEngine;

namespace Iviz.Controllers
{
    public class RosServer
    {
        public const int DefaultPort = Server.DefaultPort;

        static readonly List<(string key, string value)> DefaultKeys = new List<(string, string)>
        {
            ("/rosdistro", "noetic"),
            ("/rosversion", "1.15.8")
        };
        
        static RosServer instance;
        static RosServer Instance => instance ?? (instance = new RosServer());
        public static bool IsActive => Instance.server != null;

        public static bool Create(Uri masterUri) => Instance.TryCreate(masterUri);
        public static void Dispose() => Instance.Reset();

        Task task;
        readonly SemaphoreSlim signal1 = new SemaphoreSlim(0, 1);
        readonly SemaphoreSlim signal2 = new SemaphoreSlim(0, 1);

        Server server;
        
        bool TryCreate(Uri masterUri)
        {
            if (masterUri == null)
            {
                throw new ArgumentNullException(nameof(masterUri));
            }

            task = Task.Run(async () => await TryCreateAsync(masterUri));
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
                
                serverTask = server.Start();
            }
            catch (Exception e)
            {
                Logger.Warn(e);
                server = null;
            }

            signal1.Release();

            if (server == null || serverTask == null)
            {
                return;
            }

            //Debug.Log(Thread.CurrentThread.Name + " " + Thread.CurrentContext.ContextID +  "In: signal2: wait!");
            await signal2.WaitAsync();

            //Debug.Log(Thread.CurrentThread.Name + " " + Thread.CurrentContext.ContextID +  "In: server: dispose!");
            server.Dispose();
            //Debug.Log(Thread.CurrentThread.Name + " " + Thread.CurrentContext.ContextID +  "In: wait: serverTask!");
            await serverTask;
            //Debug.Log(Thread.CurrentThread.Name + " " + Thread.CurrentContext.ContextID +  "In: out!");
            server = null;
        }

        void Reset()
        {
            if (server == null)
            {
                return;
            }

            //Debug.Log(Thread.CurrentThread.Name + " " + Thread.CurrentContext.ContextID +  "Out: signal2: release!");
            signal2.Release();
            //Debug.Log(Thread.CurrentThread.Name + " " + Thread.CurrentContext.ContextID +  "Out: task: wait^!");
            task.Wait();
        }
    }
}