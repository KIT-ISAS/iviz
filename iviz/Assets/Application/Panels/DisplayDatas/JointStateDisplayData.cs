using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class JointStateDisplayData : DisplayableListenerData
    {
        JointStateListener listener;
        JointStatePanelContents panel;
        readonly List<string> robotNames = new List<string>();

        public override DataPanelContents Panel => panel;
        public override TopicListener Listener => listener;
        public override Resource.Module Module => Resource.Module.JointState;

        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.JointState);
            displayObject.name = "JointState:" + Topic;

            listener = displayObject.GetComponent<JointStateListener>();
            listener.Config.topic = Topic;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.JointState) as JointStatePanelContents;

            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            listener.Config = j.ToObject<JointStateListener.Configuration>();
            Topic = listener.Config.topic;
            listener.Robot = GetRobotWithName(listener.RobotName);
            return this;
        }

        public override void Start()
        {
            base.Start();
            listener.StartListening();
        }

        public override void Cleanup()
        {
            base.Cleanup();

            listener.Stop();
            ResourcePool.Dispose(Resource.Listeners.PointCloud, listener.gameObject);
            listener = null;
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
            return JToken.FromObject(listener.Config);
        }
    }
}
