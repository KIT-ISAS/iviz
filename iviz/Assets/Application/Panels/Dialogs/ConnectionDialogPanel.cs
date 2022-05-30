#nullable enable

using Iviz.Core;
using Iviz.Ros;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogData"/> 
    /// </summary>
    public sealed class ConnectionDialogPanel : DialogPanel
    {
        [SerializeField] InputFieldWithHintsWidget? masterUri;
        [SerializeField] InputFieldWithHintsWidget? myUri;
        [SerializeField] InputFieldWithHintsWidget? myId;
        [SerializeField] SimpleButtonWidget? refreshMyUri;
        [SerializeField] SimpleButtonWidget? refreshMyId;
        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] ToggleButtonWidget? serverMode;
        [SerializeField] LineLog? lineLog;

        public InputFieldWithHintsWidget MasterUri => masterUri.AssertNotNull(nameof(masterUri));
        public InputFieldWithHintsWidget MyUri => myUri.AssertNotNull(nameof(myUri));
        public InputFieldWithHintsWidget MyId => myId.AssertNotNull(nameof(myId));
        public SimpleButtonWidget RefreshMyUri => refreshMyUri.AssertNotNull(nameof(refreshMyUri));
        public SimpleButtonWidget RefreshMyId => refreshMyId.AssertNotNull(nameof(refreshMyId));
        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public LineLog LineLog => lineLog.AssertNotNull(nameof(lineLog));
        public ToggleButtonWidget ServerMode => serverMode.AssertNotNull(nameof(serverMode));

        void Awake()
        {
            ServerMode.InactiveText = "Master\nOff";
            ServerMode.ActiveText = "Master\nOn";
            ServerMode.State = RosManager.Server.IsActive;
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
