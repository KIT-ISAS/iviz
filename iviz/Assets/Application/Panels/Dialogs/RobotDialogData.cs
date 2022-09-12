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
            if (robotModuleData == null)
            {
                panel.Text.text = "Nothing to show.";
                return;
            }

            using var description = BuilderPool.Rent();

            var controller = robotModuleData.RobotController;
            bool hasSourceParameter = !string.IsNullOrWhiteSpace(controller.SourceParameter);
            bool hasSavedRobotName = !string.IsNullOrWhiteSpace(controller.SavedRobotName);

            if (hasSourceParameter)
            {
                description.Append("<b>Source:</b> Parameter '")
                    .Append(controller.SourceParameter).Append("'").AppendLine();
            }
            else if (hasSavedRobotName)
            {
                description.Append("<b>Source:</b> Saved Robot '")
                    .Append(controller.SavedRobotName).Append("'").AppendLine();
            }

            if (robotModel == null)
            {
                if (!string.IsNullOrWhiteSpace(controller.HelpText))
                {
                    description.Append("<b>Status: </b>").Append(controller.HelpText).AppendLine();
                }

                description.AppendLine();
                description.Append("Nothing to show.").AppendLine();
            }
            else
            {
                description.Append("<b>Robot: </b>").Append(robotModel.Name).AppendLine();
                description.AppendLine();
                robotModel.GenerateLog(description);
            }

            uint newHash = HashCalculator.Compute(description);
            if (newHash == textHash)
            {
                return;
            }

            textHash = newHash;
            panel.Text.SetTextRent(description);

            if (robotModel == null)
            {
                panel.Label.Text = "No Robot Loaded";
            }
            else
            {
                const int maxLabelWidth = 300;
                description.Length = 0;
                Resource.Font.Split(description, robotModel.Name, maxLabelWidth);
                panel.Label.SetText(description);
            }
        }

        public void Show(SimpleRobotModuleData moduleData)
        {
            ThrowHelper.ThrowIfNull(moduleData, nameof(moduleData));
            robotModuleData = moduleData;
            Show();
        }
    }
}