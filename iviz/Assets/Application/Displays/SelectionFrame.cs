using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Application.Displays
{
    public class SelectionFrame : MonoBehaviour, IDisplay, ISupportsTint, ISupportsAROcclusion, ISupportsPbr,
        IRecyclable
    {
        Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        MeshMarkerResource[] objects;

        Vector3 size = Vector3.one;
        float columnWidth = 0.01f;

        public Vector3 Size
        {
            get => size;
            set
            {
                if (Mathf.Approximately(value.x, size.x)
                    && Mathf.Approximately(value.y, size.y)
                    && Mathf.Approximately(value.z, size.z))
                {
                    return;
                }

                size = value;
                RebuildSize();
            }
        }

        public Color EmissiveColor
        {
            get => objects[0].EmissiveColor;
            set
            {
                foreach (var resource in objects)
                {
                    resource.EmissiveColor = value;
                }
            }
        }

        public Color Color
        {
            get => objects[0].Color;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Color = value;
                }
            }
        }

        public Color Tint
        {
            get => objects[0].Tint;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Tint = value;
                }
            }
        }

        public float Smoothness
        {
            get => objects[0].Smoothness;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Smoothness = value;
                }
            }
        }

        public float Metallic
        {
            get => objects[0].Metallic;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Metallic = value;
                }
            }
        }

        public bool ShadowsEnabled
        {
            get => objects[0].ShadowsEnabled;
            set
            {
                foreach (var resource in objects)
                {
                    resource.ShadowsEnabled = value;
                }
            }
        }

        public bool OcclusionOnly
        {
            get => objects[0].OcclusionOnly;
            set
            {
                foreach (var resource in objects)
                {
                    resource.OcclusionOnly = value;
                }
            }
        }

        void CreateObjects()
        {
            objects = new MeshMarkerResource[12];
            foreach (ref var resource in objects.Ref())
            {
                resource = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, Transform);
            }
            
            ShadowsEnabled = false;
        }

        void RebuildSize()
        {
            if (objects == null)
            {
                CreateObjects();
            }

            var (halfSizeX, halfSizeY, halfSizeZ) = size / 2;
            objects[0].Transform.position = new Vector3(halfSizeX, halfSizeY, 0);
            objects[1].Transform.position = new Vector3(halfSizeX, -halfSizeY, 0);
            objects[2].Transform.position = new Vector3(-halfSizeX, -halfSizeY, 0);
            objects[3].Transform.position = new Vector3(-halfSizeX, halfSizeY, 0);

            for (int i = 0; i < 4; i++)
            {
                objects[i].Transform.localScale = new Vector3(columnWidth, columnWidth, size.z);
            }

            objects[4].Transform.position = new Vector3(0, halfSizeY, halfSizeZ);
            objects[5].Transform.position = new Vector3(0, -halfSizeY, halfSizeZ);
            objects[6].Transform.position = new Vector3(0, -halfSizeY, -halfSizeZ);
            objects[7].Transform.position = new Vector3(0, halfSizeY, -halfSizeZ);

            for (int i = 4; i < 8; i++)
            {
                objects[i].Transform.localScale = new Vector3(size.x, columnWidth, columnWidth);
            }

            objects[8].Transform.position = new Vector3(halfSizeX, 0, halfSizeZ);
            objects[9].Transform.position = new Vector3(-halfSizeX, 0, halfSizeZ);
            objects[10].Transform.position = new Vector3(-halfSizeX, 0, -halfSizeZ);
            objects[11].Transform.position = new Vector3(halfSizeX, 0, -halfSizeZ);

            for (int i = 8; i < 12; i++)
            {
                objects[i].Transform.localScale = new Vector3(columnWidth, size.y, columnWidth);
            }
        }

        public int Layer
        {
            get => objects[0].Layer;
            set
            {
                foreach (var resource in objects)
                {
                    resource.Layer = value;
                }
            }
        }

        public void Suspend()
        {
            OcclusionOnly = false;
            ShadowsEnabled = false;
            EmissiveColor = Color.black;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        Bounds? IDisplay.Bounds => Bounds;

        public Bounds Bounds => new Bounds(Vector3.zero, size);

        [NotNull]
        public string Name
        {
            get => name;
            set => name = value;
        }

        void Awake()
        {
            RebuildSize();
        }

        public void SplitForRecycle()
        {
            foreach (var resource in objects)
            {
                ResourcePool.Return(Resource.Displays.Cube, resource.gameObject);
            }
        }
    }
}