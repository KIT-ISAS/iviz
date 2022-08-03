/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [DataContract]
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
    
        public ImageMarker()
        {
            Ns = "";
            Points = System.Array.Empty<GeometryMsgs.Point>();
            OutlineColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        public ImageMarker(ref ReadBuffer b)
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
        
        public ImageMarker RosDeserialize(ref ReadBuffer b) => new ImageMarker(ref b);
        
        public ImageMarker RosDeserialize(ref ReadBuffer2 b) => new ImageMarker(ref b);
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 93;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Ns);
                size += 24 * Points.Length;
                size += 16 * OutlineColors.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
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
    
        public const string MessageType = "visualization_msgs/ImageMarker";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1de93c67ec8858b831025a08fbf1b35c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUTW/bOBA9R79iAB/6gfgjaVEsAvjQjdPUQJoEtrHAIgiMsTS2WFCkSlJJvL9+HylZ" +
                "SdEc9rAVBJCiZt7MvDecRpnwB53PF+dXF9NJ1qTPq/n1xXq5Wsxvpycvj67my9X0tDu5vbn6+/Lmevqh" +
                "/55fr5bTj1n3/Xk26wEXF99u/roAWPZVuBBHZVoyH5wyOzL+6GhAhivxNedyTI2Xgh5VKEkVFCxtrauI" +
                "qTHqRyM4y4D64TT+7B8A9L+TqzIUSnlG7XzCvpYXPm3l4+eKxxLyUWfLeVDW9LaoaNxWku3EVhLcfl35" +
                "nR/fWthTbb1K9gM6nR0T4tfqSfQwt9YVPttqyxHU56zlKOLF9AqF/AIYQYmoMFcu1yAgJeFD0eKfW23d" +
                "4vLPz2SboJWRdR5POnK3SmspIoOPpQDTJcZwSB0FvmTUnOhs/V4BjvYtKtJv17vJaDI8GU3us6JxnCrT" +
                "aitBVQcCB/TVPpK2kDDGsZvvkgeEs40uSLMPtBHUJViizNwEWwEIBOg9FaJReDEimlAlbHykQB7QFdlr" +
                "9N7dg2CsHlFTe0TCXsjWN+i4bcSk4zGU6IWgTohfqwf2T8TGGNyxEMMI5yXF38dtDp0+0//5yb4tL8+o" +
                "T6+9KtmAloFNwa4ATYELDpySKtUOWg81KNNw4qoGKelvbHE/guOqVJ7w7sSIS6Qn5tAeua0qXJecg1AU" +
                "9Cd/eIIzppodxGo0u5Y6ZaL51qFjIzpeL7hwJheaz85gY7zkTVBIaA+E3An7qPt8RqlVY/PLD3B7t7D+" +
                "5D4brB7tEOeyiz17yAKtxCFmLU+1Ex8TZn+GYO/bKkcIApYE4QpPb9PZGp/+HSEacpHaQq63KOF2H0rb" +
                "3oEHdoo3WiJw7D+gvolOb969QDYJ2rCxB/gW8TnGf4E1PW6saVhCPB1p8M0OTMKwdvZBFTDd7BNIrpVg" +
                "fmi1cez2WbpgKWQ2+BLJhhG8kjRY2XubKyjRjrnDDE2yrDEaf1NbvnIlDx0GqgIrXOBYTD8F7Ta2UJqM" +
                "4GzrBEWlOZwG4aeP9NTv9v3un9+V/q+Xvh/Irt/t+t2m33GW/QvNDPeoJAcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
