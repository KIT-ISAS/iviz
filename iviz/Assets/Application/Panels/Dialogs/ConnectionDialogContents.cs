using UnityEngine;

namespace Iviz.App
{
    public sealed class ConnectionDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] InputFieldWidget masterUri = null;
        [SerializeField] InputFieldWidget myUri = null;
        [SerializeField] InputFieldWidget myId = null;
        [SerializeField] TrashButtonWidget refreshMyUri = null;
        [SerializeField] TrashButtonWidget refreshMyId = null;
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] LineLog lineLog = null;

        public InputFieldWidget MasterUri => masterUri;
        public InputFieldWidget MyUri => myUri;
        public InputFieldWidget MyId => myId;
        public TrashButtonWidget RefreshMyUri => refreshMyUri;
        public TrashButtonWidget RefreshMyId => refreshMyId;
        public TrashButtonWidget Close => close;
        public LineLog LineLog => lineLog;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            MasterUri.ClearSubscribers();
            MyUri.ClearSubscribers();
            MyId.ClearSubscribers();
            RefreshMyUri.ClearSubscribers();
            RefreshMyId.ClearSubscribers();
            Close.ClearSubscribers();
        }
    }
}
