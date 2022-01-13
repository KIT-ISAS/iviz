#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
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
        readonly List<OccupancyGridTextureResource> textureTiles = new();

        OccupancyGridResource[] gridTiles = Array.Empty<OccupancyGridResource>();
        
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
                       $"{cellSize.ToString("#,0.###", BuiltIns.Culture)} m/cell</b>\n" +
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
                Tint = value.Tint;
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
            get => config.Tint;
            set
            {
                config.Tint = value;
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
            cubeNode = FrameNode.Instantiate("OccupancyGrid Cube Node");
            textureNode = FrameNode.Instantiate("OccupancyGrid Texture Node");

            Config = config ?? new OccupancyGridConfiguration
            {
                Topic = topic,
            };

            Listener = new Listener<OccupancyGrid>(Config.Topic, Handler);
        }

        void Handler(OccupancyGrid msg)
        {
            if (IsProcessing)
            {
                msg.Data.TryReturn();
                return;
            }

            if (msg.Data.Length != msg.Info.Width * msg.Info.Height)
            {
                RosLogger.Debug($"{this}: Size {msg.Info.Width.ToString()}x{msg.Info.Height.ToString()} " +
                                $"does not match data length {msg.Data.Length.ToString()}");
                msg.Data.TryReturn();
                return;
            }

            if (float.IsNaN(msg.Info.Resolution))
            {
                RosLogger.Debug($"{this}: NaN in header!");
                msg.Data.TryReturn();
                return;
            }

            cubeNode.AttachTo(msg.Header);
            textureNode.AttachTo(msg.Header);

            if (msg.Info.Origin.IsInvalid())
            {
                RosLogger.Debug($"{this}: NaN in origin!");
                msg.Data.TryReturn();
                return;
            }

            var origin = msg.Info.Origin.Ros2Unity();
            Pose validatedOrigin;
            if (!origin.IsUsable())
            {
                RosLogger.Error($"{this}: Cannot use ({origin.position.x.ToString(BuiltIns.Culture)}, " +
                                $"{origin.position.y.ToString(BuiltIns.Culture)}, " +
                                $"{origin.position.z.ToString(BuiltIns.Culture)}) " +
                                "as position. Values too large!");
                validatedOrigin = Pose.identity;
            }
            else
            {
                validatedOrigin = origin;
            }

            numCellsX = (int)msg.Info.Width;
            numCellsY = (int)msg.Info.Height;
            cellSize = msg.Info.Resolution;

            var tasks = new List<Task>();

            try
            {
                IsProcessing = true;
                if (CubesVisible)
                {
                    tasks.AddRange(SetCubes(msg.Data, validatedOrigin));
                }

                if (TextureVisible)
                {
                    tasks.AddRange(SetTextures(msg.Data, validatedOrigin));
                }
            }
            finally
            {
                AwaitAndReset();
            }

            async void AwaitAndReset()
            {
                await tasks.WhenAll().AwaitNoThrow(this);
                msg.Data.TryReturn();
                IsProcessing = false;
            }
        }

        IEnumerable<Task> SetCubes(Memory<sbyte> data, Pose pose)
        {
            var tasks = new List<Task>();

            if (gridTiles.Length != 16)
            {
                gridTiles = new OccupancyGridResource[16];
                foreach (int j in ..16)
                {
                    var resource = ResourcePool.Rent<OccupancyGridResource>(
                        Resource.Displays.OccupancyGridResource,
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

                    var rect = new OccupancyGridResource.Rect
                    (
                        xMin: u * numCellsX / 4,
                        xMax: (u + 1) * numCellsX / 4,
                        yMin: v * numCellsY / 4,
                        yMax: (v + 1) * numCellsY / 4
                    );

                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            grid.SetOccupancy(data.Span, rect, pose);
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning(e);
                        }
                    }));
                }
            }


            ScaleZ = ScaleZ;
            return tasks; 
        }

        IEnumerable<Task> SetTextures(Memory<sbyte> data, Pose pose)
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
                        var resource = ResourcePool.RentDisplay<OccupancyGridTextureResource>(textureNode.Transform);
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
                    int xMax = Math.Min(xMin + MaxTileSize, numCellsX);
                    int yMin = v * MaxTileSize;
                    int yMax = Math.Min(yMin + MaxTileSize, numCellsY);

                    var rect = new OccupancyGridResource.Rect(xMin, xMax, yMin, yMax);

                    var texture = textureTiles[i++];
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            texture.Set(data.Span, cellSize, numCellsX, rect, pose);
                        }
                        catch (Exception e)
                        {
                            RosLogger.Error($"{this}: Error processing occupancy grid", e);
                        }
                    }));
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