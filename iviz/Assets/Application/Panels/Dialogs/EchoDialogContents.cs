using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class EchoDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] ToggleButtonWidget pause = null;
        [SerializeField] InputFieldWithHintsWidget topics = null;
        [SerializeField] TMP_Text text = null;
        [SerializeField] TMP_Text publishers = null;
        [SerializeField] TMP_Text messages = null;
        [SerializeField] TMP_Text kbytes = null;

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
