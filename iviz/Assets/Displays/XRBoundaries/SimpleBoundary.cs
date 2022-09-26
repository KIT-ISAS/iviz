#nullable enable

using System;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class SimpleBoundary : BaseBoundary
    {
        [SerializeField] MeshMarkerDisplay? cube;
        SelectionFrame? frame;

        MeshMarkerDisplay Cube => cube.AssertNotNull(nameof(cube));
        SelectionFrame Frame => ResourcePool.RentChecked(ref frame, Transform);

        public override Color Color
        {
            set
            {
                if (value.a == 0)
                {
                    Frame.Visible = false;
                    return;
                }

                Frame.Visible = true;
                Frame.Color = value;
                Frame.EmissiveColor = value;
            }
        }

        public override Color SecondColor
        {
            set
            {
                if (value.a == 0)
                {
                    Cube.Visible = false;
                    return;
                }

                Cube.Visible = true;
                Cube.Color = value;
                Cube.EmissiveColor = value.ScaledBy(0.7f);
            }
        }

        public float SecondaryScale
        {
            set => Frame.ColumnWidth = value;
        }

        public override Vector3 Scale
        {
            set
            {
                base.Scale = value;
                Frame.Size = value;
                Cube.Transform.localScale = value;
            }
        }

        void Awake()
        {
            Cube.Smoothness = 0;
            Cube.Metallic = 0;
            Cube.EnableShadows = true;
        }

        public override void SplitForRecycle()
        {
            base.SplitForRecycle();
            frame.ReturnToPool();
        }
    }
}