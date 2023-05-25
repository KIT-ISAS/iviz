﻿#nullable enable

using System;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;

namespace Iviz.App
{
    public abstract class ListenerModuleData : ModuleData
    {
        protected abstract ListenerController Listener { get; }

        /// ROS topic type
        public string TopicType => Listener.Listener.Type;
        /// ROS topic name
        public string Topic { get; }
        public override Controller Controller => Listener;

        protected ListenerModuleData(string topic)
        {
            ThrowHelper.ThrowIfNull(topic, nameof(topic));
            Topic = topic;
            ModuleListPanel.RegisterDisplayedTopic(Topic);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            try
            {
                Listener.Dispose();
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to dispose listener", e);
            }              
        }
        
        public override string ToString()
        {
            return $"[{GetType().Name} Topic='{Topic}' [{TopicType}] id='{Configuration.Id}']";
        }
    }
}
