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
                if (listener == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (listener != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                listener = value;
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
            Text.text = listener?.RobotController.Name ?? "(No robot listener)";
        }

        public void ClearSubscribers()
        {
            RobotListener = null;
        }
    }
}