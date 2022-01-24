#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib;
using UnityEngine;

namespace Iviz.Ros
{
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected
    }

    public sealed class ConnectionManager
    {
        static ConnectionManager? instance;

        public delegate void LogDelegate(in Log log);

        public static event LogDelegate? LogMessageArrived;

        public static LogLevel MinLogLevel { get; set; } = LogLevel.Info;
        public static IExternalServiceProvider ServiceProvider => Connection;
        public static string? MyId => instance?.connection.MyId;
        public static bool IsConnected => instance?.connection.ConnectionState == ConnectionState.Connected;
        public static IListener? LogListener => instance?.logListener;
        public static ISender? LogSender => instance?.logSender;

        public static RoslibConnection Connection => instance?.connection ??
                                                     throw new ObjectDisposedException(
                                                         "Connection manager has already been disposed");

        readonly Listener<Log> logListener;
        readonly Sender<Log> logSender;
        readonly RoslibConnection connection;

        long frameBandwidthDown;
        long frameBandwidthUp;
        uint logSeq;

        public ConnectionManager()
        {
            instance = this;
            
            Debug.Log("ConnectionManager: Creating");

            connection = new RoslibConnection();
            logSender = new Sender<Log>("/rosout");
            logListener = new Listener<Log>("/rosout_agg", Handler, RosTransportHint.PreferUdp);

            RosLogger.LogExternal += LogMessage;
        }

        static bool Handler(Log msg)
        {
            LogMessageArrived?.Invoke(msg);
            return true;
        }

        public void Dispose()
        {
            logListener.Dispose();
            logSender.Dispose();
            Connection.Dispose();

            LogMessageArrived = null;
            RosLogger.LogExternal -= LogMessage;

            instance = null;
        }

        void LogMessage(in LogMessage msg)
        {
            if (msg.Level < MinLogLevel)
            {
                return;
            }

            var logMessage = new Log
            {
                Header = new Header(logSeq++, GameThread.TimeNow, ""),
                Level = (byte)msg.Level,
                Name = Connection.MyId ?? "/iviz",
                Msg = msg.Message
            };
            logSender.Publish(logMessage);
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