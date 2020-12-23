using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public class SettingsDialogData : DialogData
    {
        [NotNull] readonly SettingsDialogContents panel;
        public override IDialogPanelContents Panel => panel;

        public SettingsDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<SettingsDialogContents>(DialogPanelType.Settings);
        }

        public override void SetupPanel()
        {
            panel.QualityInView.Options = new[] {"Very Low", "Low", "Medium", "High", "Very High", "Ultra", "Mega"};
            panel.QualityInAr.Options = new[] {"Very Low", "Low", "Medium", "High", "Very High", "Ultra"};
            panel.TargetFps.Options = new[] {"Default", "60", "30", "15"};
            panel.NetworkProcessing.Options = new[] {"Every Frame", "Every Second", "Every Fourth"};

            panel.BackgroundColor.Value = GuiInputModule.BackgroundColor;
            panel.QualityInView.Index = (int) GuiInputModule.Instance.QualityInView;
            panel.QualityInAr.Index = (int) GuiInputModule.Instance.QualityInAr;

            panel.QualityInView.ValueChanged += (f, _) =>
            {
                GuiInputModule.Instance.QualityInView = (GuiInputModule.QualityType) f;
            };
            panel.QualityInAr.ValueChanged += (f, _) =>
            {
                GuiInputModule.Instance.QualityInAr = (GuiInputModule.QualityType) f;
            };

            if (GuiInputModule.Instance.TargetFps <= 15)
            {
                panel.TargetFps.Index = 3;
            }
            else if (GuiInputModule.Instance.TargetFps <= 30)
            {
                panel.TargetFps.Index = 2;
            }
            else if (GuiInputModule.Instance.TargetFps <= 60)
            {
                panel.TargetFps.Index = 1;
            }
            else
            {
                panel.TargetFps.Index = 0;
            }

            switch (GameThread.NetworkFrameSkip)
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
                GuiInputModule.BackgroundColor = c;
            };

            panel.TargetFps.ValueChanged += (i, _) =>
            {
                switch (i)
                {
                    case 0:
                        GuiInputModule.Instance.TargetFps = -1;
                        break;
                    case 1:
                        GuiInputModule.Instance.TargetFps = 60;
                        break;
                    case 2:
                        GuiInputModule.Instance.TargetFps = 30;
                        break;
                    case 3:
                        GuiInputModule.Instance.TargetFps = 15;
                        break;
                }
            };

            panel.NetworkProcessing.ValueChanged += (i, _) =>
            {
                switch (i)
                {
                    case 0:
                        GameThread.NetworkFrameSkip = 1;
                        break;
                    case 1:
                        GameThread.NetworkFrameSkip = 2;
                        break;
                    case 2:
                        GameThread.NetworkFrameSkip = 4;
                        break;
                }
            };
            
            panel.Close.Clicked += Close;            
        }
    }
}