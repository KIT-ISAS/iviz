
namespace Iviz.App
{
    public class ARPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3Widget Origin { get; private set; }
        public NumberInputFieldWidget WorldScale { get; private set; }
        public ToggleWidget SearchMarker { get; private set; }
        public SenderWidget HeadSender { get; private set; }
        public SenderWidget MarkersSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("AR");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Origin = p.AddVector3("Offset");
            WorldScale = p.AddNumberInputField("World Scale");
            SearchMarker = p.AddToggle("Search Origin Marker");
            HeadSender = p.AddSender();
            MarkersSender = p.AddSender();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}