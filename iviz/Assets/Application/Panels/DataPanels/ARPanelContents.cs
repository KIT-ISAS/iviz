using Iviz.Displays;
using UnityEngine;

namespace Iviz.App
{
    public class ARPanelContents : DataPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }
        public Vector3Widget Origin { get; private set; }
        public InputFieldWidget WorldScale { get; private set; }
        public ToggleWidget SearchMarker { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Grid");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Origin = p.AddVector3("Offset");
            WorldScale = p.AddShortInputField("World Scale");
            SearchMarker = p.AddToggle("Search Origin Marker");
            p.UpdateSize();
            gameObject.SetActive(false);

            Widgets = new Widget[] { CloseButton, HideButton, Origin, WorldScale, SearchMarker };
        }
    }
}