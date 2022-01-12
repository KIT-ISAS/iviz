﻿#nullable enable

using System;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Roslib;

namespace Iviz.App
{
    public sealed class TfDialogData : DialogData
    {
        readonly TfDialogPanel panel;
        public override IDialogPanel Panel => panel;

        public TfDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<TfDialogPanel>(DialogPanelType.Tf);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();

            panel.FrameName.Value = "";
            panel.Close.Clicked += Close;
            panel.TfLog.Close += Close;
            panel.TfLog.Flush();
            panel.TfLog.UpdateFrameButtons();

            panel.CreateFrameClicked += () =>
            {
                string frameName = ValidateFrameName(panel.FrameName.Value.Trim());
                if (frameName is "")
                {
                    RosLogger.Error($"{this}: Cannot create frame with empty name.");
                    return;
                }

                if (frameName[0] == '/')
                {
                    RosLogger.Info($"{this}: Created frame's name has a trailing slash '/'. " +
                                   "This may cause problems.");
                }
                else if (!RosClient.IsValidResourceName(frameName))
                {
                    RosLogger.Info($"{this}: Created frame's name '{frameName}' is not a valid ROS resource name. " +
                                   "This may cause problems.");
                }

                var tfPublisher = ModuleListPanel.TfPublisher;
                if (tfPublisher.IsPublishing(frameName))
                {
                    RosLogger.Info($"{this}: A frame with name '{frameName}' already exists.");
                    return;
                }

                var tfFrame = tfPublisher.Add(frameName);
                panel.TfLog.SelectedFrame = tfFrame.TfFrame;
                panel.TfLog.Flush();
            };

            panel.ShowOnlyUsed.Value = !TfListener.Instance.KeepAllFrames;
            panel.ShowOnlyUsed.ValueChanged += f =>
            {
                TfListener.Instance.KeepAllFrames = !f;
                ModuleListPanel.Instance.ResetTfPanel();
            };
        }

        public override void UpdatePanel()
        {
            panel.TfLog.Flush();
        }

        public override void UpdatePanelFast()
        {
            panel.TfLog.UpdateFrameText();
        }

        public void Show(TfFrame frame)
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            Show();
            panel.TfLog.SelectedFrame = frame;
        }

        static string ValidateFrameName(string frameName)
        {
            if (frameName == "")
            {
                return frameName;
            }

            return frameName[0] == '/' ? frameName[1..] : frameName;
        }
    }
}