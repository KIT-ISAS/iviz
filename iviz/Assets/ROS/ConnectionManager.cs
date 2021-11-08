#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
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
        static ConnectionManager? instance;
        static RoslibConnection? connection;

        public delegate void LogDelegate(in Log log);

        public static event LogDelegate? LogMessageArrived;
        public static LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public static IExternalServiceProvider ServiceProvider => Connection;

        long frameBandwidthDown;
        long frameBandwidthUp;
        uint logSeq;
        Listener<Log>? logListener;
        Sender<Log>? logSender;


        public static RoslibConnection Connection
        {
            get
            {
                if (connection != null)
                {
                    return connection;
                }

                if (instance == null)
                {
                    throw new ObjectDisposedException("Connection manager has already been disposed");
                }

                return connection = new RoslibConnection();
            }
        }

        public static string? MyId => Connection.MyId;
        public static bool IsConnected => Connection.ConnectionState == ConnectionState.Connected;
        public static IListener? LogListener => instance != null ? instance.logListener : null;
        public static ISender? LogSender => instance != null ? instance.logSender : null;

        void Awake()
        {
            instance = this;

            logSender = new Sender<Log>("/rosout");
            Logger.LogExternal += LogMessage;

            logListener = new Listener<Log>("/rosout_agg", Handler, RosTransportHint.PreferUdp);
        }

        static bool Handler(Log msg)
        {
            LogMessageArrived?.Invoke(msg);
            return true;
        }

        void OnDestroy()
        {
            logListener?.Stop();
            logSender?.Stop();
            Connection.Stop();
            RosServerManager.Dispose();
            instance = null;
            connection = null;
            LogMessageArrived = null;
            Logger.LogExternal -= LogMessage;
        }

        void LogMessage(in LogMessage msg)
        {
            if (msg.Level < MinLogLevel)
            {
                return;
            }

            var logMessage = new Log
            {
                Header = (logSeq++, ""),
                Level = (byte) msg.Level,
                Name = Connection.MyId ?? "/iviz",
                Msg = msg.Message
            };
            logSender?.Publish(logMessage);
        }

        internal static void ReportBandwidthUp(long size)
        {
            if (instance != null)
            {
                instance.frameBandwidthUp += size;
            }
        }

        internal static void ReportBandwidthDown(long size)
        {
            if (instance != null)
            {
                instance.frameBandwidthDown += size;
            }
        }

        public static (long bandwidthDown, long bandwidthUp) CollectBandwidthReport()
        {
            if (instance == null)
            {
                return default;
            }

            var result = (instance.frameBandwidthDown, instance.frameBandwidthUp);
            instance.frameBandwidthDown = 0;
            instance.frameBandwidthUp = 0;
            return result;
        }
    }
}