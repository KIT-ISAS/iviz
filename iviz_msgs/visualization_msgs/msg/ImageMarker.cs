/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ImageMarker : IDeserializable<ImageMarker>, IMessage
    {
        public const byte CIRCLE = 0;
        public const byte LINE_STRIP = 1;
        public const byte LINE_LIST = 2;
        public const byte POLYGON = 3;
        public const byte POINTS = 4;
        public const byte ADD = 0;
        public const byte REMOVE = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> Namespace, used with id to form a unique id </summary>
        [DataMember (Name = "ns")] public string Ns;
        /// <summary> Unique id within the namespace </summary>
        [DataMember (Name = "id")] public int Id;
        /// <summary> CIRCLE/LINE_STRIP/etc. </summary>
        [DataMember (Name = "type")] public int Type;
        /// <summary> ADD/REMOVE </summary>
        [DataMember (Name = "action")] public int Action;
        /// <summary> 2D, in pixel-coords </summary>
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        /// <summary> The diameter for a circle, etc. </summary>
        [DataMember (Name = "scale")] public float Scale;
        [DataMember (Name = "outline_color")] public StdMsgs.ColorRGBA OutlineColor;
        /// <summary> Whether to fill in the shape with color </summary>
        [DataMember (Name = "filled")] public byte Filled;
        /// <summary> Color [0.0-1.0] </summary>
        [DataMember (Name = "fill_color")] public StdMsgs.ColorRGBA FillColor;
        /// <summary> How long the object should last before being automatically deleted.  0 means forever </summary>
        [DataMember (Name = "lifetime")] public duration Lifetime;
        /// <summary> Used for LINE_STRIP/LINE_LIST/POINTS/etc., 2D in pixel coords </summary>
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points;
        /// <summary> A color for each line, point, etc. </summary>
        [DataMember (Name = "outline_colors")] public StdMsgs.ColorRGBA[] OutlineColors;
    
        /// Constructor for empty message.
        public ImageMarker()
        {
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            OutlineColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// Constructor with buffer.
        public ImageMarker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Ns = b.DeserializeString();
            Id = b.Deserialize<int>();
            Type = b.Deserialize<int>();
            Action = b.Deserialize<int>();
            b.Deserialize(out Position);
            Scale = b.Deserialize<float>();
            b.Deserialize(out OutlineColor);
            Filled = b.Deserialize<byte>();
            b.Deserialize(out FillColor);
            Lifetime = b.Deserialize<duration>();
            Points = b.DeserializeStructArray<GeometryMsgs.Point>();
            OutlineColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ImageMarker(ref b);
        
        public ImageMarker RosDeserialize(ref ReadBuffer b) => new ImageMarker(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            b.Serialize(in Position);
            b.Serialize(Scale);
            b.Serialize(in OutlineColor);
            b.Serialize(Filled);
            b.Serialize(in FillColor);
            b.Serialize(Lifetime);
            b.SerializeStructArray(Points);
            b.SerializeStructArray(OutlineColors);
        }
        
        public void RosValidate()
        {
            if (Ns is null) BuiltIns.ThrowNullReference(nameof(Ns));
            if (Points is null) BuiltIns.ThrowNullReference(nameof(Points));
            if (OutlineColors is null) BuiltIns.ThrowNullReference(nameof(OutlineColors));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 93;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Ns);
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
