using UnityEngine;

namespace Iviz.App
{
    public sealed class TfDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] TfLog tfLog = null;
        [SerializeField] ToggleWidget showOnlyUsed = null;

        public TrashButtonWidget Close => close;
        public TfLog TfLog => tfLog;
        public ToggleWidget ShowOnlyUsed => showOnlyUsed;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            TfLog.ClearSubscribers();
            ShowOnlyUsed.ClearSubscribers();
        }
    }
}
