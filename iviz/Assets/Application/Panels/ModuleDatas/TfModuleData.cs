using Iviz.Controllers;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="TfPanelContents"/> 
    /// </summary>
    public sealed class TfModuleData : ListenerModuleData
    {
        [NotNull] readonly TfListener listener;
        [NotNull] readonly TfPanelContents panel;

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.TF;
        public override IConfiguration Configuration => listener.Config;

        public TfModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<TfConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<TfPanelContents>(Resource.Module.TF);
            listener = new TfListener(this);
            if (constructor.Configuration != null)
            {
                listener.Config = (TfConfiguration)constructor.Configuration;
            }
            else
            {
                listener.Config.Topic = Topic;
            }
            listener.StartListening();
            UpdateModuleButton();
        }

        public void UpdateConfiguration(TfConfiguration configuration)
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
            panel.FrameSize.Value = listener.FrameSize;
            panel.ShowFrameLabels.Value = listener.AxisLabelVisible;
            panel.ConnectToParent.Value = listener.ParentConnectorVisible;
            panel.KeepOnlyUsedFrames.Value = !listener.ShowAllFrames;
            panel.Sender.Set(listener.Publisher);
            
            panel.HideButton.Clicked += () =>
            {
                listener.AxisVisible = !listener.AxisVisible;
                panel.HideButton.State = listener.AxisVisible;
            };
            panel.ShowFrameLabels.ValueChanged += f =>
            {
                listener.AxisLabelVisible = f;
            };
            panel.FrameSize.ValueChanged += f =>
            {
                listener.FrameSize = f;
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
