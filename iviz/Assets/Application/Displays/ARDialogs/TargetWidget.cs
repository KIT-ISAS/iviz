using System;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.XRDialogs;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public sealed class TargetWidget : ARWidget, IRecyclable
    {
        public enum ModeType
        {
            Square,
            Circle,
        }

        [SerializeField] PolyGlowDisplay poly = null;
        [SerializeField] PlaneDraggable disc = null;
        [SerializeField] PlaneDraggable corner = null;
        [SerializeField] XRButton okButton = null;
        [SerializeField] XRButton cancelButton = null;
        [SerializeField] Transform buttonPivotTransform = null;
        [SerializeField] Transform pivotTransform = null;
        LineDisplay lines;

        Vector2 targetScale;
        ModeType mode;
        bool scaling;

        public event Action<Vector2, Vector3> Moved;
        public event Action Cancelled;

        Vector2 TargetScale
        {
            get => targetScale;
            set
            {
                targetScale = value;
                var scaleVector = new Vector3(value.x, 0.5f * value.x, value.y);
                switch (Mode)
                {
                    case ModeType.Circle:
                        pivotTransform.localScale = 0.5f * scaleVector;
                        break;
                    case ModeType.Square:
                        pivotTransform.localScale = 1.4142135f / 2 * scaleVector;
                        break;
                }
            }
        }

        public ModeType Mode
        {
            get => mode;
            set
            {
                mode = value;
                switch (value)
                {
                    case ModeType.Square:
                        SetLines(4);
                        poly.SetToSquare();
                        var rotation = Quaternion.AngleAxis(45, Vector3.up);
                        lines.Transform.localRotation = rotation;
                        poly.Transform.localRotation = rotation;
                        break;
                    case ModeType.Circle:
                        SetLines(40);
                        poly.SetToCircle();
                        lines.Transform.localRotation = Quaternion.identity;
                        poly.Transform.localRotation = Quaternion.identity;
                        break;
                }

                TargetScale = TargetScale;
            }
        }
        
        string mainButtonCaption = "Send!";
        public string MainButtonCaption
        {
            get => mainButtonCaption;
            set
            {
                mainButtonCaption = value;
                okButton.Caption = value;
            }
        }        


        protected override void Awake()
        {
            base.Awake();

            //Scale = 10;
            
            lines = ResourcePool.RentDisplay<LineDisplay>(pivotTransform);
            poly.EmissiveColor = poly.Color.WithAlpha(1);
            lines.ElementScale = 0.05f;
            disc.TargetTransform = Transform;

            TargetScale = Vector2.one;
            Mode = ModeType.Square;

            corner.StartDragging += () => scaling = true;
            corner.EndDragging += () => scaling = false;

            okButton.Icon = XRIcon.Ok;
            MainButtonCaption = MainButtonCaption;

            cancelButton.Caption = "Cancel";
            cancelButton.Icon = XRIcon.Cross;

            okButton.Clicked += () =>
            {
                Debug.Log($"{this}: Sending scale");
                //float totalScale = transform.parent.lossyScale.x;
                //Debug.Log(TargetScale / Scale / totalScale);
                Moved?.Invoke(TargetScale * Scale, Transform.localPosition);
            };
            cancelButton.Clicked += () => Cancelled?.Invoke();
        }

        protected override void Update()
        {
            base.Update();
            
            var camPosition = Settings.MainCameraTransform.position;
            buttonPivotTransform.LookAt(2 * buttonPivotTransform.position - camPosition, Vector3.up);

            if (scaling)
            {
                (float x, _, float z) = corner.Transform.localPosition;
                TargetScale = 2 * new Vector2(x, z).Abs();
            }
        }

        void SetLines(int numVertices)
        {
            var segments = new LineWithColor[numVertices];
            for (int i = 0; i < numVertices; i++)
            {
                float a0 = Mathf.PI * 2 / numVertices * i;
                float a1 = Mathf.PI * 2 / numVertices * (i + 1);

                Vector3 dirA0 = new Vector3(Mathf.Cos(a0), 0, Mathf.Sin(a0));
                Vector3 dirA1 = new Vector3(Mathf.Cos(a1), 0, Mathf.Sin(a1));

                segments[i] = new LineWithColor(dirA0, dirA1);
            }

            lines.Set(segments);
        }

        void IRecyclable.SplitForRecycle()
        {
            lines.ReturnToPool();
        }

        public override void Suspend()
        {
            base.Suspend();
            TargetScale = Vector2.one;
            corner.Transform.localPosition = new Vector3(0.5f, 0, 0.5f);
            Moved = null;
            Cancelled = null;
            scaling = false;
        }
    }
}