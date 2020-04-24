namespace Iviz.Msgs.visualization_msgs
{
    public sealed class ImageMarker : IMessage
    {
        public const byte CIRCLE = 0;
        public const byte LINE_STRIP = 1;
        public const byte LINE_LIST = 2;
        public const byte POLYGON = 3;
        public const byte POINTS = 4;
        
        public const byte ADD = 0;
        public const byte REMOVE = 1;
        
        public std_msgs.Header header;
        public string ns; // namespace, used with id to form a unique id
        public int id; // unique id within the namespace
        public int type; // CIRCLE/LINE_STRIP/etc.
        public int action; // ADD/REMOVE
        public geometry_msgs.Point position; // 2D, in pixel-coords
        public float scale; // the diameter for a circle, etc.
        public std_msgs.ColorRGBA outline_color;
        public byte filled; // whether to fill in the shape with color
        public std_msgs.ColorRGBA fill_color; // color [0.0-1.0]
        public duration lifetime; // How long the object should last before being automatically deleted.  0 means forever
        
        
        public geometry_msgs.Point[] points; // used for LINE_STRIP/LINE_LIST/POINTS/etc., 2D in pixel coords
        public std_msgs.ColorRGBA[] outline_colors; // a color for each line, point, etc.
    
        /// <summary> Constructor for empty message. </summary>
        public ImageMarker()
        {
            header = new std_msgs.Header();
            ns = "";
            points = System.Array.Empty<geometry_msgs.Point>();
            outline_colors = System.Array.Empty<std_msgs.ColorRGBA>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out ns, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out type, ref ptr, end);
            BuiltIns.Deserialize(out action, ref ptr, end);
            position.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out scale, ref ptr, end);
            outline_color.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out filled, ref ptr, end);
            fill_color.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out lifetime, ref ptr, end);
            BuiltIns.DeserializeStructArray(out points, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out outline_colors, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(ns, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(type, ref ptr, end);
            BuiltIns.Serialize(action, ref ptr, end);
            position.Serialize(ref ptr, end);
            BuiltIns.Serialize(scale, ref ptr, end);
            outline_color.Serialize(ref ptr, end);
            BuiltIns.Serialize(filled, ref ptr, end);
            fill_color.Serialize(ref ptr, end);
            BuiltIns.Serialize(lifetime, ref ptr, end);
            BuiltIns.SerializeStructArray(points, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(outline_colors, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 93;
            size += header.GetLength();
            size += ns.Length;
            size += 24 * points.Length;
            size += 16 * outline_colors.Length;
            return size;
        }
    
        public IMessage Create() => new ImageMarker();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "visualization_msgs/ImageMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "1de93c67ec8858b831025a08fbf1b35c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAE7VU227TQBB9rr9ipDxwUXOhIIQq5QGaUiL1piZCQghFE3sSL1rvmt112/D1nF07bhF9" +
                "4AGiSHvxzJmZc2anUSa8o5P5zcn56XSSNel4Pr88XS2WN/Pr6avHV+fzxXJ61N1cX51/Obu6nL7uz/PL" +
                "5WL6JuvO72ezHvDm9OLq8ynAsk/ChTgq05L54JTZkvEHBwMyXImvOZdDarwUdKdCSaqgYGljXUVMjVE/" +
                "GsFdBtTXR/Fj/wNA/zm5KkOhlAfUzifsannk01Y+fqh4LCEfdbacB2VNb4uKxm0l2VZsJcHtVpXf+vG1" +
                "hT3V1qtkP6Cj2SEhfq3uRQ9za13hs422HEF9zloOIl5Mr1DIL4ARlIgKc+VyDQJSEj4ULf6J1dbdnH14" +
                "T7YJWhlZ5fGmI3ejtJYiMnhXCjBdYgyX1FHgS0bNic7W7wngaN+iIv12/ToZTYavRpNvWdE4TpVptZGg" +
                "qj2BA/pk70hbSBjj2PV3yQPC2UYXpNkHWgvqEixRZm6CrQAEAvSOCtEovBgRTagSNj5SILfoiuwper9+" +
                "A8FYPaKm9oiEPZKtb9Bx24hJx0Mo0QtBnRB/Vg/s34iNMbhjIYYRzkuKnw/bHDp9pv/4l10szo6pT699" +
                "KtmAFoFNwa4ATYELDpySKtUWWg81KNNw4qoGKelrbHE/guOyVJ7w34oRl0hPzKE9cltVeC45B6Eo6G/+" +
                "8ARnTDU7iNVodi11ykTzjUPHRnT8veDBmVxoPjuGjfGSN0EhoR0Qcifso+7zGaVWjc0vP7LB8s4OcZRt" +
                "bNV9cHQQh5is3NdOfMyT/TFivGyLGwEb5AiiFJ6ep7sVjv4FIQhSkNpCpefI/HoXStu2/i07xWstETi2" +
                "HVCfRadnLx4hmwRt2Ng9fIv4EONvYE2PG2saltBMx+p9swWBMKydvVUFTNe7BJJrJRgbWq0du12W3lUK" +
                "mQ0+Ro5hBK+kCFb23uYKArTTbT86kxorTMT/1I1PvMR9Y4GqwArvNhbTDz+7iZ2TBiI42zhBUWn8pvn3" +
                "9g3d97tdv/v5v9L/8633c9j1u22/W/c7zrJfAAkQAxsHAAA=";
                
    }
}
