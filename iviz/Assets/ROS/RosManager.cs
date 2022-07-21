﻿#nullable enable

using System;
using IServiceProvider = Iviz.Displays.IServiceProvider;

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

        public static IServiceProvider ServiceProvider => Connection;
        public static string? MyId => instance?.connection.MyId;
        public static bool IsConnected => instance?.connection.IsConnected ?? false;
        public static bool HasInstance => instance != null;
        public static RoslibConnection Connection => instance?.connection ?? throw NewDisposeException();
        public static RosOutLogger Logger => instance?.logger ?? throw NewDisposeException();
        public static RosServerManager Server => instance?.server ?? throw NewDisposeException();
        public static RosModelService ModelService => instance?.modelService ?? throw NewDisposeException();

        readonly RoslibConnection connection;
        readonly RosOutLogger logger;
        readonly RosServerManager server;
        readonly RosModelService modelService;

        long bandwidthDownInFrame;
        long bandwidthUpInFrame;

        public RosManager()
        {
            instance = this;
            connection = new RoslibConnection();
            logger = new RosOutLogger();
            server = new RosServerManager();
            modelService = new RosModelService();
        }

        public void Dispose()
        {
            connection.Dispose();
            logger.Dispose();
            server.Dispose();
            modelService.Dispose();
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