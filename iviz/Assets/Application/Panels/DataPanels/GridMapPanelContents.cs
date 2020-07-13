using System.Collections.Generic;
using Iviz.Resources;
using UnityEngine.UI;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridMapModuleData"/> 
    /// </summary>

    public sealed class GridMapPanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public DataLabelWidget MinMax { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        //public DropdownWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public NumberInputFieldWidget MinIntensity { get; private set; }
        public NumberInputFieldWidget MaxIntensity { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }


        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("GridMap");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            MinMax = p.AddDataLabel("Min/Max");
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.ColormapId.hsv);

            ForceMinMax = p.AddToggle("Force Min/Max");
            MinIntensity = p.AddNumberInputField("Min");
            MaxIntensity = p.AddNumberInputField("Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}