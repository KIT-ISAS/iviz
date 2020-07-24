namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerModuleData"/> 
    /// </summary>    
    public sealed class InteractiveMarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleWidget DisableExpiration { get; private set; }
        public SenderWidget Sender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Interactive\nMarker");
            Listener = p.AddListener();
            DisableExpiration = p.AddToggle("Disable Expiration");
            Sender = p.AddSender();
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}