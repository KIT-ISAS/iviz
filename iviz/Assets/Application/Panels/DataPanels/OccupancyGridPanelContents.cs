using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine.UI;

namespace Iviz.App
{
    public class OccupancyGridPanelContents : ListenerPanelContents
    {
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public DropdownWidget Colormap { get; private set; }
        public ToggleWidget FlipColors { get; private set; }
        public NumberInputFieldWidget ScaleZ { get; private set; }


        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Listener = p.AddListener();
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.gray);
            FlipColors = p.AddToggle("Flip Color Bounds");
            ScaleZ = p.AddNumberInputField("Height Scale");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}