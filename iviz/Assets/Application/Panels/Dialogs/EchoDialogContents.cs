using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class EchoDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] DropdownWidget topics = null;
        [SerializeField] TMP_Text text = null;

        public TrashButtonWidget Close => close;
        public DropdownWidget Topics => topics;
        public TMP_Text Text => text;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            Topics.ClearSubscribers();
        }
    }
}
