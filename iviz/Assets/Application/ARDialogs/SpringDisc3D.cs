using System;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SpringDisc3D : ARWidget, IRecyclable
    {
        [SerializeField] DraggablePlane disc = null;
        [SerializeField] Transform anchor = null;
        [SerializeField] float linkWidth = 0.2f;
        LineResource line;
        bool dragBack;
        
        readonly NativeList<LineWithColor> lineBuffer = new NativeList<LineWithColor>();

        public event Action<SpringDisc3D, Vector3> Moved;

        protected override void Awake()
        {
            base.Awake();
            disc.EndDragging += () =>
            {
                dragBack = true;
                Moved?.Invoke(this, Vector3.zero);
            };

            disc.StartDragging += () =>
            {
                line.Visible = true;
                dragBack = false;
            };

            line = ResourcePool.RentDisplay<LineResource>(transform);
            line.Tint = Color.cyan.WithAlpha(0.8f);
            line.ElementScale = linkWidth / 2;
            line.Visible = false;
            
            lineBuffer.Add(new LineWithColor());
        }

        protected override void Update()
        {
            base.Update();;
            
            anchor.transform.localRotation = disc.Transform.localRotation; // copy billboard
            
            var discPosition = disc.Transform.localPosition;
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

            lineBuffer[0] = new LineWithColor(
                Vector3.zero, Color.white.WithAlpha(0), 
                disc.Transform.localPosition, Color.white);
            line.Set(lineBuffer);

            if (dragBack)
            {
                disc.Transform.localPosition = 0.9f * discPosition;
            }
            else
            {
                Moved?.Invoke(this, discPosition);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            Moved = null;
            disc.Transform.localPosition = Vector3.zero;
            dragBack = false;
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