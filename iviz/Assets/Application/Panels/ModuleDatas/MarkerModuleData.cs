#nullable enable

using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerPanelContents"/> 
    /// </summary>
    public sealed class MarkerModuleData : ListenerModuleData
    {
        readonly MarkerListener listener;
        readonly MarkerPanelContents panel;

        public override DataPanelContents Panel => panel;
        protected override ListenerController Listener => listener;
        public override ModuleType ModuleType => ModuleType.Marker;

        public override IConfiguration Configuration => listener.Config;

        public MarkerModuleData(ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<MarkerConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<MarkerConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<MarkerPanelContents>(ModuleType.Marker);
            listener = new MarkerListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            else
            {
                listener.Config = (MarkerConfiguration)constructor.Configuration;
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
            panel.ShowDescriptions.Value = listener.ShowDescriptions;
            panel.PreferUdp.Value = listener.PreferUdp;

            panel.Mask.Options = listener.GetMaskEntries();

            panel.Tint.ValueChanged += f => listener.Tint = f.WithAlpha(panel.Alpha.Value);
            panel.Alpha.ValueChanged += f => listener.Tint = panel.Tint.Value.WithAlpha(f);
            panel.OcclusionOnlyMode.ValueChanged += f => listener.RenderAsOcclusionOnly = f;
            panel.TriangleListFlipWinding.ValueChanged += f => listener.TriangleListFlipWinding = f;
            panel.ShowDescriptions.ValueChanged += f => listener.ShowDescriptions = f;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.Mask.ValueChanged += (i, _) =>
            {
                if (i == 0) return;
                listener.ToggleMaskEntry(i - 1);
                panel.Mask.Options = listener.GetMaskEntries();
                panel.Mask.OverrideCaption("---");
            };
            panel.PreferUdp.ValueChanged += f => listener.PreferUdp = f;

            HighlightAll();
        }

        void HighlightAll()
        {
            const int maxHighlights = 50;
            
            var boundsToShow = listener.GetAllBounds()
                .Where(bounds => !bounds.ShowDescription)
                .Take(maxHighlights);
            
            foreach (var bounds in boundsToShow)
            {
                FAnimator.Start(new BoundsHighlighter(bounds));
            }
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
                        listener.Tint = config.Tint.ToUnityColor();
                        break;
                    case nameof(MarkerConfiguration.TriangleListFlipWinding):
                        listener.TriangleListFlipWinding = config.TriangleListFlipWinding;
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
            config.Markers.Add(listener.Config);
        }
    }
}