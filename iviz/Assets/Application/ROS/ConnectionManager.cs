//#define PUBLISH_LOG

using Iviz.Msgs;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Iviz.Controllers
{
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
    }

    public class ConnectionManager : MonoBehaviour
    {
        static ConnectionManager Instance;

        public static RosConnection Connection { get; private set; }

#if PUBLISH_LOG
        RosSender<Log> sender;
#endif

        int collectedUp, collectedDown;

        void Awake()
        {
            Instance = this;
            Connection = new RoslibConnection();

#if PUBLISH_LOG
            Logger.Log += LogMessage;
            sender = new RosSender<Log>("/rosout");
#endif
        }

        void OnDestroy()
        {
            Connection?.Stop();
            Connection = null;

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

        public static string MyId => Connection?.MyId;
        public static Uri MyUri => Connection?.MyUri;
        public static Uri MasterUri => Connection?.MasterUri;

        public static ConnectionState ConnectionState => Connection?.ConnectionState ?? ConnectionState.Disconnected;
        public static bool IsConnected => ConnectionState == ConnectionState.Connected;

        public static void Subscribe<T>(RosListener<T> listener) where T : IMessage, new()
            => Connection.Subscribe(listener);

        public static void Unsubscribe(RosListener subscriber) => Connection.Unsubscribe(subscriber);

        public static void Advertise<T>(RosSender<T> advertiser) where T : IMessage
            => Connection.Advertise(advertiser);

        public static void Unadvertise(RosSender advertiser) => Connection.Unadvertise(advertiser);
        public static void Publish(RosSender advertiser, IMessage msg) => Connection.Publish(advertiser, msg);

        public static void AdvertiseService<T>(string service, Action<T> callback) where T : IService, new()
            => Connection.AdvertiseService(service, callback);

        public static ReadOnlyCollection<BriefTopicInfo> GetSystemPublishedTopics() =>
            Connection.GetSystemPublishedTopics();

        public static ReadOnlyCollection<string> GetSystemParameterList() => Connection.GetSystemParameterList();

        public static void ReportBandwidthUp(int size)
        {
            Instance.collectedUp += size;
        }

        public static void ReportBandwidthDown(int size)
        {
            Instance.collectedDown += size;
        }

        public static (int, int) CollectBandwidthReport()
        {
            (int, int) result = (Instance.collectedDown, Instance.collectedUp);
            Instance.collectedDown = 0;
            Instance.collectedUp = 0;
            return result;
        }
    }
}