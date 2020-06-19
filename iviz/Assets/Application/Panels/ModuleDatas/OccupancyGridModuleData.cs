using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="OccupancyGridPanelContents"/> 
    /// </summary>

    public class OccupancyGridModuleData : ListenerModuleData
    {
        readonly OccupancyGridListener listener;
        readonly OccupancyGridPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.OccupancyGrid;
        public override IConfiguration Configuration => listener.Config;


        public OccupancyGridModuleData(ModuleDataConstructor constructor) :
        base(constructor.DisplayList,
            constructor.GetConfiguration<OccupancyGridConfiguration>()?.Topic ?? constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.OccupancyGrid) as OccupancyGridPanelContents;
            listener = Instantiate<OccupancyGridListener>();
            listener.name = "OccupancyGrid:" + Topic;
            listener.ModuleData = this;
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (OccupancyGridConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.Colormap.Index = (int)listener.Colormap;
            panel.HideButton.State = listener.Visible;
            panel.FlipColors.Value = listener.FlipColors;
            panel.ScaleZ.Value = listener.ScaleZ;

            panel.OcclusionOnlyMode.Value = listener.RenderAsOcclusionOnly;
            panel.Tint.Value = listener.Tint;
            //panel.Alpha.Value = listener.Tint.a;

            panel.Tint.ValueChanged += f =>
            {
                Color color = f;
                color.a = 1;
                listener.Tint = color;
            };
            /*
            panel.Alpha.ValueChanged += f =>
            {
                Color color = panel.Tint.Value;
                color.a = f;
                listener.Tint = color;
            };
            */
            panel.OcclusionOnlyMode.ValueChanged += f =>
            {
                listener.RenderAsOcclusionOnly = f;
            };

            panel.FlipColors.ValueChanged += f =>
            {
                listener.FlipColors = f;
            };
            panel.ScaleZ.ValueChanged += f =>
            {
                listener.ScaleZ = f;
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
                UpdateButtonText();
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.OccupancyGrids.Add(listener.Config);
        }
    }
}
