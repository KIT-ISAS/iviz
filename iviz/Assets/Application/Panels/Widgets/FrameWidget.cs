#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Tools;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class FrameWidget : DraggableButtonWidget
    {
        [SerializeField] TMP_Text? text;
        TMP_Text Text => text.AssertNotNull(nameof(text));

        TfFrame? frame;
        IHasFrame? owner;
        uint? textHash;

        public IHasFrame? Owner
        {
            get => owner;
            set
            {
                if (owner == null && value != null)
                {
                    GameThread.EveryTenthOfASecond += UpdateStats;
                }
                else if (owner != null && value == null)
                {
                    GameThread.EveryTenthOfASecond -= UpdateStats;
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
                frame = value;

                using var description = BuilderPool.Rent();
                if (frame == null)
                {
                    description.Append("<i>(none)</i>");
                }
                else if (TfModule.FixedFrameId == frame.Id)
                {
                    description.Append("<b>").Append(frame.Id).Append("</b> [Fixed]");
                }
                else
                {
                    description.Append("<b>").Append(frame.Id).Append("</b>\n");
                    RosUtils.FormatPose(frame.FixedWorldPose, description, RosUtils.PoseFormat.OnlyPosition);
                }

                uint newHash = HashCalculator.Compute(description);
                if (newHash == textHash)
                {
                    return;
                }

                textHash = newHash;
                Text.SetTextRent(description);

                //UpdateStats();
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