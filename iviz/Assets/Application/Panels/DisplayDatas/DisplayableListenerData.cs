using System.Globalization;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public abstract class DisplayableListenerData : DisplayData
    {
        ListenerPanelContents ListenerPanel => (ListenerPanelContents)Panel;

        protected abstract TopicListener Listener { get; }
        public override IController Controller => Listener;

        protected DisplayableListenerData(DisplayListPanel displayList, string topic, string type) :
            base(displayList, topic, type)
        {
            DisplayListPanel.RegisterDisplayedTopic(Topic);
        }

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
            string messagesPerSecond = Listener.MessagesPerSecond.ToString("0.##", UnityUtils.Culture);
            string minJitter = Listener.MessagesJitterMin.ToString("0.##", UnityUtils.Culture);
            string maxJitter = Listener.MessagesJitterMax.ToString("0.##", UnityUtils.Culture);

            ListenerPanel.Stats.Label = $"{subscribedStatus} | {messagesPerSecond} Hz | {minJitter} - {maxJitter} sec";
        }

        protected string SanitizedTopicText()
        {
            return RosUtils.SanitizedText(Topic, MaxTextRowLength);
        }

        public override void Stop()
        {
            base.Stop();
            Listener.Stop();
            UnityEngine.Object.Destroy(Listener.gameObject);
        }
    }
}
