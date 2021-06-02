using System.Linq;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
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
            RectTransform oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public TrashButtonWidget AddTrashButton()
        {
            GameObject o = Resource.Widgets.TrashButton.Instantiate(nonstatics.transform);
            RectTransform oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }

        public ToggleButtonWidget AddHideButton()
        {
            GameObject o = Resource.Widgets.ToggleButton.Instantiate(nonstatics.transform);
            RectTransform oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<ToggleButtonWidget>();
        }

        public TrashButtonWidget AddResetButton()
        {
            GameObject o = Resource.Widgets.ResetButton.Instantiate(nonstatics.transform);
            RectTransform oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, yCloseButton);
            o.SetActive(true);
            return o.GetComponent<TrashButtonWidget>();
        }


        GameObject AddToBottom([NotNull] GameObject o)
        {
            RectTransform oTransform = (RectTransform)o.transform;
            oTransform.anchoredPosition = new Vector2(oTransform.anchoredPosition.x, -y);
            y += oTransform.rect.height + yOffset;
            o.SetActive(true);
            return o;
        }

        [NotNull]
        public HeadTitleWidget AddHeadTitleWidget([NotNull] string label)
        {
            GameObject o = Resource.Widgets.HeadTitle.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<HeadTitleWidget>().SetLabel(label);
        }

        [NotNull]
        public SectionTitleWidget AddSectionTitleWidget([NotNull] string label)
        {
            GameObject o = Resource.Widgets.SectionTitle.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<SectionTitleWidget>().SetLabel(label);
        }

        [NotNull]
        public ToggleWidget AddToggle([NotNull] string label)
        {
            GameObject o = Resource.Widgets.Toggle.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ToggleWidget>().SetLabel(label);
        }

        [NotNull]
        public SliderWidget AddSlider([NotNull] string label)
        {
            GameObject o = Resource.Widgets.Slider.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<SliderWidget>().SetLabel(label);
        }

        [NotNull]
        public InputFieldWidget AddInputField([NotNull] string label)
        {
            GameObject o = Resource.Widgets.Input.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWidget>().SetLabel(label);
        }
        
        [NotNull]
        public InputFieldWithHintsWidget AddInputFieldWithHints([NotNull] string label)
        {
            GameObject o = Resource.Widgets.InputWithHints.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWithHintsWidget>().SetLabel(label);
        }

        [NotNull]
        public InputFieldWidget AddShortInputField([NotNull] string label)
        {
            GameObject o = Resource.Widgets.ShortInput.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<InputFieldWidget>().SetLabel(label);
        }

        [NotNull]
        public NumberInputFieldWidget AddNumberInputField([NotNull] string label)
        {
            GameObject o = Resource.Widgets.NumberInput.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<NumberInputFieldWidget>().SetLabel(label);
        }

        [NotNull]
        public Vector3Widget AddVector3([NotNull] string label)
        {
            GameObject o = Resource.Widgets.Vector3.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<Vector3Widget>().SetLabel(label);
        }

        [NotNull]
        public Vector3SliderWidget AddVector3Slider([NotNull] string label)
        {
            GameObject o = Resource.Widgets.Vector3Slider.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<Vector3SliderWidget>().SetLabel(label);
        }

        [NotNull]
        public DropdownWidget AddDropdown([NotNull] string label)
        {
            GameObject o = Resource.Widgets.Dropdown.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<DropdownWidget>().SetLabel(label);
        }

        [NotNull]
        public ColorPickerWidget AddColorPicker([NotNull] string label)
        {
            GameObject o = Resource.Widgets.ColorPicker.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ColorPickerWidget>().SetLabel(label);
        }

        [NotNull]
        public ImagePreviewWidget AddImagePreviewWidget([NotNull] string label)
        {
            GameObject o = Resource.Widgets.ImagePreview.Instantiate(nonstatics.transform);
            return AddToBottom(o).GetComponent<ImagePreviewWidget>().SetLabel(label);
        }

        [NotNull]
        public DataLabelWidget AddDataLabel([NotNull] string label)
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
                Where(t => t.name[0] == '_').
                ForEach(t =>
                {
                    t.gameObject.SetActive(false);
                    Vector3 absolutePosition = t.transform.position;
                    t.SetParentLocal(statics.transform);
                    t.position = absolutePosition;
                    t.gameObject.SetActive(true);
                });
        }
    }

}