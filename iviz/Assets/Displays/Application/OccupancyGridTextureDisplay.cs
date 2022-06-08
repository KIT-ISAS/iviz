#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Burst;
using UnityEngine;

namespace Iviz.Displays
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class OccupancyGridTextureDisplay : MarkerDisplayWithColormap, IHighlightable
    {
        static float baseOffset = 0.001f;
        static readonly int AtlasTex = Shader.PropertyToID("_AtlasTex");
        [SerializeField] Texture2D? atlasLarge;
        [SerializeField] Texture2D? atlasLargeFlipped;
        [SerializeField] Texture2D? texture;
        [SerializeField] MeshRenderer? meshRenderer;

        readonly float zOffset;

        Material? material;
        sbyte[] buffer = Array.Empty<sbyte>();
        uint? previousHash;
        bool newFlipMinMax;

        float cellSize;
        RectInt bounds;

        CancellationTokenSource? tokenSource;

        Material Material => material != null
            ? material
            : material = Resource.Materials.OccupancyGridTexture.Instantiate();

        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));
        Texture2D AtlasLarge => atlasLarge.AssertNotNull(nameof(atlasLarge));
        Texture2D AtlasLargeFlipped => atlasLargeFlipped.AssertNotNull(nameof(atlasLargeFlipped));

        bool IsProcessing { get; set; }
        public int NumValidValues { get; private set; }
        public string Title { get; set; } = "";

        public override Vector2 IntensityBounds
        {
            get => default;
            set { }
        }

        public override bool FlipMinMax
        {
            get => newFlipMinMax;
            set
            {
                newFlipMinMax = value;
                Material.SetTexture(AtlasTex, value ? AtlasLargeFlipped : AtlasLarge);
            }
        }

        public OccupancyGridTextureDisplay()
        {
            zOffset = (baseOffset += 1e-5f);
        }

        protected override void Awake()
        {
            MeshRenderer.sharedMaterial = Material;

            base.Awake();
            Colormap = ColormapId.hsv;
            FlipMinMax = false;
            Layer = LayerType.Collider;
        }

        Texture2D EnsureSize(int sizeX, int sizeY)
        {
            if (texture != null)
            {
                if (texture.width == sizeX && texture.height == sizeY)
                {
                    return texture;
                }

                Destroy(texture);
            }

            texture = new Texture2D(sizeX, sizeY, TextureFormat.R8, true)
            {
                name = "OccupancyGrid Texture",
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
            };
            Material.mainTexture = texture;

            return texture;
        }

        public void Set(ReadOnlySpan<sbyte> values, float newCellSize, int numCellsX, in RectInt newBounds, Pose pose)
        {
            IsProcessing = true;

            cellSize = newCellSize;
            bounds = newBounds;

            int segmentWidth = newBounds.width;
            int segmentHeight = newBounds.height;

            float totalWidth = segmentWidth * cellSize;
            float totalHeight = segmentHeight * cellSize;

            int totalTextureSize = CalculateAllMipmapsSize(segmentWidth, segmentHeight);
            if (buffer.Length != totalTextureSize)
            {
                buffer = new sbyte[totalTextureSize];
            }

            (uint hash, int numValidValues) = Process(values, newBounds, numCellsX, buffer);
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

                var mTransform = Transform;
                var rosCenter = new Vector3(
                    bounds.xMax + bounds.xMin - 1,
                    bounds.yMax + bounds.yMin - 1,
                    0) * (cellSize / 2);
                rosCenter.z += zOffset;

                var offset = new Pose(rosCenter.Ros2Unity(), Quaternions.Rotate90AroundY);
                var newPose = pose.Multiply(offset);
                mTransform.SetLocalPose(newPose);
                mTransform.localScale = new Vector3(totalHeight, totalWidth, 1).Ros2Unity().Abs();

                var validatedTexture = EnsureSize(segmentWidth, segmentHeight);
                validatedTexture.GetRawTextureData<sbyte>().CopyFrom(buffer);
                validatedTexture.Apply(false);

                IsProcessing = false;
            });
        }

        static (uint hash, int numValidValues) Process(ReadOnlySpan<sbyte> src, RectInt bounds,
            int pitch, Span<sbyte> dest)
        {
            uint hash = HashCalculator.DefaultSeed;
            int numValidValues = 0;

            int rowSize = bounds.width;
            foreach (int v in ..bounds.height)
            {
                int srcOffset = (v + bounds.y) * pitch + bounds.x;
                int dstOffset = v * rowSize;

                var srcSpan = src.Slice(srcOffset, rowSize);
                var dstSpan = dest.Slice(dstOffset, rowSize);

                srcSpan.CopyTo(dstSpan);
                
                hash = HashCalculator.Compute(srcSpan, hash);
                numValidValues += OccupancyGridUtils.CountValidValues(srcSpan);
                /*

                ref sbyte uPtr = ref srcSpan.GetReference();
                //foreach (sbyte u in srcSpan)
                for (int i = rowSize; i > 0; i--)
                {
                    //numValidValues += (u < 0) ? 1 : 0;
                    //numValidValues += (u >> 8) + 1;
                    numValidValues += (uPtr >> 8) + 1;
                    uPtr = ref uPtr.Plus(1);
                }
                */
            }

            return (hash, numValidValues);
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
            cellSize = 0;
            previousHash = null;
            MeshRenderer.enabled = true;
            Title = "";

            tokenSource?.Cancel();
            tokenSource = null;
        }

        static void CreateMipmaps(Span<sbyte> array, int width, int height)
        {
            var srcPtr = array;
            while (width > 1 && height > 1)
            {
                int size = width * height;
                var dstPtr = srcPtr[size..];
                Reduce(srcPtr, width, height, dstPtr);
                srcPtr = dstPtr;
                width /= 2;
                height /= 2;
            }
            /*
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
            }
            */
        }

        static int CalculateAllMipmapsSize(int width, int height)
        {
            int size = 0;
            while (width != 1 || height != 1)
            {
                size += width * height;
                width = Math.Max(width / 2, 1);
                height = Math.Max(height / 2, 1);
            }

            return size + 1;
        }

        static void Reduce(Span<sbyte> srcSpan, int width, int height, Span<sbyte> dstSpan)
        {
            if (width < 2 || height < 2)
            {
                throw new InvalidOperationException("NYI!");
            }

            int halfWidth = width / 2;
            int halfHeight = height / 2;
            for (int vDst = 0; vDst < halfHeight; vDst++)
            {
                int vSrc = 2 * vDst;
                var row0 = srcSpan.Slice(width * vSrc, width);
                var row1 = srcSpan.Slice(width * (vSrc + 1), width);
                var rowDst = dstSpan.Slice(halfWidth * vDst, halfWidth);

                OccupancyGridUtils.ReduceRows(row0, row1, rowDst);
                
                #if false
                ref sbyte row0Ptr = ref row0[0];
                ref sbyte row1Ptr = ref row1[0];
                ref sbyte rowDstPtr = ref rowDst[0];

                //for (int uSrc = 0, uDst = 0; uDst < halfWidth; uSrc += 2, uDst++)
                //int uSrc = 0;
                //foreach (ref sbyte dst in rowDst)
                for (int i = halfWidth; i > 0; i--)
                {
                    /*
                    int a = row0[uSrc + 0];
                    int b = row0[uSrc + 1];
                    int c = row1[uSrc + 0];
                    int d = row1[uSrc + 1];
                    dst = (sbyte)Fuse(a, b, c, d);
                    uSrc += 2;
                    */
                    int a = row0Ptr;
                    int b = row0Ptr.Plus(1);
                    int c = row1Ptr;
                    int d = row1Ptr.Plus(1);
                    
                    rowDstPtr = (sbyte)Fuse(a, b, c, d);
                    
                    row0Ptr = ref row0Ptr.Plus(2);
                    row1Ptr = ref row1Ptr.Plus(2);
                    rowDstPtr = ref rowDstPtr.Plus(1);
                }
#endif
            }
        }

        /*
        static unsafe void Reduce(Span<sbyte> srcSpan, int width, int height, Span<sbyte> dstSpan)
        {
            fixed (sbyte* src = srcSpan)
            fixed (sbyte* dst = dstSpan)
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
                        *dst = (sbyte)Fuse(a, b, c, d);
                    }
                }
            }
        }
        */

        static int Fuse(int a, int b, int c, int d)
        {
            /*
            int4 abcd = new int4(a, b, c, d);
            int4 sign = ~abcd >> 8;
            int numValid = -(sign.x + sign.y + sign.z + sign.w);
            if (numValid <= 1)
            {
                return -1;
            }

            int4 value = abcd & sign;
            int sum = value.x + value.y + value.z + value.w;
            */

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
            return numValid switch
            {
                2 => sum >> 1, // sum / 2
                3 => (sum * 21845) >> 16, // sum * (65536/3) / 65536
                _ => sum >> 2
            };
            */

            int signA = ~a >> 8; // a >= 0 ? -1 : 0
            int valueA = a & signA; // a >= 0 ? a : 0
            int numValid = -signA;
            int sum = valueA;

            int signB = ~b >> 8; // b >= 0 ? -1 : 0
            int valueB = b & signB; // b >= 0 ? b : 0
            numValid -= signB;
            sum += valueB;

            int signC = ~c >> 8; // c >= 0 ? -1 : 0
            int valueC = c & signC; // c >= 0 ? c : 0
            numValid -= signC;
            sum += valueC;

            int signD = ~d >> 8; // d >= 0 ? -1 : 0
            int valueD = d & signD; // d >= 0 ? d : 0
            numValid -= signD;
            sum += valueD;

            return numValid switch
            {
                < 2 => -1,
                2 => sum >> 1, // sum / 2
                3 => (sum * 21845) >> 16, // sum * (65536/3) / 65536
                _ => sum >> 2
            };
        }

        public void Highlight(in Vector3 hitPoint)
        {
            int segmentWidth = bounds.width;
            int segmentHeight = bounds.height;

            var (localX, localY, localZ) = Transform.InverseTransformPoint(hitPoint);
            int coordU = Mathf.Clamp((int)((0.5f - localX) * segmentWidth), 0, segmentWidth - 1);
            int coordV = Mathf.Clamp((int)((localZ + 0.5f) * segmentHeight), 0, segmentHeight - 1);
            sbyte value = buffer[(segmentHeight - 1 - coordV) * segmentWidth + coordU];

            float clampedX = 0.5f - (coordU + 0.5f) / segmentWidth;
            float clampedZ = (coordV + 0.5f) / segmentHeight - 0.5f;
            var localClamped = new Vector3(clampedX, localY, clampedZ);
            var worldClamped = Transform.TransformPoint(localClamped);
            var unityPose = Pose.identity.WithPosition(worldClamped);

            string caption;
            using (var description = BuilderPool.Rent())
            {
                description.Append("<b>").Append(Title).Append("</b>").AppendLine();
                RosUtils.FormatPose(TfModule.RelativeToFixedFrame(unityPose), description,
                    RosUtils.PoseFormat.OnlyPosition, 2);
                description.AppendLine();
                description
                    .Append("<b>u: </b>").Append(coordU + bounds.x)
                    .Append(" <b>v: </b>").Append(coordV + bounds.y).AppendLine()
                    .Append(value);
                caption = description.ToString();
            }

            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            FAnimator.Start(new ClickedPoseHighlighter(unityPose, caption, 2, tokenSource.Token));
        }

        public bool IsAlive => Visible && cellSize != 0;
    }
}