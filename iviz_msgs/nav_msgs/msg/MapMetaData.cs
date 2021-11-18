/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/MapMetaData")]
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
        internal MapMetaData(ref Buffer b)
        {
            MapLoadTime = b.Deserialize<time>();
            Resolution = b.Deserialize<float>();
            Width = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            b.Deserialize(out Origin);
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
            b.Serialize(Origin);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 76;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/MapMetaData";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "10cfc8a2818024d3248802c00c95f11b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1TPU/DMBDdLfU/nNQFpJJWgBiQGJg6VRTBVlWV6zjxSbEdbEeh/HrOzkdbGFhQIw++" +
                "87u7956dKbwr9KBslcOeexSAprBO84DWAN/bJkBQEoTijosgHfrgwRYp+SJEU3MjDkuHOWNT6iUhoJbA" +
                "A7QKhUowzWtouYfK8lzmLAEot4vxLkZ9ZcQ56W3VpOEbPReyqrasIGC4uz05o4JVbIp5ULCJKL9lDZqI" +
                "SskeoCSWKvxEdNl+qHVYohkURQobPQNajufbDDp7aMVTJ3l101pHXtXWy76IGsX+cLWYLa7JvqFRxkpp" +
                "tQzusNO+9PN1KknjJuzpn78JW70tH+H3xAnReybmNbknTejulYgnAUS2cFKCr7mQMxBWx3Ten2P3BgzF" +
                "DofaDNjako8jgL02nN6FSX2POHYxjUQmikwXJSyNR9Nd1yiB5HCKIuszxSy9rId7+Bx3h3H3dSkFR/9G" +
                "GeN1ebL/1NVz/jH6OLof/9uM/SFq2LUk7xu4GlT8/QMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
