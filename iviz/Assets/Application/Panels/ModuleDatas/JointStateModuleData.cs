using Iviz.Resources;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    public sealed class JointStateModuleData : ListenerModuleData
    {
        [NotNull] readonly JointStateListener listener;
        [NotNull] readonly JointStatePanelContents panel;
        readonly List<string> robotNames = new List<string>();

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.JointState;

        public override IConfiguration Configuration => listener.Config;

        public JointStateModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<JointStateConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType<JointStatePanelContents>(Resource.ModuleType.JointState);
            listener = new JointStateListener(this);
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
            robotNames.Add("<none>");
            robotNames.AddRange(
                ModuleListPanel.ModuleDatas.
                    Select(x => x.Controller).
                    OfType<IJointProvider>().
                    Select(x => x.Name)
            );
            panel.Robot.Options = robotNames;
            panel.Robot.Value = listener.RobotName;

            panel.JointPrefix.Value = listener.MsgJointPrefix;
            panel.JointSuffix.Value = listener.MsgJointSuffix;
            panel.TrimFromEnd.Value = listener.MsgTrimFromEnd;

            panel.JointPrefix.EndEdit += f =>
            {
                listener.MsgJointPrefix = f;
            };
            panel.JointSuffix.EndEdit += f =>
            {
                listener.MsgJointSuffix = f;
            };
            panel.TrimFromEnd.ValueChanged += f =>
            {
                listener.MsgTrimFromEnd = (int)f;
            };
            panel.Robot.ValueChanged += (i, s) =>
            {
                listener.Robot = (i == 0) ? null : GetRobotWithName(s);
                listener.RobotName = s;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
        }

        IJointProvider GetRobotWithName(string name)
        {
            return ModuleListPanel.ModuleDatas.
                    Select(x => x.Controller).
                    OfType<IJointProvider>().
                    FirstOrDefault(x => x.Name == name);
        }
        
        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
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
                        listener.RobotName = config.RobotName;
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
                        Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
                        break;                    
                }
            }

            ResetPanel();
        }            

        public override void AddToState(StateConfiguration config)
        {
            config.JointStates.Add(listener.Config);
        }
    }
}
