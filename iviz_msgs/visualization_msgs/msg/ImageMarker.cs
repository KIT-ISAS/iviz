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
        [DataMember (Name = "ns")] public string Ns; // namespace, used with id to form a unique id
        [DataMember (Name = "id")] public int Id; // unique id within the namespace
        [DataMember (Name = "type")] public int Type; // CIRCLE/LINE_STRIP/etc.
        [DataMember (Name = "action")] public int Action; // ADD/REMOVE
        [DataMember (Name = "position")] public GeometryMsgs.Point Position; // 2D, in pixel-coords
        [DataMember (Name = "scale")] public float Scale; // the diameter for a circle, etc.
        [DataMember (Name = "outline_color")] public StdMsgs.ColorRGBA OutlineColor;
        [DataMember (Name = "filled")] public byte Filled; // whether to fill in the shape with color
        [DataMember (Name = "fill_color")] public StdMsgs.ColorRGBA FillColor; // color [0.0-1.0]
        [DataMember (Name = "lifetime")] public duration Lifetime; // How long the object should last before being automatically deleted.  0 means forever
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points; // used for LINE_STRIP/LINE_LIST/POINTS/etc., 2D in pixel coords
        [DataMember (Name = "outline_colors")] public StdMsgs.ColorRGBA[] OutlineColors; // a color for each line, point, etc.
    
        /// Constructor for empty message.
        public ImageMarker()
        {
            Ns = string.Empty;
            Points = System.Array.Empty<GeometryMsgs.Point>();
            OutlineColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// Explicit constructor.
        public ImageMarker(in StdMsgs.Header Header, string Ns, int Id, int Type, int Action, in GeometryMsgs.Point Position, float Scale, in StdMsgs.ColorRGBA OutlineColor, byte Filled, in StdMsgs.ColorRGBA FillColor, duration Lifetime, GeometryMsgs.Point[] Points, StdMsgs.ColorRGBA[] OutlineColors)
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
        
        /// Constructor with buffer.
        internal ImageMarker(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new ImageMarker(ref b);
        
        ImageMarker IDeserializable<ImageMarker>.RosDeserialize(ref Buffer b) => new ImageMarker(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            b.Serialize(ref Position);
            b.Serialize(Scale);
            b.Serialize(ref OutlineColor);
            b.Serialize(Filled);
            b.Serialize(ref FillColor);
            b.Serialize(Lifetime);
            b.SerializeStructArray(Points);
            b.SerializeStructArray(OutlineColors);
        }
        
        public void RosValidate()
        {
            if (Ns is null) throw new System.NullReferenceException(nameof(Ns));
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            if (OutlineColors is null) throw new System.NullReferenceException(nameof(OutlineColors));
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/ImageMarker";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1de93c67ec8858b831025a08fbf1b35c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVU204bMRB9Zr9ipDxAK3IpraoKKQ+UUBqJm0hUqUIocryTXVdee2t7gfTre+xNFhA8" +
                "9KFEkXzZmTMzZ46nUSZ8oePp9fHZyXiUNel4Nr04Wczm19Or8YenV2fT2Xx8sLm5ujz7eXp5Mf7YnacX" +
                "89n4U7Y5H00mHeD1yfnljxOAZd9Z5OyoTEvmg1OmION3dnpkRMW+FpL3qfGc070KJamcgqWVdRUJaoz6" +
                "3TDuMqB+PIgfux8Aus/JVRkKJT+ibnzCuuYnPm3lw8eKhxzkYGMrZFDWdLaoaNhWkhVsKw5uvah84YdX" +
                "FvZUW6+SfY8OJvuE+LV6YN2X1rrcZyttRQT1UmjeiXgxvVwhvwBGUCIqlMpJDQJSEj7kLf6x1dZdn349" +
                "ItsErQwvZLzZkLtSWnMeGbwvGZguMYbLmEKM4UuBmhOdrd8rwNG+RUX67XozGoz6Hwaj2yxvnEiVabXi" +
                "oKotgT36bu9JW7QwxrHLXywDwtlG56SFD7Rk1MVYYptFE2wFIBCg15SzRuH5gGhEFQvjIwV8B1Vkr9F7" +
                "cwuCsXpETfKIhD1pWyfQYSvE1Md9dKJrBG0a8bJ6YD8jNsYQGxZiGBayRO0GjUk5bPoz/s+/7Hx2ekhd" +
                "eu1TyXo0C8LkwuWgKYhcBJGSKlWBXvc1KNNwElUNUtLXKHE/gOO8VJ7wL9iwS6Qn5iAPaasKz0WKwBQb" +
                "+swfnuBMUC0cmtVo4VrqlInmKwfFRnT8PePBGck0nRzCxniWTVBIaA0E6Vj42PfphJJUo/j5d9ab39s+" +
                "jlxEqW6DQ0EixGT5oXbsY57CHyLG+7a4AbBBDiNK7mkv3S1w9O8IQZAC1xZd2kPmV+tQQqxRknfCKbHU" +
                "GAqeouyAuhuddt89QY5pH2JQGLuFbxEfY/wLrOlwY039Ej2DZAryTQECYVg7e6dymC7XCURqxRgbWi2d" +
                "cOssvasUMut9ixzDCF6pI1iF91YqNKCdbtvRmbqxwER8IzUWL1/iVligKgiFdxuL6YafXUXlpIEIzlaO" +
                "UVQav2n+ff5ED91u3e3+vFX6L996N4ddtyu63bLbiSz7CwAJEAMbBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
