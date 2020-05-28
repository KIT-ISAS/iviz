using Iviz.Displays;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ARPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3Widget Origin { get; private set; }
        public NumberInputFieldWidget WorldScale { get; private set; }
        public ToggleWidget SearchMarker { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("AR");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Origin = p.AddVector3("Offset");
            WorldScale = p.AddNumberInputField("World Scale");
            SearchMarker = p.AddToggle("Search Origin Marker");
            p.UpdateSize();
            gameObject.SetActive(false);

            Widgets = new Widget[] { CloseButton, HideButton, Origin, WorldScale, SearchMarker };
        }
    }
}