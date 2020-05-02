using System.Linq;
using UnityEngine;

namespace Iviz.App
{
    public class DataPanelWidgets : MonoBehaviour
    {
        const float yOffset = 5;
        const float yCloseButton = -31.5f;
        static GameObject HeadTitle;
        static GameObject SectionTitle;
        static GameObject ToggleWidget;
        static GameObject SliderWidget;
        static GameObject InputWidget;
        static GameObject Dropdown;
        static GameObject ColorPicker;
        static GameObject ImagePreview;
        static GameObject CloseButton;
        static GameObject TrashButton;
        static GameObject DataLabel;

        public GameObject content;
        public GameObject statics;
        public GameObject nonstatics;
        float y;

        void Awake()
        {
            if (HeadTitle != null)
            {
                return;
            }
            HeadTitle = Resources.Load<GameObject>("Widgets/Head Title");
            SectionTitle = Resources.Load<GameObject>("Widgets/Section Title");
            ToggleWidget = Resources.Load<GameObject>("Widgets/Toggle");
            SliderWidget = Resources.Load<GameObject>("Widgets/Slider");
            InputWidget = Resources.Load<GameObject>("Widgets/Input Field");
            ColorPicker = Resources.Load<GameObject>("Widgets/ColorPicker");
            ImagePreview = Resources.Load<GameObject>("Widgets/Image Preview");
            Dropdown = Resources.Load<GameObject>("Widgets/Dropdown");
            CloseButton = Resources.Load<GameObject>("Widgets/Close Button");
            TrashButton = Resources.Load<GameObject>("Widgets/Trash Button");
            DataLabel = Resources.Load<GameObject>("Widgets/Data Label");
        }

        public TrashButtonWidget AddCloseButton()
        {
            GameObject o = Instantiate(CloseButton, nonstatics.transform);
            RectTransform transform = o.transform as RectTransform;
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public TrashButtonWidget AddTrashButton()
        {
            GameObject o = Instantiate(TrashButton, nonstatics.transform);
            RectTransform transform = o.transform as RectTransform;
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        GameObject AddToBottom(GameObject o)
        {
            RectTransform transform = o.GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, -y);
            y += transform.rect.height + yOffset;
            o.SetActive(true);
            return o;
        }

        public HeadTitleWidget AddHeadTitleWidget(string label)
        {
            GameObject o = Instantiate(HeadTitle, nonstatics.transform);
            return AddToBottom(o).GetComponent<HeadTitleWidget>().SetLabel(label);
        }

        public SectionTitleWidget AddSectionTitleWidget(string label)
        {
            GameObject o = Instantiate(SectionTitle, nonstatics.transform);
            return AddToBottom(o).GetComponent<SectionTitleWidget>().SetLabel(label);
        }

        public ToggleWidget AddToggle(string label)
        {
            GameObject o = Instantiate(ToggleWidget, nonstatics.transform);
            return AddToBottom(o).GetComponent<ToggleWidget>().SetLabel(label);
        }

        public SliderWidget AddSlider(string label)
        {
            GameObject o = Instantiate(SliderWidget, nonstatics.transform);
            return AddToBottom(o).GetComponent<SliderWidget>().SetLabel(label);
        }

        public InputFieldWidget AddInputField(string label)
        {
            GameObject o = Instantiate(InputWidget, nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWidget>().SetLabel(label);
        }

        public DropdownWidget AddDropdown(string label)
        {
            GameObject o = Instantiate(Dropdown, nonstatics.transform);
            return AddToBottom(o).GetComponent<DropdownWidget>().SetLabel(label);
        }

        public ColorPickerWidget AddColorPicker(string label)
        {
            GameObject o = Instantiate(ColorPicker, nonstatics.transform);
            return AddToBottom(o).GetComponent<ColorPickerWidget>().SetLabel(label);
        }

        public ImagePreviewWidget AddImagePreviewWidget(string label)
        {
            GameObject o = Instantiate(ImagePreview, nonstatics.transform);
            return AddToBottom(o).GetComponent<ImagePreviewWidget>().SetLabel(label);
        }

        public DataLabelWidget AddDataLabel(string label)
        {
            GameObject o = Instantiate(DataLabel, nonstatics.transform);
            return AddToBottom(o).GetComponent<DataLabelWidget>().SetLabel(label);
        }

        public void UpdateSize()
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, y);
            content.transform.GetComponentsInChildren<Transform>().
                Where(x => x.name[0] == '_').
                ForEach(x =>
                {
                    x.gameObject.SetActive(false);
                    Vector3 absolutePosition = x.transform.position;
                    x.gameObject.transform.SetParentLocal(statics.transform);
                    x.gameObject.transform.position = absolutePosition;
                    x.gameObject.SetActive(true);
                });
        }
    }

}