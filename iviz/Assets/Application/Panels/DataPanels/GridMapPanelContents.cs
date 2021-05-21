using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="GridMapModuleData"/> 
    /// </summary>

    public sealed class GridMapPanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public DataLabelWidget Description { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public DropdownWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public NumberInputFieldWidget MinIntensity { get; private set; }
        public NumberInputFieldWidget MaxIntensity { get; private set; }
        public SliderWidget Alpha { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }


        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("GridMap");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            Description = p.AddDataLabel("Min/Max").SetHasRichText(true).SetAlignment(TextAnchor.MiddleCenter);;
            IntensityChannel = p.AddDropdown("Intensity Channel");
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)ColormapId.hsv);

            ForceMinMax = p.AddToggle("Colormap Force Min/Max");
            MinIntensity = p.AddNumberInputField("Colormap Min");
            MaxIntensity = p.AddNumberInputField("Colormap Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");

            Alpha = p.AddSlider("Alpha").SetMinValue(0).SetMaxValue(1).SetNumberOfSteps(256);

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}