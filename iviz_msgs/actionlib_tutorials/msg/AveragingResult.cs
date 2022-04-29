/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class AveragingResult : IDeserializable<AveragingResult>, IResult<AveragingActionResult>
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new AveragingResult(ref b);
        
        public AveragingResult RosDeserialize(ref ReadBuffer b) => new AveragingResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(InteriorAngle);
            b.Serialize(Apothem);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b06c6e2225f820dbc644270387cd1a7c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE+NKy8lPLDE2UsjMK0ktyswvik/MS89J5YIJJxbkl2Sk5nIBAEOeaCAoAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
