using UnityEngine;
using System.Collections.Generic;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Iviz.Resources;
using Iviz.Tools;
using JetBrains.Annotations;

namespace Iviz.Displays
{
    public sealed class GridResource : MarkerResource, IRecyclable
    {
        //Mesh mesh;
        MeshRenderer meshRenderer;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer != null ? meshRenderer : meshRenderer = GetComponent<MeshRenderer>();

        GameObject interiorObject;
        MeshRenderer interiorRenderer;
        readonly List<MeshMarkerResource> horizontals = new();
        readonly List<MeshMarkerResource> verticals = new();

        int lastX = 0, lastZ = 0;
        
        public static readonly List<string> OrientationNames = new() { "XY", "YZ", "XZ" };

        static readonly Dictionary<GridOrientation, Quaternion> RotationByOrientation =
            new()
            {
                { GridOrientation.XZ, Quaternion.identity },
                { GridOrientation.XY, Quaternion.Euler(90, 0, 0) },
                { GridOrientation.YZ, Quaternion.Euler(0, 90, 0) }
            };

        GridOrientation orientation;

        public GridOrientation Orientation
        {
            get => orientation;
            set
            {
                orientation = value;
                transform.localRotation = RotationByOrientation[value];
            }
        }

        Color gridColor;

        public Color GridColor
        {
            get => gridColor;
            set
            {
                gridColor = value;
                MeshRenderer.SetPropertyColor(value);
            }
        }

        Color interiorColor;

        public Color InteriorColor
        {
            get => interiorColor;
            set
            {
                interiorColor = value;
                interiorColor.a = 0.1f;
                interiorRenderer.SetPropertyColor(value);
            }
        }

        float gridLineWidth;

        float GridLineWidth
        {
            get => gridLineWidth;
            set
            {
                gridLineWidth = value;
                //if (!(mesh is null))
                {
                    UpdateMesh();
                }
            }
        }

        float gridCellSize = 1;

        float GridCellSize
        {
            get => gridCellSize;
            set
            {
                gridCellSize = value;
                //if (!(mesh is null))
                {
                    UpdateMesh();
                }
            }
        }

        int numberOfGridCells = 90;

        int NumberOfGridCells
        {
            get => numberOfGridCells;
            set
            {
                numberOfGridCells = value;
                //if (!(mesh is null))
                {
                    UpdateMesh();
                }
            }
        }

        bool showInterior;

        public bool ShowInterior
        {
            get => showInterior;
            set
            {
                showInterior = value;
                interiorObject.SetActive(value);
            }
        }

        public bool FollowCamera { get; set; } = true;

        void Awake()
        {
            interiorObject = Resource.Displays.Cube.Instantiate(transform);
            interiorObject.name = "Grid Interior";
            interiorObject.transform.localPosition = new Vector3(0, 0, 0.01f);
            interiorObject.layer = LayerType.IgnoreRaycast;
            interiorRenderer = interiorObject.GetComponent<MeshRenderer>();
            interiorRenderer.sharedMaterial =
                Settings.SettingsManager.QualityInView == QualityType.VeryLow
                    ? Resource.Materials.GridInteriorSimple.Object
                    : Resource.Materials.GridInterior.Object;

            Settings.QualityTypeChanged += OnQualityChanged;

            interiorRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            interiorRenderer.receiveShadows = true;


            /*
            // the grid "jumps" every meter to center itself under the camera
            // this breaks motion blur, so we tell the camera to ignore the motion of the grid
            interiorRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera; // camera only
            */

            Orientation = GridOrientation.XY;
            GridColor = Color.white.WithAlpha(0.25f);
            InteriorColor = Color.white * 0.5f;
            GridLineWidth = 0.025f;
            GridCellSize = 1;
            NumberOfGridCells = 90;
            ShowInterior = true;

            UpdateMesh();
        }

        void OnQualityChanged(QualityType newType)
        {
            interiorRenderer.sharedMaterial =
                newType == QualityType.VeryLow
                    ? Resource.Materials.GridInteriorSimple.Object
                    : Resource.Materials.GridInterior.Object;
        }

        void Update()
        {
            if (!FollowCamera)
            {
                return;
            }

            (float camX, _, float camZ) = TfListener.RelativePositionToOrigin(Settings.MainCameraTransform.position);
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

            transform.localPosition = new Vector3(x, offsetY, z);

            int baseHoriz = Mathf.FloorToInt((z + 5) / 10f) * 10;
            foreach (int i in ..horizontals.Count)
            {
                float za = (i - (horizontals.Count - 1f) / 2) * 10;
                horizontals[i].transform.localPosition = new Vector3(0, baseHoriz + za - z, -zPosForX);
            }

            int baseVert = Mathf.FloorToInt((x + 5) / 10f) * 10;
            foreach (int i in ..verticals.Count)
            {
                float xa = (i - (verticals.Count - 1f) / 2) * 10;
                verticals[i].transform.localPosition = new Vector3(baseVert + xa - x, 0, -zPosForY);
            }

            lastX = x;
            lastZ = z;
        }

        void UpdateMesh()
        {
            float totalSize = GridCellSize * NumberOfGridCells + GridLineWidth;

            const float interiorHeight = 1 / 512f;
            interiorObject.transform.localScale = new Vector3(totalSize, totalSize, interiorHeight);
            interiorObject.transform.localPosition = new Vector3(0, 0, interiorHeight / 2);

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
                    var hResource = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Square, transform);
                    hResource.transform.localRotation = Quaternion.AngleAxis(-90, Vector3.right);
                    hResource.Layer = LayerType.IgnoreRaycast;
                    horizontals.Add(hResource);

                    var vResource = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Square, transform);
                    vResource.transform.localRotation = Quaternion.AngleAxis(-90, Vector3.right);
                    vResource.Layer = LayerType.IgnoreRaycast;
                    verticals.Add(vResource);
                }
            }

            foreach (var resource in horizontals)
            {
                resource.transform.localScale = new Vector3(totalSize, 1, 2 * GridLineWidth);
                resource.Color = Resource.Colors.GridGreenLine;
            }

            foreach (var resource in verticals)
            {
                resource.transform.localScale = new Vector3(2 * GridLineWidth, 1, totalSize);
                resource.Color = Resource.Colors.GridRedLine;
            }

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
        }

        void OnDestroy()
        {
            Settings.QualityTypeChanged -= OnQualityChanged;
        }
    }
}