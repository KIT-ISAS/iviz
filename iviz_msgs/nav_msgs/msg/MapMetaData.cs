/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MapMetaData : IDeserializable<MapMetaData>, IMessage
    {
        // This hold basic information about the characterists of the OccupancyGrid
        // The time at which the map was loaded
        [DataMember (Name = "map_load_time")] public time MapLoadTime;
        // The map resolution [m/cell]
        [DataMember (Name = "resolution")] public float Resolution;
        // Map width [cells]
        [DataMember (Name = "width")] public uint Width;
        // Map height [cells]
        [DataMember (Name = "height")] public uint Height;
        // The origin of the map [m, m, rad].  This is the real-world pose of the
        // cell (0,0) in the map.
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin;
    
        /// Constructor for empty message.
        public MapMetaData()
        {
        }
        
        /// Explicit constructor.
        public MapMetaData(time MapLoadTime, float Resolution, uint Width, uint Height, in GeometryMsgs.Pose Origin)
        {
            this.MapLoadTime = MapLoadTime;
            this.Resolution = Resolution;
            this.Width = Width;
            this.Height = Height;
            this.Origin = Origin;
        }
        
        /// Constructor with buffer.
        internal MapMetaData(ref Buffer b)
        {
            MapLoadTime = b.Deserialize<time>();
            Resolution = b.Deserialize<float>();
            Width = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            b.Deserialize(out Origin);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MapMetaData(ref b);
        
        MapMetaData IDeserializable<MapMetaData>.RosDeserialize(ref Buffer b) => new MapMetaData(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(MapLoadTime);
            b.Serialize(Resolution);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Origin);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 76;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/MapMetaData";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "10cfc8a2818024d3248802c00c95f11b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TPU8DMQzd8yssdQGpXCtADEgMTJ0qimCrqipN3EukS3IkOR3l1+PkPtrCwIJ6yhDb" +
                "z/bzc24C70oHUK6SsONBC9B277zhUTsLfOeaCFEhCMU9FxG9DjGA22fnixBNza04LLyWjE2oFkLUBoFH" +
                "aJUWKsMMr6HlASrHJUqWAeTbJnubrD4z4TwGVzW5+drMBFbVhu0JGO9uT2KUsExFtYwK1gkVNqzRNqGy" +
                "swco1KWKPxGdt2/qvC61HSZKFNZmCnQ8l5sCOnnopKhHXt20zpNWtQvYJ1GhVB+u5tP5Nck3FCpYic5g" +
                "9IetCWWYrXJKbsee/vljy7fFI/zuR9yeiXZN0qGN3VKJdWZPTPceEULNBU5BOJPcso/r7gFYmSgPuQWw" +
                "lSMRRwB7bTg9CpvrHnHsQgMSlbxG2o9w1FvbblEjf5qFk5Uon43bvamHe/gcb4fx9nUZ+kfphhnGRQUS" +
                "/lTPc/LJ+jjqnn7Xgv0x0XBrGfsGRIxpGPMDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
