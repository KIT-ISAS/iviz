#nullable enable

using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PointCloudPanelContents"/> 
    /// </summary>
    public sealed class PointCloudModuleData : ListenerModuleData
    {
        readonly PointCloudListener listener;
        readonly PointCloudPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.PointCloud;
        public override IConfiguration Configuration => listener.Config;


        public PointCloudModuleData(ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<PointCloudConfiguration>()?.Topic ?? constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<PointCloudPanelContents>(ModuleType.PointCloud);
            listener = new PointCloudListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (PointCloudConfiguration) constructor.Configuration;
            }

            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.NumPoints.Label = BuildDescriptionString();

            panel.Colormap.Index = (int) listener.Colormap;
            panel.PointSize.Value = listener.PointSize;
            panel.SizeMultiplier.Value = listener.SizeMultiplierPow10;
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
            panel.IntensityChannel.EndEdit += s => listener.IntensityChannel = s;
            panel.Colormap.ValueChanged += (i, _) => listener.Colormap = (ColormapId) i;
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.OverrideMinMax = f;
                panel.MinIntensity.Interactable = f;
                panel.MaxIntensity.Interactable = f;
            };
            panel.FlipMinMax.ValueChanged += f => listener.FlipMinMax = f;
            panel.MinIntensity.ValueChanged += f => listener.MinIntensity = f;
            panel.MaxIntensity.ValueChanged += f => listener.MaxIntensity = f;
            panel.PointCloudType.ValueChanged += (f, _) => listener.PointCloudType = (PointCloudType) f;
            panel.SizeMultiplier.ValueChanged += f => listener.SizeMultiplierPow10 = (int) f;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.IntensityChannel.Hints = listener.FieldNames;
            panel.NumPoints.Label = BuildDescriptionString();
        }

        string BuildDescriptionString()
        {
            var (x, y) = listener.MeasuredIntensityBounds;
            string minIntensityStr = x.ToString("#,0.##", UnityUtils.Culture);
            string maxIntensityStr = y.ToString("#,0.##", UnityUtils.Culture);
            return
                $"<b>{listener.Size.ToString("N0")} Points</b>\n" +
                (listener.Size == 0 
                    ? "Empty" 
                    : listener.IsIntensityUsed 
                        ? $"[{minIntensityStr} .. {maxIntensityStr}]" 
                        : "Color");
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
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
                        Logger.Error($"{this}: Unknown field '{field}'");
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