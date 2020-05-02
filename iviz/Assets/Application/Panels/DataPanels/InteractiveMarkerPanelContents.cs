namespace Iviz.App
{
    public class InteractiveMarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public DataLabelWidget Topic { get; private set; }
        public ToggleWidget DisableExpiration { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("IMarkers");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            Topic = p.AddDataLabel("");
            DisableExpiration = p.AddToggle("Disable Expiration");
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);

            Topic.label.alignment = UnityEngine.TextAnchor.UpperLeft;

            Widgets = new Widget[] { CloseButton, DisableExpiration };
        }
    }
}