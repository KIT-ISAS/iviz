namespace Iviz.App
{
    public class DefaultPanel : ModulePanel
    {
        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("NYI");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}