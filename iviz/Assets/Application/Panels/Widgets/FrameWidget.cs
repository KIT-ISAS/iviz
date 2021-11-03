using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class FrameWidget : DraggableButtonWidget
    {
        [SerializeField] Text text = null;

        TfFrame frame;
        IHasFrame owner;

        [CanBeNull]
        public IHasFrame Owner
        {
            get => owner;
            set
            {
                if (owner == null && value != null)
                {
                    GameThread.EverySecond += UpdateStats;
                }
                else if (owner != null && value == null)
                {
                    GameThread.EverySecond -= UpdateStats;
                }

                owner = value;
                Frame = owner?.Frame;
            }
        }

        [CanBeNull]
        TfFrame Frame
        {
            get => frame;
            set
            {
                if (frame == value)
                {
                    return;
                }

                frame = value;
                string newText;
                if (frame == null)
                {
                    newText = "<i>➤ (none)</i>";
                }
                else if (TfListener.FixedFrameId == frame.Id)
                {
                    newText = $"<b>➤{frame.Id}</b> <i>[Fixed]</i>";
                }
                else
                {
                    newText = $"<b>➤{frame.Id}</b>";
                }

                text.text = newText;
                UpdateStats();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Frame = null;
        }

        public override void ClearSubscribers()
        {
            base.ClearSubscribers();
            Owner = null;
            Frame = null;
        }

        void UpdateStats()
        {
            Frame = Owner?.Frame;
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            if (Frame == null)
            {
                return;
            }

            var guiInputModule = ModuleListPanel.GuiInputModule;
            if (guiInputModule != null)
            {
                guiInputModule.LookAt(Frame.AbsoluteUnityPose.position);
            }

            Frame.Highlight();
        }

        protected override void OnRevealedRight()
        {
            base.OnRevealedRight();
            
            var guiInputModule = ModuleListPanel.GuiInputModule;
            if (frame != null && guiInputModule != null)
            {
                guiInputModule.OrbitCenterOverride = guiInputModule.OrbitCenterOverride != frame ? frame : null;
            }
        }
    }
}