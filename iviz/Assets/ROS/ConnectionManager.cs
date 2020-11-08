//#define PUBLISH_LOG

using System;
using System.Collections.ObjectModel;
using System.IO;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Msgs.Logger;

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

        [NotNull] public static IExternalServiceProvider ServiceProvider => Connection;

        static RosConnection connection;
        [NotNull] public static RosConnection Connection => connection ?? (connection = new RoslibConnection());

        Sender<Log> sender;

        int frameBandwidthUp;
        int frameBandwidthDown;

        void Awake()
        {
            Instance = this;

            sender = new Sender<Log>("/rosout");
            Core.Logger.LogExternal += LogMessage;
        }

        void OnDestroy()
        {
            Connection.Stop();

            RosServerManager.Dispose();
        }

        uint logSeq = 0;
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

        [CanBeNull] public static string MyId => Connection.MyId;
        public static bool IsConnected => Connection.ConnectionState == ConnectionState.Connected;

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