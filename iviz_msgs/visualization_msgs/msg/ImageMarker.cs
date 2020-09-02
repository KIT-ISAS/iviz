/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/ImageMarker")]
    public sealed class ImageMarker : IMessage
    {
        public const byte CIRCLE = 0;
        public const byte LINE_STRIP = 1;
        public const byte LINE_LIST = 2;
        public const byte POLYGON = 3;
        public const byte POINTS = 4;
        public const byte ADD = 0;
        public const byte REMOVE = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "ns")] public string Ns { get; set; } // namespace, used with id to form a unique id
        [DataMember (Name = "id")] public int Id { get; set; } // unique id within the namespace
        [DataMember (Name = "type")] public int Type { get; set; } // CIRCLE/LINE_STRIP/etc.
        [DataMember (Name = "action")] public int Action { get; set; } // ADD/REMOVE
        [DataMember (Name = "position")] public GeometryMsgs.Point Position { get; set; } // 2D, in pixel-coords
        [DataMember (Name = "scale")] public float Scale { get; set; } // the diameter for a circle, etc.
        [DataMember (Name = "outline_color")] public StdMsgs.ColorRGBA OutlineColor { get; set; }
        [DataMember (Name = "filled")] public byte Filled { get; set; } // whether to fill in the shape with color
        [DataMember (Name = "fill_color")] public StdMsgs.ColorRGBA FillColor { get; set; } // color [0.0-1.0]
        [DataMember (Name = "lifetime")] public duration Lifetime { get; set; } // How long the object should last before being automatically deleted.  0 means forever
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points { get; set; } // used for LINE_STRIP/LINE_LIST/POINTS/etc., 2D in pixel coords
        [DataMember (Name = "outline_colors")] public StdMsgs.ColorRGBA[] OutlineColors { get; set; } // a color for each line, point, etc.
    
        /// <summary> Constructor for empty message. </summary>
        public ImageMarker()
        {
            Header = new StdMsgs.Header();
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            OutlineColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ImageMarker(StdMsgs.Header Header, string Ns, int Id, int Type, int Action, in GeometryMsgs.Point Position, float Scale, in StdMsgs.ColorRGBA OutlineColor, byte Filled, in StdMsgs.ColorRGBA FillColor, duration Lifetime, GeometryMsgs.Point[] Points, StdMsgs.ColorRGBA[] OutlineColors)
        {
            this.Header = Header;
            this.Ns = Ns;
            this.Id = Id;
            this.Type = Type;
            this.Action = Action;
            this.Position = Position;
            this.Scale = Scale;
            this.OutlineColor = OutlineColor;
            this.Filled = Filled;
            this.FillColor = FillColor;
            this.Lifetime = Lifetime;
            this.Points = Points;
            this.OutlineColors = OutlineColors;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ImageMarker(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Ns = b.DeserializeString();
            Id = b.Deserialize<int>();
            Type = b.Deserialize<int>();
            Action = b.Deserialize<int>();
            Position = new GeometryMsgs.Point(b);
            Scale = b.Deserialize<float>();
            OutlineColor = new StdMsgs.ColorRGBA(b);
            Filled = b.Deserialize<byte>();
            FillColor = new StdMsgs.ColorRGBA(b);
            Lifetime = b.Deserialize<duration>();
            Points = b.DeserializeStructArray<GeometryMsgs.Point>();
            OutlineColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new ImageMarker(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Header.RosSerialize(b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            Position.RosSerialize(b);
            b.Serialize(Scale);
            OutlineColor.RosSerialize(b);
            b.Serialize(Filled);
            FillColor.RosSerialize(b);
            b.Serialize(Lifetime);
            b.SerializeStructArray(Points, 0);
            b.SerializeStructArray(OutlineColors, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Ns is null) throw new System.NullReferenceException(nameof(Ns));
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            if (OutlineColors is null) throw new System.NullReferenceException(nameof(OutlineColors));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 93;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Ns);
                size += 24 * Points.Length;
                size += 16 * OutlineColors.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/ImageMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1de93c67ec8858b831025a08fbf1b35c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
