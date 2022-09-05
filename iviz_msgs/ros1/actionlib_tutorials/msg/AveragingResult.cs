/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingResult : IDeserializable<AveragingResult>, IHasSerializer<AveragingResult>, IMessage, IResult<AveragingActionResult>
    {
        //result definition
        [DataMember (Name = "interior_angle")] public float InteriorAngle;
        [DataMember (Name = "apothem")] public float Apothem;
    
        public AveragingResult()
        {
        }
        
        public AveragingResult(float InteriorAngle, float Apothem)
        {
            this.InteriorAngle = InteriorAngle;
            this.Apothem = Apothem;
        }
        
        public AveragingResult(ref ReadBuffer b)
        {
            b.Deserialize(out InteriorAngle);
            b.Deserialize(out Apothem);
        }
        
        public AveragingResult(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out InteriorAngle);
            b.Deserialize(out Apothem);
        }
        
        public AveragingResult RosDeserialize(ref ReadBuffer b) => new AveragingResult(ref b);
        
        public AveragingResult RosDeserialize(ref ReadBuffer2 b) => new AveragingResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "actionlib_tutorials/AveragingResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b06c6e2225f820dbc644270387cd1a7c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+NKy8lPLDE2UsjMK0ktyswvik/MS89J5YIJJxbkl2Sk5nIBAEOeaCAoAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AveragingResult> CreateSerializer() => new Serializer();
        public Deserializer<AveragingResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AveragingResult>
        {
            public override void RosSerialize(AveragingResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(AveragingResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(AveragingResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(AveragingResult msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<AveragingResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out AveragingResult msg) => msg = new AveragingResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out AveragingResult msg) => msg = new AveragingResult(ref b);
        }
    }
}
