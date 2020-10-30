using Iviz.Controllers;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PathPanelContents"/> 
    /// </summary>

    public sealed class PathModuleData : ListenerModuleData
    {
        [NotNull] readonly PathListener listener;
        [NotNull] readonly PathPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Path;
        public override IConfiguration Configuration => listener.Config;


        public PathModuleData([NotNull] ModuleDataConstructor constructor) :
        base(constructor.ModuleList,
            constructor.GetConfiguration<PathConfiguration>()?.Topic ?? constructor.Topic,
            constructor.GetConfiguration<PathConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<PathPanelContents>(Resource.Module.Path);
            listener = new PathListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            else
            {
                listener.Config = (PathConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
            panel.Frame.Owner = listener;
            panel.HideButton.State = listener.Visible;

            panel.LineWidth.Value = listener.Width;
            panel.ShowAxes.Value = listener.ShowAxes;
            panel.AxesLength.Value = listener.AxisLength;
            panel.ShowLines.Value = listener.ShowLines;
            panel.LineColor.Value = listener.LineColor;

            switch (Type)
            {
                case PoseArray.RosMessageType:
                    panel.ShowAxes.Interactable = false;
                    panel.ShowLines.Interactable = true;
                    break;
                case PolygonStamped.RosMessageType:
                case Polygon.RosMessageType:
                    panel.ShowAxes.Interactable = false;
                    panel.ShowLines.Interactable = true;
                    break;
                default:
                    panel.ShowAxes.Interactable = true;
                    panel.ShowLines.Interactable = true;
                    break;
            }

            panel.LineWidth.ValueChanged += f =>
            {
                listener.Width = f;
            };
            panel.ShowAxes.ValueChanged += f =>
            {
                listener.ShowAxes = f;
            };
            panel.ShowLines.ValueChanged += f =>
            {
                listener.ShowLines = f;
            };
            panel.AxesLength.ValueChanged += f =>
            {
                listener.AxisLength = f;
            };
            panel.LineColor.ValueChanged += f =>
            {
                listener.LineColor = f;
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

        public override void AddToState(StateConfiguration config)
        {
            config.Paths.Add(listener.Config);
        }
    }
}
