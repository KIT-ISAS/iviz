using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;
using UnityEngine;

namespace Application.Displays
{
    public class SelectionFrame : MonoBehaviour
    {
        Transform mTransform;
        [NotNull] public Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        MeshMarkerResource[] objects;

        Vector3 size;
        float columnWidth;

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
                columnWidth = 0.01f;
                RebuildSize();
            }
        }

        /*
        float ColumnWidth
        {
            get => columnWidth;
            set
            {
                if (Mathf.Approximately(columnWidth, value))
                {
                    return;
                }
                
                for (int i = 0; i < 12; i++)
                {
                    objects[i].Transform.localScale = new Vector3(0.25f, 0.25f, 1);
                }
            }
        }
        */
        


        void CreateObjects()
        {
            objects = new MeshMarkerResource[12];
            for (int i = 0; i < 12; i++)
            {
                var resource = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cube, Transform);
                resource.Transform.SetParentLocal(Transform);
                resource.ShadowsEnabled = false;
                resource.Color = Color.cyan.WithAlpha(0.5f);
                resource.EmissiveColor = Color.cyan.WithValue(0.5f);
                objects[i] = resource;
            }
        }

        void RebuildSize()
        {
            if (objects == null)
            {
                CreateObjects();
            }

            Vector3 halfSize = size / 2;
            objects[0].Transform.position = new Vector3(halfSize.x, halfSize.y, 0);
            objects[1].Transform.position = new Vector3(halfSize.x, -halfSize.y, 0);
            objects[2].Transform.position = new Vector3(-halfSize.x, -halfSize.y, 0);
            objects[3].Transform.position = new Vector3(-halfSize.x, halfSize.y, 0);

            for (int i = 0; i < 4; i++)
            {
                objects[i].Transform.localScale = new Vector3(columnWidth, columnWidth, size.z);
            }

            objects[4].Transform.position = new Vector3(0, halfSize.y, halfSize.z);
            objects[5].Transform.position = new Vector3(0, -halfSize.y, halfSize.z);
            objects[6].Transform.position = new Vector3(0, -halfSize.y, -halfSize.z);
            objects[7].Transform.position = new Vector3(0, halfSize.y, -halfSize.z);

            for (int i = 4; i < 8; i++)
            {
                objects[i].Transform.localScale = new Vector3(size.x, columnWidth, columnWidth);
            }

            objects[8].Transform.position = new Vector3(halfSize.x, 0, halfSize.z);
            objects[9].Transform.position = new Vector3(-halfSize.x, 0, halfSize.z);
            objects[10].Transform.position = new Vector3(-halfSize.x, 0, -halfSize.z);
            objects[11].Transform.position = new Vector3(halfSize.x, 0, -halfSize.z);
            
            for (int i = 8; i < 12; i++)
            {
                objects[i].Transform.localScale = new Vector3(columnWidth, size.y, columnWidth);
            }
        }

        void Awake()
        {
            Size = new Vector3(2, 3, 1);
        }
    }
}