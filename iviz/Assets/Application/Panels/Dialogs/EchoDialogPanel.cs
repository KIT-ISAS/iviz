#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class EchoDialogPanel : DetachableDialogPanel
    {
        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] SimpleButtonWidget? reset;
        [SerializeField] ToggleButtonWidget? pause;
        [SerializeField] InputFieldWithHintsWidget? topics;
        [SerializeField] TMP_Text? text;
        [SerializeField] TMP_Text? messages;

        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public SimpleButtonWidget Reset => reset.AssertNotNull(nameof(reset));
        public ToggleButtonWidget Pause => pause.AssertNotNull(nameof(pause));
        public InputFieldWithHintsWidget Topics => topics.AssertNotNull(nameof(topics));
        public TMP_Text Text => text.AssertNotNull(nameof(text));
        public TMP_Text Messages => messages.AssertNotNull(nameof(messages));

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            Topics.ClearSubscribers();
            Pause.ClearSubscribers();
            Reset.ClearSubscribers();
        }
    }
}
