#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridMapPanelContents"/> 
    /// </summary>
    public sealed class GridMapModuleData : ListenerModuleData
    {
        readonly GridMapListener listener;
        readonly GridMapPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.GridMap;
        public override IConfiguration Configuration => listener.Config;

        public GridMapModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<GridMapPanelContents>(ModuleType.GridMap);
            listener = new GridMapListener(this, (GridMapConfiguration?) constructor.Configuration, Topic);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;

            string minIntensityStr = listener.MeasuredIntensityBounds.x.ToString("#,0.##", UnityUtils.Culture);
            string maxIntensityStr = listener.MeasuredIntensityBounds.y.ToString("#,0.##", UnityUtils.Culture);
            panel.Description.Text = $"Min Intensity: {minIntensityStr} Max: {maxIntensityStr}";

            panel.Colormap.Index = (int) listener.Colormap;

            panel.IntensityChannel.Options = listener.FieldNames;
            if (listener.FieldNames.Count != 0)
            {
                panel.IntensityChannel.Value = listener.IntensityChannel;
            }

            panel.HideButton.State = listener.Visible;

            panel.ForceMinMax.Value = listener.ForceMinMax;
            panel.MinIntensity.Value = listener.MinIntensity;
            panel.MaxIntensity.Value = listener.MaxIntensity;
            panel.MinIntensity.Interactable = listener.ForceMinMax;
            panel.MaxIntensity.Interactable = listener.ForceMinMax;
            panel.FlipMinMax.Value = listener.FlipMinMax;
            panel.Alpha.Value = listener.Tint.a;

            panel.IntensityChannel.ValueChanged += (_, s) => { listener.IntensityChannel = s; };
            panel.Colormap.ValueChanged += (i, _) => { listener.Colormap = (ColormapId) i; };
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.ForceMinMax = f;
                panel.MinIntensity.Interactable = f;
                panel.MaxIntensity.Interactable = f;
            };
            panel.FlipMinMax.ValueChanged += f => listener.FlipMinMax = f;
            panel.MinIntensity.ValueChanged += f => listener.MinIntensity = f;
            panel.MaxIntensity.ValueChanged += f => listener.MaxIntensity = f;
            panel.Alpha.ValueChanged += f => listener.Tint = Color.white.WithAlpha(f);
            panel.Description.Text = listener.Description;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();

            panel.IntensityChannel.Options = listener.FieldNames;
            if (listener.FieldNames.Count != 0 && listener.IntensityChannel.Length == 0)
            {
                listener.IntensityChannel = listener.FieldNames[0];
                panel.IntensityChannel.Value = listener.IntensityChannel;
            }

            panel.Description.Text = listener.Description;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<GridMapConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(GridMapConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(GridMapConfiguration.Colormap):
                        listener.Colormap = config.Colormap;
                        break;
                    case nameof(GridMapConfiguration.ForceMinMax):
                        listener.ForceMinMax = config.ForceMinMax;
                        break;
                    case nameof(GridMapConfiguration.MinIntensity):
                        listener.MinIntensity = config.MinIntensity;
                        break;
                    case nameof(GridMapConfiguration.MaxIntensity):
                        listener.MaxIntensity = config.MaxIntensity;
                        break;
                    case nameof(GridMapConfiguration.FlipMinMax):
                        listener.FlipMinMax = config.FlipMinMax;
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
            config.GridMaps.Add(listener.Config);
        }
    }
}