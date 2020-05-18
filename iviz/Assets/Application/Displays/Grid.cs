using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Iviz.App.Listeners;
using Iviz.Resources;

namespace Iviz.App.Displays
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GridOrientation
    {
        XY, YZ, XZ
    }

    [DataContract]
    public class GridConfiguration :  IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Grid;
        [DataMember] public GridOrientation Orientation { get; set; } = GridOrientation.XY;
        [DataMember] public SerializableColor GridColor { get; set; } = Color.white * 0.25f;
        [DataMember] public SerializableColor InteriorColor { get; set; } = Color.white * 0.5f;
        [DataMember] public float GridLineWidth { get; set; } = 0.02f;
        [DataMember] public float GridCellSize { get; set; } = 1;
        [DataMember] public int NumberOfGridCells { get; set; } = 20;
        [DataMember] public bool ShowInterior { get; set; } = true;
    }

    public class Grid : ClickableDisplayNode, IRecyclable
    {
        Mesh mesh;
        MeshRenderer meshRenderer;
        ReflectionProbe reflectionProbe;
        GameObject interiorObject;
        MeshRenderer interiorRenderer;

        BoxCollider boxCollider;
        public override Bounds Bounds => new Bounds(boxCollider.center, boxCollider.size);
        public override Bounds WorldBounds => boxCollider.bounds;


        public static readonly List<string> OrientationNames = new List<string> { "XY", "YZ", "XZ" };

        static readonly Dictionary<GridOrientation, Quaternion> RotationByOrientation = new Dictionary<GridOrientation, Quaternion>
        {
            { GridOrientation.XZ, Quaternion.identity },
            { GridOrientation.XY, Quaternion.Euler(90, 0, 0) },
            { GridOrientation.YZ, Quaternion.Euler(0, 90, 0) }
        };

        readonly GridConfiguration config = new GridConfiguration();
        public GridConfiguration Config
        {
            get => config;
            set
            {
                Orientation = value.Orientation;
                GridColor = value.GridColor;
                InteriorColor = value.InteriorColor;
                GridLineWidth = value.GridLineWidth;
                GridCellSize = value.GridCellSize;
                NumberOfGridCells = value.NumberOfGridCells;
                ShowInterior = value.ShowInterior;
            }
        }

        public GridOrientation Orientation
        {
            get => config.Orientation;
            set
            {
                config.Orientation = value;
                transform.localRotation = RotationByOrientation[value];
                reflectionProbe.transform.position = new Vector3(0, 2.0f, 0);
            }
        }

        public Color GridColor
        {
            get => config.GridColor;
            set
            {
                config.GridColor = value;
                meshRenderer.SetPropertyColor(value);
            }
        }

        public Color InteriorColor
        {
            get => config.InteriorColor;
            set
            {
                config.InteriorColor = value;
                interiorRenderer.SetPropertyColor(value);
            }
        }

        public float GridLineWidth
        {
            get => config.GridLineWidth;
            set
            {
                config.GridLineWidth = value;
                UpdateMesh();
            }
        }

        public float GridCellSize
        {
            get => config.GridCellSize;
            set
            {
                config.GridCellSize = value;
                UpdateMesh();
            }
        }

        public int NumberOfGridCells
        {
            get => config.NumberOfGridCells;
            set
            {
                config.NumberOfGridCells = value;
                UpdateMesh();
            }
        }

        public bool ShowInterior
        {
            get => config.ShowInterior;
            set
            {
                config.ShowInterior = value;
                interiorObject.SetActive(value);
            }
        }


        void Awake()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().sharedMesh = mesh;
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = Resource.Materials.Grid;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            //meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;

            interiorObject = ResourcePool.GetOrCreate(Resource.Markers.Cube, transform);
            interiorObject.name = "Grid Interior";
            interiorObject.transform.localPosition = new Vector3(0, 0, 0.01f);
            interiorRenderer = interiorObject.GetComponent<MeshRenderer>();
            interiorRenderer.sharedMaterial = meshRenderer.sharedMaterial;
            interiorRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            interiorRenderer.receiveShadows = true;
            //interiorRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;

            reflectionProbe = new GameObject().AddComponent<ReflectionProbe>();
            reflectionProbe.gameObject.name = "Grid Reflection Probe";
            reflectionProbe.transform.parent = transform;
            reflectionProbe.transform.position = new Vector3(0, 2.0f, 0);
            reflectionProbe.mode = UnityEngine.Rendering.ReflectionProbeMode.Realtime;
            reflectionProbe.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.ViaScripting;
            reflectionProbe.clearFlags = UnityEngine.Rendering.ReflectionProbeClearFlags.SolidColor;

            Config = new GridConfiguration();
            gameObject.layer = Resource.ClickableLayer;

            Parent = TFListener.BaseFrame;
            boxCollider = GetComponent<BoxCollider>();
        }

        void UpdateMesh()
        {
            Mesh squareMesh = Resource.Markers.Square.GameObject.GetComponent<MeshFilter>().sharedMesh;
            IEnumerable<int> squareIndices = squareMesh.GetIndices(0);

            float totalSize = GridCellSize * NumberOfGridCells + GridLineWidth;
            int gridVertSize;

            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indices = new List<int>();

            List<Vector3> squareVertices =
                squareMesh.vertices.
                Select(x => new Vector3(x.x, x.z, -x.y) * 0.1f + new Vector3(0.5f, 0.5f, -0.5f)).
                ToList();
            List<Vector3> squareNormals =
                squareMesh.normals.
                Select(x => new Vector3(x.x, x.z, -x.y)).
                ToList();

            float offsetX = -totalSize / 2;
            float offsetY = -totalSize / 2;
            Vector3 offset;

            Vector3 scaleHorizontal = new Vector3(GridLineWidth, totalSize, GridLineWidth / 8);
            List<Vector3> verticesHorizontal = squareVertices.Select(x => x.CwiseProduct(scaleHorizontal)).ToList();

            for (int i = 0; i <= NumberOfGridCells; i++)
            {
                gridVertSize = vertices.Count();
                offset = new Vector3(offsetX + i * GridCellSize, offsetY, 0);
                indices.AddRange(squareIndices.Select(x => x + gridVertSize));
                vertices.AddRange(verticesHorizontal.Select(x => x + offset));
                normals.AddRange(squareNormals);
            }


            Vector3 scaleVertical = new Vector3(totalSize, GridLineWidth, GridLineWidth / 8);
            List<Vector3> verticesVertical = squareVertices.Select(x => x.CwiseProduct(scaleVertical)).ToList();

            for (int i = 0; i <= NumberOfGridCells; i++)
            {
                gridVertSize = vertices.Count();
                offset = new Vector3(offsetX, offsetY + i * GridCellSize, 0);
                indices.AddRange(squareIndices.Select(x => x + gridVertSize));
                vertices.AddRange(verticesVertical.Select(x => x + offset));
                normals.AddRange(squareNormals);
            }

            mesh.Clear();
            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetIndices(indices.ToArray(), squareMesh.GetTopology(0), 0);
            mesh.Optimize();

            interiorObject.transform.localScale = new Vector3(totalSize, totalSize, GridLineWidth / 8.1f);

            GetComponent<BoxCollider>().size = new Vector3(totalSize, totalSize, GridLineWidth / 8);

            reflectionProbe.size = new Vector3(totalSize * 2, 4.05f, totalSize * 2);
            reflectionProbe.RenderProbe();
        }

        public override void Stop()
        {
            base.Stop();
            Config = new GridConfiguration();
        }

        public void Recycle()
        {
            //interiorRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            interiorRenderer.receiveShadows = false;
            ResourcePool.Dispose(Resource.Markers.Cube, interiorObject);
            interiorRenderer = null;
            interiorObject = null;

            Destroy(reflectionProbe);
            reflectionProbe = null;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (GetClickCount(eventData) == 2 &&
                eventData.button == PointerEventData.InputButton.Left &&
                TFListener.GuiManager.OrbitFrame != null)
            {
                TFListener.GuiManager.OrbitFrame = null;
                return;
            }
            if (GetClickCount(eventData) == 1 && IsRealClick(eventData))
            {
                TFListener.GuiManager.Select(null);
            }
            
            //base.OnPointerClick(eventData);
        }

    }
}