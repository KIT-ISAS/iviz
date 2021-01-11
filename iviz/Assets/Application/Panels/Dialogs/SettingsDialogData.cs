using Iviz.Core;
using JetBrains.Annotations;

namespace Iviz.App
{
    public class SettingsDialogData : DialogData
    {
        static readonly string[] TargetFpsOptions = {"Default", "60", "30", "15"};
        static readonly string[] NetworkProcessingOptions = {"Every Frame", "Every Second", "Every Fourth"};

        [NotNull] readonly SettingsDialogContents panel;
        public override IDialogPanelContents Panel => panel;
        static ISettingsManager SettingsManager => Settings.SettingsManager;

        public SettingsDialogData(SettingsConfiguration config = null)
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

            panel.QualityInView.ValueChanged += (f, _) =>
            {
                SettingsManager.QualityInView = (QualityType) f;
            };
            panel.QualityInAr.ValueChanged += (f, _) =>
            {
                SettingsManager.QualityInAr = (QualityType) f;
            };

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

            panel.BackgroundColor.ValueChanged += c =>
            {
                SettingsManager.BackgroundColor = c;
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
            };

            panel.SunDirection.ValueChanged += f =>
            {
                SettingsManager.SunDirection = (int) f;
            };
            
            panel.Close.Clicked += Close;            
        }
    }
}