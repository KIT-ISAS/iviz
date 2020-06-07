using Iviz.App.Listeners;
using Iviz.Msgs.GeometryMsgs;
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
            panel.ShowTrail.Value = listener.ShowTrail;
            panel.ShowAxis.Value = listener.ShowAxis;
            panel.ShowVector.Value = listener.ShowVector;
            panel.Color.Value = listener.Color;
            panel.TrailTime.Value = listener.TrailTime;
            panel.Scale.Value = listener.Scale;
            panel.VectorScale.Value = listener.VectorScale;

            panel.ShowTrail.ValueChanged += f =>
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
            panel.ShowAxis.ValueChanged += f =>
            {
                listener.ShowAxis = f;
            };
            panel.ShowVector.ValueChanged += f =>
            {
                listener.ShowVector = f;
            };
            panel.VectorScale.ValueChanged += f =>
            {
                listener.VectorScale = f;
            };

            switch (listener.Config.Type)
            {
                case PoseStamped.RosMessageType:
                    panel.ShowVector.Interactable = false;
                    break;
                case PointStamped.RosMessageType:
                    panel.ShowVector.Interactable = false;
                    break;
                case WrenchStamped.RosMessageType:
                    panel.ShowVector.Interactable = true;
                    break;
                case TwistStamped.RosMessageType:
                    panel.ShowVector.Interactable = true;
                    break;
            }

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
