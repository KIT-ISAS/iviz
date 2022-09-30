#nullable enable

using System;
using Iviz.Common;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Resources;
using Iviz.Ros;

namespace Iviz.App
{
    public sealed class SettingsDialogData : DialogData
    {
        static readonly string[] TargetFpsOptions = {"Default", "60", "30", "15"};
        static readonly string[] NetworkProcessingOptions = {"Every Frame", "Every Second", "Every Fourth"};

        readonly SettingsDialogPanel panel;
        public override IDialogPanel Panel => panel;

        static ISettingsManager SettingsManager => Settings.SettingsManager;

        public SettingsDialogData(SettingsConfiguration? config = null)
        {
            panel = DialogPanelManager.GetPanelByType<SettingsDialogPanel>(DialogPanelType.Settings);
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
            panel.QualityInAr.Interactable = Settings.SupportsMobileAR;

            panel.TargetFps.Options = TargetFpsOptions;
            panel.NetworkProcessing.Options = NetworkProcessingOptions;

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
            
            panel.Close.Clicked += Close;

            panel.ClearModelCacheClicked += async () =>
            {
                RosLogger.Info("Settings: Clearing model cache.");
                await Resource.External.ClearModelCacheAsync();
                panel.ModelCacheLabel.text = $"<b>Model Cache:</b> {Resource.External.ResourceCount.ToString()} files";
            };
            
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
        }
    }
}