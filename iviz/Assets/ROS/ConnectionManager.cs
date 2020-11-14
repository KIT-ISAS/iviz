//#define PUBLISH_LOG

using System.IO;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.RosgraphMsgs;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected
    }

    public class ConnectionManager : MonoBehaviour
    {
        static ConnectionManager Instance;
        static RoslibConnection connection;
        
        int frameBandwidthDown;
        int frameBandwidthUp;
        uint logSeq;

        Sender<Log> sender;

        [NotNull] public static IExternalServiceProvider ServiceProvider => Connection;
        [NotNull] public static RoslibConnection Connection => connection ?? (connection = new RoslibConnection());

        [CanBeNull] public static string MyId => Connection.MyId;
        public static bool IsConnected => Connection.ConnectionState == ConnectionState.Connected;

        void Awake()
        {
            Instance = this;

            sender = new Sender<Log>("/rosout");
            Logger.LogExternal += LogMessage;
        }

        void OnDestroy()
        {
            Connection.Stop();

            RosServerManager.Dispose();
        }

        void LogMessage(in LogMessage msg)
        {
            sender.Publish(new Log
            {
                Header = RosUtils.CreateHeader(logSeq++),
                Level = (byte) msg.Level,
                Name = Connection.MyId ?? "/iviz",
                Msg = msg.Message,
                File = Path.GetFileName(msg.File),
                Line = (uint) msg.Line
            });
        }

        internal static void ReportBandwidthUp(int size)
        {
            Instance.frameBandwidthUp += size;
        }

        internal static void ReportBandwidthDown(int size)
        {
            Instance.frameBandwidthDown += size;
        }

        public static (int, int) CollectBandwidthReport()
        {
            (int, int) result = (Instance.frameBandwidthDown, Instance.frameBandwidthUp);
            Instance.frameBandwidthDown = 0;
            Instance.frameBandwidthUp = 0;
            return result;
        }
    }
}