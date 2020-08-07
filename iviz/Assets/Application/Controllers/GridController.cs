using UnityEngine;
using System;
using System.Runtime.Serialization;
using Iviz.Resources;
using Iviz.Displays;

namespace Iviz.Controllers
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
        [DataMember] public float GridLineWidth { get; set; } = 0.01f;
        [DataMember] public float GridCellSize { get; set; } = 1;
        [DataMember] public int NumberOfGridCells { get; set; } = 90;
        [DataMember] public bool ShowInterior { get; set; } = true;
        [DataMember] public bool FollowCamera { get; set; } = true;
        [DataMember] public bool HideInARMode { get; set; } = true;
        [DataMember] public SerializableVector3 Offset { get; set; } = Vector3.zero;
    }

    public sealed class GridController : IController
    {
        readonly DisplayNode node;
        readonly ReflectionProbe reflectionProbe;
        readonly GridResource grid;

        public IModuleData ModuleData { get; set; }

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
                HideInARMode = value.HideInARMode;
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
                if (!value)
                {
                    reflectionProbe.transform.parent = TFListener.MapFrame.transform;
                    grid.Visible = false;
                    //node.Selected = false;
                }
                else
                {
                    grid.Visible = true;
                    reflectionProbe.transform.parent = grid.transform;
                }
                reflectionProbe.RenderProbe();
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
                grid.FollowCamera = value;
            }
        }
        
        public bool HideInARMode
        {
            get => config.HideInARMode;
            set => config.HideInARMode = value;
        }        

        public SerializableVector3 Offset
        {
            get => config.Offset;
            set
            {
                config.Offset = value;
                node.transform.localPosition = ((Vector3)value).Ros2Unity();
                UpdateMesh();
            }
        }

        //void Awake()
        public GridController(IModuleData moduleData)
        {
            //name = "Grid Controller";

            //grid = ResourcePool.GetOrCreate<GridResource>(Resource.Displays.Grid);
            grid = Resource.Displays.GetOrCreate<GridResource>();
            grid.name = "Grid";

            node = DisplayClickableNode.Instantiate("GridNode");
            grid.transform.parent = node.transform;
            //node.Target = grid;
            //node.SetName("");

            ModuleData = moduleData;

            reflectionProbe = new GameObject().AddComponent<ReflectionProbe>();
            reflectionProbe.gameObject.name = "Grid Reflection Probe";
            reflectionProbe.transform.parent = grid.transform;
            reflectionProbe.transform.localPosition = new Vector3(0, 2.0f, 0);
            reflectionProbe.nearClipPlane = 0.5f;
            reflectionProbe.farClipPlane = 100f;
            
            reflectionProbe.backgroundColor = TFListener.MainCamera.backgroundColor;
            
            reflectionProbe.mode = UnityEngine.Rendering.ReflectionProbeMode.Realtime;
            reflectionProbe.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.ViaScripting;
            reflectionProbe.clearFlags = UnityEngine.Rendering.ReflectionProbeClearFlags.SolidColor;
            UpdateMesh();

            Config = new GridConfiguration();
        }

        void UpdateMesh()
        {
            float totalSize = NumberOfGridCells * GridCellSize;
            reflectionProbe.size = new Vector3(totalSize * 2, 4.05f, totalSize * 2);
            reflectionProbe.RenderProbe();
        }

        public void Stop()
        {
            //ResourcePool.Dispose(Resource.Displays.Grid, grid.gameObject);
            grid.Suspend();
            Resource.Displays.Dispose(grid);
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
            UnityEngine.Object.Destroy(reflectionProbe.gameObject);
        }

        public void Reset()
        {
        }
    }
}