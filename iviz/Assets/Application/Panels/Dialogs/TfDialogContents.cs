#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.App
{
    public sealed class TfDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget? close = null;
        [SerializeField] TfLog? tfLog = null;
        [SerializeField] ToggleWidget? showOnlyUsed = null;

        public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        public TfLog TfLog => tfLog.AssertNotNull(nameof(tfLog));
        public ToggleWidget ShowOnlyUsed => showOnlyUsed.AssertNotNull(nameof(showOnlyUsed));

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            TfLog.ClearSubscribers();
            ShowOnlyUsed.ClearSubscribers();
        }
    }
}
