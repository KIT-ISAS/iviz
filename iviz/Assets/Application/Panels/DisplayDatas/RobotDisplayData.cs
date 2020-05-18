using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class RobotDisplayData : DisplayData
    {
        readonly RobotPanelContents panel;

        public Robot Robot { get; }
        public string RobotName => Robot.LongName;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Robot;

        public override IConfiguration Configuration => Robot.Config;

        public RobotDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.Robot);
            Robot = displayObject.GetComponent<Robot>();
            Robot.Parent = TFListener.ListenersFrame;
            Robot.DisplayData = this;
            if (constructor.Configuration != null)
            {
                Robot.Config = (RobotConfiguration)constructor.Configuration;
            }
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Robot) as RobotPanelContents;
            UpdateButtonText();
        }

        /*
        public override DisplayData Initialize(DisplayListPanel displayList, string topic, string type)
        {
            base.Initialize(displayList, topic, type);

            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.Robot);
            display = displayObject.GetComponent<Robot>();
            display.Parent = TFListener.ListenersFrame;
            display.DisplayData = this;
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Robot) as RobotPanelContents;
            return this;
        }

        public override DisplayData Deserialize(JToken j)
        {
            display.Config = j.ToObject<RobotConfiguration>();
            return this;
        }

        public override void Start()
        {
            base.Start();
            UpdateButtonText();
        }
        */

        public override void Stop()
        {
            base.Stop();

            Robot.Stop();
            ResourcePool.Dispose(Resource.Listeners.Robot, Robot.gameObject);
            //display = null;
        }

        public override void SetupPanel()
        {
            panel.ResourceType.Value = Robot.RobotResource;
            panel.FramePrefix.Value = Robot.FramePrefix;
            panel.FrameSuffix.Value = Robot.FrameSuffix;
            panel.AttachToTF.Value = Robot.AttachToTF;

            panel.ResourceType.ValueChanged += (_, f) =>
            {
                Robot.RobotResource = f;
                UpdateButtonText();
            };
            panel.AttachToTF.ValueChanged += f =>
            {
                Robot.AttachToTF = f;
            }; 
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                DisplayListPanel.RemoveDisplay(this);
            };
            panel.FramePrefix.EndEdit += f =>
            {
                Robot.FramePrefix = f;
            };
            panel.FrameSuffix.EndEdit += f =>
            {
                Robot.FrameSuffix = f;
            };
        }

        protected override void UpdateButtonText()
        {
            const int maxLength = 20;
            if (Robot.LongName.Length > maxLength)
            {
                string nameShort = Robot.LongName.Substring(0, maxLength);
                ButtonText = $"{nameShort}...\n<b>{Module}</b>";
            }
            else
            {
                ButtonText = $"{Robot.LongName}\n<b>{Module}</b>";
            }
        }

        /*
        public override JToken Serialize()
        {
            return JToken.FromObject(display.Config);
        }
        */

        public override void AddToState(StateConfiguration config)
        {
            config.Robots.Add(Robot.Config);
        }
    }
}
