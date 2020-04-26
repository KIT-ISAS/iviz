using System.Collections.Generic;

namespace Iviz.App
{
    public class MarkerPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public DataLabelWidget Topic { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Markers");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            Topic = p.AddDataLabel("");
            CloseButton = p.AddTrashButton();
            p.UpdateSize();
            gameObject.SetActive(false);

            Topic.label.alignment = UnityEngine.TextAnchor.UpperLeft;

            Widgets = new Widget[] { CloseButton };
        }
    }
}