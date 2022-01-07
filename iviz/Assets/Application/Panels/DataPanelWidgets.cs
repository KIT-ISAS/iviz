#nullable enable

using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class DataPanelWidgets : MonoBehaviour
    {
        const float YCloseButton = -17.5f;

        [SerializeField] GameObject? content = null;
        [SerializeField] GameObject? statics = null;
        [SerializeField] GameObject? nonstatics = null;

        GameObject Content => content.AssertNotNull(nameof(content));
        GameObject Statics => statics.AssertNotNull(nameof(statics));
        GameObject NonStatics => nonstatics.AssertNotNull(nameof(nonstatics));

        public TrashButtonWidget AddTrashButton()
        {
            var o = Resource.Widgets.TrashButton.Instantiate(Statics.transform);
            var oTransform = (RectTransform) o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, YCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public ToggleButtonWidget AddHideButton()
        {
            var o = Resource.Widgets.ToggleButton.Instantiate(Statics.transform);
            var oTransform = (RectTransform) o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, YCloseButton);
            o.SetActive(true);
            return o.GetComponent<ToggleButtonWidget>();
        }
        
        public TrashButtonWidget AddCloseButton()
        {
            var o = Resource.Widgets.CloseButton.Instantiate(Statics.transform);
            var oTransform = (RectTransform) o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, YCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public TrashButtonWidget AddResetButton()
        {
            var o = Resource.Widgets.ResetButton.Instantiate(Statics.transform);
            var oTransform = (RectTransform) o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, YCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public HeadTitleWidget AddHeadTitleWidget(string label)
        {
            return Resource.Widgets.HeadTitle.Instantiate<HeadTitleWidget>(NonStatics.transform).SetLabel(label);
        }

        public ToggleWidget AddToggle(string label)
        {
            return Resource.Widgets.Toggle.Instantiate<ToggleWidget>(NonStatics.transform).SetLabel(label);
        }

        public SliderWidget AddSlider(string label)
        {
            return Resource.Widgets.Slider.Instantiate<SliderWidget>(NonStatics.transform).SetLabel(label);
        }

        public SliderWidgetWithScale AddSliderWidgetWithScale(string label)
        {
            return Resource.Widgets.SliderWithScale.Instantiate<SliderWidgetWithScale>(NonStatics.transform).SetLabel(label);
        }

        public InputFieldWidget AddInputField(string label)
        {
            return Resource.Widgets.Input.Instantiate<InputFieldWidget>(NonStatics.transform).SetLabel(label);
        }

        public InputFieldWithHintsWidget AddInputFieldWithHints(string label)
        {
            return Resource.Widgets.InputWithHints.Instantiate<InputFieldWithHintsWidget>(NonStatics.transform)
                .SetLabel(label);
        }

        public NumberInputFieldWidget AddNumberInputField(string label)
        {
            return Resource.Widgets.NumberInput.Instantiate<NumberInputFieldWidget>(NonStatics.transform)
                .SetLabel(label);
        }

        public Vector3Widget AddVector3(string label)
        {
            return Resource.Widgets.Vector3.Instantiate<Vector3Widget>(NonStatics.transform).SetLabel(label);
        }

        public Vector3SliderWidget AddVector3Slider(string label)
        {
            return Resource.Widgets.Vector3Slider.Instantiate<Vector3SliderWidget>(NonStatics.transform)
                .SetLabel(label);
        }

        public DropdownWidget AddDropdown(string label)
        {
            return Resource.Widgets.Dropdown.Instantiate<DropdownWidget>(NonStatics.transform).SetLabel(label);
        }

        public ColorPickerWidget AddColorPicker(string label)
        {
            return Resource.Widgets.ColorPicker.Instantiate<ColorPickerWidget>(NonStatics.transform).SetLabel(label);
        }

        public ImagePreviewWidget AddImagePreviewWidget(string label)
        {
            return Resource.Widgets.ImagePreview.Instantiate<ImagePreviewWidget>(NonStatics.transform).SetLabel(label);
        }

        public DataLabelWidget AddDataLabel(string label)
        {
            return Resource.Widgets.DataLabel.Instantiate<DataLabelWidget>(NonStatics.transform).SetLabel(label);
        }

        public SenderWidget AddSender()
        {
            return Resource.Widgets.Sender.Instantiate<SenderWidget>(NonStatics.transform);
        }

        public ListenerWidget AddListener()
        {
            return Resource.Widgets.Listener.Instantiate<ListenerWidget>(NonStatics.transform);
        }

        public MarkerWidget AddMarker()
        {
            return Resource.Widgets.MarkerWidget.Instantiate<MarkerWidget>(NonStatics.transform);
        }

        public FrameWidget AddFrame()
        {
            return Resource.Widgets.Frame.Instantiate<FrameWidget>(NonStatics.transform);
        }

        public ARMarkerWidget AddARMarker()
        {
            return Resource.Widgets.ARMarkerWidget.Instantiate<ARMarkerWidget>(NonStatics.transform);
        }
        
        public TfPublisherWidget AddTfPublisher()
        {
            return Resource.Widgets.TfPublisherWidget.Instantiate<TfPublisherWidget>(NonStatics.transform);
        }

        public CollapsibleWidget AddCollapsibleWidget(string label)
        {
            return Resource.Widgets.CollapsibleWidget.Instantiate<CollapsibleWidget>(NonStatics.transform)
                .SetLabel(label)
                .SetParent(this);
        }

        public void UpdateSize()
        {
            const float yOffset = 5;

            var children = NonStatics.transform.Cast<RectTransform>();
            float y = 0;
            foreach (var child in children)
            {
                child.anchoredPosition = new Vector2(child.anchoredPosition.x, -y);
                y += child.rect.height + yOffset;
                child.gameObject.SetActive(true);
            }

            var contentTransform = (RectTransform) Content.transform;
            contentTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);
        }
    }
}