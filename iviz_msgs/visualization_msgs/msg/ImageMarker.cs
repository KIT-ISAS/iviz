using System.Runtime.Serialization;

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
        
        public std_msgs.Header header { get; set; }
        public string ns { get; set; } // namespace, used with id to form a unique id
        public int id { get; set; } // unique id within the namespace
        public int type { get; set; } // CIRCLE/LINE_STRIP/etc.
        public int action { get; set; } // ADD/REMOVE
        public geometry_msgs.Point position { get; set; } // 2D, in pixel-coords
        public float scale { get; set; } // the diameter for a circle, etc.
        public std_msgs.ColorRGBA outline_color { get; set; }
        public byte filled { get; set; } // whether to fill in the shape with color
        public std_msgs.ColorRGBA fill_color { get; set; } // color [0.0-1.0]
        public duration lifetime { get; set; } // How long the object should last before being automatically deleted.  0 means forever
        
        
        public geometry_msgs.Point[] points { get; set; } // used for LINE_STRIP/LINE_LIST/POINTS/etc., 2D in pixel coords
        public std_msgs.ColorRGBA[] outline_colors { get; set; } // a color for each line, point, etc.
    
        /// <summary> Constructor for empty message. </summary>
        public ImageMarker()
        {
            header = new std_msgs.Header();
            ns = "";
            points = System.Array.Empty<geometry_msgs.Point>();
            outline_colors = System.Array.Empty<std_msgs.ColorRGBA>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ImageMarker(std_msgs.Header header, string ns, int id, int type, int action, geometry_msgs.Point position, float scale, std_msgs.ColorRGBA outline_color, byte filled, std_msgs.ColorRGBA fill_color, duration lifetime, geometry_msgs.Point[] points, std_msgs.ColorRGBA[] outline_colors)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.ns = ns ?? throw new System.ArgumentNullException(nameof(ns));
            this.id = id;
            this.type = type;
            this.action = action;
            this.position = position;
            this.scale = scale;
            this.outline_color = outline_color;
            this.filled = filled;
            this.fill_color = fill_color;
            this.lifetime = lifetime;
            this.points = points ?? throw new System.ArgumentNullException(nameof(points));
            this.outline_colors = outline_colors ?? throw new System.ArgumentNullException(nameof(outline_colors));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ImageMarker(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.ns = BuiltIns.DeserializeString(b);
            this.id = BuiltIns.DeserializeStruct<int>(b);
            this.type = BuiltIns.DeserializeStruct<int>(b);
            this.action = BuiltIns.DeserializeStruct<int>(b);
            this.position = new geometry_msgs.Point(b);
            this.scale = BuiltIns.DeserializeStruct<float>(b);
            this.outline_color = new std_msgs.ColorRGBA(b);
            this.filled = BuiltIns.DeserializeStruct<byte>(b);
            this.fill_color = new std_msgs.ColorRGBA(b);
            this.lifetime = BuiltIns.DeserializeStruct<duration>(b);
            this.points = BuiltIns.DeserializeStructArray<geometry_msgs.Point>(b, 0);
            this.outline_colors = BuiltIns.DeserializeStructArray<std_msgs.ColorRGBA>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new ImageMarker(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            BuiltIns.Serialize(this.ns, b);
            BuiltIns.Serialize(this.id, b);
            BuiltIns.Serialize(this.type, b);
            BuiltIns.Serialize(this.action, b);
            this.position.Serialize(b);
            BuiltIns.Serialize(this.scale, b);
            this.outline_color.Serialize(b);
            BuiltIns.Serialize(this.filled, b);
            this.fill_color.Serialize(b);
            BuiltIns.Serialize(this.lifetime, b);
            BuiltIns.SerializeStructArray(this.points, b, 0);
            BuiltIns.SerializeStructArray(this.outline_colors, b, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (ns is null) throw new System.NullReferenceException();
            if (points is null) throw new System.NullReferenceException();
            if (outline_colors is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 93;
                size += header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(ns);
                size += 24 * points.Length;
                size += 16 * outline_colors.Length;
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "visualization_msgs/ImageMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "1de93c67ec8858b831025a08fbf1b35c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
