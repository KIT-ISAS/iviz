using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class MarkerWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text text = null;
        [CanBeNull] IMarkerDialogListener listener;
        [SerializeField] Button button = null;
        
        [CanBeNull]
        public IMarkerDialogListener MarkerListener
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
            button.onClick.AddListener(OnClick);
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
            text.text = listener?.BriefDescription ?? "(No listener)";
        }

        public void ClearSubscribers()
        {
            MarkerListener = null;
        }
    }
}
