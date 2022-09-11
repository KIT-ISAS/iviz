﻿#nullable enable

using System;

namespace Iviz.Ros
{
    /// <summary>
    /// Central repository of all things ROS.
    /// </summary>
    public sealed class RosManager
    {
        static RosManager? instance;

        static Exception NewDisposeException() =>
            new ObjectDisposedException("The ROS manager has already been disposed");

        public static string? MyId => instance?.connection.MyId;
        public static bool IsConnected => instance?.connection.IsConnected ?? false;
        public static bool HasInstance => instance != null;
        public static IRosProvider Connection => RosConnection;
        public static RosOutLogger Logger => instance?.logger ?? throw NewDisposeException();
        public static RosServerManager Server => instance?.server ?? throw NewDisposeException();
        internal static RosConnection RosConnection => instance?.connection ?? throw NewDisposeException();

        readonly RosConnection connection;
        readonly RosOutLogger logger;
        readonly RosServerManager server;

        long bandwidthDownInFrame;
        long bandwidthUpInFrame;

        public RosManager()
        {
            instance = this;
            connection = new RosConnection();
            logger = new RosOutLogger();
            server = new RosServerManager();
        }

        public void Dispose()
        {
            connection.Dispose();
            logger.Dispose();
            server.Dispose();
            instance = null;
        }

        internal static void ReportBandwidthUp(long size)
        {
            if (instance != null)
            {
                instance.bandwidthUpInFrame += size;
            }
        }

        internal static void ReportBandwidthDown(long size)
        {
            if (instance != null)
            {
                instance.bandwidthDownInFrame += size;
            }
        }

        public static (long, long) CollectBandwidthReport()
        {
            if (instance == null)
            {
                return default;
            }

            var result = (instance.bandwidthDownInFrame, instance.bandwidthUpInFrame);
            instance.bandwidthDownInFrame = 0;
            instance.bandwidthUpInFrame = 0;
            return result;
        }

        public override string ToString() => $"[{nameof(RosManager)}]";
    }
}