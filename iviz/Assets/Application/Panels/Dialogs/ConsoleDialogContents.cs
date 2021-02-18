using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ConsoleDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] InputFieldWithHintsWidget fromField = null;
        [SerializeField] DropdownWidget logLevel = null;
        [SerializeField] DropdownWidget timeFormat = null;
        [SerializeField] DropdownWidget messageFormat = null;
        [SerializeField] TMP_Text text = null;
        [SerializeField] Text bottomText = null;

        public TrashButtonWidget Close => close;
        public InputFieldWithHintsWidget FromField => fromField;
        public DropdownWidget LogLevel => logLevel;
        public DropdownWidget TimeFormat => timeFormat;
        public DropdownWidget MessageFormat => messageFormat;
        public TMP_Text Text => text;
        public Text BottomText => bottomText;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            FromField.ClearSubscribers();
            LogLevel.ClearSubscribers();
            TimeFormat.ClearSubscribers();
            MessageFormat.ClearSubscribers();
        }
    }
}
