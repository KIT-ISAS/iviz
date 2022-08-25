#nullable enable

using System;
using System.Text;
using Iviz.Common;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.App
{
    public sealed class RobotDialogData : DialogData
    {
        readonly RobotDialogPanel panel;
        SimpleRobotModuleData? robotModuleData;

        public override IDialogPanel Panel => panel;

        public RobotDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<RobotDialogPanel>(DialogPanelType.Marker);
        }

        public override void SetupPanel()
        {
            var robotModel = robotModuleData?.RobotController.Robot;
            if (robotModel == null)
            {
                return;
            }

            ResetPanelPosition();

            const int maxLabelWidth = 300;
            using (var description = BuilderPool.Rent())
            {
                Resource.Font.Split(description, robotModel.Name, maxLabelWidth);
                panel.Label.SetText(description);
            }

            panel.Close.Clicked += Close;
            panel.ResetAll += robotModel.ApplyAnyValidConfiguration;
            //panel.LinkClicked += markerId => HighlightMarker(listener, markerId);

            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            var robotModel = robotModuleData?.RobotController.Robot;
            if (robotModel == null)
            {
                return;
            }

            using var description = BuilderPool.Rent();
            robotModel.GenerateLog(description);
            panel.Text.SetTextRent(description);
        }

        public void Show(SimpleRobotModuleData moduleData)
        {
            ThrowHelper.ThrowIfNull(moduleData, nameof(moduleData));
            robotModuleData = moduleData;
            Show();
        }
    }
}