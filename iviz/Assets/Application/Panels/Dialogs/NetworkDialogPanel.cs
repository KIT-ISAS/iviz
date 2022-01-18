using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class NetworkDialogPanel : DetachableDialogPanel
    {
        [SerializeField] TrashButtonWidget close;
        [SerializeField] TMP_Text text;

        public TrashButtonWidget Close => close;
        public TMP_Text Text => text;

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
        }
    }
}
