using System;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class SpringDisc : ARWidget
    {
        [SerializeField] LinkDisplay link = null;
        [SerializeField] PlaneDraggable disc = null;
        [SerializeField] MeshMarkerResource outerDisc = null;
        [SerializeField] float linkWidth = 0.2f;
        bool dragBack;

        public event Action<SpringDisc, Vector3> Moved;

        public override Color MainColor
        {
            get => base.MainColor;
            set
            {
                base.MainColor = value;
                outerDisc.Color = value;
            }
        }

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
                dragBack = false;
            };

            link.Color = Color.cyan.WithAlpha(0.8f);
        }

        protected override void Update()
        {
            base.Update();;

            var discPosition = disc.Transform.localPosition;
            float discDistance = discPosition.Magnitude();
            if (discDistance < 0.005f)
            {
                if (dragBack)
                {
                    disc.Transform.localPosition = Vector3.zero;
                    dragBack = false;
                }

                return;
            }

            float angle = -Mathf.Atan2(discPosition.z, discPosition.x) * Mathf.Rad2Deg;
            link.Transform.localScale = new Vector3(discDistance, 0.002f, linkWidth);
            link.Transform.SetLocalPose(new Pose(discPosition / 2, Quaternion.AngleAxis(angle, Vector3.up)));

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
    }
}