/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingResult : IDeserializableRos1<AveragingResult>, IDeserializableRos2<AveragingResult>, IMessageRos1, IMessageRos2, IResult<AveragingActionResult>
    {
        //result definition
        [DataMember (Name = "interior_angle")] public float InteriorAngle;
        [DataMember (Name = "apothem")] public float Apothem;
    
        /// Constructor for empty message.
        public AveragingResult()
        {
        }
        
        /// Explicit constructor.
        public AveragingResult(float InteriorAngle, float Apothem)
        {
            this.InteriorAngle = InteriorAngle;
            this.Apothem = Apothem;
        }
        
        /// Constructor with buffer.
        public AveragingResult(ref ReadBuffer b)
        {
            b.Deserialize(out InteriorAngle);
            b.Deserialize(out Apothem);
        }
        
        /// Constructor with buffer.
        public AveragingResult(ref ReadBuffer2 b)
        {
            b.Deserialize(out InteriorAngle);
            b.Deserialize(out Apothem);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new AveragingResult(ref b);
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, InteriorAngle);
            WriteBuffer2.AddLength(ref c, Apothem);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib_tutorials/AveragingResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "b06c6e2225f820dbc644270387cd1a7c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+NKy8lPLDE2UsjMK0ktyswvik/MS89J5YIJJxbkl2Sk5nIBAEOeaCAoAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
