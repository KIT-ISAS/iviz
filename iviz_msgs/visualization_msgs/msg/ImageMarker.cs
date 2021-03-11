/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/ImageMarker")]
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
            Ns = string.Empty;
            Points = System.Array.Empty<GeometryMsgs.Point>();
            OutlineColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACrVU22obMRB9zkL+YcAPaUp8SVJKCfghjdPEkBuxKZQSjKwd76popa2kTeJ+fY+0ziYh" +
                "fehDawy67MyZmTNH0ygTPtHJ9Pbk4nQ8ypp0vJhenS5m89vpzXj/5dXFdDYfH2xubq4vvp1dX40Pu/P0" +
                "aj4bf8g25+PJpAO8Pb28/noKsOycRc6OyrRkPjhlCjJ+a6tHRlTsayF5jxrPOT2oUJLKKVhaWVeRoMao" +
                "nw3jLgPq4UH82P0A0H1OrspQKPkZdeMT1jW/8GkrHz5XPOQgBxtbIYOyprNFRcO2kqxgW3Fw60XlCz+8" +
                "sbCn2nqV7Ht0MNkjxK/VI+u+tNblPltpKyKol0LzVsSL6eUK+QUwghJRoVROahCQkvAhb/FPrLbu9uzz" +
                "MdkmaGV4IePNhtyV0przyOBDycB0iTFcxhRiDF8K1JzobP3+ABztW1Sk367fR4NRf38wusvyxolUmVYr" +
                "Dqp6IrBH5/aBtEULYxy7/MEyIJxtdE5a+EBLRl2MJbZZNMFWAAIBek05axSeD4hGVLEwPlLA91BF9id6" +
                "v9+BYKweUZM8ImEv2tYJdNgKMfVxD53oGkGbRrytHtiviI0xxIaFGIaFLFG7QWNSDm1/trPxP/5tZ5ez" +
                "syPqEmwfy3bWo1kQJhcuB1NB5CKIlFepCrS7r8GahpeoavCSvkaV+wEc56XyhH/Bhl3iPZEHhUhbVXgx" +
                "UgSm2NNX/vAEbYJq4dCvRgvXsqdMNF85iDai4+8Zb85IpunkCDbGs2yCQkJrIEjHwsfWTyeU1Br1zz+z" +
                "3vzB9nHkIqr1KThEJEJMlh9rxz7mKfwRYrxvixsAG+wwouSe3qW7BY5+lxAEKXBt0ah3yPxmHUroNary" +
                "XjgllhpzwVNUHlB3otPO7gvkmPYRZoWxT/At4nOMv4E1HW6sqV+iZ1BNQb4pQCAMa2fvVQ7T5TqBSK0Y" +
                "k0OrpRNunaWnlUJmvS+RYxjBK3UEq/DeSoUGtAPuaXqmbiwwFP+bIIu3zzGKMmkLbAWh8HpjPd0ItKso" +
                "njQWQdvKMepKQzhNwY8f6LHbrbvdr/9Xwds3v90NZNftim637HYCSf0Geg+ejiUHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
