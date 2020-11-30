using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ConsoleDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] InputFieldWithHintsWidget fromField = null;
        [SerializeField] DropdownWidget logFormat = null;
        [SerializeField] DropdownWidget timeFormat = null;
        [SerializeField] DropdownWidget messageFormat = null;
        [SerializeField] Text text = null;

        public TrashButtonWidget Close => close;
        public InputFieldWithHintsWidget FromField => fromField;
        public DropdownWidget LogFormat => logFormat;
        public DropdownWidget TimeFormat => timeFormat;
        public DropdownWidget MessageFormat => messageFormat;
        public Text Text => text;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            FromField.ClearSubscribers();
            LogFormat.ClearSubscribers();
            TimeFormat.ClearSubscribers();
            MessageFormat.ClearSubscribers();
        }
    }
}
