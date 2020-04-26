namespace Iviz.App
{
    public class DefaultPanelContents : DataPanelContents
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