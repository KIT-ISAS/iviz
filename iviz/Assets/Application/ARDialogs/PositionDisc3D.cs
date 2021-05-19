using System;
using Iviz.App.ARDialogs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public class PositionDisc3D : ARWidget, IRecyclable
    {
        [SerializeField] ARButton button = null;
        [SerializeField] DraggablePlane disc = null;
        [SerializeField] Transform anchor = null;
        [SerializeField] float linkWidth = 0.02f;
        LineResource line;
        bool dragBack;

        readonly NativeList<LineWithColor> lineBuffer = new NativeList<LineWithColor>();

        public event Action<PositionDisc3D, Vector3> Moved;

        protected override void Awake()
        {
            base.Awake();
            line = ResourcePool.RentDisplay<LineResource>(transform);
            line.ElementScale = linkWidth;
            line.Visible = false;

            lineBuffer.Add(new LineWithColor());
            lineBuffer.Add(new LineWithColor());
            lineBuffer.Add(new LineWithColor());

            button.Icon = ARButton.ButtonIcon.Ok;
            button.Caption = "Send!";
            button.Visible = false;
            button.Transform.parent = Transform.parent;

            disc.StartDragging += () =>
            {
                button.Visible = false;
                dragBack = false;
                line.Visible = true;
            };
            disc.EndDragging += () =>
            {
                button.Transform.SetLocalPose(disc.Transform.AsLocalPose().Multiply(BaseButtonPose));
                button.Visible = true;
            };            
            button.Clicked += () =>
            {
                dragBack = true;
                button.Visible = false;
                Moved?.Invoke(this, Transform.localPosition);
            };
        }

        static readonly Pose BaseButtonPose = new Pose(
            new Vector3(-0.6f, 0, -0.6f),
            Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(180, Vector3.up)
        );

        protected override void Update()
        {
            base.Update();

            anchor.transform.localRotation = disc.Transform.localRotation; // copy billboard
            if (button.Visible)
            {
                button.Transform.SetLocalPose(disc.Transform.AsLocalPose().Multiply(BaseButtonPose));
            }

            var discPosition = disc.Transform.localPosition;

            if (line.Visible)
            {
                Pose localToFixed = TfListener.FixedFramePose.Multiply(Transform.AsPose().Inverse());
                Pose fixedToLocal = localToFixed.Inverse();
                
                Vector3 start = localToFixed.Multiply(Vector3.zero);
                Vector3 end = localToFixed.Multiply(discPosition);
                Vector3 diff = end - start;
                
                Vector3 p0 = Vector3.zero;
                Vector3 p1 = fixedToLocal.Multiply(start + new Vector3(0, diff.y, 0));
                Vector3 p2 = fixedToLocal.Multiply(start + new Vector3(0, diff.y, diff.z));
                Vector3 p3 = discPosition;

                lineBuffer[0] = new LineWithColor(p0, p1, Color.blue.WithAlpha(0.5f));
                lineBuffer[1] = new LineWithColor(p1, p2, Color.red.WithAlpha(0.5f));
                lineBuffer[2] = new LineWithColor(p2, p3, Color.green.WithAlpha(0.5f));
                line.Set(lineBuffer);
            }

            float discDistance = discPosition.Magnitude();
            if (discDistance < 0.005f)
            {
                if (dragBack)
                {
                    disc.Transform.localPosition = Vector3.zero;
                    dragBack = false;
                    line.Visible = false;
                }

                return;
            }

            if (dragBack)
            {
                disc.Transform.localPosition = 0.9f * discPosition;
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            button.OnDialogDisabled();
            button.Transform.parent = Transform;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            lineBuffer.Dispose();
        }

        void IRecyclable.SplitForRecycle()
        {
            line.ReturnToPool();
        }
    }
}