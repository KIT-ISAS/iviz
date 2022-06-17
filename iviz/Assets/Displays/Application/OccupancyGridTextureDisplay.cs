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

        public bool IsAlive => Visible && cellSize != 0;

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

        public void Set(sbyte[] values, float newCellSize, int numCellsX, in RectInt newBounds, Pose pose)
        {
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
                return;
            }

            previousHash = hash;
            if (numValidValues == 0)
            {
                GameThread.PostImmediate(() =>
                {
                    MeshRenderer.enabled = false;
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
            });
        }

        static (uint hash, int numValidValues) Process(sbyte[] src, RectInt bounds,
            int pitch, Span<sbyte> dest)
        {
            uint hash = HashCalculator.DefaultSeed;
            int numValidValues = 0;

            int rowSize = bounds.width;
            foreach (int v in ..bounds.height)
            {
                int srcOffset = (v + bounds.y) * pitch + bounds.x;
                int dstOffset = v * rowSize;

                var srcSpan = (ReadOnlySpan<sbyte>)src.AsSpan(srcOffset, rowSize);
                var dstSpan = dest.Slice(dstOffset, rowSize);

                srcSpan.CopyTo(dstSpan);

                hash = HashCalculator.Compute(srcSpan, hash);
                numValidValues += OccupancyGridUtils.CountValidValues(srcSpan);
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

        static void Reduce(ReadOnlySpan<sbyte> srcSpan, int width, int height, Span<sbyte> dstSpan)
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
            }
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
    }
}