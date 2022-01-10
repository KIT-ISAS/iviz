using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogData"/> 
    /// </summary>
    public sealed class ConnectionDialogPanel : DialogPanel
    {
        [SerializeField] InputFieldWithHintsWidget masterUri;
        [SerializeField] InputFieldWithHintsWidget myUri;
        [SerializeField] InputFieldWithHintsWidget myId;
        [SerializeField] TrashButtonWidget refreshMyUri;
        [SerializeField] TrashButtonWidget refreshMyId;
        [SerializeField] TrashButtonWidget close;
        [SerializeField] ToggleButtonWidget serverMode;
        [SerializeField] LineLog lineLog;

        public InputFieldWithHintsWidget MasterUri => masterUri;
        public InputFieldWithHintsWidget MyUri => myUri;
        public InputFieldWithHintsWidget MyId => myId;
        public TrashButtonWidget RefreshMyUri => refreshMyUri;
        public TrashButtonWidget RefreshMyId => refreshMyId;
        public TrashButtonWidget Close => close;
        public LineLog LineLog => lineLog;
        public ToggleButtonWidget ServerMode => serverMode;

        void Awake()
        {
            serverMode.InactiveText = "Master Off";
            serverMode.ActiveText = "Master On";
            serverMode.State = false;
        }

        public override void ClearSubscribers()
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
