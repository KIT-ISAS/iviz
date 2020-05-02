using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class JointStateDisplayData : DisplayableListenerData
    {
        JointStateListener display;
        JointStatePanelContents panel;
        readonly List<string> robotNames = new List<string>();

        public override DataPanelContents Panel => panel;
        public override DisplayableListener Display => display;
        public override Resource.Module Module => Resource.Module.JointState;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Displays.JointState);
            displayObject.name = "JointState:" + Topic;

            display = displayObject.GetComponent<JointStateListener>();
            display.Parent = TFListener.DisplaysFrame;
            display.Config.topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.JointState) as JointStatePanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<JointStateListener.Configuration>();
            Topic = display.Config.topic;
            display.Robot = GetRobotWithName(display.RobotName);
            return this;
        }

        public override void Start()
        {
            base.Start();
            display.StartListening();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            display.Stop();
            ResourcePool.Dispose(Resource.Displays.PointCloud, display.gameObject);
            display = null;
        }

        public override void SetupPanel()
        {
            panel.Topic.Label = Topic;

            robotNames.Clear();
            robotNames.Add("<none>");
            robotNames.AddRange(
                DisplayListPanel.DisplayDatas.
                Where(x => x.Module == Resource.Module.Robot).
                Select(x => (x as RobotDisplayData).RobotName)
            );
            panel.Robot.Options = robotNames;
            panel.Robot.Value = display.RobotName;

            panel.JointPrefix.Value = display.MsgJointPrefix;
            panel.JointSuffix.Value = display.MsgJointSuffix;
            panel.TrimFromEnd.Value = display.MsgTrimFromEnd;

            panel.JointPrefix.EndEdit += f =>
            {
                display.MsgJointPrefix = f;
            };
            panel.JointSuffix.EndEdit += f =>
            {
                display.MsgJointSuffix = f;
            };
            panel.TrimFromEnd.ValueChanged += f =>
            {
                display.MsgTrimFromEnd = (int)f;
            };
            panel.Robot.ValueChanged += (i, s) =>
            {
                if (i == 0)
                {
                    display.Robot = null;
                }
                else
                {
                    display.Robot = GetRobotWithName(s);
                }
                display.RobotName = s;
            };
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
        }

        Robot GetRobotWithName(string name)
        {
            return DisplayListPanel.DisplayDatas.
                Where(x => x.Module == Resource.Module.Robot).
                Select(x => x as RobotDisplayData).
                FirstOrDefault(x => x.RobotName == name)?.Robot;
        }

        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
    }
}
