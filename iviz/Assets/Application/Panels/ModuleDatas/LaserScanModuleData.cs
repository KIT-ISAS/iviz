using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="LaserScanPanelContents"/> 
    /// </summary>

    public sealed class LaserScanModuleData : ListenerModuleData
    {
        readonly LaserScanListener listener;
        readonly LaserScanPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.LaserScan;
        public override IConfiguration Configuration => listener.Config;


        public LaserScanModuleData(ModuleDataConstructor constructor) :
        base(constructor.ModuleList,
            constructor.GetConfiguration<LaserScanConfiguration>()?.Topic ?? constructor.Topic,
            constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.LaserScan) as LaserScanPanelContents;
            //listener = Instantiate<LaserScanListener>();
            //listener.name = "LaserScan:" + Topic;
            //listener.ModuleData = this;
            listener = new LaserScanListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (LaserScanConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.NumPoints.Label = $"Number of Points: {listener.Size}";

            string minIntensityStr = listener.MeasuredIntensityBounds.x.ToString("#,0.##", UnityUtils.Culture);
            string maxIntensityStr = listener.MeasuredIntensityBounds.y.ToString("#,0.##", UnityUtils.Culture);
            panel.MinMax.Label = $"Min Intensity: {minIntensityStr} Max: {maxIntensityStr}";

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

            panel.UseIntensity.ValueChanged += f =>
            {
                listener.UseIntensity = f;
            };
            panel.PointSize.ValueChanged += f =>
            {
                listener.PointSize = f;
            };
            panel.Colormap.ValueChanged += (i, _) =>
            {
                listener.Colormap = (Resource.ColormapId)i;
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
            panel.ForceMinMax.ValueChanged += f =>
            {
                listener.ForceMinMax = f;
                panel.MinIntensity.Interactable = f;
                panel.MaxIntensity.Interactable = f;
            };
            panel.FlipMinMax.ValueChanged += f =>
            {
                listener.FlipMinMax = f;
            };
            panel.MinIntensity.ValueChanged += f =>
            {
                listener.MinIntensity = f;
            };
            panel.MaxIntensity.ValueChanged += f =>
            {
                listener.MaxIntensity = f;
            };
            panel.UseLines.ValueChanged += f =>
            {
                listener.UseLines = f;
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.NumPoints.Label = $"Number of Points: {listener.Size}";

            string minIntensityStr = listener.MeasuredIntensityBounds.x.ToString("#,0.##", UnityUtils.Culture);
            string maxIntensityStr = listener.MeasuredIntensityBounds.y.ToString("#,0.##", UnityUtils.Culture);
            panel.MinMax.Label = $"Min Intensity: {minIntensityStr} Max: {maxIntensityStr}";
        }

        public override void AddToState(StateConfiguration config)
        {
            config.LaserScans.Add(listener.Config);
        }
    }
}
