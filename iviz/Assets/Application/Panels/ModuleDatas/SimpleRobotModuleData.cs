using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotPanelContents"/> 
    /// </summary>
    public sealed class SimpleRobotModuleData : ModuleData
    {
        const string ParamSubstring = "_description";

        [NotNull] readonly SimpleRobotPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override Resource.ModuleType ModuleType => Resource.ModuleType.Robot;
        public override IConfiguration Configuration => Robot.Config;
        public override IController Controller => Robot;

        [NotNull] public SimpleRobotController Robot { get; }

        [CanBeNull] CancellationTokenSource tokenSource;

        static readonly string[] NoneStr = {"<color=#b0b0b0ff><i><none></i></color>"};

        public SimpleRobotModuleData([NotNull] ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            Robot = new SimpleRobotController(this);
            if (constructor.Configuration != null)
            {
                Robot.Config = (RobotConfiguration) constructor.Configuration;
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

            if (!string.IsNullOrEmpty(Robot.SourceParameter))
            {
                Robot.TryLoadFromSourceParameter(Robot.SourceParameter);
            }

            panel.HelpText.Label = Robot.HelpText;
            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            Robot.StopController();
            ConnectionManager.Connection.ConnectionStateChanged -= OnConnectionStateChanged;
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = Robot;
            panel.SourceParameter.Value = Robot.SourceParameter;
            panel.HelpText.Label = Robot.HelpText;

            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            panel.SourceParameter.Hints = GetParameterHints(tokenSource.Token);
            
            panel.SavedRobotName.Options = GetSavedRobots();

            //panel.FramePrefix.Value = Robot.FramePrefix;
            //panel.FrameSuffix.Value = Robot.FrameSuffix;
            panel.AttachToTf.Value = Robot.AttachedToTf;
            panel.HideButton.State = Robot.Visible;

            panel.OcclusionOnlyMode.Value = Robot.RenderAsOcclusionOnly;
            panel.Tint.Value = Robot.Tint;
            panel.Alpha.Value = Robot.Tint.a;
            panel.Metallic.Value = Robot.Metallic;
            panel.Smoothness.Value = Robot.Smoothness;

            panel.Save.Value = IsRobotSaved;
            panel.Save.Interactable = !string.IsNullOrEmpty(Robot.Robot?.Name);

            panel.Tint.ValueChanged += f =>
                Robot.Tint = f.WithAlpha(panel.Alpha.Value);
            panel.Alpha.ValueChanged += f =>
                Robot.Tint = panel.Tint.Value.WithAlpha(f);
            panel.Metallic.ValueChanged += f => Robot.Metallic = f;
            panel.Smoothness.ValueChanged += f => Robot.Smoothness = f;
            panel.OcclusionOnlyMode.ValueChanged += f => Robot.RenderAsOcclusionOnly = f;
            panel.SavedRobotName.ValueChanged += (i, name) =>
            {
                Robot.TryLoadSavedRobot(i == 0 ? null : name);
                panel.SourceParameter.Value = "";
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Label = Robot.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable =
                    !string.IsNullOrEmpty(Robot.Robot?.Name) &&
                    !Resource.Internal.ContainsRobot(name);
            };
            panel.SourceParameter.EndEdit += f =>
            {
                Robot.TryLoadFromSourceParameter(f);
                panel.SavedRobotName.Index = 0;
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Label = Robot.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable = !string.IsNullOrEmpty(Robot.Robot?.Name);
            };
            panel.AttachToTf.ValueChanged += f => 
                Robot.AttachedToTf = f;
            panel.CloseButton.Clicked += () =>
            {
                DataPanelManager.HideSelectedPanel();
                ModuleListPanel.RemoveModule(this);
            };
            //panel.FramePrefix.EndEdit += f => Robot.FramePrefix = f;
            //panel.FrameSuffix.EndEdit += f => Robot.FrameSuffix = f;
            panel.HideButton.Clicked += () =>
            {
                Robot.Visible = !Robot.Visible;
                panel.HideButton.State = Robot.Visible;
                UpdateModuleButton();
            };
            panel.Save.ValueChanged += f =>
            {
                if (string.IsNullOrEmpty(Robot.Robot?.Name) || string.IsNullOrEmpty(Robot.Robot.Description))
                {
                    return;
                }

                if (f)
                {
                    Resource.External.AddRobotResourceAsync(Robot.Robot.Name, Robot.Robot.Description);
                }
                else
                {
                    Resource.External.RemoveRobotResource(Robot.Robot.Name);
                }
            };
        }

        public override void UpdatePanel()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            panel.SourceParameter.Hints = GetParameterHints(tokenSource.Token);

            panel.HelpText.Label = Robot.HelpText;
            Robot.CheckRobotStartTask();
            UpdateModuleButton();
            panel.Save.Interactable = !string.IsNullOrEmpty(Robot.Robot?.Name);
        }

        [NotNull, ItemNotNull]
        static IEnumerable<string> GetParameterCandidates(CancellationToken token) =>
            ConnectionManager.Connection.GetSystemParameterList(token).Where(x => x.Contains(ParamSubstring));

        [NotNull, ItemNotNull]
        static IEnumerable<string> GetSavedRobots() => NoneStr.Concat(Resource.GetRobotNames());

        [NotNull, ItemNotNull]
        static IEnumerable<string> GetParameterHints(CancellationToken token) => GetParameterCandidates(token);

        bool IsRobotSaved => Robot.Robot?.Name != null && Resource.ContainsRobot(Robot.Robot.Name);

        protected override void UpdateModuleButton()
        {
            ButtonText =
                $"{Resource.Font.Split(Robot.Name, ModuleListPanel.ModuleDataCaptionWidth)}\n<b>{ModuleType}</b>";
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<RobotConfiguration>(configAsJson);
            UpdateConfiguration(config, fields);
        }

        void UpdateConfiguration(RobotConfiguration config, IEnumerable<string> fields)
        {
            bool hasRobotName = false;
            bool hasSourceParameter = false;

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(RobotConfiguration.Visible):
                        Robot.Visible = config.Visible;
                        break;
                    case nameof(RobotConfiguration.SourceParameter) when config.SourceParameter != Robot.Config.SourceParameter:
                        hasSourceParameter = true;
                        break;
                    case nameof(RobotConfiguration.SourceParameter):
                        break;
                    case nameof(RobotConfiguration.SavedRobotName) when config.SavedRobotName != Robot.Config.SavedRobotName:
                        hasRobotName = true;
                        break;
                    case nameof(RobotConfiguration.SavedRobotName):
                        break;
                    case nameof(RobotConfiguration.FramePrefix):
                        Robot.FramePrefix = config.FramePrefix;
                        break;
                    case nameof(RobotConfiguration.FrameSuffix):
                        Robot.FrameSuffix = config.FrameSuffix;
                        break;
                    case nameof(RobotConfiguration.AttachedToTf):
                        Robot.AttachedToTf = config.AttachedToTf;
                        break;
                    case nameof(RobotConfiguration.RenderAsOcclusionOnly):
                        Robot.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(RobotConfiguration.Tint):
                        Robot.Tint = config.Tint.ToUnityColor();
                        break;
                    case nameof(RobotConfiguration.Metallic):
                        Robot.Metallic = config.Metallic;
                        break;
                    case nameof(RobotConfiguration.Smoothness):
                        Robot.Smoothness = config.Smoothness;
                        break;
                    default:
                        Logger.Warn($"{this}: Unknown field '{field}'");
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

                Robot.ProcessRobotSource(config.SavedRobotName, config.SourceParameter);

                if (IsSelected)
                {
                    panel.HelpText.Label = Robot.HelpText;
                }

                UpdateModuleButton();
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.SimpleRobots.Add(Robot.Config);
        }
    }
}