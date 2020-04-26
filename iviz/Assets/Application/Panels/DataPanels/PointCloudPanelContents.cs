using System.Collections.Generic;

namespace Iviz.App
{
    public class PointCloudPanelContents : ListenerPanelContents
    {
        static readonly List<string> DefaultChannels = new List<string> { "x", "y", "z" };
        public DataLabelWidget Topic { get; private set; }
        public SliderWidget PointSize { get; private set; }
        public DropdownWidget Colormap { get; private set; }
        public DropdownWidget IntensityChannel { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }

        void Start()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("PointCloud");
            Stats = p.AddSectionTitleWidget("Off | 0 Hz | 0 - 0 ms");
            Topic = p.AddDataLabel("");
            PointSize = p.AddSlider("Point Size").SetMinValue(0.01f).SetMaxValue(0.1f);
            //p.AddToggle("Calculate Min/Max");
            Colormap = p.AddDropdown("Colormap")
                        .SetOptions(Resource.Colormaps.Names)
                        .SetIndex((int)Resource.Colormaps.Id.hsv);
            IntensityChannel = p.AddDropdown("Intensity Channel")
                        .SetOptions(DefaultChannels);
            CloseButton = p.AddTrashButton();
            //p.AddToggle("Override Color");
            p.UpdateSize();
            gameObject.SetActive(false);

            //Stats.label.alignment = UnityEngine.TextAnchor.MiddleCenter;
            Topic.label.alignment = UnityEngine.TextAnchor.UpperLeft;

            Widgets = new Widget[] { PointSize, Colormap, IntensityChannel, CloseButton };
        }
    }
}