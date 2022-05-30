#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class NetworkDialogPanel : DetachableDialogPanel
    {
        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] TMP_Text? text;

        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public TMP_Text Text => text.AssertNotNull(nameof(text));

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
        }
    }
}
