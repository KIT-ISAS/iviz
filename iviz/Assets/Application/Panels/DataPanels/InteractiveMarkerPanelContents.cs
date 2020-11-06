namespace Iviz.App
{
    /// <summary>
    /// <see cref="InteractiveMarkerModuleData"/> 
    /// </summary>    
    public sealed class InteractiveMarkerPanelContents : ListenerPanelContents
    {
        public ListenerWidget FullListener { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleWidget DisableExpiration { get; private set; }
        public SenderWidget Sender { get; private set; }
        public MarkerWidget Marker { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Interactive\nMarker");
            Listener = p.AddListener();
            FullListener = p.AddListener();
            DisableExpiration = p.AddToggle("Disable Expiration");
            Sender = p.AddSender();
            CloseButton = p.AddTrashButton();
            Marker = p.AddMarker();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}