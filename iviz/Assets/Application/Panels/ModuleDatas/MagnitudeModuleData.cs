#nullable enable

using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="MagnitudeModulePanel"/> 
    /// </summary>
    public sealed class MagnitudeModuleData : ListenerModuleData
    {
        readonly MagnitudeListener listener;
        readonly MagnitudeModulePanel panel;

        public override ModulePanel Panel => panel;
        protected override ListenerController Listener => listener;
        public override ModuleType ModuleType => ModuleType.Magnitude;
        public override IConfiguration Configuration => listener.Config;


        public MagnitudeModuleData(ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<MagnitudeModulePanel>(ModuleType.Magnitude);
            listener = new MagnitudeListener((MagnitudeConfiguration?)constructor.Configuration, Topic,
                constructor.Type);
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = listener;
            panel.ShowTrail.Value = listener.TrailVisible;
            panel.ShowAxis.Value = listener.FrameVisible;
            panel.ShowAngle.Value = listener.AngleVisible;
            panel.ShowVector.Value = listener.VectorVisible;
            panel.VectorColor.Value = listener.VectorColor;
            panel.Magnitude.Owner = listener;
            //panel.TrailTime.Value = listener.TrailTime;
            panel.Scale.Value = listener.Scale;
            panel.VectorScale.Value = listener.VectorScale;
            panel.HideButton.State = listener.Visible;

            panel.ShowTrail.ValueChanged += f => listener.TrailVisible = f;
            panel.ShowAngle.ValueChanged += f => listener.AngleVisible = f;
            panel.VectorColor.ValueChanged += f => listener.VectorColor = f;
            panel.Scale.ValueChanged += f => listener.Scale = f;
            panel.ShowAxis.ValueChanged += f => listener.FrameVisible = f;
            panel.ShowVector.ValueChanged += f => listener.VectorVisible = f;
            panel.VectorScale.ValueChanged += f => listener.VectorScale = f;

            switch (listener.Config.Type)
            {
                case PoseStamped.MessageType:
                case PointStamped.MessageType:
                    panel.ShowVector.Interactable = false;
                    panel.VectorScale.Interactable = false;
                    break;
                case WrenchStamped.MessageType:
                case TwistStamped.MessageType:
                    panel.ShowVector.Interactable = true;
                    panel.VectorScale.Interactable = true;
                    break;
            }

            panel.Listener.Listener = listener.Listener;
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<MagnitudeConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(IConfiguration.ModuleType):
                        break;
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
                    case nameof(MagnitudeConfiguration.VectorColor):
                        listener.VectorColor = config.VectorColor.ToUnity();
                        break;
                    case nameof(MagnitudeConfiguration.AngleColor):
                        listener.AngleColor = config.AngleColor.ToUnity();
                        break;
                    case nameof(MagnitudeConfiguration.TrailTime):
                        listener.TrailTime = config.TrailTime;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
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