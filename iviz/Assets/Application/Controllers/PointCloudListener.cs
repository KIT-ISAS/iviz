using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.RoslibSharp;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class PointCloudConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.PointCloud;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string IntensityChannel { get; set; } = "intensity";
        [DataMember] public float PointSize { get; set; } = 0.03f;
        [DataMember] public Resource.ColormapId Colormap { get; set; } = Resource.ColormapId.hsv;
        [DataMember] public bool ForceMinMax { get; set; } = false;
        [DataMember] public float MinIntensity { get; set; } = 0;
        [DataMember] public float MaxIntensity { get; set; } = 1;
        [DataMember] public bool FlipMinMax { get; set; } = false;
        [DataMember] public uint MaxQueueSize { get; set; } = 1;
    }

    public sealed class PointCloudListener : ListenerController
    {
        readonly DisplayNode node;
        readonly PointListResource pointCloud;

        public override ModuleData ModuleData { get; }

        public Vector2 MeasuredIntensityBounds { get; private set; }

        public int Size { get; private set; }

        public override TFFrame Frame => node.Parent;

        readonly PointCloudConfiguration config = new PointCloudConfiguration();
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
                pointCloud.Scale = value * Vector2.one;
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

        readonly List<string> fieldNames = new List<string>() { "x", "y", "z" };
        public IReadOnlyList<string> FieldNames => fieldNames;

        PointWithColor[] pointBuffer = new PointWithColor[0];

        public PointCloudListener(ModuleData moduleData)
        {
            ModuleData = moduleData;
            
            node = SimpleDisplayNode.Instantiate("[PointCloudNode]");
            pointCloud = ResourcePool.GetOrCreate<PointListResource>(Resource.Displays.PointList, node.transform);

            Config = new PointCloudConfiguration();
            //transform.localRotation = new Msgs.GeometryMsgs.Quaternion(0, 0, 0, 1).Ros2Unity();
        }

        public override void StartListening()
        {
            Listener = new RosListener<PointCloud2>(config.Topic, Handler);
            Listener.MaxQueueSize = (int)MaxQueueSize;
            node.name = "[" + config.Topic + "]";
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

        bool isProcessing;
        void Handler(PointCloud2 msg)
        {
            if (isProcessing)
            {
                return;
            }

            isProcessing = true;
            node.AttachTo(msg.Header.FrameId, msg.Header.Stamp); 

            if (msg.PointStep < 3 * 4 ||
                msg.RowStep < msg.PointStep * msg.Width ||
                msg.Data.Length < msg.RowStep * msg.Height)
            {
                Logger.Info($"{this}: Invalid point cloud dimensions!");
                return;
            }

            fieldNames.Clear();
            fieldNames.AddRange(msg.Fields.Select(x => x.Name));

            int newSize = (int)(msg.Width * msg.Height);
            if (newSize > pointBuffer.Length)
            {
                pointBuffer = new PointWithColor[newSize * 11 / 10];
            }

            Task.Run(() =>
            {
                Dictionary<string, PointField> fieldOffsets = new Dictionary<string, PointField>();
                msg.Fields.ForEach(x => fieldOffsets.Add(x.Name, x));

                if (!fieldOffsets.TryGetValue("x", out PointField xField) || xField.Datatype != PointField.FLOAT32 ||
                    !fieldOffsets.TryGetValue("y", out PointField yField) || yField.Datatype != PointField.FLOAT32 ||
                    !fieldOffsets.TryGetValue("z", out PointField zField) || zField.Datatype != PointField.FLOAT32)
                {
                    Logger.Info($"{this}: Unsupported point cloud! Expected XYZ as floats.");
                    return;
                }
                int xOffset = (int)xField.Offset;
                int yOffset = (int)yField.Offset;
                int zOffset = (int)zField.Offset;

                if (!fieldOffsets.TryGetValue(config.IntensityChannel, out PointField iField))
                {
                    iField = new PointField();
                }
                int iOffset = (int)iField.Offset;
                int iSize = FieldSizeFromType(iField.Datatype);
                if (iSize == -1 || msg.PointStep < iOffset + iSize)
                {
                    Logger.Info($"{this}: Invalid or unsupported intensity field type!");
                    return;
                }

                bool rgbaHint = iSize == 4 && (iField.Name == "rgb" || iField.Name == "rgba");

                GeneratePointBuffer(msg, xOffset, yOffset, zOffset, iOffset, iField.Datatype, rgbaHint);

                GameThread.RunOnce(() =>
                {
                    if (pointCloud is null)
                    {
                        return;
                    }
                    Size = newSize;
                    pointCloud.UseIntensityTexture = !rgbaHint;
                    pointCloud.PointsWithColor = new ArraySegment<PointWithColor>(pointBuffer, 0, Size);
                    MeasuredIntensityBounds = pointCloud.IntensityBounds;
                    if (ForceMinMax)
                    {
                        pointCloud.IntensityBounds = new Vector2(MinIntensity, MaxIntensity);
                    }

                    isProcessing = false;
                });
            });
        }

        void GeneratePointBuffer(PointCloud2 msg, int xOffset, int yOffset, int zOffset, int iOffset, int iType, bool rgbaHint)
        {
            bool xyzAligned = xOffset == 0 && yOffset == 4 && zOffset == 8;
            if (xyzAligned)
            {
                if (rgbaHint)
                {
                    GeneratePointBufferXYZ(msg, iOffset, PointField.FLOAT32);
                }
                else
                {
                    GeneratePointBufferXYZ(msg, iOffset, iType);
                }
            }
            else
            {
                GeneratePointBufferSlow(msg, xOffset, yOffset, zOffset, iOffset, iType, rgbaHint);
            }
        }

        void GeneratePointBufferSlow(PointCloud2 msg, int xOffset, int yOffset, int zOffset, int iOffset, int iType, bool rgbaHint)
        {
            int heightOffset = 0;
            int pointOffset = 0;
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;

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
                        intensityFn = (m, o) => (float)BitConverter.ToDouble(m, o);
                        break;
                    case PointField.INT8:
                        intensityFn = (m, o) => (sbyte)m[o];
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

            for (int v = (int)msg.Height; v > 0; v--, heightOffset += rowStep)
            {
                int rowOffset = heightOffset;
                for (int u = (int)msg.Width; u > 0; u--, rowOffset += pointStep, pointOffset++)
                {
                    Vector3 xyz = new Vector3(
                        BitConverter.ToSingle(msg.Data, rowOffset + xOffset),
                        BitConverter.ToSingle(msg.Data, rowOffset + yOffset),
                        BitConverter.ToSingle(msg.Data, rowOffset + zOffset)
                    );
                    pointBuffer[pointOffset] = new PointWithColor(
                        new Vector3(-xyz.y, xyz.z, xyz.x),
                        intensityFn(msg.Data, rowOffset + iOffset)
                    );
                }
            }
        }

        void GeneratePointBufferXYZ(PointCloud2 msg, int iOffset, int iType)
        {
            int rowStep = (int)msg.RowStep;
            int pointStep = (int)msg.PointStep;
            int height = (int)msg.Height;
            int width = (int)msg.Width;

            unsafe
            {
                fixed (byte* dataPtr = msg.Data)
                fixed (PointWithColor* pointBufferPtr = pointBuffer)
                {
                    PointWithColor* pointBufferOff = pointBufferPtr;
                    byte* dataRow = dataPtr;
                    for (int v = height; v > 0; v--, dataRow += rowStep)
                    {
                        byte* dataOff = dataRow;
                        switch (iType)
                        {
                            case PointField.FLOAT32:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], *(float*)(dataOff + iOffset));
                                }
                                break;
                            case PointField.FLOAT64:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], (float)*(double*)(dataOff + iOffset));
                                }
                                break;
                            case PointField.INT8:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], (sbyte)*(dataOff + iOffset));
                                }
                                break;
                            case PointField.UINT8:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], *(dataOff + iOffset));
                                }
                                break;
                            case PointField.INT16:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], *(short*)(dataOff + iOffset));
                                }
                                break;
                            case PointField.UINT16:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], *(ushort*)(dataOff + iOffset));
                                }
                                break;
                            case PointField.INT32:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], *(int*)(dataOff + iOffset));
                                }
                                break;
                            case PointField.UINT32:
                                for (int u = width; u > 0; u--, dataOff += pointStep, pointBufferOff++)
                                {
                                    float* datap = (float*)dataOff;
                                    *pointBufferOff = new PointWithColor(-datap[1], datap[2], datap[0], *(uint*)(dataOff + iOffset));
                                }
                                break;
                            default:
                                //??
                                break;
                        }
                    }
                }
            }
        }

        public override void Stop()
        {
            base.Stop();

            ResourcePool.Dispose(Resource.Displays.PointList, pointCloud.gameObject);

            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }
    }
}

