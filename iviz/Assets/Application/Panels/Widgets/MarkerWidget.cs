#nullable enable

using Iviz.Common;
using Iviz.Core;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class MarkerWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;
        IMarkerDialogListener? listener;
        
        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));
        
        public IMarkerDialogListener? MarkerListener
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
            if (MarkerListener != null)
            {
                ModuleListPanel.Instance.ShowMarkerDialog(MarkerListener);
            }
        }

        void UpdateStats()
        {
            Text.text = listener?.BriefDescription ?? "(No listener)";
        }

        public void ClearSubscribers()
        {
            MarkerListener = null;
        }
    }
}