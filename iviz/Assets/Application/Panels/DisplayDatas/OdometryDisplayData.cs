using Iviz.App.Listeners;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class OdometryDisplayData : ListenerDisplayData
    {
        readonly OdometryListener listener;
        readonly OdometryPanelContents panel;

        public override DataPanelContents Panel => panel;
        protected override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.Odometry;
        public override IConfiguration Configuration => listener.Config;


        public OdometryDisplayData(DisplayDataConstructor constructor) :
            base(constructor.DisplayList,
                constructor.GetConfiguration<OdometryConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<OdometryConfiguration>()?.Type ?? constructor.Type)
        {
            GameObject listenerObject = new GameObject("Odometry:" + Topic);

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Odometry) as OdometryPanelContents;
            listener = listenerObject.AddComponent<OdometryListener>();
            listener.DisplayData = this;
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            else
            {
                listener.Config = (OdometryConfiguration)constructor.Configuration;
            }
            listener.StartListening();
            UpdateButtonText();
        }

        public override void SetupPanel()
        {
            panel.TrailEnabled.Value = listener.ShowTrail;
            panel.Color.Value = listener.Color;
            panel.TrailTime.Value = listener.TrailTime;
            panel.Scale.Value = listener.Scale;

            panel.TrailEnabled.ValueChanged += f =>
            {
                listener.ShowTrail = f;
            };
            panel.Color.ValueChanged += f =>
            {
                listener.Color = f;
            };
            panel.TrailTime.ValueChanged += f =>
            {
                listener.TrailTime = f;
            };
            panel.Scale.ValueChanged += f =>
            {
                listener.Scale = f;
            };

            panel.Listener.RosListener = listener.Listener;
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Odometries.Add(listener.Config);
        }
    }
}
