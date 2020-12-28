using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogData"/> 
    /// </summary>
    public sealed class ConnectionDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] InputFieldWithHintsWidget masterUri = null;
        [SerializeField] InputFieldWithHintsWidget myUri = null;
        [SerializeField] InputFieldWithHintsWidget myId = null;
        [SerializeField] TrashButtonWidget refreshMyUri = null;
        [SerializeField] TrashButtonWidget refreshMyId = null;
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] ToggleButtonWidget serverMode = null;
        [SerializeField] LineLog lineLog = null;

        public InputFieldWithHintsWidget MasterUri => masterUri;
        public InputFieldWithHintsWidget MyUri => myUri;
        public InputFieldWithHintsWidget MyId => myId;
        public TrashButtonWidget RefreshMyUri => refreshMyUri;
        public TrashButtonWidget RefreshMyId => refreshMyId;
        public TrashButtonWidget Close => close;
        public LineLog LineLog => lineLog;
        public ToggleButtonWidget ServerMode => serverMode;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            serverMode.InactiveText = "Master Mode";
            serverMode.ActiveText = "Client Mode";
            serverMode.State = false;
        }

        public void ClearSubscribers()
        {
            MasterUri.ClearSubscribers();
            MyUri.ClearSubscribers();
            MyId.ClearSubscribers();
            RefreshMyUri.ClearSubscribers();
            RefreshMyId.ClearSubscribers();
            ServerMode.ClearSubscribers();
            Close.ClearSubscribers();
        }
    }
}
