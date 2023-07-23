#nullable enable

using Iviz.Core;
using Iviz.Ros;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ConnectionDialogData"/> 
    /// </summary>
    public sealed class ConnectionDialogPanel : DialogPanel
    {
        [SerializeField] DropdownWidget? rosMode;
        
        [SerializeField] InputFieldWithHintsWidget? masterUri;
        [SerializeField] InputFieldWithHintsWidget? myUri;
        [SerializeField] InputFieldWithHintsWidget? myId;
        [SerializeField] ToggleButtonWidget? serverMode;

        [SerializeField] InputFieldWithHintsWidget? myId2;
        [SerializeField] DropdownWidget? domainId;
        [SerializeField] InputFieldWithHintsWidget? discoveryServer;

        [SerializeField] InputFieldWithHintsWidget? bridgeUri;
        [SerializeField] InputFieldWithHintsWidget? myIdBridge;

        //[SerializeField] SimpleButtonWidget? rosVersion1;
        //[SerializeField] SimpleButtonWidget? rosVersion2;

        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] LineLog? lineLog;

        [SerializeField] GameObject? rosPanel1;
        [SerializeField] GameObject? rosPanel2;
        [SerializeField] GameObject? rosPanelBridge;
        
        public DropdownWidget RosMode => rosMode.AssertNotNull(nameof(rosMode));
        
        public InputFieldWithHintsWidget MasterUri => masterUri.AssertNotNull(nameof(masterUri));
        public InputFieldWithHintsWidget MyUri => myUri.AssertNotNull(nameof(myUri));
        public InputFieldWithHintsWidget MyId => myId.AssertNotNull(nameof(myId));
        public ToggleButtonWidget ServerMode => serverMode.AssertNotNull(nameof(serverMode));

        public InputFieldWithHintsWidget MyId2 => myId2.AssertNotNull(nameof(myId2));
        public DropdownWidget DomainId => domainId.AssertNotNull(nameof(domainId));
        public InputFieldWithHintsWidget DiscoveryServer => discoveryServer.AssertNotNull(nameof(discoveryServer));

        public InputFieldWithHintsWidget BridgeUri => bridgeUri.AssertNotNull(nameof(bridgeUri));
        public InputFieldWithHintsWidget MyIdBridge => myIdBridge.AssertNotNull(nameof(myIdBridge));

        
        public GameObject RosPanel1 => rosPanel1.AssertNotNull(nameof(rosPanel1));
        public GameObject RosPanel2 => rosPanel2.AssertNotNull(nameof(rosPanel2));
        public GameObject RosPanelBridge => rosPanelBridge.AssertNotNull(nameof(rosPanelBridge));
        
        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public LineLog LineLog => lineLog.AssertNotNull(nameof(lineLog));

        void Awake()
        {
            ServerMode.InactiveText = "Master\nOff";
            ServerMode.ActiveText = "Master\nOn";
            ServerMode.State = RosManager.Server.IsActive;

            DomainId.SetOptions(new[]
            {
                "0", "1", "2", "3", "4",
                "5", "6", "7", "8", "9",
            });
            
        }

        public override void ClearSubscribers()
        {
            MasterUri.ClearSubscribers();
            MyUri.ClearSubscribers();
            MyId.ClearSubscribers();
            
            DomainId.ClearSubscribers();
            DiscoveryServer.ClearSubscribers();
            MyId2.ClearSubscribers();
            
            BridgeUri.ClearSubscribers();
            MyIdBridge.ClearSubscribers();

            ServerMode.ClearSubscribers();
            Close.ClearSubscribers();

            RosMode.ClearSubscribers();
            //RosVersion1.ClearSubscribers();
            //RosVersion2.ClearSubscribers();
        }
    }
}
