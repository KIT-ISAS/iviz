using Iviz.App.Listeners;
using Iviz.Resources;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="RobotPanelContents"/> 
    /// </summary>

    public class RobotModuleData : ModuleData
    {
        readonly RobotPanelContents panel;

        readonly RobotController Robot;
        public string RobotName => Robot.LongName;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Robot;
        public override IConfiguration Configuration => Robot.Config;
        public override IController Controller => Robot;

        public RobotModuleData(ModuleDataConstructor constructor) :
        base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            Robot = Instantiate<RobotController>();
            Robot.ModuleData = this;
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
            Object.Destroy(Robot.gameObject);
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = Robot;
            panel.ResourceType.Value = Robot.RobotResource;
            panel.FramePrefix.Value = Robot.FramePrefix;
            panel.FrameSuffix.Value = Robot.FrameSuffix;
            panel.AttachToTF.Value = Robot.AttachToTF;
            panel.HideButton.State = Robot.Visible;

            panel.OcclusionOnlyMode.Value = Robot.RenderAsOcclusionOnly;
            panel.Tint.Value = Robot.Tint;
            panel.Alpha.Value = Robot.Tint.a;

            panel.Tint.ValueChanged += f =>
            {
                Color color = f;
                color.a = panel.Alpha.Value;
                Robot.Tint = color;
            };
            panel.Alpha.ValueChanged += f =>
            {
                Color color = panel.Tint.Value;
                color.a = f;
                Robot.Tint = color;
            };
            panel.OcclusionOnlyMode.ValueChanged += f =>
            {
                Robot.RenderAsOcclusionOnly = f;
            };

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
                ModuleListPanel.RemoveModule(this);
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
            ButtonText = $"{Resource.Font.Split(Robot.LongName, MaxTextRowLength)}\n<b>{Module}</b>";
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Robots.Add(Robot.Config);
        }
    }
}
