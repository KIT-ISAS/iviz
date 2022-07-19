/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.VisualizationMsgs
{
    [DataContract]
    public sealed class ImageMarker : IDeserializableRos2<ImageMarker>, IMessageRos2
    {
        public const int CIRCLE = 0;
        public const int LINE_STRIP = 1;
        public const int LINE_LIST = 2;
        public const int POLYGON = 3;
        public const int POINTS = 4;
        public const int ADD = 0;
        public const int REMOVE = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Namespace which is used with the id to form a unique id.
        [DataMember (Name = "ns")] public string Ns;
        // Unique id within the namespace.
        [DataMember (Name = "id")] public int Id;
        // One of the above types, e.g. CIRCLE, LINE_STRIP, etc.
        [DataMember (Name = "type")] public int Type;
        // Either ADD or REMOVE.
        [DataMember (Name = "action")] public int Action;
        // Two-dimensional coordinate position, in pixel-coordinates.
        [DataMember (Name = "position")] public GeometryMsgs.Point Position;
        // The scale of the object, e.g. the diameter for a CIRCLE.
        [DataMember (Name = "scale")] public float Scale;
        // The outline color of the marker.
        [DataMember (Name = "outline_color")] public StdMsgs.ColorRGBA OutlineColor;
        // Whether or not to fill in the shape with color.
        [DataMember (Name = "filled")] public byte Filled;
        // Fill color; in the range: [0.0-1.0]
        [DataMember (Name = "fill_color")] public StdMsgs.ColorRGBA FillColor;
        // How long the object should last before being automatically deleted.
        // 0 indicates forever.
        [DataMember (Name = "lifetime")] public duration Lifetime;
        // Coordinates in 2D in pixel coords. Used for LINE_STRIP, LINE_LIST, POINTS, etc.
        [DataMember (Name = "points")] public GeometryMsgs.Point[] Points;
        // The color for each line, point, etc. in the points field.
        [DataMember (Name = "outline_colors")] public StdMsgs.ColorRGBA[] OutlineColors;
    
        /// Constructor for empty message.
        public ImageMarker()
        {
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            OutlineColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        /// Constructor with buffer.
        public ImageMarker(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Ns);
            b.Deserialize(out Id);
            b.Deserialize(out Type);
            b.Deserialize(out Action);
            b.Deserialize(out Position);
            b.Deserialize(out Scale);
            b.Deserialize(out OutlineColor);
            b.Deserialize(out Filled);
            b.Deserialize(out FillColor);
            b.Deserialize(out Lifetime);
            b.DeserializeStructArray(out Points);
            b.DeserializeStructArray(out OutlineColors);
        }
        
        public ImageMarker RosDeserialize(ref ReadBuffer2 b) => new ImageMarker(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (Ns is null) BuiltIns.ThrowNullReference();
            if (Points is null) BuiltIns.ThrowNullReference();
            if (OutlineColors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Ns);
            WriteBuffer2.AddLength(ref c, Id);
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, Action);
            WriteBuffer2.AddLength(ref c, Position);
            WriteBuffer2.AddLength(ref c, Scale);
            WriteBuffer2.AddLength(ref c, OutlineColor);
            WriteBuffer2.AddLength(ref c, Filled);
            WriteBuffer2.AddLength(ref c, FillColor);
            WriteBuffer2.AddLength(ref c, Lifetime);
            WriteBuffer2.AddLength(ref c, Points);
            WriteBuffer2.AddLength(ref c, OutlineColors);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/ImageMarker";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
