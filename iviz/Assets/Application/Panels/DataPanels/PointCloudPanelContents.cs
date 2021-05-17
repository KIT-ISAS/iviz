using System.Collections.Generic;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="PointCloudModuleData"/> 
    /// </summary>

    public sealed class PointCloudPanelContents : ListenerPanelContents
    {
        static readonly List<string> DefaultChannels = new List<string> { "x", "y", "z" };
        public FrameWidget Frame { get; private set; }
        public DataLabelWidget NumPoints { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public DropdownWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public ToggleButtonWidget HideButton { get; private set; }

        public ToggleWidget ForceMinMax { get; private set; }
        public NumberInputFieldWidget MinIntensity { get; private set; }
        public NumberInputFieldWidget MaxIntensity { get; private set; }
        public ToggleWidget FlipMinMax { get; private set; }
        public DropdownWidget PointCloudType { get; private set; }


        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Listener = p.AddListener();
            Frame = p.AddFrame();
            NumPoints = p.AddDataLabel("Number of Points").SetHasRichText(true).SetAlignment(TextAnchor.MiddleCenter);
            PointSize = p.AddSlider("Point Size").SetMinValue(0.005f).SetMaxValue(0.1f);
            //p.AddToggle("Calculate Min/Max");
            IntensityChannel = p.AddDropdown("Intensity Channel")
                        .SetOptions(DefaultChannels);
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)ColormapId.hsv);

            ForceMinMax = p.AddToggle("Colormap Override Min/Max");
            MinIntensity = p.AddNumberInputField("Colormap Min");
            MaxIntensity = p.AddNumberInputField("Colormap Max");
            FlipMinMax = p.AddToggle("Flip Min/Max");
            PointCloudType = p.AddDropdown("Show as").SetOptions(new[] {"Points", "Cubes", "Spheres"});
            

            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            //p.AddToggle("Override Color");
            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}