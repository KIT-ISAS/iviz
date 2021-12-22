using System;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class RotationDisc : ARWidget, IRecyclable
    {
        [SerializeField] LinkDisplay link = null;
        [SerializeField] PlaneDraggable disc = null;
        [SerializeField] RingDisplay ringUp = null;
        [SerializeField] RingDisplay ringDown = null;
        [SerializeField] float linkWidth = 0.2f;
        [SerializeField] float dragBackDistance = 0.5f;
        [SerializeField] LineResource lines;
        //[SerializeField] ARLineConnector connector = null;

        readonly NativeList<LineWithColor> lineBuffer = new();
        float? currentAngle;
        bool dragBack;
        
        Vector3 DiscRestPosition => new(0, 0, dragBackDistance);
        
        public event Action<float> Moved;

        protected override void Awake()
        {
            base.Awake();
            disc.EndDragging += () =>
            {
                dragBack = true;
                Moved?.Invoke(0);
                lines.Reset();
                currentAngle = null;
            };
            
            disc.StartDragging += () =>
            {
                dragBack = false;
            };
            

            link.Color = Color.cyan.WithAlpha(0.8f);
            link.EmissiveColor = Color.cyan;
            ringUp.Color = Color.cyan.WithAlpha(0.5f);
            ringUp.EmissiveColor = Color.cyan;
            ringDown.Color = Color.cyan.WithAlpha(0.5f);
            ringDown.EmissiveColor = Color.cyan;
            lines = ResourcePool.RentDisplay<LineResource>(transform);
            lines.ElementScale = 0.02f;
            lines.RenderType = LineResource.LineRenderType.AlwaysCapsule;
        }

        protected override void Update()
        {
            base.Update();;

            var discPosition = disc.Transform.localPosition;
            float discDistance = discPosition.Magnitude();
            if (Vector3.Distance(discPosition, DiscRestPosition) < 0.005f)
            {
                if (dragBack)
                {
                    disc.Transform.localPosition = DiscRestPosition;
                    ringUp.Transform.localScale = dragBackDistance * Vector3.one;
                    ringDown.Transform.localScale = dragBackDistance * Vector3.one;
                    dragBack = false;
                    lines.Reset();
                }

                return;
            }

            float angle = -Mathf.Atan2(discPosition.z, discPosition.x) * Mathf.Rad2Deg;
            link.Transform.localScale = new Vector3(discDistance, 0.002f, linkWidth);
            link.Transform.SetLocalPose(new Pose(discPosition / 2, Quaternion.AngleAxis(angle, Vector3.up)));

            float ringSize = dragBackDistance + 0.1f * (discDistance - dragBackDistance);
            ringUp.Transform.localScale = ringSize * Vector3.one;
            ringDown.Transform.localScale = ringSize * Vector3.one;

            if (dragBack)
            {
                disc.Transform.localPosition += 0.2f * (DiscRestPosition - discPosition);
                return;
            }

            float openingAngle = -90 - angle;
            if (currentAngle != null)
            {
                if (openingAngle - currentAngle.Value > 180)
                {
                    openingAngle -= 360;
                }
                else if (openingAngle - currentAngle.Value < -180)
                {
                    openingAngle += 360;
                }
            }

            currentAngle = openingAngle;

            const int ringElements = 32;
            
            lineBuffer.Clear();
            lineBuffer.EnsureCapacity(ringElements + 1);
            Vector3 a0 = 1.1f * ringSize * Vector3.forward;
            Vector3 a = a0;
            for (int i = 0; i < ringElements; i++)
            {
                float bAngle = Mathf.Deg2Rad / ringElements * openingAngle * (i + 1) + Mathf.PI / 2;
                Vector3 b = 1.1f * ringSize * new Vector3(Mathf.Cos(bAngle), 0, Mathf.Sin(bAngle));
                lineBuffer.Add(new LineWithColor(a, b));
                a = b;
            }

            lineBuffer.Add(new LineWithColor(Vector3.zero, a0));
            lines.Set(lineBuffer);

            Moved?.Invoke(openingAngle);
        }

        void IRecyclable.SplitForRecycle()
        {
            lines.ReturnToPool();
        }

        public override void Suspend()
        {
            base.Suspend();
            Moved = null;
            disc.Transform.localPosition = DiscRestPosition;
            ringUp.Transform.localScale = dragBackDistance * Vector3.one;
            ringDown.Transform.localScale = dragBackDistance * Vector3.one;
            dragBack = false;
            lines.Reset();            
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            lineBuffer.Dispose();
        }
    }
}