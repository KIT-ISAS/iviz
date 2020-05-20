using System.Globalization;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public abstract class DisplayableListenerData : DisplayData
    {
        ListenerPanelContents ListenerPanel => (ListenerPanelContents)Panel;

        protected abstract TopicListener Listener { get; }

        protected DisplayableListenerData(DisplayListPanel displayList, string topic, string type) :
            base(displayList, topic, type)
        {
            DisplayListPanel.RegisterDisplayedTopic(Topic);
        }

        /*
        public override void Start()
        {
            base.Start();
            DisplayListPanel.RegisterDisplayedTopic(Topic);
        }
        */

        public override void UpdatePanel()
        {
            string subscribedStatus;
            switch(Listener.Subscribed)
            {
                case TopicListener.SubscriberStatus.Active:
                    subscribedStatus = "On";
                    break;
                case TopicListener.SubscriberStatus.Inactive:
                    subscribedStatus = "Inactive";
                    break;
                case TopicListener.SubscriberStatus.Disconnected:
                    subscribedStatus = "Off";
                    break;
                default:
                    subscribedStatus = "?";
                    break;
            }
            string messagesPerSecond = Listener.MessagesPerSecond.ToString("0.##", CultureInfo.InvariantCulture);
            string minJitter = Listener.MessagesJitterMin.ToString("0.##", CultureInfo.InvariantCulture);
            string maxJitter = Listener.MessagesJitterMax.ToString("0.##", CultureInfo.InvariantCulture);

            ListenerPanel.Stats.Label = $"{subscribedStatus} | {messagesPerSecond} Hz | {minJitter} - {maxJitter} sec";
        }

        public override void Stop()
        {
            base.Stop();
            Listener.Stop();
            UnityEngine.Object.Destroy(Listener.gameObject);
        }
    }
}
