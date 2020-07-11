using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.App
{
    public sealed class JointStateModuleData : ListenerModuleData
    {
        readonly JointStateListener listener;
        readonly JointStatePanelContents panel;
        readonly List<string> robotNames = new List<string>();

        protected override ListenerController Listener => listener;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.JointState;

        public override IConfiguration Configuration => listener.Config;

        public JointStateModuleData(ModuleDataConstructor constructor) :
            base(constructor.ModuleList,
                constructor.GetConfiguration<JointStateConfiguration>()?.Topic ?? constructor.Topic,
                constructor.Type)
        {
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.JointState) as JointStatePanelContents;
            //listener = Instantiate<JointStateListener>();
            //listener.name = "JointState:" + Topic;
            //listener.ModuleData = this;
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
            panel.Listener.RosListener = listener.Listener;

            robotNames.Clear();
            robotNames.Add("<none>");
            robotNames.AddRange(
                ModuleListPanel.ModuleDatas.
                Where(x => x.Module == Resource.Module.Robot).
                Select(x => (x as RobotModuleData).RobotName)
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
                if (i == 0)
                {
                    listener.Robot = null;
                }
                else
                {
                    listener.Robot = GetRobotWithName(s);
                }
                listener.RobotName = s;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
        }

        RobotController GetRobotWithName(string name)
        {
            return (RobotController)
                ModuleListPanel.ModuleDatas.
                Where(x => x.Module == Resource.Module.Robot).
                Cast<RobotModuleData>().
                FirstOrDefault(x => x.RobotName == name)?.
                Controller;
        }

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(listener.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            config.JointStates.Add(listener.Config);
        }
    }
}
