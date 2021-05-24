using Iviz.Core;
using UnityEngine;
using Iviz.Resources;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class AxisFrameResource : MarkerResource, IRecyclable, ISupportsAROcclusion, ISupportsTint
    {
        static readonly string[] Names = { "Axis-X", "Axis-Y", "Axis-Z" };

        readonly MeshMarkerResource[] axisObjects = new MeshMarkerResource[3];

        float axisLength;
        public float AxisLength
        {
            get => axisLength;
            set
            {
                axisLength = value;
                UpdateFrameMesh(axisLength, axisLength / 10);
            }
        }

        public Color ColorX
        {
            get => axisObjects[0].Color;
            set => axisObjects[0].Color = value;
        }

        public Color ColorY
        {
            get => axisObjects[1].Color;
            set => axisObjects[1].Color = value;
        }

        public Color ColorZ
        {
            get => axisObjects[2].Color;
            set => axisObjects[2].Color = value;
        }

        public override int Layer
        {
            get => base.Layer;
            set
            {
                base.Layer = value;
                axisObjects[0].Layer = value;
                axisObjects[1].Layer = value;
                axisObjects[2].Layer = value;
            }
        }

        bool occlusionOnly;
        public bool OcclusionOnly
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                axisObjects[0].OcclusionOnly = value;
                axisObjects[1].OcclusionOnly = value;
                axisObjects[2].OcclusionOnly = value;
            }
        }

        Color tint;
        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                axisObjects[0].Tint = value;
                axisObjects[1].Tint = value;
                axisObjects[2].Tint = value;
            }
        }

        public bool CastsShadows
        {
            get => axisObjects[0].CastsShadows;
            set
            {
                axisObjects[0].CastsShadows = value;
                axisObjects[1].CastsShadows = value;
                axisObjects[2].CastsShadows = value;
            }
        }
        
        public float Emissive
        {
            set
            {
                axisObjects[0].EmissiveColor = value * axisObjects[0].EmissiveColor;
                axisObjects[1].EmissiveColor = value * axisObjects[1].EmissiveColor;
                axisObjects[2].EmissiveColor =  value * axisObjects[2].EmissiveColor;
            }
        }        

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < 3; i++)
            {
                axisObjects[i] = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, transform);
                axisObjects[i].gameObject.name = Names[i];
                axisObjects[i].ColliderEnabled = false;
                axisObjects[i].Layer = Layer;
            }

            AxisLength = 0.25f;

            ColorX = Color.red;
            ColorY = Color.green;
            ColorZ = Color.blue;
        }

        void UpdateFrameMesh(float newFrameAxisLength, float newFrameAxisWidth)
        {
            axisObjects[0].transform.localScale = new Vector3(newFrameAxisLength, newFrameAxisWidth, newFrameAxisWidth);
            axisObjects[0].transform.localPosition = -0.5f * newFrameAxisLength * Vector3.right;
            axisObjects[1].transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisWidth, newFrameAxisLength);
            axisObjects[1].transform.localPosition = newFrameAxisLength * new Vector3(0, 0.001f, -0.5f);
            axisObjects[2].transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisLength, newFrameAxisWidth);
            axisObjects[2].transform.localPosition = newFrameAxisLength * new Vector3(0.001f, 0.5f, 0.001f);

            BoxCollider.center = 0.5f * (newFrameAxisLength - newFrameAxisWidth / 2) * new Vector3(-1, 1, -1);
            BoxCollider.size = (newFrameAxisLength + newFrameAxisWidth / 2) * Vector3.one;
        }

        public void SplitForRecycle()
        {
            axisObjects[0].ReturnToPool(Resource.Displays.Cube);
            axisObjects[1].ReturnToPool(Resource.Displays.Cube);
            axisObjects[2].ReturnToPool(Resource.Displays.Cube);
        }

        public override void Suspend()
        {
            base.Suspend();

            AxisLength = 0.25f;
            ColorX = Color.red;
            ColorY = Color.green;
            ColorZ = Color.blue;
            OcclusionOnly = false;
            Tint = Color.white;
            Emissive = 0;
        }
    }
}