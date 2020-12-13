using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class OccupancyGridTextureResource : MarkerResourceWithColormap
    {
        [SerializeField] Texture2D texture;
        Material material;
        
        [CanBeNull] MeshRenderer meshRenderer;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer != null ? meshRenderer : meshRenderer = GetComponent<MeshRenderer>();        

        protected override void Awake()
        {
            material = Resource.Materials.OccupancyGridTexture.Instantiate();
            MeshRenderer.sharedMaterial = material;
            
            base.Awake();
            Colormap = Resource.ColormapId.hsv;
        }

        void Start()
        {
            const int n = 256;
            sbyte[] values = new sbyte[n * n];
            int i = 0;
            for (int v = 0; v < n; v++)
            {
                for (int u = 0; u < n; u++, i++)
                {
                    float dist = (new Vector2(u, v) - new Vector2(n / 2f, n / 2f)).magnitude;
                    if (dist > n/2f)
                    {
                        values[i] = -1;
                    }
                    else if (i % 3 == 0)
                    {
                        values[i] = -1;
                    }
                    else
                    {
                        values[i] = (sbyte)(dist / (n/2f) * 127);                        
                    }
                }
            }

            Set(n, n, 10, 10, values);
        }

        void EnsureSize(int sizeX, int sizeY)
        {
            if (texture != null)
            {
                if (texture.width == sizeX && texture.height == sizeY)
                {
                    return;
                }

                Destroy(texture);
            }

            texture = new Texture2D(sizeX, sizeY, TextureFormat.R8, false)
            {
                name = "OccupancyGrid Texture",
                filterMode = FilterMode.Point,
                
            };
            material.mainTexture = texture;
        }

        public void Set(int newCellsX, int newCellsY, float width, float height, [NotNull] sbyte[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            EnsureSize(newCellsX, newCellsY);

            Transform mTransform = transform;
            mTransform.localScale = new Vector3(width, height, 1).Ros2Unity().Abs() * 0.1f;
            mTransform.localPosition = new Vector3(-width / 2, -height / 2, 0).Ros2Unity();

            texture.GetRawTextureData<sbyte>().CopyFrom(values);
            texture.Apply(false);
        }
        
        void OnDestroy()
        {
            if (texture != null)
            {
                Destroy(texture);
            }

            if (material != null)
            {
                Destroy(material);
            }
        }        
        
        protected override void Rebuild()
        {
            // not needed
        }        
        
        protected override void UpdateProperties()
        {
            MeshRenderer.SetPropertyBlock(Properties);
        }        
    }
}