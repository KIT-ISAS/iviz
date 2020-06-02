using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class TFDisplayData : ListenerDisplayData
    {
        readonly TFListener listener;
        readonly TFPanelContents panel;

        protected override TopicListener Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.TF;
        public override IConfiguration Configuration => listener.Config;

        public TFDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList,
                constructor.GetConfiguration<TFConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.TF) as TFPanelContents;
            listener = Resource.Listeners.Instantiate<TFListener>();
            listener.name = "TF";
            listener.DisplayData = this;
            if (constructor.Configuration != null)
            {
                listener.Config = (TFConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public void UpdateConfiguration(TFConfiguration configuration)
        {
            listener.Config = configuration;
        }

        public override void SetupPanel()
        {
            panel.Listener.RosListener = listener.Listener;
            panel.ShowAxes.Value = listener.AxisVisible;
            panel.FrameSize.Value = listener.AxisSize;
            panel.ShowFrameLabels.Value = listener.AxisLabelVisible;
            panel.FrameLabelSize.Value = listener.AxisLabelSize;
            panel.FrameLabelSize.Interactable = listener.AxisLabelVisible;
            panel.ConnectToParent.Value = listener.ParentConnectorVisible;
            panel.ShowAllFrames.Value = listener.ShowAllFrames;
            panel.Sender.RosSender = listener.Publisher;

            panel.ShowAxes.ValueChanged += f =>
            {
                listener.AxisVisible = f;
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
            panel.ShowAllFrames.ValueChanged += f =>
            {
                listener.ShowAllFrames = f;
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Tf = listener.Config;
        }
    }
}
