using System.Collections.Generic;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class TopicListener : MonoBehaviour, IController
    {
        public RosListener Listener { get; protected set; }
        public IList<RosSender> Senders { get; protected set; }

        public int NumPublishers =>
            (!ConnectionManager.Connected || Listener == null) ? -1 : Listener.NumPublishers;

        public string Topic => Listener?.Topic;
        public int MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        public float MessagesJitterMax => Listener?.Stats.JitterMax ?? 0;
        public float MessagesJitterMin => Listener?.Stats.JitterMin ?? 0;
        public int BytesPerSecond => Listener?.Stats.BytesPerSecond ?? 0;

        public virtual void StartListening()
        {
            GameThread.EverySecond += UpdateStats;
        }

        public virtual void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            Listener?.Stop();
            Listener = null;
        }

        void UpdateStats()
        {
            Listener?.UpdateStats();
        }
    }

}