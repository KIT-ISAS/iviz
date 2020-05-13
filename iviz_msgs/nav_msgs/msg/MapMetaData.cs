using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    [DataContract]
    public sealed class MapMetaData : IMessage
    {
        // This hold basic information about the characterists of the OccupancyGrid
        
        // The time at which the map was loaded
        [DataMember] public time map_load_time { get; set; }
        // The map resolution [m/cell]
        [DataMember] public float resolution { get; set; }
        // Map width [cells]
        [DataMember] public uint width { get; set; }
        // Map height [cells]
        [DataMember] public uint height { get; set; }
        // The origin of the map [m, m, rad].  This is the real-world pose of the
        // cell (0,0) in the map.
        [DataMember] public geometry_msgs.Pose origin { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MapMetaData()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public MapMetaData(time map_load_time, float resolution, uint width, uint height, geometry_msgs.Pose origin)
        {
            this.map_load_time = map_load_time;
            this.resolution = resolution;
            this.width = width;
            this.height = height;
            this.origin = origin;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MapMetaData(Buffer b)
        {
            this.map_load_time = b.Deserialize<time>();
            this.resolution = b.Deserialize<float>();
            this.width = b.Deserialize<uint>();
            this.height = b.Deserialize<uint>();
            this.origin = new geometry_msgs.Pose(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MapMetaData(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.map_load_time);
            b.Serialize(this.resolution);
            b.Serialize(this.width);
            b.Serialize(this.height);
            b.Serialize(this.origin);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 76;
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/MapMetaData";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "10cfc8a2818024d3248802c00c95f11b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71TPU8DMQzd8yssdQGpXCtADEgMTJ0qimCrqipN3EukS3IkOR3l1+PkPtrCwIJ6yhDb" +
                "z/bzc24C70oHUK6SsONBC9B277zhUTsLfOeaCFEhCMU9FxG9DjGA22fnixBNza04LLyWjE2oFkLUBoFH" +
                "aJUWKsMMr6HlASrHJUqWAeTbJnubrD4z4TwGVzW5+drMBFbVhu0JGO9uT2KUsExFtYwK1gkVNqzRNqGy" +
                "swco1KWKPxGdt2/qvC61HSZKFNZmCnQ8l5sCOnnopKhHXt20zpNWtQvYJ1GhVB+u5tP5Nck3FCpYic5g" +
                "9IetCWWYrXJKbsee/vljy7fFI/zuR9yeiXZN0qGN3VKJdWZPTPceEULNBU5BOJPcso/r7gFYmSgPuQWw" +
                "lSMRRwB7bTg9CpvrHnHsQgMSlbxG2o9w1FvbblEjf5qFk5Uon43bvamHe/gcb4fx9nUZ+kfphhnGRQUS" +
                "/lTPc/LJ+jjqnn7Xgv0x0XBrGfsGRIxpGPMDAAA=";
                
    }
}
