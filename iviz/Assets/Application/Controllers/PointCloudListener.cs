using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    [DataContract]
    public class PointCloudConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string IntensityChannel { get; set; } = "y";
        [DataMember] public float PointSize { get; set; } = 0.03f;
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool ForceMinMax { get; set; }
        [DataMember] public float MinIntensity { get; set; }
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; }
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.Module Module => Resource.Module.PointCloud;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class PointCloudListener : ListenerController
    {
        static readonly PointField EmptyPointField = new PointField();

        readonly PointCloudConfiguration config = new PointCloudConfiguration();
        readonly List<string> fieldNames = new List<string> {"x", "y", "z"};
        readonly DisplayNode node;
        readonly PointListResource pointCloud;

        bool isProcessing;

        NativeList<float4> pointBuffer = new NativeList<float4>(Allocator.Persistent);

        public PointCloudListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));

            FieldNames = new ReadOnlyCollection<string>(fieldNames);

            node = SimpleDisplayNode.Instantiate("[PointCloudNode]");

            pointCloud = ResourcePool.GetOrCreateDisplay<PointListResource>(node.transform);

            Config = new PointCloudConfiguration();
        }

        public override IModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds { get; private set; }

        public int Size { get; private set; }

        public override TfFrame Frame => node.Parent;

        public PointCloudConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                Visible = value.Visible;
                IntensityChannel = value.IntensityChannel;
                PointSize = value.PointSize;
                Colormap = value.Colormap;
                ForceMinMax = value.ForceMinMax;
                MinIntensity = value.MinIntensity;
                MaxIntensity = value.MaxIntensity;
                FlipMinMax = value.FlipMinMax;
                MaxQueueSize = value.MaxQueueSize;
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                pointCloud.Visible = value;
            }
        }

        public string IntensityChannel
        {
            get => config.IntensityChannel;
            set => config.IntensityChannel = value;
        }

        public float PointSize
        {
            get => config.PointSize;
            set
            {
                config.PointSize = value;
                pointCloud.ElementScale = value;
            }
        }

        public Resource.ColormapId Colormap
        {
            get => config.Colormap;
            set
            {
                config.Colormap = value;
                pointCloud.Colormap = value;
            }
        }

        public bool ForceMinMax
        {
            get => config.ForceMinMax;
            set
            {
                config.ForceMinMax = value;
                if (config.ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
                else
                {
                    pointCloud.IntensityBounds = MeasuredIntensityBounds;
                }
            }
        }

        public bool FlipMinMax
        {
            get => config.FlipMinMax;
            set
            {
                config.FlipMinMax = value;
                pointCloud.FlipMinMax = value;
            }
        }


        public float MinIntensity
        {
            get => config.MinIntensity;
            set
            {
                config.MinIntensity = value;
                if (config.ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public float MaxIntensity
        {
            get => config.MaxIntensity;
            set
            {
                config.MaxIntensity = value;
                if (config.ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }
            }
        }

        public bool IsIntensityUsed => pointCloud != null && pointCloud.UseColormap;

        uint MaxQueueSize
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

        public ReadOnlyCollection<string> FieldNames { get; }

        public override void StartListening()
        {
            Listener = new Listener<PointCloud2>(config.Topic, Handler) {MaxQueueSize = (int) MaxQueueSize};
            node.name = $"[{config.Topic}]";
        }

        static int FieldSizeFromType(int datatype)
        {
            switch (datatype)
            {
                case PointField.FLOAT64:
                    return 8;
                case PointField.FLOAT32:
                case PointField.INT32:
                case PointField.UINT32:
                    return 4;
                case PointField.INT16:
                case PointField.UINT16:
                    return 2;
                case PointField.INT8:
                case PointField.UINT8:
                    return 1;
                default:
                    return -1;
            }
        }

        bool Handler(PointCloud2 msg)
        {
            if (isProcessing)
            {
                return false;
            }

            isProcessing = true;
            Task.Run(() => ProcessMessage(msg));

            return true;
        }

        [ContractAnnotation("=> false, result:null; => true, result:notnull")]
        static bool TryGetField([NotNull] PointField[] fields, string name, out PointField result)
        {
            foreach (var field in fields)
            {
                if (field.Name != name)
                {
                    continue;
                }

                result = field;
                return true;
            }

            result = null;
            return false;
        }

        bool FieldsEqual([NotNull] PointField[] fields)
        {
            if (fieldNames.Count != fields.Length)
            {
                return false;
            }

            for (int i = 0; i < fieldNames.Count; i++)
            {
                if (fieldNames[i] != fields[i].Name)
                {
                    return false;
                }
            }

            return true;
        }

        void ProcessMessage([NotNull] PointCloud2 msg)
        {
            if (msg.PointStep < 3 * sizeof(float) ||
                msg.RowStep < msg.PointStep * msg.Width ||
                msg.Data.Length < msg.RowStep * msg.Height)
            {
                Logger.Info($"{this}: Invalid point cloud dimensions!");
                isProcessing = false;
                return;
            }

            if (!FieldsEqual(msg.Fields))
            {
                fieldNames.Clear();
                foreach (PointField field in msg.Fields)
                {
                    fieldNames.Add(field.Name);
                }
            }

            if (!TryGetField(msg.Fields, "x", out PointField xField) || xField.Datatype != PointField.FLOAT32 ||
                !TryGetField(msg.Fields, "y", out PointField yField) || yField.Datatype != PointField.FLOAT32 ||
                !TryGetField(msg.Fields, "z", out PointField zField) || zField.Datatype != PointField.FLOAT32)
            {
                Logger.Info($"{this}: Unsupported point cloud! Expected XYZ as floats.");
                isProcessing = false;
                return;
            }

            int xOffset = (int) xField.Offset;
            int yOffset = (int) yField.Offset;
            int zOffset = (int) zField.Offset;

            if (!TryGetField(msg.Fields, config.IntensityChannel, out PointField iField))
            {
                iField = EmptyPointField;
            }

            int iOffset = (int) iField.Offset;
            int iSize = FieldSizeFromType(iField.Datatype);
            if (iSize == -1 || msg.PointStep < iOffset + iSize)
            {
                Logger.Info($"{this}: Invalid or unsupported intensity field type!");
                isProcessing = false;
                return;
            }

            bool rgbaHint = iSize == 4 && (iField.Name == "rgb" || iField.Name == "rgba");

            int numPoints = (int) (msg.Width * msg.Height);

            pointBuffer.Clear();

            GeneratePointBuffer(msg, xOffset, yOffset, zOffset, iOffset, iField.Datatype, rgbaHint);

            var msgHeader = msg.Header;

            GameThread.Post(() =>
            {
                if (node.gameObject == null)
                {
                    // we're dead
                    return;
                }

                node.AttachTo(msgHeader.FrameId, msgHeader.Stamp);

                Size = numPoints;
                pointCloud.UseColormap = !rgbaHint;
                pointCloud.SetDirect(pointBuffer);

                //Debug.LogError("listener:"  + pointBuffer.Capacity);

                MeasuredIntensityBounds = pointCloud.IntensityBounds;
                if (ForceMinMax)
                {
                    pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                }

                isProcessing = false;
            });
        }

        void GeneratePointBuffer([NotNull] PointCloud2 msg, int xOffset, int yOffset, int zOffset, int iOffset,
            int iType,
            bool rgbaHint)
        {
            var xyzAligned = xOffset == 0 && yOffset == 4 && zOffset == 8;
            if (xyzAligned)
            {
                GeneratePointBufferXYZ(msg, iOffset, rgbaHint ? PointField.FLOAT32 : iType);
            }
            else
            {
                GeneratePointBufferSlow(msg, xOffset, yOffset, zOffset, iOffset, iType, rgbaHint);
            }
        }

        void GeneratePointBufferSlow([NotNull] PointCloud2 msg, int xOffset, int yOffset, int zOffset, int iOffset,
            int iType,
            bool rgbaHint)
        {
            var heightOffset = 0;
            var rowStep = (int) msg.RowStep;
            var pointStep = (int) msg.PointStep;

            Func<byte[], int, float> intensityFn;
            if (rgbaHint)
            {
                intensityFn = BitConverter.ToSingle;
            }
            else
            {
                switch (iType)
                {
                    case PointField.FLOAT32:
                        intensityFn = BitConverter.ToSingle;
                        break;
                    case PointField.FLOAT64:
                        intensityFn = (m, o) => (float) BitConverter.ToDouble(m, o);
                        break;
                    case PointField.INT8:
                        intensityFn = (m, o) => (sbyte) m[o];
                        break;
                    case PointField.UINT8:
                        intensityFn = (m, o) => m[o];
                        break;
                    case PointField.INT16:
                        intensityFn = (m, o) => BitConverter.ToInt16(m, o);
                        break;
                    case PointField.UINT16:
                        intensityFn = (m, o) => BitConverter.ToUInt16(m, o);
                        break;
                    case PointField.INT32:
                        intensityFn = (m, o) => BitConverter.ToInt32(m, o);
                        break;
                    case PointField.UINT32:
                        intensityFn = (m, o) => BitConverter.ToUInt32(m, o);
                        break;
                    default:
                        intensityFn = (m, o) => 0;
                        break;
                }
            }

            for (var v = (int) msg.Height; v > 0; v--, heightOffset += rowStep)
            {
                var rowOffset = heightOffset;
                for (var u = (int) msg.Width; u > 0; u--, rowOffset += pointStep)
                {
                    var xyz = new Vector3(
                        BitConverter.ToSingle(msg.Data, rowOffset + xOffset),
                        BitConverter.ToSingle(msg.Data, rowOffset + yOffset),
                        BitConverter.ToSingle(msg.Data, rowOffset + zOffset)
                    );
                    pointBuffer.Add(new float4(
                        new float3(-xyz.y, xyz.z, xyz.x),
                        intensityFn(msg.Data, rowOffset + iOffset)
                    ));
                }
            }
        }

        void GeneratePointBufferXYZ([NotNull] PointCloud2 msg, int iOffset, int iType)
        {
            const float maxPositionMagnitudeSq = PointListResource.MaxPositionMagnitudeSq;

            int rowStep = (int) msg.RowStep;
            int pointStep = (int) msg.PointStep;
            int height = (int) msg.Height;
            int width = (int) msg.Width;

            pointBuffer.ResizeUninitialized(width * height);

            unsafe
            {
                float4* pointBufferPtr = (float4*) pointBuffer.GetUnsafePtr();
                fixed (byte* dataPtr = msg.Data)
                {
                    float4* pointBufferOff = pointBufferPtr;
                    byte* dataRowOff = dataPtr;
                    switch (iType)
                    {
                        case PointField.FLOAT32 when iOffset == 12:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float4 data = *(float4*) dataOff;
                                    if (data.HasNaN() || data.xyz.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, data.w);
                                }
                            }

                            break;
                        case PointField.FLOAT32:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    float f = *(float*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                        case PointField.FLOAT64:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    double f = *(double*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, (float) f);
                                }
                            }

                            break;
                        case PointField.INT8:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    sbyte f = *(sbyte*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                        case PointField.UINT8:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    byte f = *(dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                        case PointField.INT16:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    short f = *(short*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                        case PointField.UINT16:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    ushort f = *(ushort*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                        case PointField.INT32:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    int f = *(int*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                        case PointField.UINT32:
                            for (int v = height; v > 0; v--, dataRowOff += rowStep)
                            {
                                byte* dataOff = dataRowOff;
                                for (int u = width; u > 0; u--, dataOff += pointStep)
                                {
                                    float3 data = *(float3*) dataOff;
                                    if (data.HasNaN() || data.MagnitudeSq() > maxPositionMagnitudeSq)
                                    {
                                        continue;
                                    }

                                    uint f = *(uint*) (dataOff + iOffset);
                                    *pointBufferOff++ = new float4(-data.y, data.z, data.x, f);
                                }
                            }

                            break;
                    }

                    pointBuffer.ResizeUninitialized((int) (pointBufferOff - pointBufferPtr));
                }
            }
        }

        public override void StopController()
        {
            base.StopController();

            ResourcePool.Dispose(Resource.Displays.PointList, pointCloud.gameObject);

            node.Stop();
            Object.Destroy(node.gameObject);

            pointBuffer.Dispose();
        }
    }
}