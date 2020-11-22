using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class OccupancyGridConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.OccupancyGrid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.gray;
        [DataMember] public bool FlipMinMax { get; set; } = true;
        [DataMember] public float ScaleZ { get; set; } = 0.5f;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class OccupancyGridListener : ListenerController
    {
        readonly FrameNode node;
        readonly OccupancyGridResource[] grids;
        float lastCellSize;

        public override IModuleData ModuleData { get; }

        public override TfFrame Frame => node.Parent;

        readonly OccupancyGridConfiguration config = new OccupancyGridConfiguration();

        public OccupancyGridConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                Colormap = value.Colormap;
                FlipMinMax = value.FlipMinMax;
                ScaleZ = config.ScaleZ;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint;
                MaxQueueSize = value.MaxQueueSize;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                foreach (var grid in grids)
                {
                    grid.Visible = value;
                }
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                foreach (var grid in grids)
                {
                    grid.Colormap = value;
                }
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                foreach (var grid in grids)
                {
                    grid.FlipMinMax = value;
                }
            }
        }

        public float ScaleZ
        {
            get => config.ScaleZ;
            set
            {
                config.ScaleZ = value;

                float yScale = Mathf.Approximately(lastCellSize, 0) ? 1 : value / lastCellSize;
                node.transform.localScale = new Vector3(1, yScale, 1);
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
                    Listener.MaxQueueSize = (int) value;
                }
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                foreach (var grid in grids)
                {
                    grid.OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;
                foreach (var grid in grids)
                {
                    grid.Tint = value;
                }
            }
        }

        public OccupancyGridListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            node = FrameNode.Instantiate("Node");

            grids = new OccupancyGridResource[16];
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i] = ResourcePool.GetOrCreate<OccupancyGridResource>(Resource.Displays.OccupancyGridResource,
                    node.transform);
            }

            Config = new OccupancyGridConfiguration();
        }

        public override void StartListening()
        {
            Listener = new Listener<OccupancyGrid>(config.Topic, Handler) {MaxQueueSize = (int) MaxQueueSize};
        }

        void Handler(OccupancyGrid msg)
        {
            if (grids.Any(x => x.IsProcessing))
            {
                return;
            }

            if (msg.Data.Length != msg.Info.Width * msg.Info.Height)
            {
                Logger.Debug(
                    $"{this}: Size {msg.Info.Width}x{msg.Info.Height} but data length {msg.Data.Length}");
                return;
            }

            if (float.IsNaN(msg.Info.Resolution))
            {
                Logger.Debug($"{this}: NaN in header!");
                return;
            }

            if (msg.Info.Origin.HasNaN())
            {
                Logger.Debug($"{this}: NaN in origin!");
                return;
            }

            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            Pose origin = msg.Info.Origin.Ros2Unity();

            int numCellsX = (int) msg.Info.Width;
            int numCellsY = (int) msg.Info.Height;
            float cellSize = msg.Info.Resolution;
            lastCellSize = cellSize;

            //float totalWidth = numCellsX * cellSize;
            //float totalHeight = numCellsY * cellSize;

            //origin.position += new Vector3(numCellsX, numCellsY, 0).Ros2Unity() * (cellSize / 2f);

            int i = 0;
            for (int v = 0; v < 4; v++)
            {
                for (int u = 0; u < 4; u++, i++)
                {
                    OccupancyGridResource grid = grids[i];
                    grid.NumCellsX = numCellsX;
                    grid.NumCellsY = numCellsY;
                    grid.CellSize = cellSize;
                    grid.transform.SetLocalPose(origin);

                    var rect = new OccupancyGridResource.Rect
                    (
                        xmin: u * numCellsX / 4,
                        xmax: (u + 1) * numCellsX / 4,
                        ymin: v * numCellsY / 4,
                        ymax: (v + 1) * numCellsY / 4
                    );
                    Task.Run(() => grid.SetOccupancy(msg.Data, rect));
                }
            }

            ScaleZ = ScaleZ;
        }


        public override void StopController()
        {
            base.StopController();
            if (grids != null)
            {
                foreach (var grid in grids)
                {
                    grid.DisposeDisplay();
                }
            }

            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}