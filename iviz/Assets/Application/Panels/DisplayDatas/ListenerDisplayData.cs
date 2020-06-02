using System.Globalization;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public abstract class ListenerDisplayData : DisplayData
    {
        protected abstract TopicListener Listener { get; }
        public override IController Controller => Listener;

        protected ListenerDisplayData(DisplayListPanel displayList, string topic, string type) :
            base(displayList, topic, type)
        {
            DisplayListPanel.RegisterDisplayedTopic(Topic);
        }

        public override void Stop()
        {
            base.Stop();
            Listener.Stop();
            UnityEngine.Object.Destroy(Listener.gameObject);
        }
    }
}
