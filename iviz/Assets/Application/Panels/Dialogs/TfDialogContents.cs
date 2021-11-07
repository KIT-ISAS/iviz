using UnityEngine;

namespace Iviz.App
{
    public sealed class TfDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] TfLog tfLog = null;
        [SerializeField] ToggleWidget showOnlyUsed = null;

        public TrashButtonWidget Close => close;
        public TfLog TfLog => tfLog;
        public ToggleWidget ShowOnlyUsed => showOnlyUsed;

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            TfLog.ClearSubscribers();
            ShowOnlyUsed.ClearSubscribers();
        }
    }
}
