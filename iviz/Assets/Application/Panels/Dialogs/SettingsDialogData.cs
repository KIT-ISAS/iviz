using System.Linq;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public class SettingsDialogData : DialogData
    {
        static readonly string[] QualityInViewOptions = {"Very Low", "Low", "Medium", "High", "Very High", "Ultra", "Mega"};
        static readonly string[] QualityInArOptions = {"Very Low", "Low", "Medium", "High", "Very High", "Ultra"};
        static readonly string[] TargetFpsOptions = {"Default", "60", "30", "15"};
        static readonly string[] NetworkProcessingOptions = {"Every Frame", "Every Second", "Every Fourth"};

        [NotNull] readonly SettingsDialogContents panel;
        public override IDialogPanelContents Panel => panel;
        static GuiInputModule InputModule => GuiInputModule.Instance;

        public SettingsDialogData(SettingsConfiguration config = null)
        {
            panel = DialogPanelManager.GetPanelByType<SettingsDialogContents>(DialogPanelType.Settings);
            if (config != null)
            {
                InputModule.Config = config;
            }
        }

        public override void SetupPanel()
        {
            panel.QualityInView.Options = QualityInViewOptions;
            panel.QualityInAr.Options = QualityInArOptions;
            panel.TargetFps.Options = TargetFpsOptions;
            panel.NetworkProcessing.Options = NetworkProcessingOptions;
            panel.SunDirection.SetMinValue(-60).SetMaxValue(60).SetIntegerOnly(true);

            panel.BackgroundColor.Value = InputModule.BackgroundColor.WithAlpha(1);
            panel.QualityInView.Index = (int) InputModule.QualityInView;
            panel.QualityInAr.Index = (int) InputModule.QualityInAr;
            panel.SunDirection.Value = InputModule.SunDirection;

            panel.QualityInView.ValueChanged += (f, _) =>
            {
                InputModule.QualityInView = (QualityType) f;
            };
            panel.QualityInAr.ValueChanged += (f, _) =>
            {
                InputModule.QualityInAr = (QualityType) f;
            };

            if (InputModule.TargetFps == GuiInputModule.DefaultFps)
            {
                panel.TargetFps.Index = 0;
            }
            else if (InputModule.TargetFps <= 15)
            {
                panel.TargetFps.Index = 3;
            }
            else if (InputModule.TargetFps <= 30)
            {
                panel.TargetFps.Index = 2;
            }
            else if (InputModule.TargetFps <= 60)
            {
                panel.TargetFps.Index = 1;
            }
            else
            {
                panel.TargetFps.Index = 0;
            }

            switch (InputModule.NetworkFrameSkip)
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
                InputModule.BackgroundColor = c;
            };

            panel.TargetFps.ValueChanged += (i, _) =>
            {
                switch (i)
                {
                    case 0:
                        InputModule.TargetFps = GuiInputModule.DefaultFps;
                        break;
                    case 1:
                        InputModule.TargetFps = 60;
                        break;
                    case 2:
                        InputModule.TargetFps = 30;
                        break;
                    case 3:
                        InputModule.TargetFps = 15;
                        break;
                }
            };

            panel.NetworkProcessing.ValueChanged += (i, _) =>
            {
                switch (i)
                {
                    case 0:
                        InputModule.NetworkFrameSkip = 1;
                        break;
                    case 1:
                        InputModule.NetworkFrameSkip = 2;
                        break;
                    case 2:
                        InputModule.NetworkFrameSkip = 4;
                        break;
                }
            };

            panel.SunDirection.ValueChanged += f =>
            {
                InputModule.SunDirection = (int) f;
            };
            
            panel.Close.Clicked += Close;            
        }
    }
}