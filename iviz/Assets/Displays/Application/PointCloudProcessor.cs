#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays.PointCloudHelpers;
using Iviz.Msgs;
using Iviz.Msgs.SensorMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Resources;
using Iviz.Roslib.Utils;
using Unity.Mathematics;
using UnityEngine;
using Time = Iviz.Msgs.StdMsgs.Time;

namespace Iviz.Displays.Helpers
{
    public sealed class PointCloudProcessor
    {
        readonly List<string> fieldNames = new() { "x", "y", "z" };
        readonly PointListDisplay pointCloud;
        readonly MeshListDisplay meshCloud;
        readonly SelfClearingBuffer pointBuffer = new();

        string intensityChannel = "";
        PointCloudType pointCloudType;
        PointCloud2? lastMessage;

        float minIntensity, maxIntensity;

        bool overrideMinMax;
        InterlockedBoolean isProcessing;

        public event Action<bool>? IsProcessingChanged;

        public FrameNode Node { get; }

        public Vector2 MeasuredIntensityBounds =>
            pointCloudType == PointCloudType.Points
                ? pointCloud.MeasuredIntensityBounds
                : meshCloud.MeasuredIntensityBounds;

        public int NumValidPoints { get; private set; }

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
                meshCloud.OverrideIntensityBounds = value;

                var intensityBounds = value
                    ? new Vector2(minIntensity, maxIntensity)
                    : MeasuredIntensityBounds;
                pointCloud.IntensityBounds = intensityBounds;
                meshCloud.IntensityBounds = intensityBounds;
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
                if (!overrideMinMax)
                {
                    return;
                }

                var intensityBounds = new Vector2(minIntensity, maxIntensity);
                pointCloud.IntensityBounds = intensityBounds;
                meshCloud.IntensityBounds = intensityBounds;
            }
        }

        public float MaxIntensity
        {
            set
            {
                maxIntensity = value;
                if (!overrideMinMax)
                {
                    return;
                }

                var intensityBounds = new Vector2(minIntensity, maxIntensity);
                pointCloud.IntensityBounds = intensityBounds;
                meshCloud.IntensityBounds = intensityBounds;
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

        public PointCloudProcessor(Transform? parent = null)
        {
            Node = new FrameNode(nameof(PointCloudProcessor));
            pointCloud = ResourcePool.RentDisplay<PointListDisplay>(Node.Transform);
            pointCloud.UseRosSwizzle = true;
            meshCloud = ResourcePool.RentDisplay<MeshListDisplay>(Node.Transform);
            meshCloud.EnableShadows = false;
            meshCloud.UseRosSwizzle = true;
            isProcessing.Changed = IsProcessingChanged;

            if (parent != null)
            {
                Node.Transform.SetParentLocal(parent);
            }
        }

        public void Reset()
        {
            pointCloud.Reset();
            meshCloud.Reset();
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
            if (!Node.IsAlive)
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
            ReadOnlyMemory<float4> pointBufferToUse;

            try
            {
                if (!PointCloudHelper.GeneratePointBuffer(pointBuffer, msg, intensityChannel,
                        out rgbaHint,
                        out pointBufferToUse))
                {
                    return false;
                }
            }
            catch (OverflowException e)
            {
                PointCloudHelperException.Throw("Validation of point cloud size failed", e);
                return false; // unreachable
            }

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
            if (!Node.IsAlive)
            {
                // we're dead
                return;
            }

            Node.AttachTo(header);

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

            Node.Dispose();
            pointBuffer.Dispose();

            IsProcessingChanged = null;
        }

        public bool TryGetLastMessage([NotNullWhen(true)] out PointCloud2? msg)
        {
            msg = lastMessage;
            return msg != null;
        }

        public override string ToString() => $"[{nameof(PointCloudProcessor)} '{Node.Name}']";
    }
}