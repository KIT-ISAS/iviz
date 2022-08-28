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
        uint? textHash;

        public override IDialogPanel Panel => panel;

        public RobotDialogData()
        {
            panel = DialogPanelManager.GetPanelByType<RobotDialogPanel>(DialogPanelType.Robot);
        }

        public override void SetupPanel()
        {
            ResetPanelPosition();
            panel.Close.Clicked += Close;
            UpdatePanel();
        }

        public override void UpdatePanel()
        {
            var robotModel = robotModuleData?.RobotController.Robot;
            if (robotModel == null)
            {
                panel.Label.Text = "No Robot Loaded";
                panel.Text.text = "Nothing to show.";
                textHash = null;
                return;
            }

            const int maxLabelWidth = 300;

            using var description = BuilderPool.Rent();
            Resource.Font.Split(description, robotModel.Name, maxLabelWidth);
            panel.Label.SetText(description);

            description.Length = 0;

            robotModel.GenerateLog(description);
            
            uint newHash = HashCalculator.Compute(description);
            if (newHash == textHash)
            {
                return;
            }

            textHash = newHash;
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