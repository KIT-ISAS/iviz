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
        [SerializeField] DraggablePlane disc = null;
        [SerializeField] float linkWidth = 0.2f;
        bool dragBack;

        public event Action<SpringDisc, Vector3> Moved; 

        protected override void Awake()
        {
            base.Awake();
            
            disc.PointerUp += () =>
            {
                dragBack = true;
                Moved?.Invoke(this, Vector3.zero);
            };

            link.Color = Color.cyan.WithAlpha(0.8f);
        }

        void LateUpdate()
        {
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