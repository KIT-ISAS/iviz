using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class JointStateModuleData : ListenerModuleData
    {
        const string NoneStr = "<none>";

        [NotNull] readonly JointStateListener listener;
        [NotNull] readonly JointStateModulePanel panel;
        readonly List<string> robotNames = new List<string>();

        protected override ListenerController Listener => listener;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.JointState;

        public override IConfiguration Configuration => listener.Config;

        public JointStateModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.TryGetConfigurationTopic() ?? constructor.Topic)
        {
            panel = ModulePanelManager.GetPanelByResourceType<JointStateModulePanel>(ModuleType.JointState);
            listener = new JointStateListener();
            if (constructor.Configuration != null)
            {
                listener.Config = (JointStateConfiguration)constructor.Configuration;
                listener.Robot = GetRobotWithName(listener.RobotName);
            }
            else
            {
                listener.Config.Topic = Topic;
            }

            listener.StartListening();
            UpdateModuleButton();
        }

        public override void SetupPanel()
        {
            panel.Listener.Listener = listener.Listener;

            robotNames.Clear();
            robotNames.Add(NoneStr);
            robotNames.AddRange(
                ModuleListPanel.ModuleDatas.Select(x => x.Controller).OfType<IJointProvider>().Select(x => x.Name)
            );
            panel.Robot.Options = robotNames;
            panel.Robot.Value = listener.RobotName.Length != 0 ? listener.RobotName : NoneStr;

            panel.JointPrefix.Value = listener.MsgJointPrefix;
            panel.JointSuffix.Value = listener.MsgJointSuffix;
            panel.TrimFromEnd.Value = listener.MsgTrimFromEnd;

            panel.JointPrefix.Submit += f => { listener.MsgJointPrefix = f; };
            panel.JointSuffix.Submit += f => { listener.MsgJointSuffix = f; };
            panel.TrimFromEnd.ValueChanged += f => { listener.MsgTrimFromEnd = (int)f; };
            panel.Robot.ValueChanged += (i, s) =>
            {
                listener.Robot = (i == 0) ? null : GetRobotWithName(s);
                listener.RobotName = s;
            };
            panel.CloseButton.Clicked += Close;
        }

        [CanBeNull]
        static IJointProvider GetRobotWithName(string name)
        {
            return ModuleListPanel.ModuleDatas
                .Select(moduleData => moduleData.Controller)
                .OfType<IJointProvider>()
                .FirstOrDefault(provider => provider.Name == name);
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<JointStateConfiguration>(configAsJson);

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(JointStateConfiguration.Visible):
                        listener.Visible = config.Visible;
                        break;
                    case nameof(JointStateConfiguration.RobotName):
                        listener.Robot = GetRobotWithId(config.RobotName);
                        listener.RobotName = listener.Robot.Name;
                        break;
                    case nameof(JointStateConfiguration.MsgJointPrefix):
                        listener.MsgJointPrefix = config.MsgJointPrefix;
                        break;
                    case nameof(JointStateConfiguration.MsgJointSuffix):
                        listener.MsgJointSuffix = config.MsgJointSuffix;
                        break;
                    case nameof(JointStateConfiguration.MsgTrimFromEnd):
                        listener.MsgTrimFromEnd = config.MsgTrimFromEnd;
                        break;
                    default:
                        RosLogger.Error($"{this}: Unknown field '{field}'");
                        break;
                }
            }

            ResetPanel();
        }

        [NotNull]
        static SimpleRobotController GetRobotWithId(string robotId)
        {
            var robotData =
                ModuleListPanel.Instance.ModuleDatas.FirstOrDefault(data => data.Configuration.Id == robotId);
            if (robotData == null)
            {
                throw new InvalidOperationException($"No robot with id '{robotId}' found");
            }

            if (robotData.ModuleType != ModuleType.Robot)
            {
                throw new InvalidOperationException($"Module with id '{robotId}' is not a robot");
            }

            return ((SimpleRobotModuleData)robotData).RobotController;
        }

        public override void AddToState(StateConfiguration config)
        {
            config.JointStates.Add(listener.Config);
        }
    }
}