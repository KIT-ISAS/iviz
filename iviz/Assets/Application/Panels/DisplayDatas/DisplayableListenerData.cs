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
            int numPublishers = Listener.NumPublishers;
            if (numPublishers == -1)
            {
                subscribedStatus = "Off";
            }
            else
            {
                subscribedStatus = "On → " + numPublishers;
            }
            string messagesPerSecond = Listener.MessagesPerSecond.ToString(UnityUtils.Culture);
            string kbPerSecond = (Listener.BytesPerSecond * 0.001f).ToString("#,0.#", UnityUtils.Culture);

            ListenerPanel.Stats.Label = $"{subscribedStatus} | {messagesPerSecond} Hz | {kbPerSecond} kbs";
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
