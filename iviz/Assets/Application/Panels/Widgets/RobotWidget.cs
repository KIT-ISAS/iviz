#nullable enable

using Iviz.Common;
using Iviz.Core;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class RobotWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;
        SimpleRobotModuleData? listener;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));

        public SimpleRobotModuleData? RobotListener
        {
            private get => listener;
            set
            {
                if (listener != null)
                {
                    listener.RobotController.RobotFinishedLoading -= UpdateStats;
                }

                listener = value;
                if (listener != null)
                {
                    listener.RobotController.RobotFinishedLoading += UpdateStats;
                }

                if (value != null)
                {
                    UpdateStats();
                }
            }
        }

        void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            if (RobotListener != null)
            {
                ModuleListPanel.Instance.ShowMarkerDialog(RobotListener);
            }
        }

        void UpdateStats()
        {
            if (listener == null)
            {
                Text.text = "(no robot listener)";
                return;
            }

            if (listener.RobotController.Robot is not { } robot)
            {
                Text.text = "No Robot Loaded";
                return;
            }

            int count = robot.LinkObjects.Count;
            Text.text = "<b>" + robot.Name + "</b>\n" +
                        (count == 1 ? "1 link" : $"{count.ToString()} links");
        }

        public void ClearSubscribers()
        {
            RobotListener = null;
        }
    }
}