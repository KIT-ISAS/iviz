/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract (Name = "nav_msgs/MapMetaData")]
    public sealed class MapMetaData : IDeserializable<MapMetaData>, IMessage
    {
        // This hold basic information about the characterists of the OccupancyGrid
        // The time at which the map was loaded
        [DataMember (Name = "map_load_time")] public time MapLoadTime { get; set; }
        // The map resolution [m/cell]
        [DataMember (Name = "resolution")] public float Resolution { get; set; }
        // Map width [cells]
        [DataMember (Name = "width")] public uint Width { get; set; }
        // Map height [cells]
        [DataMember (Name = "height")] public uint Height { get; set; }
        // The origin of the map [m, m, rad].  This is the real-world pose of the
        // cell (0,0) in the map.
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MapMetaData()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MapMetaData(time MapLoadTime, float Resolution, uint Width, uint Height, in GeometryMsgs.Pose Origin)
        {
            this.MapLoadTime = MapLoadTime;
            this.Resolution = Resolution;
            this.Width = Width;
            this.Height = Height;
            this.Origin = Origin;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MapMetaData(ref Buffer b)
        {
            MapLoadTime = b.Deserialize<time>();
            Resolution = b.Deserialize<float>();
            Width = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            Origin = new GeometryMsgs.Pose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MapMetaData(ref b);
        }
        
        MapMetaData IDeserializable<MapMetaData>.RosDeserialize(ref Buffer b)
        {
            return new MapMetaData(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(MapLoadTime);
            b.Serialize(Resolution);
            b.Serialize(Width);
            b.Serialize(Height);
            Origin.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 76;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/MapMetaData";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "10cfc8a2818024d3248802c00c95f11b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TwWrDMAy9B/wPgl426NqyjR0GO+zUU1nHdiuluI4TG2I7sx2y7usnOU3S0ssOo8EH" +
                "S36S3pOUCXwqHUC5Koc9D1qAtoXzhkftLPC9ayJEJUEo7rmI0usQA7giOd+EaGpuxWHpdc4ylk0wm4So" +
                "jQQeoVVaqAQ0vIaWB6gczyUiEwKdO3LsyOpjCellcFWTCGzMXMiq2rKsQGh8uD95pJAVJdZ5VLAhXEBg" +
                "oy3hkreHKKlLFS8wnbsv7bwute21EZGNmQIez/PtDLpG4aFXL3l11zqPXatdkMcgykQl4GYxXdxiJ/tM" +
                "M5aV0hkZ/WFnQhnm6xSUCrLs5Z8/lq0+ls9wWZH4vSL3GpsobexmjNSTBGRbeCkh1FzIKQhnyJ0f33W3" +
                "DxZtr/vYGbBs7bCXA4Jl7w3HLbEp84ik5biSTKTTTRRHJRzW17ab2aACFXG0iPeZ6OOSPT3C93g9jNef" +
                "q6kYmzhIGaYWcAqnrT3XQNbXOAL6lXH5/qCsv7aE/gU/SLrgFgQAAA==";
                
    }
}
