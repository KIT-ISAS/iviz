using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Resources;
using Iviz.Roslib;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotPanelContents"/> 
    /// </summary>
    public sealed class SimpleRobotModuleData : ModuleData
    {
        const string ParamSuffix = "_description";

        readonly SimpleRobotPanelContents panel;
        readonly SimpleRobotController robot;

        public override DataPanelContents Panel => panel;
        public override Resource.Module Module => Resource.Module.Robot;
        public override IConfiguration Configuration => robot.Config;
        public override IController Controller => robot;

        public SimpleRobotModuleData(ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            robot = new SimpleRobotController(this);
            if (constructor.Configuration != null)
            {
                robot.Config = (SimpleRobotConfiguration) constructor.Configuration;
            }

            panel = DataPanelManager.GetPanelByResourceType(Resource.Module.Robot) as SimpleRobotPanelContents;
            UpdateModuleButton();

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            if (state != ConnectionState.Connected)
            {
                return;
            }

            robot.SourceParameter = robot.SourceParameter;
            panel.HelpText.Label = robot.HelpText;
            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            robot.Stop();
            ConnectionManager.Connection.ConnectionStateChanged -= OnConnectionStateChanged;
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = robot;
            panel.SourceParam.Value = robot.SourceParameter;
            panel.HelpText.Label = robot.HelpText;
            
            panel.SourceParam.Hints = GetParameterHints();

            panel.FramePrefix.Value = robot.FramePrefix;
            panel.FrameSuffix.Value = robot.FrameSuffix;
            panel.AttachToTf.Value = robot.AttachedToTf;
            panel.HideButton.State = robot.Visible;

            panel.OcclusionOnlyMode.Value = robot.RenderAsOcclusionOnly;
            panel.Tint.Value = robot.Tint;
            panel.Alpha.Value = robot.Tint.a;

            panel.Save.Value = IsRobotSaved();
            panel.Save.Interactable = !string.IsNullOrEmpty(robot.Robot?.Name);

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
            panel.SourceParam.EndEdit += f =>
            {
                robot.SourceParameter = f;
                panel.Save.Value = IsRobotSaved();

                panel.HelpText.Label = robot.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable = !string.IsNullOrEmpty(robot.Robot?.Name);
            };
            panel.AttachToTf.ValueChanged += f =>
            {
                robot.AttachedToTf = f;
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
            panel.Save.ValueChanged += f =>
            {
                if (string.IsNullOrEmpty(robot?.Robot?.Name) || string.IsNullOrEmpty(robot.Robot.Description))
                {
                    return;
                }

                if (f)
                {
                    Resource.External.AddRobot(robot.Robot.Name, robot.Robot.Description);
                }
                else
                {
                    Resource.External.RemoveRobot(robot.Robot.Name);
                }
            };
        }

        public override void UpdatePanel()
        {
            base.UpdatePanel();
            panel.SourceParam.Hints = GetParameterHints();
        }

        static IEnumerable<string> GetParameterCandidates() =>
            ConnectionManager.GetSystemParameterList().Where(x => x.HasSuffix(ParamSuffix));

        static IEnumerable<string> GetSavedRobotsCandidates() =>
            Resource.External.GetRobotNames().Select(name => $"{SimpleRobotController.LocalPrefix}{name}");
        
        static IEnumerable<string> GetParameterHints() => GetParameterCandidates().Concat(GetSavedRobotsCandidates());
        
        bool IsRobotSaved() => robot.Robot?.Name != null && Resource.External.ContainsRobot(robot.Robot.Name);
        
        protected override void UpdateModuleButton()
        {
            ButtonText = $"{Resource.Font.Split(robot.Name, ModuleListPanel.ModuleDataCaptionWidth)}\n<b>{Module}</b>";
        }

        public override void AddToState(StateConfiguration config)
        {
            config.SimpleRobots.Add(robot.Config);
        }
    }
}