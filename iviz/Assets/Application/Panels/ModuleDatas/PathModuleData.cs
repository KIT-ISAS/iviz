using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Newtonsoft.Json;

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
        public override Resource.ModuleType ModuleType => Resource.ModuleType.Path;
        public override IConfiguration Configuration => listener.Config;


        public PathModuleData([NotNull] ModuleDataConstructor constructor) :
        base(constructor.GetConfiguration<PathConfiguration>()?.Topic ?? constructor.Topic,
            constructor.GetConfiguration<PathConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<PathPanelContents>(Resource.ModuleType.Path);
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
            panel.Listener.Listener = listener.Listener;
            panel.Frame.Owner = listener;
            panel.HideButton.State = listener.Visible;

            panel.LineWidth.Value = listener.LineWidth;
            panel.ShowAxes.Value = listener.FramesVisible;
            panel.AxesLength.Value = listener.FrameSize;
            panel.ShowLines.Value = listener.LinesVisible;
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
                listener.LineWidth = f;
            };
            panel.ShowAxes.ValueChanged += f =>
            {
                listener.FramesVisible = f;
            };
            panel.ShowLines.ValueChanged += f =>
            {
                listener.LinesVisible = f;
            };
            panel.AxesLength.ValueChanged += f =>
            {
                listener.FrameSize = f;
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
        
        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<PathConfiguration>(configAsJson);
            
            foreach (string field in fields)
            {
                switch (field) 
                {
                    case nameof(PathConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(PathConfiguration.LineWidth):
                        listener.LineWidth = config.LineWidth;
                        break;
                    case nameof(PathConfiguration.FramesVisible):
                        listener.FramesVisible = config.FramesVisible;
                        break;
                    case nameof(PathConfiguration.FrameSize):
                        listener.FrameSize = config.FrameSize;
                        break;
                    case nameof(PathConfiguration.LinesVisible):
                        listener.LinesVisible = config.LinesVisible;
                        break;
                    case nameof(PathConfiguration.LineColor):
                        listener.LineColor = config.LineColor;
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
            config.Paths.Add(listener.Config);
        }
    }
}
