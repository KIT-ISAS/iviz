#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ConsoleDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget? close = null;
        [SerializeField] ToggleButtonWidget? pause = null;
        [SerializeField] InputFieldWithHintsWidget? fromField = null;
        [SerializeField] DropdownWidget? logLevel = null;
        [SerializeField] DropdownWidget? timeFormat = null;
        [SerializeField] DropdownWidget? messageFormat = null;
        [SerializeField] TMP_Text? text = null;
        [SerializeField] Text? bottomText = null;

        public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        public ToggleButtonWidget Pause => pause.AssertNotNull(nameof(pause));
        public InputFieldWithHintsWidget FromField => fromField.AssertNotNull(nameof(fromField));
        public DropdownWidget LogLevel => logLevel.AssertNotNull(nameof(logLevel));
        DropdownWidget TimeFormat => timeFormat.AssertNotNull(nameof(timeFormat));
        DropdownWidget MessageFormat => messageFormat.AssertNotNull(nameof(messageFormat));
        public TMP_Text Text => text.AssertNotNull(nameof(text));
        public Text BottomText => bottomText.AssertNotNull(nameof(bottomText));

        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            FromField.ClearSubscribers();
            LogLevel.ClearSubscribers();
            TimeFormat.ClearSubscribers();
            MessageFormat.ClearSubscribers();
            Pause.ClearSubscribers();
        }
    }
}
