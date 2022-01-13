#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    public abstract class ListenerModuleData : ModuleData
    {
        protected abstract ListenerController Listener { get; }
        /// ROS topic type
        public string TopicType { get; }
        /// ROS topic name
        public string Topic { get; }
        public override IController Controller => Listener;

        protected ListenerModuleData(string topic, string type)
        {
            Topic = topic ?? throw new ArgumentNullException(nameof(topic));
            TopicType = type ?? throw new ArgumentNullException(nameof(type));
            ModuleListPanel.RegisterDisplayedTopic(Topic);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            Listener.Dispose();
        }
        
        public override string ToString()
        {
            return $"[{ModuleType} Topic='{Topic}' [{TopicType}] guid={Configuration.Id}]";
        }
    }
}
