using System;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class OccupancyGridTextureResource : MarkerResourceWithColormap
    {
        [CanBeNull] static Texture2D atlasLarge;

        static AppAssetHolder AssetHolder =>
            UnityEngine.Resources.Load<GameObject>("App Asset Holder").GetComponent<AppAssetHolder>();
        
        [NotNull]
        static Texture2D AtlasLarge => (atlasLarge != null)
            ? atlasLarge
            : atlasLarge = AssetHolder.AtlasLarge;

        [CanBeNull] static Texture2D atlasLargeFlipped;

        [NotNull]
        static Texture2D AtlasLargeFlipped => (atlasLargeFlipped != null)
            ? atlasLargeFlipped
            : atlasLargeFlipped = AssetHolder.AtlasLargeFlip;

        static readonly int AtlasTex = Shader.PropertyToID("_AtlasTex");

        [SerializeField] Texture2D texture = null;
        Material material;
        [NotNull] sbyte[] buffer = Array.Empty<sbyte>();
        uint? previousHash;

        [SerializeField] MeshRenderer meshRenderer = null;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer;

        bool IsProcessing { get; set; }

        public int NumValidValues { get; private set; }

        public override Vector2 IntensityBounds
        {
            get => default;
            set { }
        }

        bool newFlipMinMax;

        public override bool FlipMinMax
        {
            get => newFlipMinMax;
            set
            {
                newFlipMinMax = value;
                material.SetTexture(AtlasTex, value ? AtlasLargeFlipped : AtlasLarge);
            }
        }

        protected override void Awake()
        {
            material = Resource.Materials.OccupancyGridTexture.Instantiate();

            MeshRenderer.sharedMaterial = material;

            base.Awake();
            Colormap = ColormapId.hsv;
            FlipMinMax = false;
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
                wrapMode = TextureWrapMode.Clamp,
            };
            material.mainTexture = texture;
        }

        public void Set([NotNull] sbyte[] values, float cellSize, int numCellsX, int numCellsY,
            OccupancyGridResource.Rect? inBounds, Pose pose)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length < numCellsX * numCellsY)
            {
                throw new ArgumentException("Values argument is too small", nameof(values));
            }

            IsProcessing = true;

            var bounds = inBounds ?? new OccupancyGridResource.Rect(0, numCellsX, 0, numCellsY);
            int segmentWidth = bounds.Width;
            int segmentHeight = bounds.Height;

            float totalWidth = segmentWidth * cellSize;
            float totalHeight = segmentHeight * cellSize;


            int totalTextureSize = CalculateAllMipmapsSize(segmentWidth, segmentHeight);
            if (buffer.Length != totalTextureSize)
            {
                buffer = new sbyte[totalTextureSize];
            }

            (uint hash, int numValidValues) = Process(values, bounds, numCellsX, buffer);
            NumValidValues = numValidValues;

            if (previousHash == null)
            {
                previousHash = hash;
            }
            else if (hash == previousHash)
            {
                IsProcessing = false;
                return;
            }

            previousHash = hash;
            if (numValidValues == 0)
            {
                GameThread.PostImmediate(() =>
                {
                    MeshRenderer.enabled = false;
                    IsProcessing = false;
                });

                return;
            }

            CreateMipmaps(buffer, segmentWidth, segmentHeight);

            GameThread.PostImmediate(() =>
            {
                MeshRenderer.enabled = true;

                Transform mTransform = transform;
                //Vector3 rosCenter = new Vector3(width / 2 - cellSize / 2, height / 2 - cellSize / 2, 0);
                Vector3 rosCenter =
                    new Vector3(bounds.XMax + bounds.XMin - 1, bounds.YMax + bounds.YMin - 1, 0) * (cellSize / 2);
                rosCenter.z += 0.001f;

                Pose offset = new Pose(rosCenter.Ros2Unity(), Quaternion.Euler(0, 90, 0));
                Pose newPose = pose.Multiply(offset);
                mTransform.SetLocalPose(newPose);
                mTransform.localScale = new Vector3(totalHeight, totalWidth, 1).Ros2Unity().Abs() * 0.1f;

                EnsureSize(segmentWidth, segmentHeight);
                var array = texture.GetRawTextureData<sbyte>();
                //Debug.Log("Native array: " + array.Length);

                array.CopyFrom(buffer);
                texture.Apply(false);
                IsProcessing = false;
            });
        }

        static unsafe (uint hash, int numValidValues)
            Process(sbyte[] src, OccupancyGridResource.Rect bounds, int pitch, sbyte[] dest)
        {
            Crc32Calculator crc32 = Crc32Calculator.Instance;
            uint hash = Crc32Calculator.DefaultSeed;
            long numValidValues = 0;

            fixed (sbyte* dstPtr0 = dest, srcPtr0 = src)
            {
                sbyte* dstPtr = dstPtr0;
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

        public override void Suspend()
        {
            base.Suspend();
            previousHash = null;
            MeshRenderer.enabled = true;
        }

        static unsafe void CreateMipmaps([NotNull] sbyte[] array, int width, int height)
        {
            fixed (sbyte* srcPtr = array)
            {
                sbyte* maxPtr = srcPtr + array.Length;
                sbyte* srcMipmap = srcPtr;
                while (width > 1 && height > 1)
                {
                    sbyte* dstPtr = srcMipmap + width * height;
                    if (dstPtr > maxPtr)
                    {
                        throw new InvalidOperationException("Possible Buffer Overflow!");
                    }

                    Reduce(srcMipmap, width, height, dstPtr);
                    srcMipmap += width * height;
                    width /= 2;
                    height /= 2;
                }
                
                //Debug.Log("Mipmap used " + (srcMipmap - srcPtr) + " expected: " + array.Length);
            }
        }

        static int CalculateAllMipmapsSize(int width, int height)
        {
            int size = 0;
            while (width != 1 || height != 1)
            {
                //Debug.Log(width + " " + height + " -> " + width * height);
                size += width * height;
                width = Math.Max(width / 2, 1);
                height = Math.Max(height / 2, 1);
            }

            //Debug.Log("Total: " + size);
            return size + 1;
        }


        static unsafe void Reduce(sbyte* src, int width, int height, sbyte* dst)
        {
            if (width < 2 || height < 2)
            {
                throw new InvalidOperationException("NYI!");
            }
            
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
        }

        static int Fuse(int a, int b, int c, int d)
        {
            int4 abcd = new int4(a, b, c, d);
            int4 sign = ~abcd >> 8;
            int numValid = -(sign.x + sign.y + sign.z + sign.w);
            if (numValid <= 1)
            {
                return -1;
            }

            int4 value = abcd & sign;
            int sum = value.x + value.y + value.z + value.w;

            /*
            int signA = ~a >> 8; // a >= 0 ? -1 : 0
            int signB = ~b >> 8; // b >= 0 ? -1 : 0
            int signC = ~c >> 8; // c >= 0 ? -1 : 0
            int signD = ~d >> 8; // d >= 0 ? -1 : 0

            int numValid = -(signA + signB + signC + signD);
            if (numValid <= 1)
            {
                return -1;
            }

            int valueA = a & signA; // a >= 0 ? a : 0
            int valueB = b & signB; // b >= 0 ? b : 0
            int valueC = c & signC; // c >= 0 ? c : 0
            int valueD = d & signD; // d >= 0 ? d : 0

            int sum = valueA + valueB + valueC + valueD;
            */
            switch (numValid)
            {
                case 2:
                    return sum >> 1; // sum / 2
                case 3:
                    return (sum * 21845) >> 16; // sum * (65536/3) / 65536
                default:
                    return sum >> 2; // sum / 4
            }
        }
    }
}