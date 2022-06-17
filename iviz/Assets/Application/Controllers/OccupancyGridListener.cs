#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.NavMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using Nito.AsyncEx;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class OccupancyGridListener : ListenerController
    {
        const int MaxTileSize = 256;

        readonly FrameNode cubeNode;
        readonly FrameNode textureNode;
        readonly List<OccupancyGridTextureDisplay> textureTiles = new();

        OccupancyGridDisplay[] gridTiles = Array.Empty<OccupancyGridDisplay>();

        int numCellsX;
        int numCellsY;
        float cellSize;
        bool isProcessing;

        public override TfFrame? Frame => cubeNode.Parent;

        bool IsProcessing
        {
            get => isProcessing;
            set
            {
                isProcessing = value;
                Listener.SetPause(value);
            }
        }

        public string Description
        {
            get
            {
                int numValid;
                if (TextureVisible)
                {
                    numValid = textureTiles.Sum(texture => texture.NumValidValues);
                }
                else if (CubesVisible)
                {
                    numValid = gridTiles.Sum(grid => grid.NumValidValues);
                }
                else
                {
                    numValid = 0;
                }

                return $"<b>{numCellsX.ToString("N0", BuiltIns.Culture)}x" +
                       $"{numCellsY.ToString("N0", BuiltIns.Culture)} cells | " +
                       $"{UnityUtils.FormatFloat(cellSize)} m/cell</b>\n" +
                       $"{numValid.ToString("N0", BuiltIns.Culture)} valid";
            }
        }

        readonly OccupancyGridConfiguration config = new();

        public OccupancyGridConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                Colormap = value.Colormap;
                FlipMinMax = value.FlipMinMax;
                ScaleZ = config.ScaleZ;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint.ToUnity();
                CubesVisible = value.CubesVisible;
                TextureVisible = value.TextureVisible;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                foreach (var grid in gridTiles)
                {
                    grid.Visible = value;
                }

                foreach (var texture in textureTiles)
                {
                    texture.Visible = value;
                }
            }
        }

        public ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                foreach (var grid in gridTiles)
                {
                    grid.Colormap = value;
                }

                foreach (var texture in textureTiles)
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
                foreach (var grid in gridTiles)
                {
                    grid.FlipMinMax = value;
                }

                foreach (var texture in textureTiles)
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

                float yScale = cellSize.ApproximatelyZero() ? 1 : value / cellSize;
                cubeNode.Transform.localScale = new Vector3(1, yScale, 1);
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;
                foreach (var grid in gridTiles)
                {
                    grid.OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            get => config.Tint.ToUnity();
            set
            {
                config.Tint = value.ToRos();
                foreach (var grid in gridTiles)
                {
                    grid.Tint = value;
                }

                foreach (var texture in textureTiles)
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
                cubeNode.Visible = value;
            }
        }

        public bool TextureVisible
        {
            get => config.TextureVisible;
            set
            {
                config.TextureVisible = value;
                textureNode.Visible = value;
            }
        }

        public override IListener Listener { get; }

        public OccupancyGridListener(OccupancyGridConfiguration? config, string topic)
        {
            cubeNode = new FrameNode("OccupancyGrid Cube Node");
            textureNode = new FrameNode("OccupancyGrid Texture Node");

            Config = config ?? new OccupancyGridConfiguration
            {
                Topic = topic,
            };

            Listener = new Listener<OccupancyGrid>(Config.Topic, Handle);
        }

        void Handle(OccupancyGrid msg)
        {
            if (IsProcessing)
            {
                return;
            }

            sbyte[] data = msg.Data;
            var info = msg.Info;

            checked
            {
                if (data.Length != info.Width * info.Height)
                {
                    RosLogger.Error($"{this}: Size {info.Width.ToString()}x{info.Height.ToString()} " +
                                    $"does not match data length {data.Length.ToString()}");
                    return;
                }
            }

            if (info.Resolution.IsInvalid() || info.Resolution < 0)
            {
                RosLogger.Error($"{this}: {nameof(MapMetaData)} has invalid values");
                return;
            }

            if (info.Origin.IsInvalid())
            {
                RosLogger.Error($"{this}: Origin has invalid values");
                return;
            }

            int maxGridSize = Settings.MaxTextureSize;
            if (info.Width > maxGridSize || info.Height > maxGridSize)
            {
                RosLogger.Error($"{this}: Gridmap is too large! Iviz only supports gridmap sizes " +
                                $"up to {maxGridSize.ToString()}");
                return;
            }

            var origin = info.Origin.Ros2Unity();
            Pose validatedOrigin;
            if (!origin.IsUsable())
            {
                RosLogger.Warn($"{this}: Cannot use ({origin.position.x.ToString(BuiltIns.Culture)}, " +
                               $"{origin.position.y.ToString(BuiltIns.Culture)}, " +
                               $"{origin.position.z.ToString(BuiltIns.Culture)}) " +
                               "as position. Values too large!");
                validatedOrigin = Pose.identity;
            }
            else
            {
                validatedOrigin = origin;
            }

            cubeNode.AttachTo(msg.Header);
            textureNode.AttachTo(msg.Header);

            numCellsX = (int)info.Width;
            numCellsY = (int)info.Height;
            cellSize = info.Resolution;

            var tasks = new List<Task>();

            try
            {
                IsProcessing = true;
                if (CubesVisible)
                {
                    tasks.AddRange(SetCubes(data, validatedOrigin));
                }

                if (TextureVisible)
                {
                    tasks.AddRange(SetTextures(data, validatedOrigin));
                }
            }
            finally
            {
                AwaitAndReset();
            }

            async void AwaitAndReset()
            {
                await tasks.WhenAll().AwaitNoThrow(this);
                IsProcessing = false;
            }
        }

        IEnumerable<Task> SetCubes(sbyte[] data, Pose pose)
        {
            var tasks = new List<Task>();

            if (gridTiles.Length != 16)
            {
                gridTiles = new OccupancyGridDisplay[16];
                foreach (int j in ..16)
                {
                    var resource = ResourcePool.Rent<OccupancyGridDisplay>(
                        Resource.Displays.OccupancyGridDisplay,
                        cubeNode.Transform);
                    resource.gameObject.name = $"OccupancyGridResource-{j.ToString()}";
                    resource.Transform.SetLocalPose(Pose.identity);
                    resource.Colormap = Colormap;
                    resource.FlipMinMax = FlipMinMax;
                    gridTiles[j] = resource;
                }
            }

            int i = 0;
            foreach (int v in ..4)
            {
                foreach (int u in ..4)
                {
                    var grid = gridTiles[i++];
                    grid.NumCellsX = numCellsX;
                    grid.NumCellsY = numCellsY;
                    grid.CellSize = cellSize;

                    var rect = new RectInt
                    {
                        xMin = u * numCellsX / 4,
                        xMax = (u + 1) * numCellsX / 4,
                        yMin = v * numCellsY / 4,
                        yMax = (v + 1) * numCellsY / 4
                    };

                    tasks.Add(Task.Run(ProcessCube));

                    void ProcessCube()
                    {
                        try
                        {
                            grid.SetOccupancy(data, rect, pose);
                        }
                        catch (Exception e)
                        {
                            RosLogger.Error($"{this}: Error processing occupancy grid cube", e);
                        }                        
                    }
                }
            }


            ScaleZ = ScaleZ;
            return tasks;
        }

        IEnumerable<Task> SetTextures(sbyte[] data, Pose pose)
        {
            var tasks = new List<Task>();

            int tileSizeX = (numCellsX + MaxTileSize - 1) / MaxTileSize;
            int tileSizeY = (numCellsY + MaxTileSize - 1) / MaxTileSize;
            int tileTotalSize = tileSizeY * tileSizeX;

            if (tileTotalSize != textureTiles.Count)
            {
                if (tileTotalSize > textureTiles.Count)
                {
                    foreach (int j in textureTiles.Count..tileTotalSize)
                    {
                        var resource = ResourcePool.RentDisplay<OccupancyGridTextureDisplay>(textureNode.Transform);
                        resource.gameObject.name = $"OccupancyGridTextureResource-{j.ToString()}";
                        resource.Visible = Visible;
                        resource.Colormap = Colormap;
                        resource.FlipMinMax = FlipMinMax;
                        resource.Title = Listener.Topic;
                        textureTiles.Add(resource);
                    }
                }
                else
                {
                    foreach (var textureTile in textureTiles.Skip(tileTotalSize))
                    {
                        textureTile.ReturnToPool();
                    }

                    textureTiles.RemoveRange(tileTotalSize, textureTiles.Count - tileTotalSize);
                }
            }

            int i = 0;
            foreach (int v in ..tileSizeY)
            {
                foreach (int u in ..tileSizeX)
                {
                    int xMin = u * MaxTileSize;
                    int xMax = Mathf.Min(xMin + MaxTileSize, numCellsX);
                    int yMin = v * MaxTileSize;
                    int yMax = Mathf.Min(yMin + MaxTileSize, numCellsY);

                    var rect = new RectInt
                    {
                        xMin = xMin,
                        xMax = xMax,
                        yMin = yMin,
                        yMax = yMax
                    };

                    var texture = textureTiles[i++];
                    tasks.Add(Task.Run(ProcessTexture));

                    void ProcessTexture()
                    {
                        try
                        {
                            texture.Set(data, cellSize, numCellsX, rect, pose);
                        }
                        catch (Exception e)
                        {
                            RosLogger.Error($"{this}: Error processing occupancy grid texture", e);
                        }
                    }
                }
            }

            return tasks;
        }


        public override void Dispose()
        {
            base.Dispose();
            foreach (var grid in gridTiles)
            {
                grid.ReturnToPool();
            }

            foreach (var texture in textureTiles)
            {
                texture.ReturnToPool();
            }

            cubeNode.Dispose();
            textureNode.Dispose();
        }
    }
}