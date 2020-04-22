
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
                "H4sIAAAAAAAACr1TPU/DMBDdLfU/nNQFpJJWgBiQGJg6VRTBVlWV6zjxSbEdbEeh/HrOzkdbGFhQIw++" +
                "87u7956dKbwr9KBslcOeexSAprBO84DWAN/bJkBQEoTijosgHfrgwRYp+SJEU3MjDkuHOWNT6iUhoJbA" +
                "A7QKhUowzWtouYfK8lzmLAEot4vxLkZ9ZcQ56W3VpOEbPReyqrasIGC4uz05o4JVbIp5ULCJKL9lDZqI" +
                "SskeoCSWKvxEdNl+qHVYohkURQobPQNajufbDDp7aMVTJ3l101pHXtXWy76IGsX+cLWYLa7JvqFRxkpp" +
                "tQzusNO+9PN1KknjJuzpn78JW70tH+H3xAnReybmNbknTejulYgnAUS2cFKCr7mQMxBWx3Ten2P3BgzF" +
                "DofaDNjako8jgL02nN6FSX2POHYxjUQmikwXJSyNR9Nd1yiB5HCKIuszxSy9rId7+Bx3h3H3dSkFR/9G" +
                "GeN1ebL/1NVz/jH6OLof/9uM/SFq2LUk7xu4GlT8/QMAAA==";
                
    }
}
