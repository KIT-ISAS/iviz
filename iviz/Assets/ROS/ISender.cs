#nullable enable

using System;
using Iviz.Core;
using Iviz.Msgs;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Ros
{
    public interface ISender
    {
        string Topic { get; }
        string Type { get; }
        int Id { get; }
        RosSenderStats Stats { get; }
        int NumSubscribers { get; }
        void Stop();
        void Publish(IMessage msg);
    }

    public sealed class Sender<T> : ISender where T : IMessage
    {
        static RoslibConnection Connection => ConnectionManager.Connection;

        int lastMsgBytes;
        int lastMsgCounter;
        int totalMsgCounter;

        public Sender(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException("Invalid topic!", nameof(topic));
            }

            Topic = topic;
            Type = BuiltIns.GetMessageType(typeof(T));

            Logger.Info($"Advertising <b>{topic}</b> <i>[{Type}]</i>.");
            GameThread.EverySecond += UpdateStats;
            Connection.Advertise(this);
        }

        public string Topic { get; }
        public string Type { get; }
        public int Id { get; internal set; }
        public RosSenderStats Stats { get; private set; }
        public int NumSubscribers { get; private set; }

        void ISender.Publish(IMessage msg)
        {
            Publish((T)msg);
        }

        public void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            Connection.Unadvertise(this);
        }

        public void Publish(in T msg)
        {
            Connection.Publish(this, msg);

            totalMsgCounter++;
            lastMsgCounter++;
            lastMsgBytes += msg.RosMessageLength;
        }

        public void Reset()
        {
            Connection.Unadvertise(this);
            Connection.Advertise(this);
        }

        void UpdateStats()
        {
            if (lastMsgCounter == 0)
            {
                Stats = default;
                NumSubscribers = Connection.GetNumSubscribers(this);
                return;
            }

            Stats = new RosSenderStats(totalMsgCounter, lastMsgCounter, lastMsgBytes);

            ConnectionManager.ReportBandwidthUp(lastMsgBytes);

            lastMsgBytes = 0;
            lastMsgCounter = 0;

            NumSubscribers = Connection.GetNumSubscribers(this);
        }

        public override string ToString() => $"[Sender {Topic} [{Type}]]";
    }
}