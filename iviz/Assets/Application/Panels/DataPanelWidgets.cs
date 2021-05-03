using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App
{
    public class DataPanelWidgets : MonoBehaviour
    {
        const float yOffset = 5;
        const float yCloseButton = -17.5f;

        public GameObject content;
        public GameObject statics;
        public GameObject nonstatics;
        float y;

        public TrashButtonWidget AddCloseButton()
        {
            GameObject o = Resource.Widgets.CloseButton.Instantiate(nonstatics.transform);
            RectTransform ttransform = (RectTransform)o.transform;
            ttransform.anchoredPosition = new Vector2(ttransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public TrashButtonWidget AddTrashButton()
        {
            GameObject o = Resource.Widgets.TrashButton.Instantiate(nonstatics.transform);
            RectTransform transform = (RectTransform)o.transform;
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public ToggleButtonWidget AddHideButton()
        {
            GameObject o = Resource.Widgets.ToggleButton.Instantiate(nonstatics.transform);
            RectTransform transform = (RectTransform)o.transform;
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<ToggleButtonWidget>();
        }

        public TrashButtonWidget AddResetButton()
        {
            GameObject o = Resource.Widgets.ResetButton.Instantiate(nonstatics.transform);
            RectTransform transform = (RectTransform)o.transform;
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }


        GameObject AddToBottom(GameObject o)
        {
            RectTransform transform = (RectTransform)o.transform;
            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, -y);
            y += transform.rect.height + yOffset;
            o.SetActive(true);
            return o;
        }

        public HeadTitleWidget AddHeadTitleWidget(string label)
        {
            GameObject o = Resource.Widgets.HeadTitle.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<HeadTitleWidget>().SetLabel(label);
        }

        public SectionTitleWidget AddSectionTitleWidget(string label)
        {
            GameObject o = Resource.Widgets.SectionTitle.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<SectionTitleWidget>().SetLabel(label);
        }

        public ToggleWidget AddToggle(string label)
        {
            GameObject o = Resource.Widgets.Toggle.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ToggleWidget>().SetLabel(label);
        }

        public SliderWidget AddSlider(string label)
        {
            GameObject o = Resource.Widgets.Slider.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<SliderWidget>().SetLabel(label);
        }

        public InputFieldWidget AddInputField(string label)
        {
            GameObject o = Resource.Widgets.Input.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWidget>().SetLabel(label);
        }
        
        public InputFieldWithHintsWidget AddInputFieldWithHints(string label)
        {
            GameObject o = Resource.Widgets.InputWithHints.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWithHintsWidget>().SetLabel(label);
        }

        public InputFieldWidget AddShortInputField(string label)
        {
            GameObject o = Resource.Widgets.ShortInput.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWidget>().SetLabel(label);
        }

        public NumberInputFieldWidget AddNumberInputField(string label)
        {
            GameObject o = Resource.Widgets.NumberInput.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<NumberInputFieldWidget>().SetLabel(label);
        }

        public Vector3Widget AddVector3(string label)
        {
            GameObject o = Resource.Widgets.Vector3.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<Vector3Widget>().SetLabel(label);
        }

        public Vector3SliderWidget AddVector3Slider(string label)
        {
            GameObject o = Resource.Widgets.Vector3Slider.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<Vector3SliderWidget>().SetLabel(label);
        }

        public DropdownWidget AddDropdown(string label)
        {
            GameObject o = Resource.Widgets.Dropdown.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<DropdownWidget>().SetLabel(label);
        }

        public ColorPickerWidget AddColorPicker(string label)
        {
            GameObject o = Resource.Widgets.ColorPicker.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ColorPickerWidget>().SetLabel(label);
        }

        public ImagePreviewWidget AddImagePreviewWidget(string label)
        {
            GameObject o = Resource.Widgets.ImagePreview.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ImagePreviewWidget>().SetLabel(label);
        }

        public DataLabelWidget AddDataLabel(string label)
        {
            GameObject o = Resource.Widgets.DataLabel.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<DataLabelWidget>().SetLabel(label);
        }

        public SenderWidget AddSender()
        {
            GameObject o = Resource.Widgets.Sender.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<SenderWidget>();
        }

        public ListenerWidget AddListener()
        {
            GameObject o = Resource.Widgets.Listener.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ListenerWidget>();
        }
        
        public MarkerWidget AddMarker()
        {
            GameObject o = Resource.Widgets.MarkerWidget.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<MarkerWidget>();
        }

        public FrameWidget AddFrame()
        {
            GameObject o = Resource.Widgets.Frame.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<FrameWidget>();
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
                    x.transform.SetParentLocal(statics.transform);
                    x.transform.position = absolutePosition;
                    x.gameObject.SetActive(true);
                });
        }
    }

}