#nullable enable

using Iviz.Common;
using Iviz.Core;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class MarkerDialogWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;
        IMarkerDialogListener? listener;
        
        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));
        
        public IMarkerDialogListener? DialogListener
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
            if (DialogListener != null)
            {
                ModuleListPanel.Instance.ShowMarkerDialog(DialogListener);
            }
        }

        void UpdateStats()
        {
            Text.text = listener?.BriefDescription ?? "(No listener)";
        }

        public void ClearSubscribers()
        {
            DialogListener = null;
        }
    }
}