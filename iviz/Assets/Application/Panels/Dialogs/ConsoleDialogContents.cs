using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ConsoleDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] ToggleButtonWidget pause = null;
        [SerializeField] InputFieldWithHintsWidget fromField = null;
        [SerializeField] DropdownWidget logLevel = null;
        [SerializeField] DropdownWidget timeFormat = null;
        [SerializeField] DropdownWidget messageFormat = null;
        [SerializeField] TMP_Text text = null;
        [SerializeField] Text bottomText = null;

        public TrashButtonWidget Close => close;
        public ToggleButtonWidget Pause => pause;
        public InputFieldWithHintsWidget FromField => fromField;
        public DropdownWidget LogLevel => logLevel;
        DropdownWidget TimeFormat => timeFormat;
        DropdownWidget MessageFormat => messageFormat;
        public TMP_Text Text => text;
        public Text BottomText => bottomText;

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
