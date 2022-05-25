using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class NetworkDialogPanel : DetachableDialogPanel
    {
        [SerializeField] SimpleButtonWidget close;
        [SerializeField] TMP_Text text;

        public SimpleButtonWidget Close => close;
        public TMP_Text Text => text;

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
        }
    }
}
