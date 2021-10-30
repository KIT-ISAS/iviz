using UnityEngine;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Iviz.Resources;
using JetBrains.Annotations;

namespace Iviz.Displays
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GridOrientation
    {
        XY,
        YZ,
        XZ
    }

    public sealed class GridResource : MarkerResource, IRecyclable
    {
        //Mesh mesh;
        MeshRenderer meshRenderer;

        [NotNull]
        MeshRenderer MeshRenderer => meshRenderer != null ? meshRenderer : meshRenderer = GetComponent<MeshRenderer>();

        GameObject interiorObject;
        MeshRenderer interiorRenderer;
        readonly List<MeshMarkerResource> horizontals = new List<MeshMarkerResource>();
        readonly List<MeshMarkerResource> verticals = new List<MeshMarkerResource>();

        public static readonly List<string> OrientationNames = new List<string> {"XY", "YZ", "XZ"};

        static readonly Dictionary<GridOrientation, Quaternion> RotationByOrientation =
            new Dictionary<GridOrientation, Quaternion>
            {
                {GridOrientation.XZ, Quaternion.identity},
                {GridOrientation.XY, Quaternion.Euler(90, 0, 0)},
                {GridOrientation.YZ, Quaternion.Euler(0, 90, 0)}
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

        protected override void Awake()
        {
            base.Awake();

            /*
            mesh = new Mesh {name = "Grid Mesh"};
            GetComponent<MeshFilter>().sharedMesh = mesh;
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = Resource.Materials.Grid.Object;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            */
            //meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;

            interiorObject = Resource.Displays.Cube.Instantiate(transform);
            interiorObject.name = "Grid Interior";
            interiorObject.transform.localPosition = new Vector3(0, 0, 0.01f);
            interiorObject.layer = LayerType.IgnoreRaycast;
            interiorRenderer = interiorObject.GetComponent<MeshRenderer>();
            interiorRenderer.sharedMaterial =
                Settings.SettingsManager == null
                || Settings.SettingsManager.QualityInView == QualityType.VeryLow
                    ? Resource.Materials.GridInteriorSimple.Object
                    : Resource.Materials.GridInterior.Object;

            Settings.QualityTypeChanged += OnQualityChanged;

            interiorRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            interiorRenderer.receiveShadows = true;


            // the grid "jumps" every meter to center itself under the camera
            // this breaks motion blur, so we tell the camera to ignore the motion of the grid
            interiorRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.Camera; // camera only

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

        int lastX = 0, lastZ = 0;

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
                    int x = (int) (camX + 0.5f);
                    int z = (int) (camZ + 0.5f);
                    float offsetY = transform.localPosition.y;
                    if (x != lastX || z != lastZ)
                    {
                        UpdatePosition(x, z, offsetY);
                    }

                    break;
            }
        }

        void UpdatePosition(int x, int z, float offsetY)
        {
            transform.localPosition = new Vector3(x, offsetY, z);


            int baseHoriz = Mathf.FloorToInt((z + 5) / 10f) * 10;
            for (int i = 0; i < horizontals.Count; i++)
            {
                float za = (i - (horizontals.Count - 1f) / 2) * 10;
                horizontals[i].transform.localPosition = new Vector3(0, baseHoriz + za - z, -0.002f);
            }

            int baseVert = Mathf.FloorToInt((x + 5) / 10f) * 10;
            for (int i = 0; i < verticals.Count; i++)
            {
                float xa = (i - (verticals.Count - 1f) / 2) * 10;
                verticals[i].transform.localPosition = new Vector3(baseVert + xa - x, 0, -0.003f);
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

            BoxCollider.size = new Vector3(totalSize, totalSize, interiorHeight);
            BoxCollider.center = new Vector3(0, 0, interiorHeight / 2);

            int size = NumberOfGridCells / 10;
            if (horizontals.Count > size)
            {
                while (horizontals.Count != size)
                {
                    horizontals[horizontals.Count - 1].ReturnToPool(Resource.Displays.Square);
                    horizontals.RemoveAt(horizontals.Count - 1);
                    verticals[verticals.Count - 1].ReturnToPool(Resource.Displays.Square);
                    verticals.RemoveAt(verticals.Count - 1);
                }
            }
            else if (horizontals.Count < size)
            {
                for (int i = horizontals.Count; i < size; i++)
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

            foreach (MeshMarkerResource resource in horizontals)
            {
                resource.transform.localScale = new Vector3(totalSize, 1, 2 * GridLineWidth);
                resource.Color = Resource.Colors.GridGreenLine;
            }

            foreach (MeshMarkerResource resource in verticals)
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