using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.NavMsgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class OccupancyGridConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.OccupancyGrid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.gray;
        [DataMember] public bool FlipColors { get; set; } = true;
        [DataMember] public float ScaleZ { get; set; } = 1.0f;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class OccupancyGridListener : ListenerController
    {
        DisplayClickableNode node;
        OccupancyGridResource[] grid;

        public override ModuleData ModuleData { get; set; }

        public override TFFrame Frame => node.Parent;

        readonly OccupancyGridConfiguration config = new OccupancyGridConfiguration();
        public OccupancyGridConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                Colormap = value.Colormap;
                FlipColors = value.FlipColors;
                MaxQueueSize = value.MaxQueueSize;
                Tint = value.Tint;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                for (int i = 0; i < grid.Length; i++)
                {
                    grid[i].Visible = value;
                }
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                for (int i = 0; i < grid.Length; i++)
                {
                    grid[i].Colormap = value;
                }
            }
        }

        public bool FlipColors
        {
            get => config.FlipColors;
            set
            {
                config.FlipColors = value;
                for (int i = 0; i < grid.Length; i++)
                {
                    grid[i].IntensityBounds = value ? new Vector2(1, 0) : new Vector2(0, 1);
                }
            }
        }

        public float ScaleZ
        {
            get => config.ScaleZ;
            set
            {
                config.ScaleZ = value;
                node.transform.localScale = new Vector3(1, value, 1);
            }
        }

        public uint MaxQueueSize
        {
            get => config.MaxQueueSize;
            set
            {
                config.MaxQueueSize = value;
                if (Listener != null)
                {
                    Listener.MaxQueueSize = (int)value;
                }
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                for (int i = 0; i < grid.Length; i++)
                {
                    grid[i].OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                for (int i = 0; i < grid.Length; i++)
                {
                    grid[i].Tint = value;
                }
            }
        }

        void Awake()
        {
            node = DisplayClickableNode.Instantiate("Node");

            grid = new OccupancyGridResource[16];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = ResourcePool.GetOrCreate<OccupancyGridResource>(Resource.Displays.OccupancyGridResource, node.transform);
            }

            Config = new OccupancyGridConfiguration();
        }

        public override void StartListening()
        {
            Listener = new RosListener<OccupancyGrid>(config.Topic, Handler);
            Listener.MaxQueueSize = (int)MaxQueueSize;
            name = "OccupancyGrid:" + config.Topic;
            node.SetName($"[{config.Topic}]");
        }

        void Handler(OccupancyGrid msg)
        {
            if (msg.Data.Length != msg.Info.Width * msg.Info.Height)
            {
                Logger.Debug($"OccupancyGrid: Size {msg.Info.Width}x{msg.Info.Height} but data length {msg.Data.Length}");
                return;
            }
            if (float.IsNaN(msg.Info.Resolution))
            {
                Logger.Debug($"OccupancyGrid: NaN in header!");
                return;
            }
            if (msg.Info.Origin.HasNaN())
            {
                Logger.Debug($"OccupancyGrid: NaN in origin!");
                return;
            }

            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            Pose origin = msg.Info.Origin.Ros2Unity();

            int numCellsX = (int)msg.Info.Width;
            int numCellsY = (int)msg.Info.Height;
            float cellSize = msg.Info.Resolution;

            //float totalWidth = numCellsX * cellSize;
            //float totalHeight = numCellsY * cellSize;

            //origin.position += new Vector3(numCellsX, numCellsY, 0).Ros2Unity() * (cellSize / 2f);

            int i = 0;
            for (int v = 0; v < 4; v++)
            {
                for (int u = 0; u < 4; u++, i++)
                {
                    grid[i].NumCellsX = numCellsX;
                    grid[i].NumCellsY = numCellsY;
                    grid[i].CellSize = cellSize;
                    grid[i].transform.SetLocalPose(origin);

                    var rect = new OccupancyGridResource.Rect
                    (
                        xmin: u * numCellsX / 4,
                        xmax: (u + 1) * numCellsX / 4,
                        ymin: v * numCellsY / 4,
                        ymax: (v + 1) * numCellsY / 4
                    );
                    grid[i].SetOccupancy(msg.Data, rect);
                }
            }
        }


        public override void Stop()
        {
            base.Stop();
            if (grid != null)
            {
                for (int i = 0; i < grid.Length; i++)
                {
                    grid[i].Stop();
                    ResourcePool.Dispose(Resource.Displays.OccupancyGridResource, grid[i].gameObject);
                }
            }
            node.Stop();
            Destroy(node.gameObject);
        }
    }
}


