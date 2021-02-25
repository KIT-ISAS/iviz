using System;
using System.IO;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
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

    public sealed class ConnectionManager : MonoBehaviour
    {
        static ConnectionManager instance;
        static RoslibConnection connection;

        Listener<Log> logListener;
        public delegate void LogDelegate(in Log log);
        public static event LogDelegate LogMessageArrived;


        long frameBandwidthDown;
        long frameBandwidthUp;
        uint logSeq;

        Sender<Log> logSender;

        [NotNull] public static IExternalServiceProvider ServiceProvider => Connection;
        [NotNull] public static RoslibConnection Connection => connection ?? (connection = new RoslibConnection());

        [CanBeNull] public static string MyId => Connection.MyId;
        public static bool IsConnected => Connection.ConnectionState == ConnectionState.Connected;

        [CanBeNull] public static IListener LogListener => instance != null ? instance.logListener : null;
        [CanBeNull] public static ISender LogSender => instance != null ? instance.logSender : null;

        void Awake()
        {
            instance = this;

            logSender = new Sender<Log>("/rosout");
            Logger.LogExternal += LogMessage;

            logListener = new Listener<Log>("/rosout_agg", Handler);
        }

        static bool Handler(Log msg)
        {
            LogMessageArrived?.Invoke(msg);
            return true;
        }

        void OnDestroy()
        {
            logListener.Stop();
            logSender.Stop();
            Connection.Stop();
            RosServerManager.Dispose();
            instance = null;
            connection = null;
            LogMessageArrived = null;
            Logger.LogExternal -= LogMessage;
        }

        void LogMessage(in LogMessage msg)
        {
            logSender.Publish(new Log
            (
                Header: (logSeq++, ""),
                Level: (byte) msg.Level,
                Name: Connection.MyId ?? "/iviz",
                Msg: msg.Message,
                File: Path.GetFileName(msg.File),
                Function: "",
                Line: (uint) msg.Line,
                Topics: Array.Empty<string>()
            ));
        }

        internal static void ReportBandwidthUp(long size)
        {
            instance.frameBandwidthUp += size;
        }

        internal static void ReportBandwidthDown(long size)
        {
            instance.frameBandwidthDown += size;
        }

        public static (long, long) CollectBandwidthReport()
        {
            (long, long) result = (instance.frameBandwidthDown, instance.frameBandwidthUp);
            instance.frameBandwidthDown = 0;
            instance.frameBandwidthUp = 0;
            return result;
        }
    }
}