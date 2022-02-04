using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="OctomapModulePanel"/> 
    /// </summary>
    public sealed class OctomapModuleData : ListenerModuleData
    {
        [NotNull] readonly OctomapListener listener;
        [NotNull] readonly OctomapModulePanel panel;

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.Octomap;
        public override IConfiguration Configuration => listener.Config;


        public OctomapModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<OctomapModulePanel>(ModuleType.Octomap);
            listener = new OctomapListener();
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (OctomapConfiguration)constructor.Configuration;
            }

            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.HideButton.State = listener.Visible;
            panel.OcclusionOnlyMode.Value = listener.RenderAsOcclusionOnly;
            panel.Tint.Value = listener.Tint;
            panel.MaxDepth.Value = listener.MaxDepth;

            panel.Tint.ValueChanged += f => listener.Tint = f.WithAlpha(1);
            panel.OcclusionOnlyMode.ValueChanged += f => listener.RenderAsOcclusionOnly = f;
            panel.MaxDepth.ValueChanged += f => listener.MaxDepth = (int)f;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<OctomapConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(OctomapConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(OctomapConfiguration.RenderAsOcclusionOnly):
                        listener.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(OctomapConfiguration.Tint):
                        listener.Tint = config.Tint.ToUnityColor();
                        break;
                    case nameof(OctomapConfiguration.MaxDepth):
                        listener.MaxDepth = config.MaxDepth;
                        break;

                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Octomaps.Add(listener.Config);
        }
    }
}