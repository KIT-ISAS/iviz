using Iviz.Ros;
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
        [SerializeField] SimpleButtonWidget refreshMyUri;
        [SerializeField] SimpleButtonWidget refreshMyId;
        [SerializeField] SimpleButtonWidget close;
        [SerializeField] ToggleButtonWidget serverMode;
        [SerializeField] LineLog lineLog;

        public InputFieldWithHintsWidget MasterUri => masterUri;
        public InputFieldWithHintsWidget MyUri => myUri;
        public InputFieldWithHintsWidget MyId => myId;
        public SimpleButtonWidget RefreshMyUri => refreshMyUri;
        public SimpleButtonWidget RefreshMyId => refreshMyId;
        public SimpleButtonWidget Close => close;
        public LineLog LineLog => lineLog;
        public ToggleButtonWidget ServerMode => serverMode;

        void Awake()
        {
            serverMode.InactiveText = "Master\nOff";
            serverMode.ActiveText = "Master\nOn";
            serverMode.State = RosManager.Server.IsActive;
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
