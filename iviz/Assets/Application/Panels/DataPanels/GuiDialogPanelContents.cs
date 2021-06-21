
using UnityEngine;

namespace Iviz.App
{
    /// <summary>
    /// <see cref="ARModuleData"/> 
    /// </summary>
    public sealed class GuiDialogPanelContents : ListenerPanelContents
    {
        public FrameWidget Frame { get; private set; }
        public TrashButtonWidget CloseButton { get; private set; }
        public SenderWidget FeedbackSender { get; private set; }

        //public ToggleWidget PublishHead { get; private set; }
        //public ToggleWidget PublishPlanes { get; private set; }

        //public SenderWidget HeadSender { get; private set; }
        //public SenderWidget MarkersSender { get; private set; }

        void Awake()
        {
            DataPanelWidgets p = GetComponent<DataPanelWidgets>();
            p.AddHeadTitleWidget("GUI Dialogs");
            CloseButton = p.AddTrashButton();
            HideButton = p.AddHideButton();
            Listener = p.AddListener();
            Frame = p.AddFrame();
            FeedbackSender = p.AddSender();

            p.UpdateSize();
            gameObject.SetActive(false);
        }
    }
}