using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class EchoDialogPanel : DetachableDialogPanel
    {
        [SerializeField] TrashButtonWidget close;
        [SerializeField] ToggleButtonWidget pause;
        [SerializeField] InputFieldWithHintsWidget topics;
        [SerializeField] TMP_Text text;
        [SerializeField] TMP_Text publishers;
        [SerializeField] TMP_Text messages;
        [SerializeField] TMP_Text kbytes;

        public TrashButtonWidget Close => close;
        public ToggleButtonWidget Pause => pause;
        public InputFieldWithHintsWidget Topics => topics;
        public TMP_Text Text => text;
        public TMP_Text Publishers => publishers;
        public TMP_Text Messages => messages;
        public TMP_Text KBytes => kbytes;

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            Topics.ClearSubscribers();
            Pause.ClearSubscribers();
        }
    }
}
