using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Iviz.App.Displays;

namespace Iviz.App
{
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

        [JsonConverter(typeof(StringEnumConverter))]
        public enum OrientationType
        {
            XY, YZ, XZ
        }

        public static readonly List<string> OrientationNames = new List<string> { "XY", "YZ", "XZ" };

        static readonly Dictionary<OrientationType, Quaternion> RotationByOrientation = new Dictionary<OrientationType, Quaternion>
        {
            { OrientationType.XZ, Quaternion.identity },
            { OrientationType.XY, Quaternion.Euler(90, 0, 0) },
            { OrientationType.YZ, Quaternion.Euler(0, 90, 0) }
        };

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.Grid;
            public OrientationType orientation = OrientationType.XY;
            public SerializableColor gridColor = Color.white * 0.25f;
            public SerializableColor interiorColor = Color.white * 0.5f;
            public float gridLineWidth = 0.02f;
            public float gridCellSize = 1;
            public int numberOfGridCells = 20;
            public bool showInterior = true;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                Orientation = value.orientation;
                GridColor = value.gridColor;
                InteriorColor = value.interiorColor;
                GridLineWidth = value.gridLineWidth;
                GridCellSize = value.gridCellSize;
                NumberOfGridCells = value.numberOfGridCells;
                ShowInterior = value.showInterior;
            }
        }

        public OrientationType Orientation
        {
            get => config.orientation;
            set
            {
                config.orientation = value;
                transform.rotation = RotationByOrientation[value];
                reflectionProbe.transform.position = new Vector3(0, 2.0f, 0);
            }
        }

        public Color GridColor
        {
            get => config.gridColor;
            set
            {
                config.gridColor = value;
                meshRenderer.SetPropertyColor(value);
            }
        }

        public Color InteriorColor
        {
            get => config.interiorColor;
            set
            {
                config.interiorColor = value;
                interiorRenderer.SetPropertyColor(value);
            }
        }

        public float GridLineWidth
        {
            get => config.gridLineWidth;
            set
            {
                config.gridLineWidth = value;
                UpdateMesh();
            }
        }

        public float GridCellSize
        {
            get => config.gridCellSize;
            set
            {
                config.gridCellSize = value;
                UpdateMesh();
            }
        }

        public int NumberOfGridCells
        {
            get => config.numberOfGridCells;
            set
            {
                config.numberOfGridCells = value;
                UpdateMesh();
            }
        }

        public bool ShowInterior
        {
            get => config.showInterior;
            set
            {
                config.showInterior = value;
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

            Config = new Configuration();
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
            Config = new Configuration();
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