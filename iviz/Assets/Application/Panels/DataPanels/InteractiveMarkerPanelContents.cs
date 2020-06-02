namespace Iviz.App
{
    public class InteractiveMarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleWidget DisableExpiration { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("IMarkers");
            Listener = p.AddListener();
            DisableExpiration = p.AddToggle("Disable Expiration");
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}