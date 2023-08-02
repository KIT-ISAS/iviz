using System;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class VizWidgetModulePanel : ListenerModulePanel
    {
        public SimpleButtonWidget CloseButton { get; private set; }
        public SimpleButtonWidget ResetButton { get; private set; }
        public MarkerDialogWidget MarkerDialog { get; private set; }
        public SenderWidget FeedbackSender { get; private set; }

        public SliderWidget MinValidScore { get; private set; }

        CollapsibleWidget DetectionWidgets { get; set; }

        public enum ModeType
        {
            Detections
        }

        public ModeType Mode
        {
            set
            {
                var widget = value switch
                {
                    ModeType.Detections => DetectionWidgets,
                    _ => throw new NotImplementedException()
                };
                widget.Visible = true;
                widget.Open = true;
            }
        }

        protected override void Initialize()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("Viz Widgets");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Listener = p.AddListener();
            FeedbackSender = p.AddSender();
            ResetButton = p.AddResetButton();
            MarkerDialog = p.AddMarkerDialog();

            MinValidScore = p.AddSlider("Min Valid Score").SetMinValue(0).SetMaxValue(1);

            DetectionWidgets = p.AddCollapsibleWidget("Detections")
                .Attach(MinValidScore)
                .FinishAttaching();

            p.UpdateSize();

            DetectionWidgets.Visible = false;

            gameObject.SetActive(false);
        }
    }
}