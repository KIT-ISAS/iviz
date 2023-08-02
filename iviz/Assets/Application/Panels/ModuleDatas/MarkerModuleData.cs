#nullable enable

using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.Markers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays.Highlighters;
using Iviz.Tools;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="MarkerModulePanel"/> 
    /// </summary>
    public sealed class MarkerModuleData : ListenerModuleData
    {
        readonly MarkerListener listener;
        readonly MarkerModulePanel panel;

        public override ModulePanel Panel => panel;
        protected override ListenerController Listener => listener;
        public override ModuleType ModuleType => ModuleType.Marker;

        public override IConfiguration Configuration => listener.Config;

        public MarkerModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<MarkerModulePanel>(ModuleType.Marker);
            listener = new MarkerListener((MarkerConfiguration?)constructor.Configuration, Topic, constructor.Type);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;

            panel.OcclusionOnlyMode.Value = listener.RenderAsOcclusionOnly;
            panel.Tint.Value = listener.Tint;
            panel.Alpha.Value = listener.Tint.a;
            panel.Metallic.Value = listener.Metallic;
            panel.Smoothness.Value = listener.Smoothness;
            panel.MarkerDialog.DialogListener = listener;
            panel.HideButton.State = listener.Visible;
            panel.TriangleListFlipWinding.Value = listener.TriangleListFlipWinding;
            panel.ShowDescriptions.Value = listener.ShowDescriptions;
            //panel.PreferUdp.Value = listener.PreferUdp;

            panel.Mask.Options = GetMaskEntries();

            panel.TriangleListFlipWinding.ValueChanged += f => listener.TriangleListFlipWinding = f;
            panel.ShowDescriptions.ValueChanged += f => listener.ShowDescriptions = f;
            panel.OcclusionOnlyMode.ValueChanged += f => listener.RenderAsOcclusionOnly = f;
            panel.Tint.ValueChanged += f => listener.Tint = f.WithAlpha(panel.Alpha.Value);
            panel.Alpha.ValueChanged += f => listener.Tint = panel.Tint.Value.WithAlpha(f);
            panel.Smoothness.ValueChanged += f => listener.Smoothness = panel.Smoothness.Value;
            panel.Metallic.ValueChanged += f => listener.Metallic = panel.Metallic.Value;

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.Mask.ValueChanged += (i, _) =>
            {
                if (i == 0) return;
                listener.ToggleVisibleMask(i - 1);
                panel.Mask.Options = GetMaskEntries();
                panel.Mask.OverrideCaption("---");
            };
            panel.ResetButton.Clicked += listener.ResetController;
            //panel.PreferUdp.ValueChanged += f => listener.PreferUdp = f;

            listener.HighlightAll();
        }
        
        IEnumerable<string> GetMaskEntries()
        {
            var masks = listener.VisibleMask;
            yield return "---";
            for (int i = 0; i < masks.Count; i++)
            {
                string name = ((MarkerType) i).ToString();
                yield return masks[i] ? name : $"<color=#A0A0A0>({name})</color>";
            }
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonUtils.DeserializeObject<MarkerConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(IConfiguration.ModuleType):
                        break;
                    case nameof(MarkerConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(MarkerConfiguration.RenderAsOcclusionOnly):
                        listener.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(MarkerConfiguration.Tint):
                        listener.Tint = config.Tint.ToUnity();
                        break;
                    case nameof(MarkerConfiguration.TriangleListFlipWinding):
                        listener.TriangleListFlipWinding = config.TriangleListFlipWinding;
                        break;
                    case nameof(MarkerConfiguration.Metallic):
                        listener.Metallic = config.Metallic;
                        break;
                    case nameof(MarkerConfiguration.Smoothness):
                        listener.Smoothness = config.Smoothness;
                        break;
                    case nameof(MarkerConfiguration.ShowDescriptions):
                        listener.ShowDescriptions = config.ShowDescriptions;
                        break;
                    case nameof(MarkerConfiguration.VisibleMask):
                        listener.VisibleMask = config.VisibleMask;
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