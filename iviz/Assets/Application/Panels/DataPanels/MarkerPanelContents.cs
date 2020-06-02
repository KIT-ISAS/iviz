namespace Iviz.App
{
    public class MarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Listener = p.AddListener();
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}