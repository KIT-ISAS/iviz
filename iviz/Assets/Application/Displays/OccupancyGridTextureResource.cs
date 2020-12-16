using System;
using BigGustave;
using Iviz.Controllers;
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

        uint? previousHash;

        protected override void Awake()
        {
            material = Resource.Materials.OccupancyGridTexture.Instantiate();
            MeshRenderer.sharedMaterial = material;

            base.Awake();
            Colormap = Resource.ColormapId.hsv;
        }

        /*
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
        */

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

            //texture = new Texture2D(sizeX, sizeY, TextureFormat.R8, true)
            texture = new Texture2D(sizeX, sizeY, TextureFormat.R8, true)
            {
                name = "OccupancyGrid Texture",
                filterMode = FilterMode.Point,
            };
            material.mainTexture = texture;
        }

        public void Set([NotNull] sbyte[] values, float cellSize, int numCellsX, int numCellsY,
            OccupancyGridResource.Rect? inBounds = null)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            var bounds = inBounds ?? new OccupancyGridResource.Rect(0, numCellsX, 0, numCellsY);
            int segmentWidth = bounds.Width;
            int segmentHeight = bounds.Height;

            EnsureSize(segmentWidth, segmentHeight);

            float totalWidth = segmentWidth * cellSize;
            float totalHeight = segmentHeight * cellSize;

            Transform mTransform = transform;
            //Vector3 rosCenter = new Vector3(width / 2 - cellSize / 2, height / 2 - cellSize / 2, 0);
            Vector3 rosCenter =
                new Vector3(bounds.XMax + bounds.XMin - 1, bounds.YMax + bounds.YMin - 1, 0) * (cellSize / 2);
            mTransform.localPosition = rosCenter.Ros2Unity();
            mTransform.localRotation = Quaternion.Euler(0, 90, 0);
            mTransform.localScale = new Vector3(totalHeight, totalWidth, 1).Ros2Unity().Abs() * 0.1f;

            var textureData = texture.GetRawTextureData<sbyte>();

            (uint hash, int numValidValues) = Copy(values, bounds, numCellsX, textureData);
            if (previousHash == null)
            {
                previousHash = hash;
            }
            else if (hash == previousHash)
            {
                return;
            }

            previousHash = hash;
            if (numValidValues == 0)
            {
                Visible = false;
                return;
            }

            Visible = true;
            Reduce(textureData, segmentWidth, segmentHeight);
            texture.Apply(false);
        }

        static unsafe (uint hash, int numValidValues) Copy(sbyte[] src, OccupancyGridResource.Rect bounds, int pitch,
            NativeArray<sbyte> dest)
        {
            Crc32Calculator crc32 = Crc32Calculator.Instance;
            uint hash = Crc32Calculator.DefaultSeed;
            sbyte* dstPtr = (sbyte*) dest.GetUnsafePtr();
            //int dstPtr = 0;
            long numValidValues = 0;


            /*
            for (int v = bounds.YMin; v < bounds.YMax; v++)
            {
                int srcPtr = bounds.YMin * pitch + bounds.XMin;
                for (int u = bounds.XMin; u < bounds.XMax; u++, srcPtr++, dstPtr++)
                {
                    hash = crc32.Update(hash, (byte) src[srcPtr]);
                    dest[dstPtr] = src[srcPtr];
                    numValidValues += (src[srcPtr] >> 8) + 1;
                }
            }
            */


            fixed (sbyte* srcPtr0 = src)
            {
                for (int v = bounds.YMin; v < bounds.YMax; v++)
                {
                    sbyte* srcPtr = srcPtr0 + v * pitch + bounds.XMin;
                    for (int u = bounds.XMin; u < bounds.XMax; u++, srcPtr++, dstPtr++)
                    {
                        *dstPtr = *srcPtr;
                        hash = crc32.Update(hash, (byte) *srcPtr);
                        numValidValues += (*srcPtr >> 8) + 1;
                    }
                }
            }


            return (hash, (int) numValidValues);
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
            while (width > 1 && height > 1)
            {
                Reduce(src, width, height, src + width * height);
                src += width * height;
                width /= 2;
                height /= 2;
            }


            /*
            int src = 0;
            while (width > 1 && height > 1)
            {
                Debug.Log(width + "x" + height + " offset " + src);
                Reduce(array, src, width, height, src + width * height);
                src += width * height;
                width /= 2;
                height /= 2;
            }
            */

            //Debug.Log("Used: " + (src - (sbyte*)array.GetUnsafePtr()));
        }

        static unsafe void Reduce(sbyte* src, int width, int height, sbyte* dst)
        {
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

        static void Reduce(NativeArray<sbyte> array, int src, int width, int height, int dst)
        {
            for (int v = 0; v < height; v += 2)
            {
                int row0 = src + width * v;
                int row1 = row0 + width;
                for (int u = 0; u < width; u += 2, row0 += 2, row1 += 2, dst++)
                {
                    int a = array[row0 + 0];
                    int b = array[row0 + 1];
                    int c = array[row1 + 0];
                    int d = array[row1 + 1];
                    array[dst] = (sbyte) Fuse(a, b, c, d);
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

            int sum1 = signA + signB + signC + signD;
            if (sum1 >= -1)
            {
                return -1;
            }

            int valueA = a & signA;
            int valueB = b & signB;
            int valueC = c & signC;
            int valueD = d & signD;

            int sum2 = valueA + valueB + valueC + valueD;
            switch (sum1)
            {
                case -2:
                    return sum2 / 2;
                case -3:
                    return (sum2 * 21845) >> 16;
                default:
                    return sum2 / 4;
            }
        }
    }
}