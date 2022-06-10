#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Resources;
using Iviz.Ros;
using Newtonsoft.Json;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="SimpleRobotModulePanel"/> 
    /// </summary>
    public sealed class SimpleRobotModuleData : ModuleData
    {
        const string ParamSubstring = "_description";
        const string NoneStr = "<color=#b0b0b0ff><i><none></i></color>";

        readonly SimpleRobotModulePanel panel;

        public override ModulePanel Panel => panel;
        public override ModuleType ModuleType => ModuleType.Robot;
        public override IConfiguration Configuration => RobotController.Config;
        public override IController Controller => RobotController;

        public SimpleRobotController RobotController { get; }

        CancellationTokenSource? tokenSource;

        public SimpleRobotModuleData(ModuleDataConstructor constructor)
        {
            RobotController = new SimpleRobotController((RobotConfiguration?) constructor.Configuration);
            panel = ModulePanelManager.GetPanelByResourceType<SimpleRobotModulePanel>(ModuleType.Robot);
            UpdateModuleButton();

            RobotController.RobotFinishedLoading += OnRobotFinishedLoading;
            RosConnection.ConnectionStateChanged += OnConnectionStateChanged;
        }

        void OnConnectionStateChanged(ConnectionState state)
        {
            if (state != ConnectionState.Connected)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(RobotController.SourceParameter))
            {
                RobotController.TryLoadFromSourceParameter(RobotController.SourceParameter);
            }

            panel.HelpText.Text = RobotController.HelpText;
            UpdateModuleButton();
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                RobotController.RobotFinishedLoading -= OnRobotFinishedLoading;
                RobotController.Dispose();
                RosConnection.ConnectionStateChanged -= OnConnectionStateChanged;
            }
            catch (Exception e)
            {
                RosLogger.Error($"{this}: Failed to dispose controller", e);
            }               
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
            if (string.IsNullOrWhiteSpace(RobotController.SavedRobotName) ||
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
            panel.EnableColliders.Value = RobotController.Interactable;
            panel.Tint.Value = RobotController.Tint;
            panel.Alpha.Value = RobotController.Tint.a;
            panel.Metallic.Value = RobotController.Metallic;
            panel.Smoothness.Value = RobotController.Smoothness;

            panel.Prefix.Value = RobotController.FramePrefix;
            panel.Suffix.Value = RobotController.FrameSuffix;

            panel.Save.Value = IsRobotSaved;
            panel.Save.Interactable = !string.IsNullOrWhiteSpace(RobotController.Robot?.Name);

            panel.Tint.ValueChanged += f =>
                RobotController.Tint = f.WithAlpha(panel.Alpha.Value);
            panel.Alpha.ValueChanged += f =>
                RobotController.Tint = panel.Tint.Value.WithAlpha(f);
            panel.Metallic.ValueChanged += f => RobotController.Metallic = f;
            panel.Smoothness.ValueChanged += f => RobotController.Smoothness = f;
            panel.OcclusionOnlyMode.ValueChanged += f => RobotController.RenderAsOcclusionOnly = f;
            panel.EnableColliders.ValueChanged += f => RobotController.Interactable = f;
            panel.SavedRobotName.ValueChanged += (i, name) =>
            {
                RobotController.TryLoadSavedRobot(i == 0 ? null : name);
                panel.SourceParameter.Value = "";
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Text = RobotController.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable =
                    !string.IsNullOrWhiteSpace(RobotController.Robot?.Name) &&
                    !Resource.Internal.ContainsRobot(name);
            };
            panel.SourceParameter.Submit += f =>
            {
                RobotController.TryLoadFromSourceParameter(f);
                panel.SavedRobotName.Index = 0;
                panel.Save.Value = IsRobotSaved;

                panel.HelpText.Text = RobotController.HelpText;
                UpdateModuleButton();

                panel.Save.Interactable = !string.IsNullOrWhiteSpace(RobotController.Robot?.Name);
            };
            panel.AttachToTf.ValueChanged += f =>
                RobotController.AttachedToTf = f;
            panel.CloseButton.Clicked += Close;
            panel.HideButton.Clicked += ToggleVisible;
            panel.Save.ValueChanged += f =>
            {
                var robot = RobotController.Robot;
                if (robot?.Name is null or "" || robot.Description is null or "")
                {
                    return;
                }

                if (f)
                {
                    async ValueTask AddRobotResourceAsync()
                    {
                        await Resource.External.AddRobotResourceAsync(robot.Name, robot.Description);
                        panel.SavedRobotName.Options = GetSavedRobots();
                    }
                    
                    _ = AddRobotResourceAsync();
                }
                else
                {
                    _ = Resource.External.RemoveRobotResourceAsync(robot.Name);
                }
            };

            panel.Prefix.Submit += f => RobotController.FramePrefix = f;
            panel.Suffix.Submit += f => RobotController.FrameSuffix = f;
            panel.ResetButton.Clicked += RobotController.ResetController;

            RobotController.UpdateStartTaskStatus();
            UpdateModuleButton();
        }

        public override void UpdatePanel()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();
            panel.SourceParameter.Hints = GetParameterHints(tokenSource.Token);

            panel.HelpText.Text = RobotController.HelpText;
            RobotController.UpdateStartTaskStatus();
            UpdateModuleButton();
            panel.Save.Interactable = !string.IsNullOrWhiteSpace(RobotController.Robot?.Name);
        }

        static IEnumerable<string> GetParameterCandidates(CancellationToken token)
        {
            var list = RosManager.Connection.GetSystemParameterList(token)
                .Where(x => x.Contains(ParamSubstring))
                .ToList();
            list.Sort();
            return list;
        }

        static IEnumerable<string> GetSavedRobots() => Resource.GetRobotNames().Prepend(NoneStr);

        static IEnumerable<string> GetParameterHints(CancellationToken token) => GetParameterCandidates(token);

        bool IsRobotSaved => RobotController.Robot?.Name != null && Resource.IsRobotSaved(RobotController.Robot.Name);

        protected override void UpdateModuleButton()
        {
            ModuleListButtonText = ModuleListPanel.CreateButtonTextForModule(this, RobotController.Name);
        }

        void OnRobotFinishedLoading()
        {
            UpdateModuleButton();
        }

        public override void UpdateConfiguration(string configAsJson, string[] fields)
        {
            var config = JsonConvert.DeserializeObject<RobotConfiguration>(configAsJson);
            UpdateConfiguration(config, fields);
        }

        public void UpdateConfiguration(RobotConfiguration config, string[] fields)
        {
            bool hasRobotName = false;
            bool hasSourceParameter = false;

            foreach (string field in fields)
            {
                switch (field)
                {
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
                        RobotController.Tint = config.Tint.ToUnity();
                        break;
                    case nameof(RobotConfiguration.Metallic):
                        RobotController.Metallic = config.Metallic;
                        break;
                    case nameof(RobotConfiguration.Smoothness):
                        RobotController.Smoothness = config.Smoothness;
                        break;
                    case nameof(RobotConfiguration.Visible):
                        RobotController.Visible = config.Visible;
                        break;
                    case nameof(RobotConfiguration.Interactable):
                        RobotController.Interactable = config.Interactable;
                        break;
                    case nameof(RobotConfiguration.KeepMeshMaterials):
                        RobotController.KeepMeshMaterials = config.KeepMeshMaterials;
                        break;
                    case nameof(IConfiguration.ModuleType):
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

                if (IsPanelSelected)
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