/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeResult : IHasSerializer<ShapeResult>, IMessage, IResult<ShapeActionResult>
    {
        //result definition
        [DataMember (Name = "interior_angle")] public float InteriorAngle;
        [DataMember (Name = "apothem")] public float Apothem;
    
        public ShapeResult()
        {
        }
        
        public ShapeResult(float InteriorAngle, float Apothem)
        {
            this.InteriorAngle = InteriorAngle;
            this.Apothem = Apothem;
        }
        
        public ShapeResult(ref ReadBuffer b)
        {
            b.Deserialize(out InteriorAngle);
            b.Deserialize(out Apothem);
        }
        
        public ShapeResult(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out InteriorAngle);
            b.Deserialize(out Apothem);
        }
        
        public ShapeResult RosDeserialize(ref ReadBuffer b) => new ShapeResult(ref b);
        
        public ShapeResult RosDeserialize(ref ReadBuffer2 b) => new ShapeResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        [IgnoreDataMember] public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        [IgnoreDataMember] public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "turtle_actionlib/ShapeResult";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b06c6e2225f820dbc644270387cd1a7c";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+NKy8lPLDE2UsjMK0ktyswvik/MS89J5YIJJxbkl2Sk5nIBAEOeaCAoAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ShapeResult> CreateSerializer() => new Serializer();
        public Deserializer<ShapeResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ShapeResult>
        {
            public override void RosSerialize(ShapeResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ShapeResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ShapeResult _) => RosFixedMessageLength;
            public override int Ros2MessageLength(ShapeResult _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<ShapeResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ShapeResult msg) => msg = new ShapeResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ShapeResult msg) => msg = new ShapeResult(ref b);
        }
    }
}
