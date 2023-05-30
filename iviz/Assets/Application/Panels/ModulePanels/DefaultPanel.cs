namespace Iviz.App
{
    public class DefaultPanel : ModulePanel
    {
        protected override void Initialize()
        {
        }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("NYI");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}