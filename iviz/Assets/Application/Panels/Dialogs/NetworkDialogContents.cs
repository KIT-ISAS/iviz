using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class NetworkDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] Text text = null;

        public TrashButtonWidget Close => close;
        public Text Text => text;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
        }
    }
}
