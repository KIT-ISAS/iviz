#nullable enable

using UnityEngine;
using System.Collections.Generic;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Resources;
using Iviz.Tools;
using Unity.Mathematics;

namespace Iviz.Displays
{
    public sealed class GridDisplay : MarkerDisplay, IRecyclable, ISupportsColor, ISupportsShadows, ISupportsPbr,
        ISupportsAROcclusion
    {
        public static readonly List<string> OrientationNames = new() { "XY", "YZ", "XZ" };

        static readonly Dictionary<GridOrientation, Quaternion> RotationByOrientation = new()
        {
            { GridOrientation.XZ, Quaternion.identity },
            { GridOrientation.XY, Quaternion.Euler(90, 0, 0) },
            { GridOrientation.YZ, Quaternion.Euler(0, 90, 0) }
        };

        readonly List<MeshMarkerDisplay> horizontals = new();
        readonly List<MeshMarkerDisplay> verticals = new();

        [SerializeField] Texture2D? lightTexture;
        [SerializeField] Texture2D? darkTexture;
        [SerializeField] MeshRenderer? meshRenderer;

        Transform? pivot;

        MeshMarkerDisplay? interiorObject;
        MeshRenderer? interiorRenderer;
        LineDisplay? interiorLines;
        GridOrientation orientation;
        Color gridColor;
        Color interiorColor;
        int numberOfGridCells = 90;
        float gridCellSize = 1;
        float gridLineWidth;
        bool followCamera = true;

        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));

        MeshRenderer InteriorRenderer =>
            interiorRenderer != null
                ? interiorRenderer
                : interiorRenderer = InteriorObject.GetComponent<MeshRenderer>();

        MeshMarkerDisplay InteriorObject =>
            ResourcePool.RentChecked(ref interiorObject, Resource.Displays.Cube, Transform);

        LineDisplay InteriorLines => ResourcePool.RentChecked(ref interiorLines, Transform);

        public GridOrientation Orientation
        {
            get => orientation;
            set
            {
                orientation = value;
                Transform.localRotation = RotationByOrientation[value];
            }
        }

        public Color GridColor
        {
            get => gridColor;
            set
            {
                gridColor = value;
                MeshRenderer.SetPropertyColor(value);
                InteriorLines.Tint = value;
            }
        }

        public Color Color
        {
            get => interiorColor;
            set
            {
                interiorColor = value;
                InteriorObject.Color = value;
                InteriorLines.Tint = value;

                float colorValue = value.GetValue();
                var verticalColor = Color.red.WithValue(colorValue).WithSaturation(0.75f);
                var horizontalColor = Color.green.WithValue(colorValue).WithSaturation(0.75f);
                foreach (var display in verticals)
                {
                    display.Color = verticalColor;
                }

                foreach (var display in horizontals)
                {
                    display.Color = horizontalColor;
                }
            }
        }

        public Color EmissiveColor
        {
            set => InteriorObject.EmissiveColor = value;
        }

        float GridLineWidth
        {
            get => gridLineWidth;
            set
            {
                gridLineWidth = value;
                UpdateMesh();
            }
        }

        float GridCellSize
        {
            get => gridCellSize;
            set
            {
                gridCellSize = value;
                UpdateMesh();
            }
        }

        public int NumberOfGridCells
        {
            get => numberOfGridCells;
            set
            {
                numberOfGridCells = value;
                UpdateMesh();
                UpdatePosition(0, 0, Transform.localPosition.y);
            }
        }

        public bool ShowInterior
        {
            set
            {
                InteriorObject.Visible = value;
                InteriorLines.Visible = !value;
            } 
        }

        public bool FollowCamera
        {
            get => followCamera;
            set
            {
                followCamera = value;
                if (!value)
                {
                    UpdatePosition(0, 0, Transform.localPosition.y);
                }
            }
        }


        public bool DarkMode
        {
            set => InteriorObject.DiffuseTexture = value ? darkTexture : lightTexture;
        }

        public bool EnableShadows
        {
            set => InteriorObject.EnableShadows = value;
        }

        public float Metallic
        {
            set => InteriorObject.Metallic = value;
        }

        public float Smoothness
        {
            set => InteriorObject.Smoothness = value;
        }

        public bool OcclusionOnly
        {
            set => InteriorObject.OcclusionOnly = value;
        }

        Transform PivotTransform
        {
            get
            {
                if (pivot != null) return pivot;
                pivot = new GameObject("Pivot").transform;
                pivot.SetParent(Transform, false);
                return pivot;
            }
        }

        void Awake()
        {
            InteriorLines.ElementScale = 0.005f;
            
            InteriorObject.name = "Grid Interior";
            InteriorObject.Transform.localPosition = new Vector3(0, 0, 0.01f);
            InteriorObject.Layer = LayerType.IgnoreRaycast;
            InteriorObject.Metallic = 0.5f;
            InteriorObject.Smoothness = 0.5f;

            DarkMode = true;

            InteriorRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            InteriorRenderer.receiveShadows = true;

            Orientation = GridOrientation.XY;
            GridColor = Color.white.WithAlpha(0.25f);
            Color = Color.white * 0.5f;
            GridLineWidth = 0.025f;
            GridCellSize = 1;
            NumberOfGridCells = 90;
            ShowInterior = true;

            UpdateMesh();
        }

        void Update()
        {
            if (!FollowCamera) return;

            (float camX, _, float camZ) = TfModule.RelativeToFixedFrame(Settings.MainCameraPose.position);
            // TODO: Orientation
            int x = (int)(camX + 0.5f);
            int z = (int)(camZ + 0.5f);
            var (offsetX, offsetY, offsetZ) = Transform.localPosition;

            int lastX = (int)(offsetX + 0.5f);
            int lastZ = (int)(offsetZ + 0.5f);

            if (x != lastX || z != lastZ)
            {
                UpdatePosition(x, z, offsetY);
            }
        }

        void UpdatePosition(int x, int z, float offsetY)
        {
            const float zPosForX = 3e-4f;
            const float zPosForY = 2e-4f; // prevent z-fighting

            Transform.localPosition = new Vector3(x, offsetY, z);

            int baseHoriz = Mathf.FloorToInt((z + 5) / 10f) * 10;
            int baseVert = Mathf.FloorToInt((x + 5) / 10f) * 10;

            for (int i = 0; i < horizontals.Count; i++)
            {
                float za = (i - (horizontals.Count - 1f) / 2) * 10;
                horizontals[i].Transform.localPosition = new Vector3(0, baseHoriz + za - z, -zPosForX);
            }

            for (int i = 0; i < verticals.Count; i++)
            {
                float xa = (i - (verticals.Count - 1f) / 2) * 10;
                verticals[i].Transform.localPosition = new Vector3(baseVert + xa - x, 0, -zPosForY);
            }
        }

        void UpdateMesh()
        {
            var tilingScale = new Vector2(NumberOfGridCells, NumberOfGridCells);

            Resource.TexturedMaterials.GetFull(lightTexture).mainTextureScale = tilingScale;
            Resource.TexturedMaterials.GetSimple(lightTexture).mainTextureScale = tilingScale;

            Resource.TexturedMaterials.GetFull(darkTexture).mainTextureScale = tilingScale;
            Resource.TexturedMaterials.GetSimple(darkTexture).mainTextureScale = tilingScale;

            float totalSize = GridCellSize * NumberOfGridCells + GridLineWidth;

            const float interiorHeight = 1 / 512f;
            InteriorObject.Transform.localScale = new Vector3(totalSize, totalSize, interiorHeight);
            InteriorObject.Transform.localPosition = new Vector3(0, 0, interiorHeight / 2);

            Collider.size = new Vector3(totalSize, totalSize, interiorHeight);
            Collider.center = new Vector3(0, 0, interiorHeight / 2);

            int size = NumberOfGridCells / 10;
            if (horizontals.Count > size)
            {
                while (horizontals.Count != size)
                {
                    horizontals[^1].ReturnToPool(Resource.Displays.Square);
                    horizontals.RemoveAt(horizontals.Count - 1);
                    verticals[^1].ReturnToPool(Resource.Displays.Square);
                    verticals.RemoveAt(verticals.Count - 1);
                }
            }
            else if (horizontals.Count < size)
            {
                for (int i = horizontals.Count; i < size; i++)
                {
                    var hResource = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Square, PivotTransform);
                    hResource.Transform.localRotation = Quaternions.Rotate270AroundX;
                    hResource.Layer = LayerType.IgnoreRaycast;
                    horizontals.Add(hResource);

                    var vResource = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Square, PivotTransform);
                    vResource.Transform.localRotation = Quaternions.Rotate270AroundX;
                    vResource.Layer = LayerType.IgnoreRaycast;
                    verticals.Add(vResource);
                }
            }

            foreach (var resource in horizontals)
            {
                resource.Transform.localScale = new Vector3(totalSize, 1, 2 * GridLineWidth);
            }

            foreach (var resource in verticals)
            {
                resource.Transform.localScale = new Vector3(2 * GridLineWidth, 1, totalSize);
            }

            float color = UnityUtils.AsFloat(Color.white);
            var lines = new List<float4x2>();
            for (int i = 0; i < NumberOfGridCells; i++)
            {
                float4x2 f;
                f.c0 = new float4(i, 0, 0, color);
                f.c1 = new float4(i, NumberOfGridCells, 0, color);
                lines.Add(f);
                
                float4x2 g;
                g.c0 = new float4(0, i, 0, color);
                g.c1 = new float4(NumberOfGridCells, i, 0, color);
                lines.Add(g);
            }
            
            InteriorLines.Set(lines.AsReadOnlySpan(), false);
            
            float zPosForLines = InteriorLines.ElementScale / 2;
            InteriorLines.Transform.localPosition = new Vector3(-NumberOfGridCells / 2f, -NumberOfGridCells / 2f, zPosForLines);
            
            Color = Color;
        }

        public void SplitForRecycle()
        {
            foreach (var plane in horizontals)
            {
                plane.ReturnToPool(Resource.Displays.Square);
            }

            foreach (var plane in verticals)
            {
                plane.ReturnToPool(Resource.Displays.Square);
            }

            interiorObject.ReturnToPool(Resource.Displays.Square);
            interiorLines.ReturnToPool();
        }
    }
}