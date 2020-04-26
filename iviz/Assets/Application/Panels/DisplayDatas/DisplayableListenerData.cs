using System.Globalization;

namespace Iviz.App
{
    public abstract class DisplayableListenerData : DisplayData
    {
        ListenerPanelContents ListenerPanel => Panel as ListenerPanelContents;

        public abstract DisplayableListener Display { get; }

        public override void Start()
        {
            base.Start();
            DisplayListPanel.RegisterDisplayedTopic(Topic);
        }

        public override void UpdatePanel()
        {
            string subscribedStatus = Display.Subscribed ? "On" : "Off";
            string messagesPerSecond = Display.MessagesPerSecond.ToString("0.##", CultureInfo.InvariantCulture);
            string minJitter = Display.MessagesJitterMin.ToString("0.##", CultureInfo.InvariantCulture);
            string maxJitter = Display.MessagesJitterMax.ToString("0.##", CultureInfo.InvariantCulture);

            ListenerPanel.Stats.Label = $"{subscribedStatus} | {messagesPerSecond} Hz | {minJitter} - {maxJitter} sec";
        }
    }
}
