﻿using UnityEngine;
using Iviz.Resources;

namespace Iviz.Displays
{
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
        public bool OcclusionOnlyActive
        {
            get => occlusionOnly;
            set
            {
                occlusionOnly = value;
                axisObjects[0].OcclusionOnlyActive = value;
                axisObjects[1].OcclusionOnlyActive = value;
                axisObjects[2].OcclusionOnlyActive = value;
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

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < 3; i++)
            {
                axisObjects[i] = ResourcePool.GetOrCreate<MeshMarkerResource>(Resource.Displays.Cube, transform);
                axisObjects[i].gameObject.name = Names[i];
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
            axisObjects[1].transform.localPosition = newFrameAxisLength * new Vector3(0, 0.001f, -0.5f);
            axisObjects[2].transform.localScale = new Vector3(newFrameAxisWidth, newFrameAxisLength, newFrameAxisWidth);
            axisObjects[2].transform.localPosition = newFrameAxisLength * new Vector3(0.001f, 0.5f, 0.001f);

            Collider.center = 0.5f * (newFrameAxisLength - newFrameAxisWidth / 2) * new Vector3(-1, 1, -1);
            Collider.size = (newFrameAxisLength + newFrameAxisWidth / 2) * Vector3.one;
        }

        public void SplitForRecycle()
        {
            axisObjects[0].Suspend();
            axisObjects[1].Suspend();
            axisObjects[2].Suspend();
            ResourcePool.Dispose(Resource.Displays.Cube, axisObjects[0].gameObject);
            ResourcePool.Dispose(Resource.Displays.Cube, axisObjects[1].gameObject);
            ResourcePool.Dispose(Resource.Displays.Cube, axisObjects[2].gameObject);
        }

        public override void Suspend()
        {
            base.Suspend();

            AxisLength = 0.25f;
            ColorX = Color.red;
            ColorY = Color.green;
            ColorZ = Color.blue;
            OcclusionOnlyActive = false;
        }
    }
}