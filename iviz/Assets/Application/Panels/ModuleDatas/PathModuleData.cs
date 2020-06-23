using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PointCloudPanelContents"/> 
    /// </summary>

    public class PathModuleData : ListenerModuleData
    {
        readonly PathListener listener;
        readonly PathPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Path;
        public override IConfiguration Configuration => listener.Config;


        public PathModuleData(ModuleDataConstructor constructor) :
        base(constructor.ModuleList,
            constructor.GetConfiguration<PathConfiguration>()?.Topic ?? constructor.Topic,
            constructor.GetConfiguration<PathConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Path) as PathPanelContents;
            listener = Instantiate<PathListener>();
            listener.name = "Path:" + Topic;
            listener.ModuleData = this;
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
            }
            else
            {
                listener.Config = (PathConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
            panel.Frame.Owner = listener;

            panel.LineWidth.Value = listener.Width;
            panel.ShowAxes.Value = listener.ShowAxes;
            panel.AxesLength.Value = listener.AxisLength;
            panel.ShowLines.Value = listener.ShowLines;
            panel.LineColor.Value = listener.LineColor;

            panel.LineWidth.ValueChanged += f =>
            {
                listener.Width = f;
            };
            panel.ShowAxes.ValueChanged += f =>
            {
                listener.ShowAxes = f;
            };
            panel.AxesLength.ValueChanged += f =>
            {
                listener.AxisLength = f;
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
            config.Paths.Add(listener.Config);
        }
    }
}
