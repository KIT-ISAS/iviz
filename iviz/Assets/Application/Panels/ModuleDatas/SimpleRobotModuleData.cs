#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Ros;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotPanelContents"/> 
    /// </summary>
    public sealed class SimpleRobotModuleData : ModuleData
    {
        const string ParamSubstring = "_description";

        readonly SimpleRobotPanelContents panel;

        public override DataPanelContents Panel => panel;
        public override ModuleType ModuleType => ModuleType.Robot;
        public override IConfiguration Configuration => RobotController.Config;
        public override IController Controller => RobotController;

        public SimpleRobotController RobotController { get; }

        CancellationTokenSource? tokenSource;

        static readonly string[] NoneStr = {"<color=#b0b0b0ff><i><none></i></color>"};

        public SimpleRobotModuleData(ModuleDataConstructor constructor) :
            base(constructor.Topic, constructor.Type)
        {
            RobotController = new SimpleRobotController(this);
            if (constructor.Configuration != null)
            {
                RobotController.Config = (RobotConfiguration) constructor.Configuration;
            }

            panel = DataPanelManager.GetPanelByResourceType<SimpleRobotPanelContents>(ModuleType.Robot);
            UpdateModuleButton();

            ConnectionManager.Connection.ConnectionStateChanged += OnConnectionStateChanged;
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            if (state != ConnectionState.Connected)
            {
                return;
            }

            if (!string.IsNullOrEmpty(RobotController.SourceParameter))
            {
                RobotController.TryLoadFromSourceParameter(RobotController.SourceParameter);
            }

            panel.HelpText.Text = RobotController.HelpText;
            UpdateModuleButton();
        }

        public override void Stop()
        {
            base.Stop();
            RobotController.StopController();
            ConnectionManager.Connection.ConnectionStateChanged -= OnConnectionStateChanged;
        }

        public override void SetupPanel()
        {
            panel.Frame.Owner = RobotController;
            panel.SourceParameter.Value = RobotController.SourceParameter;
            panel.HelpText.Text = RobotController.HelpText;

            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            panel.SourceParameter.Hints = GetParameterHints(tokenSource.Token);

            panel.SavedRobotName.Options = GetSavedRobots();
            if (string.IsNullOrEmpty(RobotController.SavedRobotName) ||
                !GetSavedRobots().Contains(RobotController.SavedRobotName))
            {
                panel.SavedRobotName.Index = 0;
            }
            else
            {
                panel.SavedRobotName.Value = RobotController.SavedRobotName;
            }

            panel.AttachToTf.Value = RobotController.AttachedToTf;
            panel.HideButton.State = RobotController.Visible;

            panel.OcclusionOnlyMode.Value = RobotController.RenderAsOcclusionOnly;
            panel.Tint.Value = RobotController.Tint;
            panel.Alpha.Value = RobotController.Tint.a;
            panel.Metallic.Value = RobotController.Metallic;
            panel.Smoothness.Value = RobotController.Smoothness;

            panel.Save.Value = IsRobotSaved;
            panel.Save.Interactable = !string.IsNullOrEmpty(RobotController.Robot?.Name);

            panel.Tint.ValueChanged += f =>
                RobotController.Tint = f.WithAlpha(panel.Alpha.Value);
            panel.Alpha.ValueChanged += f =>
                RobotController.Tint = panel.Tint.Value.WithAlpha(f);
            panel.Metallic.ValueChanged += f => RobotController.Metallic = f;
            panel.Smoothness.ValueChanged += f => RobotController.Smoothness = f;
            panel.OcclusionOnlyMode.ValueChanged += f => RobotController.RenderAsOcclusionOnly = f;
            panel.SavedRobotName.ValueChanged += (i, name) =>
            {
                RobotController.TryLoadSavedRobot(i == 0 ? null : name);
                panel.SourceParameter.Value = "";
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Text = RobotController.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable =
                    !string.IsNullOrEmpty(RobotController.Robot?.Name) &&
                    !Resource.Internal.ContainsRobot(name);
            };
            panel.SourceParameter.EndEdit += f =>
            {
                RobotController.TryLoadFromSourceParameter(f);
                panel.SavedRobotName.Index = 0;
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Text = RobotController.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable = !string.IsNullOrEmpty(RobotController.Robot?.Name);
            };
            panel.AttachToTf.ValueChanged += f =>
                RobotController.AttachedToTf = f;
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.Save.ValueChanged += f =>
            {
                if (string.IsNullOrEmpty(RobotController.Robot?.Name) ||
                    string.IsNullOrEmpty(RobotController.Robot.Description))
                {
                    return;
                }

                if (f)
                {
                    Resource.External.AddRobotResourceAsync(RobotController.Robot.Name,
                        RobotController.Robot.Description);
                }
                else
                {
                    Resource.External.RemoveRobotResource(RobotController.Robot.Name);
                }
            };

            RobotController.CheckRobotStartTask();

            RobotController.CheckRobotStartTask();
            UpdateModuleButton();
        }

        public override void UpdatePanel()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            panel.SourceParameter.Hints = GetParameterHints(tokenSource.Token);

            panel.HelpText.Text = RobotController.HelpText;
            RobotController.CheckRobotStartTask();
            UpdateModuleButton();
            panel.Save.Interactable = !string.IsNullOrEmpty(RobotController.Robot?.Name);
        }

        static IEnumerable<string> GetParameterCandidates(CancellationToken token)
        {
            var list = ConnectionManager.Connection.GetSystemParameterList(token)
                .Where(x => x.Contains(ParamSubstring))
                .ToList();
            list.Sort();
            return list;
        }


        static IEnumerable<string> GetSavedRobots() => NoneStr.Concat(Resource.GetRobotNames());

        static IEnumerable<string> GetParameterHints(CancellationToken token) => GetParameterCandidates(token);

        bool IsRobotSaved => RobotController.Robot?.Name != null && Resource.IsRobotSaved(RobotController.Robot.Name);

        protected override void UpdateModuleButton()
        {
            string text =
                $"{Resource.Font.Split(RobotController.Name, ModuleListPanel.ModuleDataCaptionWidth)}\n<b>{ModuleType}</b>";
            ButtonText = RobotController.Visible ? text : $"<color=grey>{text}</color>";
        }

        public void OnRobotFinishedLoading()
        {
            UpdateModuleButton();
        }

        public override void UpdateConfiguration(string configAsJson, IEnumerable<string> fields)
        {
            var config = JsonConvert.DeserializeObject<RobotConfiguration>(configAsJson);
            UpdateConfiguration(config, fields);
        }

        public void UpdateConfiguration(RobotConfiguration config, IEnumerable<string> fields)
        {
            bool hasRobotName = false;
            bool hasSourceParameter = false;

            foreach (string field in fields)
            {
                switch (field)
                {
                    case nameof(RobotConfiguration.Visible):
                        RobotController.Visible = config.Visible;
                        break;
                    case nameof(RobotConfiguration.SourceParameter)
                        when config.SourceParameter != RobotController.Config.SourceParameter:
                        hasSourceParameter = true;
                        break;
                    case nameof(RobotConfiguration.SourceParameter):
                        break;
                    case nameof(RobotConfiguration.SavedRobotName)
                        when config.SavedRobotName != RobotController.Config.SavedRobotName:
                        hasRobotName = true;
                        break;
                    case nameof(RobotConfiguration.SavedRobotName):
                        break;
                    case nameof(RobotConfiguration.FramePrefix):
                        RobotController.FramePrefix = config.FramePrefix;
                        break;
                    case nameof(RobotConfiguration.FrameSuffix):
                        RobotController.FrameSuffix = config.FrameSuffix;
                        break;
                    case nameof(RobotConfiguration.AttachedToTf):
                        RobotController.AttachedToTf = config.AttachedToTf;
                        break;
                    case nameof(RobotConfiguration.RenderAsOcclusionOnly):
                        RobotController.RenderAsOcclusionOnly = config.RenderAsOcclusionOnly;
                        break;
                    case nameof(RobotConfiguration.Tint):
                        RobotController.Tint = config.Tint.ToUnityColor();
                        break;
                    case nameof(RobotConfiguration.Metallic):
                        RobotController.Metallic = config.Metallic;
                        break;
                    case nameof(RobotConfiguration.Smoothness):
                        RobotController.Smoothness = config.Smoothness;
                        break;
                    default:
                        RosLogger.Warn($"{this}: Unknown field '{field}'");
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

                RobotController.ProcessRobotSource(config.SavedRobotName, config.SourceParameter);

                if (IsSelected)
                {
                    panel.HelpText.Text = RobotController.HelpText;
                }

                UpdateModuleButton();
            }

            ResetPanel();
        }

        public override void AddToState(StateConfiguration config)
        {
            config.SimpleRobots.Add(RobotController.Config);
        }
    }
}