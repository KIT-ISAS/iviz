﻿#nullable enable

using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Ros;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="JoystickPanelContents"/> 
    /// </summary>
    public sealed class JoystickModuleData : ModuleData
    {
        readonly JoystickController controller;
        readonly JoystickPanelContents panel;

        public override ModuleType ModuleType => ModuleType.Joystick;
        public override DataPanelContents Panel => panel;
        public override IConfiguration Configuration => controller.Config;
        public override IController Controller => controller;

        public JoystickModuleData(ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<JoystickPanelContents>(ModuleType.Joystick);

            controller = new JoystickController(this, ModuleListPanel.TwistJoystick);
            if (constructor.Configuration != null)
            {
                controller.Config = (JoystickConfiguration)constructor.Configuration;
            }

            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            controller.StopController();
        }

        public override void SetupPanel()
        {
            panel.HideButton.State = controller.Visible;

            panel.JoySender.Set(controller.SenderJoy);
            panel.TwistSender.Set(controller.SenderTwist);
            panel.SendJoy.Value = controller.PublishJoy;

            panel.SendTwist.Value = controller.PublishTwist;

            panel.JoyTopic.Value = controller.JoyTopic;
            panel.TwistTopic.Value = controller.TwistTopic;

            UpdateHints();

            panel.UseStamped.Value = controller.UseTwistStamped;

            panel.MaxSpeed.Value = controller.MaxSpeed;
            panel.AttachToFrame.Value = controller.AttachToFrame;
            panel.AttachToFrame.Hints = TfListener.FramesUsableAsHints;
            panel.XIsFront.Value = controller.XIsFront;

            panel.JoyTopic.Interactable = controller.PublishJoy;
            panel.MaxSpeed.Interactable = controller.PublishTwist;
            panel.AttachToFrame.Interactable = controller.PublishTwist;
            panel.TwistTopic.Interactable = controller.PublishTwist;

            panel.XIsFront.Interactable = controller.PublishTwist;
            panel.UseStamped.Interactable = controller.PublishTwist;

            panel.SendJoy.ValueChanged += f =>
            {
                controller.PublishJoy = f;
                panel.JoySender.Set(controller.SenderJoy);
                panel.JoyTopic.Interactable = f;
            };
            panel.SendTwist.ValueChanged += f =>
            {
                controller.PublishTwist = f;
                panel.MaxSpeed.Interactable = f;
                panel.AttachToFrame.Interactable = f && controller.UseTwistStamped;
                panel.XIsFront.Interactable = f;
                panel.TwistSender.Set(controller.SenderTwist);
                panel.TwistTopic.Interactable = f;
                panel.UseStamped.Interactable = f;
            };
            panel.MaxSpeed.ValueChanged += f =>
            {
                controller.MaxSpeed = f;
            };
            panel.AttachToFrame.EndEdit += f =>
            {
                controller.AttachToFrame = f;
            };
            panel.XIsFront.ValueChanged += f =>
            {
                controller.XIsFront = f;
            };
            panel.JoyTopic.EndEdit += f =>
            {
                controller.JoyTopic = f;
                panel.JoySender.Set(controller.SenderJoy);
            };
            panel.TwistTopic.EndEdit += f =>
            {
                controller.TwistTopic = f;
                panel.TwistSender.Set(controller.SenderTwist);
            };
            panel.UseStamped.ValueChanged += f =>
            {
                controller.UseTwistStamped = f;
                panel.AttachToFrame.Interactable = f && controller.PublishTwist;
            };

            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            UpdateHints();
            panel.AttachToFrame.Hints = TfListener.FramesUsableAsHints;
        }

        void UpdateHints()
        {
            var topicTypes = ConnectionManager.Connection.GetSystemTopicTypes();
            panel.JoyTopic.Hints = topicTypes
                .Where(info => info.Type == Joy.RosMessageType)
                .Select(info => info.Topic);            
            string expectedType = controller.UseTwistStamped
                ? TwistStamped.RosMessageType
                : Twist.RosMessageType;
            panel.TwistTopic.Hints = topicTypes
                .Where(info => info.Type == expectedType)
                .Select(info => info.Topic);            
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<JoystickConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(JoystickConfiguration.Visible):
                        controller.Visible = config.Visible;
                        break;
                    case nameof(JoystickConfiguration.JoyTopic):
                        controller.JoyTopic = config.JoyTopic;
                        break;
                    case nameof(JoystickConfiguration.PublishJoy):
                        controller.PublishJoy = config.PublishJoy;
                        break;
                    case nameof(JoystickConfiguration.TwistTopic):
                        controller.TwistTopic = config.TwistTopic;
                        break;
                    case nameof(JoystickConfiguration.PublishTwist):
                        controller.PublishTwist = config.PublishTwist;
                        break;
                    case nameof(JoystickConfiguration.UseTwistStamped):
                        controller.UseTwistStamped = config.UseTwistStamped;
                        break;
                    case nameof(JoystickConfiguration.MaxSpeed):
                        controller.MaxSpeed = config.MaxSpeed;
                        break;
                    case nameof(JoystickConfiguration.AttachToFrame):
                        controller.AttachToFrame = config.AttachToFrame;
                        break;
                    case nameof(JoystickConfiguration.XIsFront):
                        controller.XIsFront = config.XIsFront;
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
            config.Joystick = controller.Config;
        }
    }
}
