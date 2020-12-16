using System;
using System.Collections.Generic;
using System.Linq;
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
        [DataMember] public bool CubesVisible { get; set; } = false;
        [DataMember] public bool TextureVisible { get; set; } = true;
        [DataMember] public float ScaleZ { get; set; } = 0.5f;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class OccupancyGridListener : ListenerController
    {
        const int MaxTextureSize = 256;

        readonly FrameNode cubeNode;
        readonly FrameNode textureNode;

        [NotNull] OccupancyGridResource[] grids = Array.Empty<OccupancyGridResource>();
        readonly List<OccupancyGridTextureResource> textures = new List<OccupancyGridTextureResource>();

        int numCellsX;
        int numCellsY;
        float cellSize;

        public override IModuleData ModuleData { get; }

        public override TfFrame Frame => cubeNode.Parent;

        [NotNull]
        public string Description
        {
            get
            {
                int numValid;
                if (TextureVisible)
                {
                    numValid = textures.Sum(texture => texture.NumValidValues);
                }
                else if (CubesVisible)
                {
                    numValid = grids.Sum(grid => grid.NumValidValues);
                }
                else
                {
                    numValid = 0;
                }

                return $"{numCellsX}x{numCellsY} cells | {cellSize} m/cell\n{numValid} valid";
            }
        }

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
                CubesVisible = value.CubesVisible;
                TextureVisible = value.TextureVisible;
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

                float yScale = Mathf.Approximately(cellSize, 0) ? 1 : value / cellSize;
                cubeNode.transform.localScale = new Vector3(1, yScale, 1);
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

        public bool CubesVisible
        {
            get => config.CubesVisible;
            set
            {
                config.CubesVisible = value;
                cubeNode.gameObject.SetActive(value);
            }
        }

        public bool TextureVisible
        {
            get => config.TextureVisible;
            set
            {
                config.TextureVisible = value;
                textureNode.gameObject.SetActive(value);  
            } 
        }

        public OccupancyGridListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            cubeNode = FrameNode.Instantiate("OccupancyGrid Cube Node");
            textureNode = FrameNode.Instantiate("OccupancyGrid Texture Node");

            Config = new OccupancyGridConfiguration();
        }

        public override void StartListening()
        {
            Listener = new Listener<OccupancyGrid>(config.Topic, Handler) {MaxQueueSize = (int) MaxQueueSize};
        }

        void Handler(OccupancyGrid msg)
        {
            if (grids.Any(grid => grid.IsProcessing)
                || textures.Any(texture => texture.IsProcessing))
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

            cubeNode.AttachTo(msg.Header.FrameId, msg.Header.Stamp);
            textureNode.AttachTo(msg.Header.FrameId, msg.Header.Stamp);

            Pose origin = msg.Info.Origin.Ros2Unity();
            cubeNode.Transform.SetLocalPose(origin);
            textureNode.Transform.SetLocalPose(origin);
            textureNode.Transform.position += new Vector3(0, 0.001f, 0);

            numCellsX = (int) msg.Info.Width;
            numCellsY = (int) msg.Info.Height;
            cellSize = msg.Info.Resolution;

            if (CubesVisible)
            {
                SetCubes(msg);
            }

            if (TextureVisible)
            {
                SetTextures(msg);
            }
        }

        void SetCubes([NotNull] OccupancyGrid msg)
        {
            if (grids.Length != 16)
            {
                grids = new OccupancyGridResource[16];
                for (int j = 0; j < grids.Length; j++)
                {
                    grids[j] = ResourcePool.GetOrCreate<OccupancyGridResource>(Resource.Displays.OccupancyGridResource,
                        cubeNode.transform);
                    grids[j].transform.SetLocalPose(Pose.identity);
                    grids[j].Colormap = Colormap;
                    grids[j].FlipMinMax = FlipMinMax;
                }
            }

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
                    Task.Run(() =>
                    {
                        try
                        {
                            grid.SetOccupancy(msg.Data, rect);
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning(e);
                        }
                    });
                }
            }

            ScaleZ = ScaleZ;
        }

        void SetTextures([NotNull] OccupancyGrid msg)
        {
            int expectedWidth = numCellsX / 256 + 1;
            int expectedHeight = numCellsY / 256 + 1;
            int expectedSize = expectedHeight * expectedWidth;

            if (expectedSize != textures.Count)
            {
                if (expectedSize > textures.Count)
                {
                    for (int j = textures.Count; j < expectedSize; j++)
                    {
                        textures.Add(
                            ResourcePool.GetOrCreateDisplay<OccupancyGridTextureResource>(textureNode.transform));
                        textures[j].Colormap = Colormap;
                        textures[j].FlipMinMax = FlipMinMax;
                    }
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
                    int xMin = u * MaxTextureSize;
                    int xMax = Math.Min(xMin + MaxTextureSize, numCellsX);
                    int yMin = v * MaxTextureSize;
                    int yMax = Math.Min(yMin + MaxTextureSize, numCellsY);
                    OccupancyGridResource.Rect rect = new OccupancyGridResource.Rect(xMin, xMax, yMin, yMax);

                    var texture = textures[i];
                    Task.Run(() =>
                    {
                        try
                        {
                            texture.Set(msg.Data, cellSize, numCellsX, numCellsY, rect);
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning(e);
                        }
                    });
                }
            }
        }


        public override void StopController()
        {
            base.StopController();
            foreach (var grid in grids)
            {
                grid.DisposeDisplay();
            }

            foreach (var texture in textures)
            {
                texture.DisposeDisplay();
            }

            cubeNode.Stop();
            textureNode.Stop();
            UnityEngine.Object.Destroy(cubeNode.gameObject);
            UnityEngine.Object.Destroy(textureNode.gameObject);
        }
    }
}