
namespace Iviz.Msgs.nav_msgs
{
    public sealed class MapMetaData : IMessage 
    {
        // This hold basic information about the characterists of the OccupancyGrid
        
        // The time at which the map was loaded
        public time map_load_time;
        // The map resolution [m/cell]
        public float resolution;
        // Map width [cells]
        public uint width;
        // Map height [cells]
        public uint height;
        // The origin of the map [m, m, rad].  This is the real-world pose of the
        // cell (0,0) in the map.
        public geometry_msgs.Pose origin;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/MapMetaData";

        public IMessage Create() => new MapMetaData();

        public int GetLength() => 76;

        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out map_load_time, ref ptr, end);
            BuiltIns.Deserialize(out resolution, ref ptr, end);
            BuiltIns.Deserialize(out width, ref ptr, end);
            BuiltIns.Deserialize(out height, ref ptr, end);
            origin.Deserialize(ref ptr, end);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(map_load_time, ref ptr, end);
            BuiltIns.Serialize(resolution, ref ptr, end);
            BuiltIns.Serialize(width, ref ptr, end);
            BuiltIns.Serialize(height, ref ptr, end);
            origin.Serialize(ref ptr, end);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "10cfc8a2818024d3248802c00c95f11b";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
            "H4sIAAAAAAAAE71TPU8DMQzd8yssdQGpXCtADEgMTJ0qimCrqipN3EukS3IkOR3l1+PkPtrCwIJ6yhDb" +
            "z/bzc24C70oHUK6SsONBC9B277zhUTsLfOeaCFEhCMU9FxG9DjGA22fnixBNza04LLyWjE2oFkLUBoFH" +
            "aJUWKsMMr6HlASrHJUqWAeTbJnubrD4z4TwGVzW5+drMBFbVhu0JGO9uT2KUsExFtYwK1gkVNqzRNqGy" +
            "swco1KWKPxGdt2/qvC61HSZKFNZmCnQ8l5sCOnnopKhHXt20zpNWtQvYJ1GhVB+u5tP5Nck3FCpYic5g" +
            "9IetCWWYrXJKbsee/vljy7fFI/zuR9yeiXZN0qGN3VKJdWZPTPceEULNBU5BOJPcso/r7gFYmSgPuQWw" +
            "lSMRRwB7bTg9CpvrHnHsQgMSlbxG2o9w1FvbblEjf5qFk5Uon43bvamHe/gcb4fx9nUZ+kfphhnGRQUS" +
            "/lTPc/LJ+jjqnn7Xgv0x0XBrGfsGRIxpGPMDAAA=";

    }
}
