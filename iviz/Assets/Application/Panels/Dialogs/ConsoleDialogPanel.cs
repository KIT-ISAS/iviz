#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class ConsoleDialogPanel : DetachableDialogPanel
    {
        [SerializeField] TrashButtonWidget? close;
        [SerializeField] ToggleButtonWidget? pause;
        [SerializeField] TrashButtonWidget? reset;
        [SerializeField] InputFieldWithHintsWidget? fromField;
        [SerializeField] DropdownWidget? logLevel;
        [SerializeField] DropdownWidget? timeFormat;
        [SerializeField] DropdownWidget? messageFormat;
        [SerializeField] TMP_Text? text;
        [SerializeField] TMP_Text? bottomText;

        public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        public ToggleButtonWidget Pause => pause.AssertNotNull(nameof(pause));
        public TrashButtonWidget Reset => reset.AssertNotNull(nameof(reset));
        public InputFieldWithHintsWidget FromField => fromField.AssertNotNull(nameof(fromField));
        public DropdownWidget LogLevel => logLevel.AssertNotNull(nameof(logLevel));
        DropdownWidget TimeFormat => timeFormat.AssertNotNull(nameof(timeFormat));
        DropdownWidget MessageFormat => messageFormat.AssertNotNull(nameof(messageFormat));
        public TMP_Text Text => text.AssertNotNull(nameof(text));
        public TMP_Text BottomText => bottomText.AssertNotNull(nameof(bottomText));

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            FromField.ClearSubscribers();
            LogLevel.ClearSubscribers();
            TimeFormat.ClearSubscribers();
            MessageFormat.ClearSubscribers();
            Pause.ClearSubscribers();
            Reset.ClearSubscribers();
        }
    }
}
