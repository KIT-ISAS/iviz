using Iviz.Controllers;

namespace Iviz.App
{
    public abstract class ListenerModuleData : ModuleData
    {
        protected abstract ListenerController Listener { get; }
        public override IController Controller => Listener;

        protected ListenerModuleData(ModuleListPanel moduleList, string topic, string type) :
            base(moduleList, topic, type)
        {
            ModuleListPanel.RegisterDisplayedTopic(Topic);
        }

        public override void Stop()
        {
            base.Stop();
            Listener.StopController();
            //UnityEngine.Object.Destroy(Listener.gameObject);
        }
    }
}
