using System;
using Iviz.App.ARDialogs;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XRDialogs;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class PositionDisc : ARWidget, IRecyclable
    {
        [SerializeField] XRButton button = null;
        [SerializeField] PlaneDraggable disc = null;
        [SerializeField] Transform anchor = null;
        [SerializeField] float linkWidth = 0.02f;
        LineResource line;
        Tooltip tooltipX;
        Tooltip tooltipY;
        
        bool dragBack;

        readonly NativeList<LineWithColor> lineBuffer = new NativeList<LineWithColor>();

        public event Action<PositionDisc, Vector3> Moved;
        
        string mainButtonCaption = "Send!";
        public string MainButtonCaption
        {
            get => mainButtonCaption;
            set
            {
                mainButtonCaption = value;
                button.Caption = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            line = ResourcePool.RentDisplay<LineResource>(Transform);
            line.ElementScale = linkWidth;
            line.Visible = false;

            lineBuffer.Add(new LineWithColor());
            lineBuffer.Add(new LineWithColor());

            button.Icon = XRButton.ButtonIcon.Ok;
            button.Caption = "Send!";
            button.Visible = false;
            button.Transform.SetParentLocal(Transform);
            MainButtonCaption = mainButtonCaption;

            tooltipX = ResourcePool.RentDisplay<Tooltip>(Transform);
            tooltipY = ResourcePool.RentDisplay<Tooltip>(Transform);
            tooltipX.Color = Color.red.WithAlpha(0.5f);
            tooltipY.Color = Color.green.WithAlpha(0.5f);
            tooltipX.Visible = false;
            tooltipY.Visible = false;


            disc.StartDragging += () =>
            {
                button.Visible = false;
                dragBack = false;
                line.Visible = true;
                tooltipX.Visible = true;
                tooltipY.Visible = true;
            };
            disc.EndDragging += () =>
            {
                button.Transform.localPosition = disc.Transform.localPosition + Vector3.up;
                button.Visible = true;
            };            
            button.Clicked += () =>
            {
                dragBack = true;
                button.Visible = false;
                tooltipX.Visible = false;
                tooltipY.Visible = false;
                Moved?.Invoke(this, disc.Transform.localPosition);
            };
        }

        protected override void Update()
        {
            base.Update();

            anchor.transform.localRotation = disc.Transform.localRotation; // copy billboard
            if (button.Visible)
            {
                var camPosition = Settings.MainCameraTransform.position;
                button.Transform.LookAt(2 * button.Transform.position - camPosition, Vector3.up);
            }

            var discPosition = disc.Transform.localPosition;
            if (line.Visible)
            {
                Vector3 start = Vector3.zero;
                Vector3 diff = discPosition;
                
                Vector3 p0 = start;
                Vector3 p1 = start + new Vector3(0, 0, diff.z);
                Vector3 p2 = discPosition;

                lineBuffer[0] = new LineWithColor(p0, p1, Color.red.WithAlpha(0.5f));
                lineBuffer[1] = new LineWithColor(p1, p2, Color.green.WithAlpha(0.5f));
                
                tooltipX.Transform.localPosition = ((p1 + p0) / 2).WithY(0.5f);
                tooltipY.Transform.localPosition = ((p2 + p1) / 2).WithY(0.5f);
                
                tooltipX.Caption = "X: " + ((p1 - p0) / Scale).WithY(0).magnitude.ToString("0.###") + " m";
                tooltipY.Caption = "Y: " + ((p2 - p1) / Scale).WithY(0).magnitude.ToString("0.###") + " m";
                
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
                    tooltipX.Visible = false;
                    tooltipY.Visible = false;
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
            button.Transform.parent = Transform;
            disc.Transform.localPosition = Vector3.zero;
            dragBack = false;
            line.Visible = false;
            Moved = null;
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