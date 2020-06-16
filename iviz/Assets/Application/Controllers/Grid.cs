using UnityEngine;
using System;
using System.Runtime.Serialization;
using Iviz.Resources;
using Iviz.Displays;
using Iviz.App.Displays;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class GridConfiguration : IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Grid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public GridOrientation Orientation { get; set; } = GridOrientation.XY;
        [DataMember] public SerializableColor GridColor { get; set; } = Color.white * 0.25f;
        [DataMember] public SerializableColor InteriorColor { get; set; } = Color.white * 0.5f;
        [DataMember] public float GridLineWidth { get; set; } = 0.02f;
        [DataMember] public float GridCellSize { get; set; } = 1;
        [DataMember] public int NumberOfGridCells { get; set; } = 30;
        [DataMember] public bool ShowInterior { get; set; } = true;
        [DataMember] public bool FollowCamera { get; set; } = true;
        [DataMember] public SerializableVector3 Offset { get; set; } = Vector3.zero;
    }

    public class Grid : MonoBehaviour, IController
    {
        DisplayClickableNode node;
        ReflectionProbe reflectionProbe;
        GridResource grid;

        public ModuleData ModuleData
        {
            get => node.DisplayData;
            set => node.DisplayData = value;
        }

        readonly GridConfiguration config = new GridConfiguration();
        public GridConfiguration Config
        {
            get => config;
            set
            {
                Orientation = value.Orientation;
                Visible = value.Visible;
                GridColor = value.GridColor;
                InteriorColor = value.InteriorColor;
                GridLineWidth = value.GridLineWidth;
                GridCellSize = value.GridCellSize;
                NumberOfGridCells = value.NumberOfGridCells;
                ShowInterior = value.ShowInterior;
                FollowCamera = value.FollowCamera;
                Offset = value.Offset;
            }
        }

        public GridOrientation Orientation
        {
            get => config.Orientation;
            set
            {
                config.Orientation = value;
                grid.Orientation = value;
                reflectionProbe.transform.position = new Vector3(0, 2.0f, 0);
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                grid.Visible = value;
            }
        }

        public Color GridColor
        {
            get => config.GridColor;
            set
            {
                config.GridColor = value;
                grid.GridColor = value;
                UpdateMesh();
            }
        }

        public Color InteriorColor
        {
            get => config.InteriorColor;
            set
            {
                config.InteriorColor = value;
                grid.InteriorColor = value;
                UpdateMesh();
            }
        }

        public float GridLineWidth
        {
            get => config.GridLineWidth;
            set
            {
                config.GridLineWidth = value;
                grid.GridLineWidth = value;
                UpdateMesh();
            }
        }

        public float GridCellSize
        {
            get => config.GridCellSize;
            set
            {
                config.GridCellSize = value;
                grid.GridCellSize = value;
                UpdateMesh();
            }
        }

        public int NumberOfGridCells
        {
            get => config.NumberOfGridCells;
            set
            {
                config.NumberOfGridCells = value;
                grid.NumberOfGridCells = value;
                UpdateMesh();
            }
        }

        public bool ShowInterior
        {
            get => config.ShowInterior;
            set
            {
                config.ShowInterior = value;
                grid.ShowInterior = value;
                UpdateMesh();
            }
        }

        public bool FollowCamera
        {
            get => config.FollowCamera;
            set
            {
                config.FollowCamera = value;
            }
        }

        public SerializableVector3 Offset
        {
            get => config.Offset;
            set
            {
                config.Offset = value;
                node.transform.position = ((Vector3)value).Ros2Unity();
                UpdateMesh();
            }
        }

        void Awake()
        {
            name = "Grid Controller";

            grid = ResourcePool.GetOrCreate<GridResource>(Resource.Markers.Grid);
            grid.name = "Grid";

            node = DisplayClickableNode.Instantiate("GridNode");
            node.Target = grid;
            node.SetName("");

            reflectionProbe = new GameObject().AddComponent<ReflectionProbe>();
            reflectionProbe.gameObject.name = "Grid Reflection Probe";
            reflectionProbe.transform.parent = grid.transform;
            reflectionProbe.transform.position = new Vector3(0, 2.0f, 0);
            reflectionProbe.mode = UnityEngine.Rendering.ReflectionProbeMode.Realtime;
            reflectionProbe.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.ViaScripting;
            reflectionProbe.clearFlags = UnityEngine.Rendering.ReflectionProbeClearFlags.SolidColor;
            UpdateMesh();

            Config = new GridConfiguration();
            gameObject.layer = Resource.ClickableLayer;
        }

        void UpdateMesh()
        {
            float totalSize = NumberOfGridCells * GridCellSize;
            reflectionProbe.size = new Vector3(totalSize * 2, 4.05f, totalSize * 2);
            reflectionProbe.RenderProbe();
        }

        public void Stop()
        {
            ResourcePool.Dispose(Resource.Markers.Grid, grid.gameObject);
            node.Stop();
            Destroy(node.gameObject);
            Destroy(reflectionProbe.gameObject);
        }

        /*
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
        */

    }
}