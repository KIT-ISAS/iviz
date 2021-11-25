#nullable enable

using System.Text;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class FrameWidget : DraggableButtonWidget
    {
        [SerializeField] TMP_Text? text = null;
        TMP_Text Text => text.AssertNotNull(nameof(text));

        TfFrame? frame;
        IHasFrame? owner;

        public IHasFrame? Owner
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

        TfFrame? Frame
        {
            get => frame;
            set
            {
                if (frame == value)
                {
                    return;
                }

                frame = value;


                var description = BuilderPool.Rent();
                try
                {
                    if (frame == null)
                    {
                        description.Append("<i>(none)</i>");
                    }
                    else if (TfListener.FixedFrameId == frame.Id)
                    {
                        description.Append("<b>").Append(frame.Id).Append("</b> <i>[Fixed]</i>");
                    }
                    else
                    {
                        description.Append("<b>").Append(frame.Id).Append("</b>\n");
                        RosUtils.FormatPose(frame.OriginWorldPose, description, RosUtils.PoseFormat.OnlyPosition);
                    }

                    Text.SetText(description);
                }
                finally
                {
                    BuilderPool.Return(description);
                }

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

            Frame.Highlight();
            GuiInputModule.Instance.LookAt(Frame.Transform);
        }

        protected override void OnRevealedRight()
        {
            base.OnRevealedRight();

            if (Frame != null)
            {
                GuiInputModule.Instance.OrbitCenterOverride =
                    GuiInputModule.Instance.OrbitCenterOverride != frame ? frame : null;
            }
        }
    }
}