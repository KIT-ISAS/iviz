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
    /// <see cref="PointCloudModulePanel"/> 
    /// </summary>
    public sealed class PointCloudModuleData : ListenerModuleData
    {
        readonly PointCloudListener listener;
        readonly PointCloudModulePanel panel;

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.PointCloud;
        public override IConfiguration Configuration => listener.Config;


        public PointCloudModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<PointCloudModulePanel>(ModuleType.PointCloud);
            listener = new PointCloudListener((PointCloudConfiguration?) constructor.Configuration, Topic);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.NumPoints.Text = BuildDescriptionString();

            panel.Colormap.Index = (int) listener.Colormap;
            panel.PointSize.Value = listener.PointSize;
            panel.IntensityChannel.Hints = listener.FieldNames;
            panel.IntensityChannel.Value = listener.IntensityChannel;

            panel.HideButton.State = listener.Visible;

            panel.ForceMinMax.Value = listener.OverrideMinMax;
            panel.MinIntensity.Value = listener.MinIntensity;
            panel.MaxIntensity.Value = listener.MaxIntensity;
            panel.MinIntensity.Interactable = listener.OverrideMinMax;
            panel.MaxIntensity.Interactable = listener.OverrideMinMax;
            panel.FlipMinMax.Value = listener.FlipMinMax;
            panel.PointCloudType.Index = (int) listener.PointCloudType;

            panel.PointSize.ValueChanged += f => listener.PointSize = f;
            panel.IntensityChannel.Submit += s => listener.IntensityChannel = s;
            panel.Colormap.ValueChanged += (i, _) => listener.Colormap = (ColormapId) i;
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.OverrideMinMax = f;
                
                if (listener.MeasuredIntensityBounds is var (min, max))
                {
                    panel.MinIntensity.Value = min;
                    panel.MaxIntensity.Value = max;
                }
                
                panel.MinIntensity.Interactable = f;
                panel.MaxIntensity.Interactable = f;
            };
            panel.FlipMinMax.ValueChanged += f => listener.FlipMinMax = f;
            panel.MinIntensity.ValueChanged += f => listener.MinIntensity = f;
            panel.MaxIntensity.ValueChanged += f => listener.MaxIntensity = f;
            panel.PointCloudType.ValueChanged += (f, _) => listener.PointCloudType = (PointCloudType) f;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.IntensityChannel.Hints = listener.FieldNames;
            panel.NumPoints.Text = BuildDescriptionString();
        }

        string BuildDescriptionString()
        {
            var (x, y) = listener.MeasuredIntensityBounds;
            string minIntensityStr = UnityUtils.FormatFloat(x);
            string maxIntensityStr = UnityUtils.FormatFloat(y);
            return
                $"<b>{listener.NumValidPoints.ToString("N0")} Points</b>\n" +
                (listener.NumValidPoints == 0 
                    ? "Empty" 
                    : listener.IsIntensityUsed 
                        ? $"[{minIntensityStr} .. {maxIntensityStr}]" 
                        : "Color");
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<PointCloudConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(PointCloudConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(PointCloudConfiguration.IntensityChannel):
                        listener.IntensityChannel = config.IntensityChannel;
                        break;
                    case nameof(PointCloudConfiguration.PointSize):
                        listener.PointSize = config.PointSize;
                        break;
                    case nameof(PointCloudConfiguration.Colormap):
                        listener.Colormap = config.Colormap;
                        break;
                    case nameof(PointCloudConfiguration.OverrideMinMax):
                        listener.OverrideMinMax = config.OverrideMinMax;
                        break;
                    case nameof(PointCloudConfiguration.MinIntensity):
                        listener.MinIntensity = config.MinIntensity;
                        break;
                    case nameof(PointCloudConfiguration.MaxIntensity):
                        listener.MaxIntensity = config.MaxIntensity;
                        break;
                    case nameof(PointCloudConfiguration.FlipMinMax):
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
            config.PointClouds.Add(listener.Config);
        }
    }
}