using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class RobotDisplayData : DisplayData
    {
        readonly RobotPanelContents panel;

        readonly Robot Robot;
        public string RobotName => Robot.LongName;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Robot;
        public override IConfiguration Configuration => Robot.Config;
        public override IController Controller => Robot;

        public RobotDisplayData(DisplayDataConstructor constructor) :
        base(constructor.DisplayList, constructor.Topic, constructor.Type)
        {
            GameObject displayObject = ResourcePool.GetOrCreate(Resource.Listeners.Robot);
            Robot = displayObject.GetComponent<Robot>();
            //Robot.Parent = TFListener.ListenersFrame;
            //Robot.DisplayData = this;
            if (constructor.Configuration != null)
            {
                Robot.Config = (RobotConfiguration)constructor.Configuration;
            }
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Robot) as RobotPanelContents;
            UpdateButtonText();
        }

        public override void Stop()
        {
            base.Stop();

            Robot.Stop();
            ResourcePool.Dispose(Resource.Listeners.Robot, Robot.gameObject);
        }

        public override void SetupPanel()
        {
            panel.ResourceType.Value = Robot.RobotResource;
            panel.FramePrefix.Value = Robot.FramePrefix;
            panel.FrameSuffix.Value = Robot.FrameSuffix;
            panel.AttachToTF.Value = Robot.AttachToTF;
            panel.HideButton.State = Robot.Visible;

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
            panel.HideButton.Clicked += () =>
            {
                Robot.Visible = !Robot.Visible;
                panel.HideButton.State = Robot.Visible;
                UpdateButtonText();
            };
        }

        protected override void UpdateButtonText()
        {
            ButtonText = $"{RosUtils.SanitizedText(Robot.LongName, MaxTextRowLength)}\n<b>{Module}</b>";
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Robots.Add(Robot.Config);
        }
    }
}
