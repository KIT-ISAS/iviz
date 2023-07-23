#nullable enable

using System;
using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class SimpleBoundary : BaseBoundary
    {
        [SerializeField] MeshMarkerDisplay? cube;
        SelectionFrame? frame;

        MeshMarkerDisplay Cube => cube.AssertNotNull(nameof(cube));
        SelectionFrame Frame => ResourcePool.RentChecked(ref frame, Transform);

        public override Color FrameColor
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

        public override Color InteriorColor
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
                Cube.EmissiveColor = value.ScaledBy(0.75f);
            }
        }

        public override float FrameWidth
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

        public bool EnableShadows
        {
            set
            {
                Frame.EnableShadows = value;
                Cube.EnableShadows = value;
            }
        }

        public bool UseFresnelLighting
        {
            set => Cube.OverrideMaterial(value 
                ? Resource.Materials.FancyTransparentLit.Object 
                : null);
        }


        void Awake()
        {
            Cube.Smoothness = 0.25f;
            Cube.Metallic = 0.25f;
        }

        public override void SplitForRecycle()
        {
            base.SplitForRecycle();
            frame.ReturnToPool();
        }
        
        public override void Suspend()
        {
            base.Suspend();
            Cube.OverrideMaterial(null);
        }
    }
}