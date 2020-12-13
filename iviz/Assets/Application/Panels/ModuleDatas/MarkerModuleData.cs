using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerPanelContents"/> 
    /// </summary>
    public sealed class MarkerModuleData : ListenerModuleData
    {
        [NotNull] readonly MarkerListener listener;
        [NotNull] readonly MarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        protected override ListenerController Listener => listener;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.Marker;

        public override IConfiguration Configuration => listener.Config;

        public MarkerModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<MarkerConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<MarkerConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<MarkerPanelContents>(Resource.ModuleType.Marker);
            listener = new MarkerListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            else
            {
                listener.Config = (MarkerConfiguration) constructor.Configuration;
            }

            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;

            panel.OcclusionOnlyMode.Value = listener.RenderAsOcclusionOnly;
            panel.Tint.Value = listener.Tint;
            panel.Alpha.Value = listener.Tint.a;
            panel.Marker.MarkerListener = listener;
            panel.HideButton.State = listener.Visible;
            panel.TriangleListFlipWinding.Value = listener.TriangleListFlipWinding;

            panel.Tint.ValueChanged += f =>
            {
                Color color = f;
                color.a = panel.Alpha.Value;
                listener.Tint = color;
            };
            panel.Alpha.ValueChanged += f =>
            {
                Color color = panel.Tint.Value;
                color.a = f;
                listener.Tint = color;
            };
            panel.OcclusionOnlyMode.ValueChanged += f =>
            {
                listener.RenderAsOcclusionOnly = f;
            };
            panel.TriangleListFlipWinding.ValueChanged += f =>
            {
                listener.TriangleListFlipWinding = f;
            };

            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.HideButton.Clicked += () =>
            {
                listener.Visible = !listener.Visible;
                panel.HideButton.State = listener.Visible;
                UpdateModuleButton();
            };
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<MarkerConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(MarkerConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(MarkerConfiguration.RenderAsOcclusionOnly):
                        listener.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(MarkerConfiguration.Tint):
                        listener.Tint = config.Tint;
                        break;
                    case nameof(MarkerConfiguration.TriangleListFlipWinding):
                        listener.TriangleListFlipWinding = config.TriangleListFlipWinding;
                        break;
                    default:
                        Logger.External($"{this}: Unknown field '{field}'", LogLevel.Warn);
                        break;
                }
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Markers.Add(listener.Config);
        }
    }
}