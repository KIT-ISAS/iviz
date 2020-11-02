//#define PUBLISH_LOG

using System;
using System.Collections.ObjectModel;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;

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

#if PUBLISH_LOG
        RosSender<Log> sender;
#endif

        int frameBandwidthUp;
        int frameBandwidthDown;

        void Awake()
        {
            Instance = this;

#if PUBLISH_LOG
            Logger.Log += LogMessage;
            sender = new RosSender<Log>("/rosout");
#endif
        }

        void OnDestroy()
        {
            Connection.Stop();

            RosServerManager.Dispose();
        }

#if PUBLISH_LOG
        uint logSeq = 0;
        void LogMessage(in LogMessage msg)
        {
            if (msg.Level == LogLevel.Debug)
            {
                return;
            }

            sender.Publish(new Log()
            {
                Header = RosUtils.CreateHeader(logSeq++),
                Level = (byte) msg.Level,
                Name = Connection.MyId,
                Msg = (msg.Message is Exception ex) ? ex.Message : msg.Message.ToString(),
                File = msg.File,
                Line = (uint) msg.Line
            });
        }
#endif

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