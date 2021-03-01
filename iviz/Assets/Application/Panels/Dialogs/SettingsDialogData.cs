using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine.PlayerLoop;

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

        [NotNull] readonly SettingsDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        [NotNull]
        static ISettingsManager SettingsManager => Settings.SettingsManager ??
                                                   throw new InvalidOperationException(
                                                       "Settings Dialog used without a SettingsManager!");

        public SettingsDialogData([CanBeNull] SettingsConfiguration config = null)
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
            panel.QualityInView.Interactable = SettingsManager.SupportsView;

            panel.QualityInAr.Options = SettingsManager.QualityLevelsInAR;
            panel.QualityInAr.Index = (int) SettingsManager.QualityInAr;
            panel.QualityInAr.Interactable = SettingsManager.SupportsAR;

            panel.TargetFps.Options = TargetFpsOptions;
            panel.NetworkProcessing.Options = NetworkProcessingOptions;
            panel.SunDirection.SetMinValue(-60).SetMaxValue(60).SetIntegerOnly(true);

            panel.BackgroundColor.Value = SettingsManager.BackgroundColor.WithAlpha(1);
            panel.BackgroundColor.Interactable = SettingsManager.SupportsView;

            panel.SunDirection.Value = SettingsManager.SunDirection;

            if (SettingsManager.TargetFps == Settings.DefaultFps)
            {
                panel.TargetFps.Index = 0;
            }
            else if (SettingsManager.TargetFps <= 15)
            {
                panel.TargetFps.Index = 3;
            }
            else if (SettingsManager.TargetFps <= 30)
            {
                panel.TargetFps.Index = 2;
            }
            else if (SettingsManager.TargetFps <= 60)
            {
                panel.TargetFps.Index = 1;
            }
            else
            {
                panel.TargetFps.Index = 0;
            }

            switch (SettingsManager.NetworkFrameSkip)
            {
                case 1:
                    panel.NetworkProcessing.Index = 0;
                    break;
                case 2:
                    panel.NetworkProcessing.Index = 1;
                    break;
                default:
                    panel.NetworkProcessing.Index = 2;
                    break;
            }

            panel.ModelCacheLabel.text = $"<b>Model Cache:</b> {Resource.External.ResourceCount} files";
            panel.SavedFilesLabel.text = $"<b>Saved:</b> {ModuleListPanel.NumSavedFiles} files";
            panel.HostHistoryLabel.text = $"<b>Host History:</b> {ModuleListPanel.Instance.NumMastersInCache} entries";

            panel.ModelService.Options = ModelServerModesNames;
            panel.ModelService.Label = UpdateModelServiceLabel();
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
                switch (i)
                {
                    case 0:
                        SettingsManager.TargetFps = Settings.DefaultFps;
                        break;
                    case 1:
                        SettingsManager.TargetFps = 60;
                        break;
                    case 2:
                        SettingsManager.TargetFps = 30;
                        break;
                    case 3:
                        SettingsManager.TargetFps = 15;
                        break;
                }

                ModuleListPanel.UpdateSettings();
            };

            panel.NetworkProcessing.ValueChanged += (i, _) =>
            {
                switch (i)
                {
                    case 0:
                        SettingsManager.NetworkFrameSkip = 1;
                        break;
                    case 1:
                        SettingsManager.NetworkFrameSkip = 2;
                        break;
                    case 2:
                        SettingsManager.NetworkFrameSkip = 4;
                        break;
                }

                ModuleListPanel.UpdateSettings();
            };

            panel.SunDirection.ValueChanged += f =>
            {
                SettingsManager.SunDirection = (int) f;
                ModuleListPanel.UpdateSettings();
            };

            panel.Close.Clicked += Close;

            panel.ClearModelCacheClicked += async () =>
            {
                Logger.Info("Settings: Clearing model cache.");
                await Resource.External.ClearModelCacheAsync();
                panel.ModelCacheLabel.text = $"<b>Model Cache:</b> {Resource.External.ResourceCount} files";
            };

            panel.ClearHostHistoryClicked += async () =>
            {
                Logger.Info("Settings: Clearing cache of master uris.");
                await ModuleListPanel.Instance.ClearMastersCacheAsync();
                panel.HostHistoryLabel.text =
                    $"<b>Host History:</b> {ModuleListPanel.Instance.NumMastersInCache} entries";
            };

            panel.ClearSavedFilesClicked += () =>
            {
                Logger.Info("Settings: Clearing saved files.");
                ModuleListPanel.ClearSavedFiles();
                panel.SavedFilesLabel.text = $"<b>Saved:</b> {ModuleListPanel.NumSavedFiles} files";
            };

            panel.ModelService.ValueChanged += async (i, s) =>
            {
                switch ((ModelServerModes) i)
                {
                    case ModelServerModes.Off:
                        ModuleListPanel.ModelService.Dispose();
                        break;
                    case ModelServerModes.On:
                        await ModuleListPanel.ModelService.Restart(false);
                        break;
                    case ModelServerModes.OnWithFile:
                        await ModuleListPanel.ModelService.Restart(true);
                        break;
                    default:
                        break;
                }

                panel.ModelService.Label = UpdateModelServiceLabel();
            };
        }

        string UpdateModelServiceLabel()
        {
            if (Settings.IsMobile)
            {
                return "<b>Model Service:</b> Off (Mobile)";
            }

            var modelService = ModuleListPanel.ModelService;
            if (!modelService.IsEnabled)
            {
                return "<b>Model Service:</b> Off";
            }

            if (modelService.IsFileSchemaEnabled)
            {
                return "<b>Model Service:</b> " + modelService.NumPackages +
                       " packages\n<b>[File schema enabled]</b>";
            }

            return "<b>Model Service:</b> " + modelService.NumPackages + " packages";
        }
    }
}