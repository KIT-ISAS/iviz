using System.Globalization;

namespace Iviz.App
{
    public abstract class DisplayableListenerData : DisplayData
    {
        ListenerPanelContents ListenerPanel => Panel as ListenerPanelContents;

        public abstract TopicListener Listener { get; }

        public override void Start()
        {
            base.Start();
            DisplayListPanel.RegisterDisplayedTopic(Topic);
        }

        public override void UpdatePanel()
        {
            string subscribedStatus = Listener.Subscribed ? "On" : "Off";
            string messagesPerSecond = Listener.MessagesPerSecond.ToString("0.##", CultureInfo.InvariantCulture);
            string minJitter = Listener.MessagesJitterMin.ToString("0.##", CultureInfo.InvariantCulture);
            string maxJitter = Listener.MessagesJitterMax.ToString("0.##", CultureInfo.InvariantCulture);

            ListenerPanel.Stats.Label = $"{subscribedStatus} | {messagesPerSecond} Hz | {minJitter} - {maxJitter} sec";
        }
    }
}
