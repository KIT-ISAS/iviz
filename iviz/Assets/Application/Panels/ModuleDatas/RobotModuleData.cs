using Iviz.Controllers;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="RobotPanelContents"/> 
    /// </summary>

    public sealed class RobotModuleData : ModuleData
    {
        readonly RobotPanelContents panel;

        readonly RobotController robot;
        public string RobotName => robot.LongName;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Robot;
        public override IConfiguration Configuration => robot.Config;
        public override IController Controller => robot;

        public RobotModuleData(ModuleDataConstructor constructor) :
        base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            //robot = Instantiate<RobotController>();
            robot = new RobotController(this);
            //robot.ModuleData = this;
            if (constructor.Configuration != null)
            {
                robot.Config = (RobotConfiguration)constructor.Configuration;
            }
            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Robot) as RobotPanelContents;
            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            robot.Stop();
            //Object.Destroy(robot.gameObject);
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = robot;
            panel.ResourceType.Value = robot.RobotResource;
            panel.FramePrefix.Value = robot.FramePrefix;
            panel.FrameSuffix.Value = robot.FrameSuffix;
            panel.AttachToTF.Value = robot.AttachToTF;
            panel.HideButton.State = robot.Visible;

            panel.OcclusionOnlyMode.Value = robot.RenderAsOcclusionOnly;
            panel.Tint.Value = robot.Tint;
            panel.Alpha.Value = robot.Tint.a;

            panel.Tint.ValueChanged += f =>
            {
                Color color = f;
                color.a = panel.Alpha.Value;
                robot.Tint = color;
            };
            panel.Alpha.ValueChanged += f =>
            {
                Color color = panel.Tint.Value;
                color.a = f;
                robot.Tint = color;
            };
            panel.OcclusionOnlyMode.ValueChanged += f =>
            {
                robot.RenderAsOcclusionOnly = f;
            };

            panel.ResourceType.ValueChanged += (_, f) =>
            {
                robot.RobotResource = f;
                UpdateModuleButton();
            };
            panel.AttachToTF.ValueChanged += f =>
            {
                robot.AttachToTF = f;
            }; 
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            panel.FramePrefix.EndEdit += f =>
            {
                robot.FramePrefix = f;
            };
            panel.FrameSuffix.EndEdit += f =>
            {
                robot.FrameSuffix = f;
            };
            panel.HideButton.Clicked += () =>
            {
                robot.Visible = !robot.Visible;
                panel.HideButton.State = robot.Visible;
                UpdateModuleButton();
            };

        }

        protected override void UpdateModuleButton()
        {
            ButtonText = $"{Resource.Font.Split(robot.LongName, App.ModuleListPanel.ModuleDataCaptionWidth)}\n<b>{Module}</b>";
        }

        public override void AddToState(StateConfiguration config)
        {
            config.Robots.Add(robot.Config);
        }
    }
}
