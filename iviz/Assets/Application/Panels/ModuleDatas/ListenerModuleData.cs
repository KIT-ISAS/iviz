using System.Globalization;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public abstract class ListenerModuleData : ModuleData
    {
        protected abstract ListenerController Listener { get; }
        public override IController Controller => Listener;

        protected ListenerModuleData(DisplayListPanel moduleList, string topic, string type) :
            base(moduleList, topic, type)
        {
            ModuleListPanel.RegisterDisplayedTopic(Topic);
        }

        public override void Stop()
        {
            base.Stop();
            Listener.Stop();
            //UnityEngine.Object.Destroy(Listener.gameObject);
        }
    }
}
