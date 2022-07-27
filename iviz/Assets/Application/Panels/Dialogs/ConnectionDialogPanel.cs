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
        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] ToggleButtonWidget? serverMode;
        [SerializeField] LineLog? lineLog;

        [SerializeField] InputFieldWithHintsWidget? myId2;
        [SerializeField] SimpleButtonWidget? rosVersion1;
        [SerializeField] SimpleButtonWidget? rosVersion2;

        [SerializeField] GameObject? rosPanel1;
        [SerializeField] GameObject? rosPanel2;

        
        public InputFieldWithHintsWidget MasterUri => masterUri.AssertNotNull(nameof(masterUri));
        public InputFieldWithHintsWidget MyUri => myUri.AssertNotNull(nameof(myUri));
        public InputFieldWithHintsWidget MyId => myId.AssertNotNull(nameof(myId));
        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public LineLog LineLog => lineLog.AssertNotNull(nameof(lineLog));
        public ToggleButtonWidget ServerMode => serverMode.AssertNotNull(nameof(serverMode));

        public InputFieldWithHintsWidget MyId2 => myId2.AssertNotNull(nameof(myId2));
        public SimpleButtonWidget RosVersion1 => rosVersion1.AssertNotNull(nameof(rosVersion1));
        public SimpleButtonWidget RosVersion2 => rosVersion2.AssertNotNull(nameof(rosVersion2));

        public GameObject RosPanel1 => rosPanel1.AssertNotNull(nameof(rosPanel1));
        public GameObject RosPanel2 => rosPanel2.AssertNotNull(nameof(rosPanel2));
        
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
            ServerMode.ClearSubscribers();
            Close.ClearSubscribers();
            
            MyId2.ClearSubscribers();
            RosVersion1.ClearSubscribers();
            RosVersion2.ClearSubscribers();
        }
    }
}
