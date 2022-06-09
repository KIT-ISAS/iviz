#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerModulePanel"/> 
    /// </summary>
    public sealed class InteractiveMarkerModuleData : ListenerModuleData, IInteractableModuleData
    {
        readonly InteractiveMarkerListener listener;
        readonly InteractiveMarkerModulePanel panel;

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.InteractiveMarker;

        public override IConfiguration Configuration => listener.Config;

        public InteractiveMarkerModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<InteractiveMarkerModulePanel>(
                ModuleType.InteractiveMarker);
            listener = new InteractiveMarkerListener((InteractiveMarkerConfiguration?)constructor.Configuration, Topic);
            Interactable = ModuleListPanel.SceneInteractable;
            UpdateModuleButton();
        }

        public bool Interactable
        {
            set => listener.Interactable = value;
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.FullListener.Listener = listener.FullListener;
            panel.DescriptionsVisible.Value = listener.DescriptionsVisible;
            panel.Sender.Set(listener.Publisher);
            panel.Marker.MarkerListener = listener;
            panel.HideButton.State = listener.Visible;

            panel.TriangleListFlipWinding.Value = listener.TriangleListFlipWinding;
            panel.Alpha.Value = listener.Alpha;
            panel.Smoothness.Value = listener.Smoothness;
            panel.Metallic.Value = listener.Metallic;
            panel.Tint.Value = listener.Tint;

            panel.DescriptionsVisible.ValueChanged += f => listener.DescriptionsVisible = f;
            panel.TriangleListFlipWinding.ValueChanged += f => listener.TriangleListFlipWinding = f;
            panel.Alpha.ValueChanged += f => listener.Alpha = f;
            panel.Tint.ValueChanged += f => listener.Tint = f;
            panel.Smoothness.ValueChanged += f => listener.Smoothness = f;
            panel.Metallic.ValueChanged += f => listener.Metallic = f;
            
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.InteractiveMarkers.Add(listener.Config);
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<InteractiveMarkerConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(InteractiveMarkerConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(InteractiveMarkerConfiguration.DescriptionsVisible):
                        listener.DescriptionsVisible = config.DescriptionsVisible;
                        break;
                    case nameof(InteractiveMarkerConfiguration.Tint):
                        listener.Tint = config.Tint.ToUnity();
                        break;
                    case nameof(InteractiveMarkerConfiguration.Alpha):
                        listener.Alpha = config.Alpha;
                        break;
                    case nameof(InteractiveMarkerConfiguration.Smoothness):
                        listener.Smoothness = config.Smoothness;
                        break;
                    case nameof(InteractiveMarkerConfiguration.Metallic):
                        listener.Metallic = config.Metallic;
                        break;
                    case nameof(InteractiveMarkerConfiguration.TriangleListFlipWinding):
                        listener.TriangleListFlipWinding = config.TriangleListFlipWinding;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }
    }
}