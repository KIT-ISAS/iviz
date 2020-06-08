using UnityEngine;
using System.Collections;
using Iviz.Resources;
using Iviz.App;

namespace Iviz.Displays
{
    public sealed class AxisFrameResource : MarkerResource, IRecyclable
    {
        public override string Name => "AxisFrame";

        static readonly string[] names = { "Axis-X", "Axis-Y", "Axis-Z" };

        readonly MeshMarkerResource[] axisObjects = new MeshMarkerResource[3];

        float axisLength;
        public float AxisLength
        {
            get => axisLength;
            set
            {
                axisLength = value;
                UpdateFrameMesh(axisLength, axisLength / 20);
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

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < 3; i++)
            {
                axisObjects[i] = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Markers.Cube, transform);
                axisObjects[i].gameObject.name = names[i];
                axisObjects[i].ColliderEnabled = false;
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
            axisObjects[1].transform.localPosition = -0.5f * newFrameAxisLength * Vector3.forward;
            axisObjects[2].transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisLength, newFrameAxisWidth);
            axisObjects[2].transform.localPosition = 0.5f * newFrameAxisLength * Vector3.up;

            Collider.center = 0.5f * (newFrameAxisLength - newFrameAxisWidth / 2) * new Vector3(-1, 1, -1);
            Collider.size = (newFrameAxisLength + newFrameAxisWidth / 2) * Vector3.one;
        }

        public void Recycle()
        {
            ResourcePool.Dispose(Resource.Markers.Cube, axisObjects[0].gameObject);
            ResourcePool.Dispose(Resource.Markers.Cube, axisObjects[1].gameObject);
            ResourcePool.Dispose(Resource.Markers.Cube, axisObjects[2].gameObject);
        }

        public override void Stop()
        {
            base.Stop();

            AxisLength = 0.25f;
            ColorX = Color.red;
            ColorY = Color.green;
            ColorZ = Color.blue;
        }
    }
}