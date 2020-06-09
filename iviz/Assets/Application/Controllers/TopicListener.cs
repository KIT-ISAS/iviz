using System.Collections.Generic;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class TopicListener : MonoBehaviour, IController, IHasFrame
    {
        public RosListener Listener { get; protected set; }

        public abstract TFFrame Frame { get; }

        public int NumPublishers =>
            (!ConnectionManager.Connected || Listener == null) ? -1 : Listener.NumPublishers;

        public string Topic => Listener?.Topic;
        public int MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        public float MessagesJitterMax => Listener?.Stats.JitterMax ?? 0;
        public float MessagesJitterMin => Listener?.Stats.JitterMin ?? 0;
        public int BytesPerSecond => Listener?.Stats.BytesPerSecond ?? 0;

        public abstract DisplayData DisplayData { get; set; }

        public virtual void StartListening()
        {
        }

        public virtual void Stop()
        {
            Listener?.Stop();
            Listener = null;
        }
    }

}