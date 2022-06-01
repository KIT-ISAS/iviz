#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="LaserScanModulePanel"/> 
    /// </summary>
    public sealed class LaserScanModuleData : ListenerModuleData
    {
        readonly LaserScanListener listener;
        readonly LaserScanModulePanel panel;

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.LaserScan;
        public override IConfiguration Configuration => listener.Config;

        public LaserScanModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<LaserScanModulePanel>(ModuleType.LaserScan);
            listener = new LaserScanListener((LaserScanConfiguration?)constructor.Configuration, Topic);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.NumPoints.Text = BuildDescriptionString();

            panel.Colormap.Index = (int)listener.Colormap;
            panel.PointSize.Value = listener.PointSize;
            panel.UseIntensity.Value = listener.UseIntensity;
            panel.HideButton.State = listener.Visible;

            panel.ForceMinMax.Value = listener.ForceMinMax;
            panel.MinIntensity.Value = listener.MinIntensity;
            panel.MaxIntensity.Value = listener.MaxIntensity;
            panel.MinIntensity.Interactable = listener.ForceMinMax;
            panel.MaxIntensity.Interactable = listener.ForceMinMax;
            panel.FlipMinMax.Value = listener.FlipMinMax;

            panel.UseLines.Value = listener.UseLines;

            panel.UseIntensity.ValueChanged += f => { listener.UseIntensity = f; };
            panel.PointSize.ValueChanged += f => { listener.PointSize = f; };
            panel.Colormap.ValueChanged += (i, _) => { listener.Colormap = (ColormapId)i; };
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.ForceMinMax = f;
                panel.MinIntensity.Interactable = f;
                panel.MaxIntensity.Interactable = f;
            };
            panel.FlipMinMax.ValueChanged += f => { listener.FlipMinMax = f; };
            panel.MinIntensity.ValueChanged += f => { listener.MinIntensity = f; };
            panel.MaxIntensity.ValueChanged += f => { listener.MaxIntensity = f; };
            panel.UseLines.ValueChanged += f => { listener.UseLines = f; };
        }


        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.NumPoints.Text = BuildDescriptionString();
        }

        string BuildDescriptionString()
        {
            string minIntensityStr = UnityUtils.FormatFloat(listener.MeasuredIntensityBounds.x);
            string maxIntensityStr = UnityUtils.FormatFloat(listener.MeasuredIntensityBounds.y);
            return $"<b>{listener.Size.ToString("N0")} Points</b>\n" +
                   (listener.Size == 0
                       ? "Empty"
                       : $"[{minIntensityStr} .. {maxIntensityStr}]");
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<LaserScanConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(LaserScanConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(LaserScanConfiguration.PointSize):
                        listener.PointSize = config.PointSize;
                        break;
                    case nameof(LaserScanConfiguration.Colormap):
                        listener.Colormap = config.Colormap;
                        break;
                    case nameof(LaserScanConfiguration.UseIntensity):
                        listener.UseIntensity = config.UseIntensity;
                        break;
                    case nameof(LaserScanConfiguration.UseLines):
                        listener.UseLines = config.UseLines;
                        break;
                    case nameof(LaserScanConfiguration.ForceMinMax):
                        listener.ForceMinMax = config.ForceMinMax;
                        break;
                    case nameof(LaserScanConfiguration.MinIntensity):
                        listener.MinIntensity = config.MinIntensity;
                        break;
                    case nameof(LaserScanConfiguration.MaxIntensity):
                        listener.MaxIntensity = config.MaxIntensity;
                        break;
                    case nameof(LaserScanConfiguration.FlipMinMax):
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
            config.LaserScans.Add(listener.Config);
        }
    }
}