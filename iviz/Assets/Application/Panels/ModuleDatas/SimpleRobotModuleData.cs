using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotPanelContents"/> 
    /// </summary>
    public sealed class SimpleRobotModuleData : ModuleData
    {
        const string ParamSuffix = "_description";

        [NotNull] readonly SimpleRobotPanelContents panel;
        [NotNull] readonly SimpleRobotController robot;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.Robot;
        public override IConfiguration Configuration => robot.Config;
        public override IController Controller => robot;

        static readonly string[] NoneStr = {"<color=#b0b0b0ff><i><none></i></color>"};

        public SimpleRobotModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.ModuleList, constructor.Topic, constructor.Type)
        {
            robot = new SimpleRobotController(this);
            if (constructor.Configuration != null)
            {
                robot.Config = (SimpleRobotConfiguration) constructor.Configuration;
            }

            panel = DataPanelManager.GetPanelByResourceType<SimpleRobotPanelContents>(Resource.ModuleType.Robot);
            UpdateModuleButton();

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            if (state != ConnectionState.Connected)
            {
                return;
            }

            if (!string.IsNullOrEmpty(robot.SourceParameter))
            {
                robot.TryLoadFromSourceParameter(robot.SourceParameter);
            }

            panel.HelpText.Label = robot.HelpText;
            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            robot.StopController();
            ConnectionManager.Connection.ConnectionStateChanged -= OnConnectionStateChanged;
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = robot;
            panel.SourceParam.Value = robot.SourceParameter;
            panel.HelpText.Label = robot.HelpText;

            panel.SourceParam.Hints = GetParameterHints();
            panel.SavedRobotName.Options = GetSavedRobots();

            panel.FramePrefix.Value = robot.FramePrefix;
            panel.FrameSuffix.Value = robot.FrameSuffix;
            panel.AttachToTf.Value = robot.AttachedToTf;
            panel.HideButton.State = robot.Visible;

            panel.OcclusionOnlyMode.Value = robot.RenderAsOcclusionOnly;
            panel.Tint.Value = robot.Tint;
            panel.Alpha.Value = robot.Tint.a;

            panel.Save.Value = IsRobotSaved;
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
            panel.SavedRobotName.ValueChanged += (i, name) =>
            {
                robot.TryLoadSavedRobot(i == 0 ? null : name);
                panel.SourceParam.Value = "";
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Label = robot.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable =
                    !string.IsNullOrEmpty(robot.Robot?.Name) &&
                    !Resource.Internal.ContainsRobot(name);
            };
            panel.SourceParam.EndEdit += f =>
            {
                robot.TryLoadFromSourceParameter(f);
                panel.SavedRobotName.Index = 0;
                panel.Save.Value = IsRobotSaved;

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
            ConnectionManager.Connection.GetSystemParameterList().Where(x => x.HasSuffix(ParamSuffix));

        static IEnumerable<string> GetSavedRobots() => NoneStr.Concat(Resource.GetRobotNames());

        static IEnumerable<string> GetParameterHints() => GetParameterCandidates();

        bool IsRobotSaved => robot.Robot?.Name != null && Resource.ContainsRobot(robot.Robot.Name);

        protected override void UpdateModuleButton()
        {
            ButtonText = $"{Resource.Font.Split(robot.Name, ModuleListPanel.ModuleDataCaptionWidth)}\n<b>{ModuleType}</b>";
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<SimpleRobotConfiguration>(configAsJson);
            bool hasRobotName = false;
            bool hasSourceParameter = false;

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(SimpleRobotConfiguration.Visible):
                        robot.Visible = config.Visible;
                        break;
                    case nameof(SimpleRobotConfiguration.SourceParameter):
                        hasSourceParameter = true;
                        break;
                    case nameof(SimpleRobotConfiguration.SavedRobotName):
                        hasRobotName = true;
                        break;
                    case nameof(SimpleRobotConfiguration.FramePrefix):
                        robot.FramePrefix = config.FramePrefix;
                        break;
                    case nameof(SimpleRobotConfiguration.FrameSuffix):
                        robot.FrameSuffix = config.FrameSuffix;
                        break;
                    case nameof(SimpleRobotConfiguration.AttachedToTf):
                        robot.AttachedToTf = config.AttachedToTf;
                        break;
                    case nameof(SimpleRobotConfiguration.RenderAsOcclusionOnly):
                        robot.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(SimpleRobotConfiguration.Tint):
                        robot.Tint = config.Tint;
                        break;
                    default:
                        Core.Logger.External(LogLevel.Warn, $"{this}: Unknown field '{field}'");
                        break;
                }
            }

            if (hasRobotName || hasSourceParameter)
            {
                if (!hasRobotName)
                {
                    config.SavedRobotName = "";
                }

                if (!hasSourceParameter)
                {
                    config.SourceParameter = "";
                }

                robot.ProcessRobotSource(config.SavedRobotName, config.SourceParameter);

                panel.HelpText.Label = robot.HelpText;
                UpdateModuleButton();
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.SimpleRobots.Add(robot.Config);
        }
    }
}