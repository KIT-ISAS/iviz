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
    /// <see cref="MagnitudePanelContents"/> 
    /// </summary>
    public sealed class MagnitudeModuleData : ListenerModuleData
    {
        [NotNull] readonly MagnitudeListener listener;
        [NotNull] readonly MagnitudePanelContents panel;

        public override DataPanelContents Panel => panel;
        protected override ListenerController Listener => listener;
        public override ModuleType ModuleType => ModuleType.Magnitude;
        public override IConfiguration Configuration => listener.Config;


        public MagnitudeModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.GetConfiguration<MagnitudeConfiguration>()?.Topic ?? constructor.Topic,
                constructor.GetConfiguration<MagnitudeConfiguration>()?.Type ?? constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<MagnitudePanelContents>(ModuleType.Magnitude);
            listener = new MagnitudeListener(this);
            if (constructor.Configuration == null)
            {
                listener.Config.Topic = Topic;
                listener.Config.Type = Type;
            }
            else
            {
                listener.Config = (MagnitudeConfiguration) constructor.Configuration;
            }

            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = listener;
            panel.ShowTrail.Value = listener.TrailVisible;
            panel.ShowAxis.Value = listener.FrameVisible;
            panel.ShowAngle.Value = listener.AngleVisible;
            panel.ShowVector.Value = listener.VectorVisible;
            panel.VectorColor.Value = listener.Color;
            //panel.TrailTime.Value = listener.TrailTime;
            panel.Scale.Value = listener.Scale;
            panel.VectorScale.Value = listener.VectorScale;
            panel.HideButton.State = listener.Visible;
            panel.ScaleMultiplier.Value = listener.VectorScaleMultiplierPow10;
            panel.PreferUdp.Value = listener.PreferUdp;


            panel.ShowTrail.ValueChanged += f => { listener.TrailVisible = f; };
            panel.ShowAngle.ValueChanged += f => { listener.AngleVisible = f; };
            panel.VectorColor.ValueChanged += f => { listener.Color = f; };
            //panel.TrailTime.ValueChanged += f => { listener.TrailTime = f; };
            panel.Scale.ValueChanged += f => { listener.Scale = f; };
            panel.ShowAxis.ValueChanged += f => { listener.FrameVisible = f; };
            panel.ShowVector.ValueChanged += f => { listener.VectorVisible = f; };
            panel.VectorScale.ValueChanged += f => { listener.VectorScale = f; };
            panel.ScaleMultiplier.ValueChanged += f => { listener.VectorScaleMultiplierPow10 = f; };
            panel.PreferUdp.ValueChanged += f => listener.PreferUdp = f;

            switch (listener.Config.Type)
            {
                case PoseStamped.RosMessageType:
                case PointStamped.RosMessageType:
                    panel.ShowVector.Interactable = false;
                    panel.VectorScale.Interactable = false;
                    break;
                case WrenchStamped.RosMessageType:
                case TwistStamped.RosMessageType:
                    panel.ShowVector.Interactable = true;
                    panel.VectorScale.Interactable = true;
                    break;
            }

            panel.Listener.Listener = listener.Listener;
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<MagnitudeConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(MagnitudeConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(MagnitudeConfiguration.AngleVisible):
                        listener.AngleVisible = config.AngleVisible;
                        break;
                    case nameof(MagnitudeConfiguration.TrailVisible):
                        listener.TrailVisible = config.TrailVisible;
                        break;
                    case nameof(MagnitudeConfiguration.FrameVisible):
                        listener.FrameVisible = config.FrameVisible;
                        break;
                    case nameof(MagnitudeConfiguration.Scale):
                        listener.Scale = config.Scale;
                        break;
                    case nameof(MagnitudeConfiguration.VectorVisible):
                        listener.VectorVisible = config.VectorVisible;
                        break;
                    case nameof(MagnitudeConfiguration.VectorScale):
                        listener.VectorScale = config.VectorScale;
                        break;
                    case nameof(MagnitudeConfiguration.Color):
                        listener.Color = config.Color;
                        break;
                    case nameof(MagnitudeConfiguration.TrailTime):
                        listener.TrailTime = config.TrailTime;
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
            config.Odometries.Add(listener.Config);
        }
    }
}