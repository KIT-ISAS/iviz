/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract (Name = "visualization_msgs/ImageMarker")]
    public sealed class ImageMarker : IDeserializable<ImageMarker>, IMessage
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
        public ImageMarker(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Ns = b.DeserializeString();
            Id = b.Deserialize<int>();
            Type = b.Deserialize<int>();
            Action = b.Deserialize<int>();
            Position = new GeometryMsgs.Point(ref b);
            Scale = b.Deserialize<float>();
            OutlineColor = new StdMsgs.ColorRGBA(ref b);
            Filled = b.Deserialize<byte>();
            FillColor = new StdMsgs.ColorRGBA(ref b);
            Lifetime = b.Deserialize<duration>();
            Points = b.DeserializeStructArray<GeometryMsgs.Point>();
            OutlineColors = b.DeserializeStructArray<StdMsgs.ColorRGBA>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ImageMarker(ref b);
        }
        
        ImageMarker IDeserializable<ImageMarker>.RosDeserialize(ref Buffer b)
        {
            return new ImageMarker(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Ns);
            b.Serialize(Id);
            b.Serialize(Type);
            b.Serialize(Action);
            Position.RosSerialize(ref b);
            b.Serialize(Scale);
            OutlineColor.RosSerialize(ref b);
            b.Serialize(Filled);
            FillColor.RosSerialize(ref b);
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
                "H4sIAAAAAAAACrVU227bOBB9jgD9wwB+6AXxpWlRLAL4oY2zqYE0CWKjwKIoDFoaS1xQokpSSbxfv2eo" +
                "ykrQPPSh9QWkRpwzM2cOp9V1+IvOlrdnl+fzWZq08flyeXW+Wa1vlzfzN09sl8vVen7Sm26uL/+5uL6a" +
                "vx0My6v1av4uTXrLh8VigL09/3z95Vwg0+QTq5wdlXFJEx+crguq/dHRiGpVsW9UxsfUes7pXoeSdE7B" +
                "0s66ihS1tf7eMmxpAuS3J/L28AHC4X301TWFkgfY3insG37k1LEwHYqfcsgm/WGVBW3rw2EUNu3qSZOC" +
                "bcXB7TeVL/z0xsKBGut1dBjRyeKYkEKjH9iMM2td7tNkZ6wSWJ8pw0eCKCnmGjkG8II6UWamXWbAQpeH" +
                "D3kX4cwa624vPn4g2waja95kYulp3mljOBci70sGqovEwShZSBRfKhQeWf3h+Ay0OHS4KKFbv84ms/Gb" +
                "yexbmuStU7E8o3ccdNXzOKJP9p6MRS8lkt3+y1lAQNuanIzygbaM2hiL9Fu1wVYAAglmTzkbFJ9PiGZU" +
                "saq90MB3og/5PkPz128gGqtH4CgV4e1RAw+inXbSjB09RkcODaG+IT9TAPAn/EoQ9YMKicMqK1F/jQbF" +
                "JPo+zX/zJ00+ry5O6ZBgd3XSZESroOpcuRxsBZWroGJepS7Q9LEBcwZeqmpATHwrgvfIcETrUnvCr+Ca" +
                "XSQ/0gehZLaqcH0yFZiksU8AxBXMKWqUQ9dao1xHoK7l/M5BvhFf/p5xB+uMabk4xanac9YGjaT2wMgc" +
                "Ky8SWC6oE65cBv4Ox/W9HeOZC1FunwHkpIJkzA+NYy/JKn8qYV53NU4AD5IYgXJPL6Ntg0f/ihAHWXBj" +
                "0a+XSP9mH0pIVwR6p5xWW4NR4UlECNgX4vTi1WNoSf0U86O2PX4HOQT5Fdx6AJayxiWaB/kU5NsCPOJk" +
                "4+ydznF2u48omdGMWWL01im3T5N40WJQgPwtZOMY/GJvsCrvbabRiW7uHcZq7MtGpuUfU+czlzPtdQbK" +
                "gtK4zlLTYTDanegoDktwt3OM0rrpHGfj+3f0MGxRfL/97w9W8fMQGCY1bly/LYbtdtgqSex/qIXEh1EH" +
                "AAA=";
                
    }
}
