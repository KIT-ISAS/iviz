using Iviz.Controllers;
using Iviz.Resources;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="TFPanelContents"/> 
    /// </summary>
    public sealed class TFModuleData : ListenerModuleData
    {
        readonly TFListener listener;
        readonly TFPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.TF;
        public override IConfiguration Configuration => listener.Config;

        public TFModuleData(ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<TFConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.TF) as TFPanelContents;
            listener = new TFListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (TFConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public void UpdateConfiguration(TFConfiguration configuration)
        {
            listener.Config = configuration;
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = listener;
            panel.Listener.RosListener = listener.Listener;
            panel.ListenerStatic.RosListener = listener.ListenerStatic;
            //panel.ShowAxes.Value = listener.AxisVisible;
            panel.HideButton.State = listener.AxisVisible;
            panel.FrameSize.Value = listener.AxisSize;
            panel.ShowFrameLabels.Value = listener.AxisLabelVisible;
            panel.FrameLabelSize.Value = listener.AxisLabelSize;
            panel.FrameLabelSize.Interactable = listener.AxisLabelVisible;
            panel.ConnectToParent.Value = listener.ParentConnectorVisible;
            panel.KeepOnlyUsedFrames.Value = !listener.ShowAllFrames;
            panel.Sender.Set(listener.Publisher);

            /*
            panel.ShowAxes.ValueChanged += f =>
            {
                listener.AxisVisible = f;
            };
            */
            panel.HideButton.Clicked += () =>
            {
                listener.AxisVisible = !listener.AxisVisible;
                panel.HideButton.State = listener.AxisVisible;
            };
            panel.ShowFrameLabels.ValueChanged += f =>
            {
                listener.AxisLabelVisible = f;
                panel.FrameLabelSize.Interactable = f;
            };
            panel.FrameSize.ValueChanged += f =>
            {
                listener.AxisSize = f;
            };
            panel.FrameLabelSize.ValueChanged += f =>
            {
                listener.AxisLabelSize = f;
            };
            panel.ConnectToParent.ValueChanged += f =>
            {
                listener.ParentConnectorVisible = f;
            };
            panel.KeepOnlyUsedFrames.ValueChanged += f =>
            {
                listener.ShowAllFrames = !f;
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Tf = listener.Config;
        }

        public override void OnARModeChanged(bool value)
        {
            listener.OnARModeChanged(value);
        }
    }
}
