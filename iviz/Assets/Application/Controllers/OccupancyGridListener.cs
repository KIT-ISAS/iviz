using System;
using System.Collections.Generic;
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
        readonly List<OccupancyGridTextureResource> textures = new List<OccupancyGridTextureResource>();
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

                foreach (var texture in textures)
                {
                    texture.Colormap = value;
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

                foreach (var texture in textures)
                {
                    texture.FlipMinMax = value;
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

                foreach (var texture in textures)
                {
                    texture.Tint = value;
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
                grids[i].transform.SetLocalPose(Pose.identity);
            }

            Config = new OccupancyGridConfiguration();
        }

        public override void StartListening()
        {
            Listener = new Listener<OccupancyGrid>(config.Topic, Handler) {MaxQueueSize = (int) MaxQueueSize};
        }

        void Handler(OccupancyGrid msg)
        {
            if (grids != null && grids.Any(grid => grid.IsProcessing))
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
            node.Transform.SetLocalPose(origin);

            //SetCubes(msg);
            SetTextures(msg);
        }

        void SetCubes(OccupancyGrid msg)
        {
            int numCellsX = (int) msg.Info.Width;
            int numCellsY = (int) msg.Info.Height;
            float cellSize = msg.Info.Resolution;
            
            int i = 0;
            for (int v = 0; v < 4; v++)
            {
                for (int u = 0; u < 4; u++, i++)
                {
                    OccupancyGridResource grid = grids[i];
                    grid.NumCellsX = numCellsX;
                    grid.NumCellsY = numCellsY;
                    grid.CellSize = cellSize;

                    var rect = new OccupancyGridResource.Rect
                    (
                        xMin: u * numCellsX / 4,
                        xMax: (u + 1) * numCellsX / 4,
                        yMin: v * numCellsY / 4,
                        yMax: (v + 1) * numCellsY / 4
                    );
                    Task.Run(() => grid.SetOccupancy(msg.Data, rect));
                }
            }

            ScaleZ = ScaleZ;            
        }

        void SetTextures(OccupancyGrid msg)
        {
            int numCellsX = (int) msg.Info.Width;
            int numCellsY = (int) msg.Info.Height;
            float cellSize = msg.Info.Resolution;
            lastCellSize = cellSize;
            
            int expectedWidth = numCellsX / 256 + 1;
            int expectedHeight = numCellsY / 256 + 1;
            int expectedSize = expectedHeight * expectedWidth;

            if (expectedSize != textures.Count)
            {
                if (expectedSize > textures.Count)
                {
                    for (int j = textures.Count; j < expectedSize; j++)
                    {
                        textures.Add(ResourcePool.GetOrCreateDisplay<OccupancyGridTextureResource>(node.transform));
                    }

                    Colormap = Colormap;
                }
                else
                {
                    for (int j = expectedSize; j < textures.Count; j++)
                    {
                        ResourcePool.DisposeDisplay(textures[j]);
                        textures[j] = null;
                    }
                    
                    textures.RemoveRange(expectedSize, textures.Count - expectedSize);
                }
            }

            int i = 0;
            for (int v = 0; v < expectedHeight; v++)
            {
                for (int u = 0; u < expectedWidth; u++, i++)
                {
                    int xMin = u * 256;
                    int xMax = Math.Min(xMin + 256, numCellsX);
                    int yMin = v * 256;
                    int yMax = Math.Min(yMin + 256, numCellsY);
                    OccupancyGridResource.Rect rect = new OccupancyGridResource.Rect(xMin, xMax, yMin, yMax);
                    textures[i].Set(msg.Data, cellSize, numCellsX, numCellsY, rect);
                }
            }            
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