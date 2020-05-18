using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class TopicListener : MonoBehaviour
    {
        public RosListener Listener { get; protected set; }

        public enum SubscriberStatus
        {
            Disconnected,
            Inactive,
            Active
        }

        public SubscriberStatus Subscribed =>
            (Listener == null) ? SubscriberStatus.Disconnected :
            Listener.HasPublishers ? SubscriberStatus.Active : SubscriberStatus.Inactive;

        public string Topic => Listener?.Topic;
        public float MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        public float MessagesJitterMax => Listener?.Stats.JitterMax ?? 0;
        public float MessagesJitterMin => Listener?.Stats.JitterMin ?? 0;


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