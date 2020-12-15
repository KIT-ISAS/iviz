using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public class OccupancyGridTextureResource : MarkerResourceWithColormap
    {
        [SerializeField] Texture2D texture = null;
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

            texture = new Texture2D(sizeX, sizeY, TextureFormat.R8, true)
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

            //Debug.Log(texture.GetRawTextureData<sbyte>().Length);
            //Debug.Log(texture.mipmapCount);
            //texture.GetRawTextureData<sbyte>().CopyFrom(values);
            //texture.SetPixelData(values, 0);
            
            var textureData = texture.GetRawTextureData<sbyte>();
            NativeArray<sbyte>.Copy(values, 0, textureData, 0, newCellsX * newCellsY);
            Reduce(textureData, newCellsX, newCellsY);

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

        static unsafe void Reduce(NativeArray<sbyte> array, int width, int height)
        {
            sbyte* src = (sbyte*) array.GetUnsafePtr();
            while (width > 0 && height > 0)
            {
                Reduce(src, width, height, src + width * height);
                src += width * height;
                width /= 2;
                height /= 2;
            }
        }

        static unsafe void Reduce(sbyte* src, int width, int height, sbyte* dst)
        {
            sbyte* dst0 = dst;
            for (int v = 0; v < height; v += 2)
            {
                sbyte* row0 = src + width * v;
                sbyte* row1 = row0 + width;
                for (int u = 0; u < width; u += 2, row0 += 2, row1 += 2, dst++)
                {
                    int a = row0[0];
                    int b = row0[1];
                    int c = row1[0];
                    int d = row1[1];
                    *dst = (sbyte) Fuse(a, b, c, d);
                }
            }
            
            //Debug.Log((dst - dst0) + " " + width*height/4);
        }

        static int Fuse(int a, int b, int c, int d)
        {
            int signA = ~a >> 8;
            int signB = ~b >> 8;
            int signC = ~c >> 8;
            int signD = ~d >> 8;

            int valueA = a & signA;
            int valueB = b & signB;
            int valueC = c & signC;
            int valueD = d & signD;

            int sum1 = signA + signB + signC + signD;
            int sum2 = valueA + valueB + valueC + valueD;

            int tmpA = (sum2 * 21845) >> 16;
            int tmpB = sum2 >> 2;

            return sum1 >= -2 
                ? -1 
                : sum1 == -3 
                    ? tmpA 
                    : tmpB;
        }
    }
}