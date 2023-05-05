#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;
using Unity.Mathematics;
using UnityEngine;

namespace Iviz.Displays.Helpers
{
    public sealed class PointCloudProcessor
    {
        readonly List<string> fieldNames = new() { "x", "y", "z" };
        readonly FrameNode node;
        readonly PointListDisplay pointCloud;
        readonly MeshListDisplay meshCloud;

        string intensityChannel = "";
        PointCloudType pointCloudType;
        PointCloud2? lastMessage;
        float4[] pointBuffer = Array.Empty<float4>();

        float minIntensity, maxIntensity;

        bool overrideMinMax;
        InterlockedBoolean isProcessing;

        public event Action<bool>? IsProcessingChanged;

        public string Name
        {
            set => node.Name = value;
        }

        public Vector2 MeasuredIntensityBounds =>
            pointCloudType == PointCloudType.Points
                ? pointCloud.MeasuredIntensityBounds
                : meshCloud.MeasuredIntensityBounds;

        public int NumValidPoints { get; private set; }

        public TfFrame? Frame => node.Parent;

        public bool Visible
        {
            set
            {
                pointCloud.Visible = value;
                meshCloud.Visible = value;
            }
        }

        public string IntensityChannel
        {
            set
            {
                intensityChannel = value;
                ReHandle();
            }
        }

        public float PointSize
        {
            set
            {
                pointCloud.ElementScale = value;
                meshCloud.ElementScale = value;
            }
        }

        public ColormapId Colormap
        {
            set
            {
                pointCloud.Colormap = value;
                meshCloud.Colormap = value;
            }
        }

        public bool OverrideMinMax
        {
            set
            {
                overrideMinMax = value;
                pointCloud.OverrideIntensityBounds = value;
                pointCloud.IntensityBounds = value
                    ? new Vector2(minIntensity, maxIntensity)
                    : MeasuredIntensityBounds;
                meshCloud.OverrideIntensityBounds = value;
                meshCloud.IntensityBounds = value
                    ? new Vector2(minIntensity, maxIntensity)
                    : MeasuredIntensityBounds;
            }
        }

        public bool FlipMinMax
        {
            set
            {
                pointCloud.FlipMinMax = value;
                meshCloud.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            set
            {
                minIntensity = value;
                if (overrideMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(minIntensity, maxIntensity);
                    meshCloud.IntensityBounds = new Vector2(minIntensity, maxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            set
            {
                maxIntensity = value;
                if (overrideMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(minIntensity, maxIntensity);
                    meshCloud.IntensityBounds = new Vector2(minIntensity, maxIntensity);
                }
            }
        }

        public PointCloudType PointCloudType
        {
            set
            {
                if (pointCloudType == value)
                {
                    return;
                }

                pointCloudType = value;
                pointCloud.Reset();
                meshCloud.Reset();

                switch (value)
                {
                    case PointCloudType.Cubes:
                        meshCloud.MeshResource = Resource.Displays.Cube;
                        break;
                    case PointCloudType.Spheres:
                        meshCloud.MeshResource = Resource.Displays.Sphere;
                        break;
                }

                ReHandle();
            }
        }

        public bool IsIntensityUsed => pointCloud.UseColormap;

        public IEnumerable<string> FieldNames => fieldNames;

        public PointCloudProcessor()
        {
            node = new FrameNode(nameof(PointCloudProcessor));
            pointCloud = ResourcePool.RentDisplay<PointListDisplay>(node.Transform);
            meshCloud = ResourcePool.RentDisplay<MeshListDisplay>(node.Transform);
            meshCloud.EnableShadows = false;
            isProcessing.Changed = IsProcessingChanged;
        }
        
        public bool Handle(PointCloud2 msg, IRosConnection? _ = null)
        {
            if (!isProcessing.TrySet())
            {
                return false;
            }

            msg.IncreaseRefCount();
            Task.Run(() =>
            {
                try
                {
                    if (!ProcessMessage(msg))
                    {
                        isProcessing.Reset();
                    }
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{ToString()}: Error handling point cloud", e);
                    isProcessing.Reset();
                }
                finally
                {
                    lastMessage?.Dispose();
                    lastMessage = msg;
                }
            });

            return true;
        }

        void ReHandle()
        {
            if (lastMessage is { } msg)
            {
                Handle(msg);
            }
        }

        bool ProcessMessage(PointCloud2 msg)
        {
            if (!node.IsAlive)
            {
                // we're dead
                return false;
            }

            if (!PointCloudHelper.FieldsEqual(fieldNames, msg.Fields))
            {
                fieldNames.Clear();
                foreach (var field in msg.Fields)
                {
                    fieldNames.Add(field.Name);
                }
            }


            bool rgbaHint;
            int pointBufferLength;

            try
            {
                if (!PointCloudHelper.GeneratePointBuffer(ref pointBuffer, msg, intensityChannel,
                        out rgbaHint,
                        out pointBufferLength))
                {
                    return false;
                }
            }
            catch (OverflowException e)
            {
                PointCloudHelperException.Throw("Validation of point cloud size failed", e);
                return false; // unreachable
            }

            var pointBufferToUse = new ReadOnlyMemory<float4>(pointBuffer, 0, pointBufferLength);
            var header = msg.Header;
            bool useColormap = !rgbaHint;

            GameThread.PostInListenerQueue(() =>
            {
                try
                {
                    ProcessInListenerQueue(pointBufferToUse.Span, header, useColormap);
                }
                finally
                {
                    isProcessing.Reset();
                }
            });

            return true;
        }

        void ProcessInListenerQueue(ReadOnlySpan<float4> pointBufferToUse, in Header header, bool useColormap)
        {
            if (!node.IsAlive)
            {
                // we're dead
                return;
            }

            node.AttachTo(header);

            int numValidPoints = pointBufferToUse.Length;

            NumValidPoints = numValidPoints;

            if (numValidPoints == 0)
            {
                pointCloud.Reset();
                meshCloud.Reset();
                return;
            }

            if (pointCloudType == PointCloudType.Points)
            {
                pointCloud.UseColormap = useColormap;
                pointCloud.Set(pointBufferToUse);
                meshCloud.Reset();
            }
            else
            {
                meshCloud.UseColormap = useColormap;
                meshCloud.Set(pointBufferToUse);
                pointCloud.Reset();
            }
        }

        public void Dispose()
        {
            lastMessage?.Dispose();
            lastMessage = null;

            pointCloud.ReturnToPool();
            meshCloud.ReturnToPool();

            node.Dispose();
            pointBuffer = Array.Empty<float4>();

            IsProcessingChanged = null;
        }

        public override string ToString() => $"[{nameof(PointCloudProcessor)} '{node.Name}']";
    }

    public struct InterlockedBoolean
    {
        int value;
        
        public Action<bool>? Changed;

        public bool TrySet()
        {
            bool result = Interlocked.CompareExchange(ref value, 1, 0) == 0;
            if (result)
            {
                Changed?.Invoke(true);
            }

            return result;
        }

        public void Reset()
        {
            value = 0;
            Changed?.Invoke(false);
        }
    }
}