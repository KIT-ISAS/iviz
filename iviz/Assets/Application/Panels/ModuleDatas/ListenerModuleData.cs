#nullable enable

using Iviz.Common;
using Iviz.Controllers;

namespace Iviz.App
{
    public abstract class ListenerModuleData : ModuleData
    {
        protected abstract ListenerController Listener { get; }
        public override IController Controller => Listener;

        protected ListenerModuleData(string topic, string type) : base(topic, type)
        {
            ModuleListPanel.RegisterDisplayedTopic(Topic);
        }

        public override void Stop()
        {
            base.Stop();
            Listener.Dispose();
        }
        
        public override string ToString()
        {
            return $"[{ModuleType} Topic='{Topic}' [{Type}] guid={Configuration.Id}]";
        }
    }
}
