using System.Collections;
using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public class DataPanelWidgets : MonoBehaviour
    {
        const float yCloseButton = -17.5f;

        [SerializeField] GameObject content = null;
        [SerializeField] GameObject statics = null;

        [SerializeField] GameObject nonstatics = null;
        //float y;

        public TrashButtonWidget AddTrashButton()
        {
            var o = Resource.Widgets.TrashButton.Instantiate(statics.transform);
            var oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public ToggleButtonWidget AddHideButton()
        {
            var o = Resource.Widgets.ToggleButton.Instantiate(statics.transform);
            var oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<ToggleButtonWidget>();
        }

        public TrashButtonWidget AddResetButton()
        {
            var o = Resource.Widgets.ResetButton.Instantiate(statics.transform);
            var oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        [NotNull]
        public HeadTitleWidget AddHeadTitleWidget([NotNull] string label)
        {
            return Resource.Widgets.HeadTitle.Instantiate<HeadTitleWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public SectionTitleWidget AddSectionTitleWidget([NotNull] string label)
        {
            return Resource.Widgets.SectionTitle.Instantiate<SectionTitleWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public ToggleWidget AddToggle([NotNull] string label)
        {
            return Resource.Widgets.Toggle.Instantiate<ToggleWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public SliderWidget AddSlider([NotNull] string label)
        {
            return Resource.Widgets.Slider.Instantiate<SliderWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public InputFieldWidget AddInputField([NotNull] string label)
        {
            return Resource.Widgets.Input.Instantiate<InputFieldWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public InputFieldWithHintsWidget AddInputFieldWithHints([NotNull] string label)
        {
            return Resource.Widgets.InputWithHints.Instantiate<InputFieldWithHintsWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public InputFieldWidget AddShortInputField([NotNull] string label)
        {
            return Resource.Widgets.ShortInput.Instantiate<InputFieldWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public NumberInputFieldWidget AddNumberInputField([NotNull] string label)
        {
            return Resource.Widgets.NumberInput.Instantiate<NumberInputFieldWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public Vector3Widget AddVector3([NotNull] string label)
        {
            return Resource.Widgets.Vector3.Instantiate<Vector3Widget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public Vector3SliderWidget AddVector3Slider([NotNull] string label)
        {
            return Resource.Widgets.Vector3Slider.Instantiate<Vector3SliderWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public DropdownWidget AddDropdown([NotNull] string label)
        {
            return Resource.Widgets.Dropdown.Instantiate<DropdownWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public ColorPickerWidget AddColorPicker([NotNull] string label)
        {
            return Resource.Widgets.ColorPicker.Instantiate<ColorPickerWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public ImagePreviewWidget AddImagePreviewWidget([NotNull] string label)
        {
            return Resource.Widgets.ImagePreview.Instantiate<ImagePreviewWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public DataLabelWidget AddDataLabel([NotNull] string label)
        {
            return Resource.Widgets.DataLabel.Instantiate<DataLabelWidget>(nonstatics.transform).SetLabel(label);
        }

        [NotNull]
        public SenderWidget AddSender()
        {
            return Resource.Widgets.Sender.Instantiate<SenderWidget>(nonstatics.transform);
        }

        [NotNull]
        public ListenerWidget AddListener()
        {
            return Resource.Widgets.Listener.Instantiate<ListenerWidget>(nonstatics.transform);
        }

        [NotNull]
        public MarkerWidget AddMarker()
        {
            return Resource.Widgets.MarkerWidget.Instantiate<MarkerWidget>(nonstatics.transform);
        }

        [NotNull]
        public FrameWidget AddFrame()
        {
            return Resource.Widgets.Frame.Instantiate<FrameWidget>(nonstatics.transform);
        }

        [NotNull]
        public ARMarkerWidget AddARMarker()
        {
            return Resource.Widgets.ARMarkerWidget.Instantiate<ARMarkerWidget>(nonstatics.transform);
        }
        
        [NotNull]
        public CollapsibleWidget AddCollapsibleWidget([NotNull] string label)
        {
            return Resource.Widgets.CollapsibleWidget.Instantiate<CollapsibleWidget>(nonstatics.transform)
                .SetLabel(label)
                .SetParent(this);
        }        

        public void UpdateSize()
        {
            const float yOffset = 5;

            var children = nonstatics.transform.Cast<RectTransform>();
            float y = 0;
            foreach (var child in children)
            {
                child.anchoredPosition = new Vector2(child.anchoredPosition.x, -y);
                y += child.rect.height + yOffset;
                child.gameObject.SetActive(true);
            }

            var contentTransform = (RectTransform)content.transform;
            contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, y);
        }
    }
}