/*
var hexviz = new Dictionary<string, int[]> {
    {"lines", new[] {0x0072bd, 0xd95319, 0xedb120, 0x7e2f8e, 0x77ac30, 0x4dbeee, 0xa2142f, 0x0072bd, 0xd95319, 0xedb120, 0x7e2f8e, 0x77ac30, 0x4dbeee, 0xa2142f, 0x0072bd, 0xd95319}},
    {"pink", new[] {0x3c0000, 0x653636, 0x814c4c, 0x985d5d, 0xac6c6c, 0xbe7878, 0xc69184, 0xcda68e, 0xd4b898, 0xdac9a1, 0xe1d9aa, 0xe7e7b2, 0xededc8, 0xf3f3dc, 0xf9f9ee, 0xffffff}},
    {"copper", new[] {0x000000, 0x150d08, 0x2b1b11, 0x402819, 0x553522, 0x6a422a, 0x805033, 0x955d3b, 0xaa6a44, 0xbf784c, 0xd48555, 0xea925d, 0xff9f65, 0xffad6e, 0xffba76, 0xffc77f}},
    {"bone", new[] {0x000005, 0x0f0f1a, 0x1e1e2e, 0x2d2d42, 0x3c3c56, 0x4a4a6a, 0x595f79, 0x687388, 0x778797, 0x869ba6, 0x95afb5, 0xa4c3c3, 0xbad2d2, 0xd1e1e1, 0xe8f0f0, 0xffffff}},
    {"gray", new[] {0x000000, 0x111111, 0x222222, 0x333333, 0x444444, 0x555555, 0x666666, 0x777777, 0x888888, 0x999999, 0xaaaaaa, 0xbbbbbb, 0xcccccc, 0xdddddd, 0xeeeeee, 0xffffff}},
    {"winter", new[] {0x0000ff, 0x0011f7, 0x0022ee, 0x0033e6, 0x0044dd, 0x0055d5, 0x0066cc, 0x0077c3, 0x0088bb, 0x0099b3, 0x00aaaa, 0x00bba2, 0x00cc99, 0x00dd91, 0x00ee88, 0x00ff80}},
    {"autumn", new[] {0xff0000, 0xff1100, 0xff2200, 0xff3300, 0xff4400, 0xff5500, 0xff6600, 0xff7700, 0xff8800, 0xff9900, 0xffaa00, 0xffbb00, 0xffcc00, 0xffdd00, 0xffee00, 0xffff00}},
    {"summer", new[] {0x008066, 0x118866, 0x229166, 0x339966, 0x44a266, 0x55aa66, 0x66b366, 0x77bb66, 0x88c366, 0x99cc66, 0xaad466, 0xbbdd66, 0xcce666, 0xddee66, 0xeef766, 0xffff66}},
    {"spring", new[] {0xff00ff, 0xff11ee, 0xff22dd, 0xff33cc, 0xff44bb, 0xff55aa, 0xff6699, 0xff7788, 0xff8877, 0xff9966, 0xffaa55, 0xffbb44, 0xffcc33, 0xffdd22, 0xffee11, 0xffff00}},
    {"cool",new[] { 0x00ffff, 0x11eeff, 0x22ddff, 0x33ccff, 0x44bbff, 0x55aaff, 0x6699ff, 0x7788ff, 0x8877ff, 0x9966ff, 0xaa55ff, 0xbb44ff, 0xbf40ff, 0xdd22ff, 0xee11ff, 0xff00ff}},
    {"hot", new[] {0x2b0000, 0x550000, 0x800000, 0xaa0000, 0xd50000, 0xff0000, 0xff0000, 0xff5500, 0xff8000, 0xffaa00, 0xffd500, 0xffff00, 0xffff40, 0xffff80, 0xffffbf, 0xffffff}},
    {"hsv", new[] {0xff0000, 0xff6000, 0xffbf00, 0xdfff00, 0x80ff00, 0x20ff00, 0x00ff40, 0x00ff9f, 0x00ffff, 0x009fff, 0x0040ff, 0x2000ff, 0x8000ff, 0xdf00ff, 0xff00bf, 0xff0060}},
    {"jet", new[] {0x0000bf, 0x0000ff, 0x0040ff, 0x0080ff, 0x00bfff, 0x00ffff, 0x40ffbf, 0x80ff80, 0xbfff40, 0xffff00, 0xffbf00, 0xff8000, 0xff4000, 0xff0000, 0xbf0000, 0x800000}},
    {"parula", new[] {0x352a87, 0x3145bc, 0x0265e1, 0x0f77db, 0x1388d3, 0x079ccf, 0x07aac1, 0x20b4ad, 0x49bc94, 0x7abf7c, 0xa5be6b, 0xcabb5c, 0xecb94c, 0xfec634, 0xf6dd22, 0xf9fb0e } }
};

Texture2D t = new Texture2D(16, 1);
foreach (var entry in hexviz)
{
    Color32[] pixels = entry.Value.Select(x => new Color32((byte)((x >> 16) & 0xff), (byte)((x >> 8) & 0xff), (byte)((x >> 0) & 0xff), 255)).ToArray();
    t.SetPixels32(pixels);
    byte[] png = t.EncodeToPNG();
    File.WriteAllBytes(entry.Key + ".png", png);
}
*/
