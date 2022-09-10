/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class MapMetaData : IHasSerializer<MapMetaData>, IMessage
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
    
        public MapMetaData()
        {
        }
        
        public MapMetaData(ref ReadBuffer b)
        {
            b.Deserialize(out MapLoadTime);
            b.Deserialize(out Resolution);
            b.Deserialize(out Width);
            b.Deserialize(out Height);
            b.Deserialize(out Origin);
        }
        
        public MapMetaData(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out MapLoadTime);
            b.Deserialize(out Resolution);
            b.Deserialize(out Width);
            b.Deserialize(out Height);
            b.Align8();
            b.Deserialize(out Origin);
        }
        
        public MapMetaData RosDeserialize(ref ReadBuffer b) => new MapMetaData(ref b);
        
        public MapMetaData RosDeserialize(ref ReadBuffer2 b) => new MapMetaData(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(MapLoadTime);
            b.Serialize(Resolution);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(in Origin);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(MapLoadTime);
            b.Serialize(Resolution);
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(in Origin);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 76;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 80;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "nav_msgs/MapMetaData";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "10cfc8a2818024d3248802c00c95f11b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71TPU8DMQzd8yssdQGpXCtADEgMTJ0qimCrqipN3EukS3IkOR3l1+PkPtrCwIJ6yhDb" +
                "z/bzc24C70oHUK6SsONBC9B277zhUTsLfOeaCFEhCMU9FxG9DjGA22fnixBNza04LLyWjE2oFkLUBoFH" +
                "aJUWKsMMr6HlASrHJUqWAeTbJnubrD4z4TwGVzW5+drMBFbVhu0JGO9uT2KUsExFtYwK1gkVNqzRNqGy" +
                "swco1KWKPxGdt2/qvC61HSZKFNZmCnQ8l5sCOnnopKhHXt20zpNWtQvYJ1GhVB+u5tP5Nck3FCpYic5g" +
                "9IetCWWYrXJKbsee/vljy7fFI/zuR9yeiXZN0qGN3VKJdWZPTPceEULNBU5BOJPcso/r7gFYmSgPuQWw" +
                "lSMRRwB7bTg9CpvrHnHsQgMSlbxG2o9w1FvbblEjf5qFk5Uon43bvamHe/gcb4fx9nUZ+kfphhnGRQUS" +
                "/lTPc/LJ+jjqnn7Xgv0x0XBrGfsGRIxpGPMDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MapMetaData> CreateSerializer() => new Serializer();
        public Deserializer<MapMetaData> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MapMetaData>
        {
            public override void RosSerialize(MapMetaData msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MapMetaData msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MapMetaData _) => RosFixedMessageLength;
            public override int Ros2MessageLength(MapMetaData _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<MapMetaData>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MapMetaData msg) => msg = new MapMetaData(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MapMetaData msg) => msg = new MapMetaData(ref b);
        }
    }
}
