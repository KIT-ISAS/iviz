#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Resources;

namespace Iviz.App
{
    public class SettingsDialogData : DialogData
    {
        static readonly string[] TargetFpsOptions = {"Default", "60", "30", "15"};
        static readonly string[] NetworkProcessingOptions = {"Every Frame", "Every Second", "Every Fourth"};

        enum ModelServerModes
        {
            Off,
            On,
            OnWithFile
        }

        static readonly string[] ModelServerModesNames = {"Off", "On", "On + File"};

        readonly SettingsDialogContents panel;
        readonly Controllers.ModelService modelService = new();
        public override IDialogPanelContents Panel => panel;

        static ISettingsManager SettingsManager => Settings.SettingsManager ??
                                                   throw new InvalidOperationException(
                                                       "Settings Dialog used without a SettingsManager!");

        public SettingsDialogData(SettingsConfiguration? config = null)
        {
            panel = DialogPanelManager.GetPanelByType<SettingsDialogContents>(DialogPanelType.Settings);
            if (config != null)
            {
                SettingsManager.Config = config;
            }
        }

        public override void SetupPanel()
        {
            panel.QualityInView.Options = SettingsManager.QualityLevelsInView;
            panel.QualityInView.Index = (int) SettingsManager.QualityInView;

            panel.QualityInAr.Options = SettingsManager.QualityLevelsInAR;
            panel.QualityInAr.Index = (int) SettingsManager.QualityInAr;
            panel.QualityInAr.Interactable = Settings.SupportsAR;

            panel.TargetFps.Options = TargetFpsOptions;
            panel.NetworkProcessing.Options = NetworkProcessingOptions;
            panel.SunDirection.SetMinValue(-60).SetMaxValue(60).SetIntegerOnly(true);

            panel.BackgroundColor.Value = SettingsManager.BackgroundColor.WithAlpha(1);

            panel.SunDirection.Value = SettingsManager.SunDirection;

            panel.TargetFps.Index = SettingsManager.TargetFps switch
            {
                Settings.DefaultFps => 0,
                <= 15 => 3,
                <= 30 => 2,
                <= 60 => 1,
                _ => 0
            };

            panel.NetworkProcessing.Index = SettingsManager.NetworkFrameSkip switch
            {
                1 => 0,
                2 => 1,
                _ => 2
            };

            panel.ModelCacheLabel.text = $"<b>Model Cache:</b> {Resource.External.ResourceCount.ToString()} files";
            panel.SavedFilesLabel.text = $"<b>Saved Files:</b> {ModuleListPanel.NumSavedFiles.ToString()} files";
            panel.HostHistoryLabel.text = $"<b>Host History:</b> {ModuleListPanel.Instance.NumMastersInCache.ToString()} entries";

            panel.ModelService.Options = ModelServerModesNames;
            panel.ModelService.Text = UpdateModelServiceLabel();
            panel.ModelService.Interactable = !Settings.IsMobile;

            panel.QualityInView.ValueChanged += (f, _) =>
            {
                SettingsManager.QualityInView = (QualityType) f;
                ModuleListPanel.UpdateSettings();
            };
            panel.QualityInAr.ValueChanged += (f, _) =>
            {
                SettingsManager.QualityInAr = (QualityType) f;
                ModuleListPanel.UpdateSettings();
            };

            panel.BackgroundColor.ValueChanged += c =>
            {
                SettingsManager.BackgroundColor = c;
                ModuleListPanel.UpdateSettings();
            };

            panel.TargetFps.ValueChanged += (i, _) =>
            {
                SettingsManager.TargetFps = i switch
                {
                    0 => Settings.DefaultFps,
                    1 => 60,
                    2 => 30,
                    3 => 15,
                    _ => SettingsManager.TargetFps
                };

                ModuleListPanel.UpdateSettings();
            };

            panel.NetworkProcessing.ValueChanged += (i, _) =>
            {
                SettingsManager.NetworkFrameSkip = i switch
                {
                    0 => 1,
                    1 => 2,
                    2 => 4,
                    _ => SettingsManager.NetworkFrameSkip
                };

                ModuleListPanel.UpdateSettings();
            };

            panel.SunDirection.ValueChanged += f =>
            {
                SettingsManager.SunDirection = (int) f;
                ModuleListPanel.UpdateSettings();
            };

            panel.Close.Clicked += Close;

            // ReSharper disable once AsyncVoidLambda
            panel.ClearModelCacheClicked += async () =>
            {
                RosLogger.Info("Settings: Clearing model cache.");
                await Resource.External.ClearModelCacheAsync();
                panel.ModelCacheLabel.text = $"<b>Model Cache:</b> {Resource.External.ResourceCount.ToString()} files";
            };

            // ReSharper disable once AsyncVoidLambda
            panel.ClearHostHistoryClicked += async () =>
            {
                RosLogger.Info("Settings: Clearing cache of master uris.");
                await ModuleListPanel.Instance.ClearMastersCacheAsync();
                panel.HostHistoryLabel.text =
                    $"<b>Host History:</b> {ModuleListPanel.Instance.NumMastersInCache.ToString()} entries";
            };

            panel.ClearSavedFilesClicked += () =>
            {
                RosLogger.Info("Settings: Clearing saved files.");
                ModuleListPanel.ClearSavedFiles();
                panel.SavedFilesLabel.text = $"<b>Saved:</b> {ModuleListPanel.NumSavedFiles.ToString()} files";
            };

            panel.ModelService.ValueChanged += (i, s) =>
            {
                switch ((ModelServerModes) i)
                {
                    case ModelServerModes.Off:
                        modelService.Stop();
                        break;
                    case ModelServerModes.On:
                        _ = modelService.RestartAsync(false);
                        break;
                    case ModelServerModes.OnWithFile:
                        _ = modelService.RestartAsync(true);
                        break;
                }

                panel.ModelService.Text = UpdateModelServiceLabel();
            };
        }

        string UpdateModelServiceLabel()
        {
            if (Settings.IsMobile)
            {
                return "<b>Model Service:</b> Off (Mobile)";
            }

            if (!modelService.IsEnabled)
            {
                return "<b>Model Service:</b> Off";
            }

            if (modelService.IsFileSchemeEnabled)
            {
                return "<b>Model Service:</b> " + modelService.NumPackages +
                       " packages\n<b>[File scheme enabled]</b>";
            }

            return "<b>Model Service:</b> " + modelService.NumPackages + " packages";
        }
    }
}