using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class NetworkDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] TMP_Text text = null;

        public TrashButtonWidget Close => close;
        public TMP_Text Text => text;

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
        }
    }
}
