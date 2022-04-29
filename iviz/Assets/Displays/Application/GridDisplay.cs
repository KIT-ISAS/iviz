#nullable enable

using UnityEngine;
using System.Collections.Generic;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;

namespace Iviz.Displays
{
    public sealed class GridDisplay : MarkerDisplay, IRecyclable
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

        MeshMarkerDisplay? interiorObject;
        MeshRenderer? interiorRenderer;
        GridOrientation orientation;
        Color gridColor;
        Color interiorColor;
        int numberOfGridCells = 90;
        int lastX, lastZ;
        float gridCellSize = 1;
        float gridLineWidth;

        MeshRenderer MeshRenderer => meshRenderer.AssertNotNull(nameof(meshRenderer));

        MeshRenderer InteriorRenderer =>
            interiorRenderer != null
                ? interiorRenderer
                : interiorRenderer = InteriorObject.GetComponent<MeshRenderer>();

        MeshMarkerDisplay InteriorObject => interiorObject != null
            ? interiorObject
            : interiorObject = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Cube, Transform);


        public GridOrientation Orientation
        {
            get => orientation;
            set
            {
                orientation = value;
                transform.localRotation = RotationByOrientation[value];
            }
        }

        public Color GridColor
        {
            get => gridColor;
            set
            {
                gridColor = value;
                MeshRenderer.SetPropertyColor(value);
            }
        }


        public Color InteriorColor
        {
            get => interiorColor;
            set
            {
                interiorColor = value;
                InteriorObject.Color = value;

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

        int NumberOfGridCells
        {
            get => numberOfGridCells;
            set
            {
                numberOfGridCells = value;
                UpdateMesh();
            }
        }

        public bool ShowInterior
        {
            set => InteriorObject.Visible = value;
        }

        public bool FollowCamera { get; set; } = true;

        public bool DarkMode
        {
            set => InteriorObject.DiffuseTexture = value ? darkTexture : lightTexture;
        }

        void Awake()
        {
            InteriorObject.name = "Grid Interior";
            InteriorObject.transform.localPosition = new Vector3(0, 0, 0.01f);
            InteriorObject.Layer = LayerType.IgnoreRaycast;
            InteriorObject.Metallic = 0.5f;
            InteriorObject.Smoothness = 0.5f;
            
            DarkMode = true;

            InteriorRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            InteriorRenderer.receiveShadows = true;

            Orientation = GridOrientation.XY;
            GridColor = Color.white.WithAlpha(0.25f);
            InteriorColor = Color.white * 0.5f;
            GridLineWidth = 0.025f;
            GridCellSize = 1;
            NumberOfGridCells = 90;
            ShowInterior = true;

            UpdateMesh();
        }

        void Update()
        {
            if (!FollowCamera)
            {
                return;
            }

            (float camX, _, float camZ) = TfModule.RelativeToOrigin(Settings.MainCameraTransform.position);
            switch (Orientation)
            {
                case GridOrientation.XY:
                    int x = (int)(camX + 0.5f);
                    int z = (int)(camZ + 0.5f);
                    float offsetY = transform.localPosition.y;
                    if (x != lastX || z != lastZ)
                    {
                        UpdatePosition(x, z, offsetY);
                    }

                    break;
                // TODO: others
            }
        }

        void UpdatePosition(int x, int z, float offsetY)
        {
            const float zPosForX = 3e-4f;
            const float zPosForY = 2e-4f; // prevent z-fighting

            Transform.localPosition = new Vector3(x, offsetY, z);

            int baseHoriz = Mathf.FloorToInt((z + 5) / 10f) * 10;
            foreach (int i in ..horizontals.Count)
            {
                float za = (i - (horizontals.Count - 1f) / 2) * 10;
                horizontals[i].Transform.localPosition = new Vector3(0, baseHoriz + za - z, -zPosForX);
            }

            int baseVert = Mathf.FloorToInt((x + 5) / 10f) * 10;
            foreach (int i in ..verticals.Count)
            {
                float xa = (i - (verticals.Count - 1f) / 2) * 10;
                verticals[i].Transform.localPosition = new Vector3(baseVert + xa - x, 0, -zPosForY);
            }

            lastX = x;
            lastZ = z;
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
                foreach (int _ in horizontals.Count..size)
                {
                    var hResource = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Square, transform);
                    hResource.Transform.localRotation = Quaternions.Rotate270AroundX;
                    hResource.Layer = LayerType.IgnoreRaycast;
                    horizontals.Add(hResource);

                    var vResource = ResourcePool.Rent<MeshMarkerDisplay>(Resource.Displays.Square, transform);
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

            InteriorColor = InteriorColor;
            lastX = int.MaxValue;
            lastZ = int.MaxValue;
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
        }
    }
}