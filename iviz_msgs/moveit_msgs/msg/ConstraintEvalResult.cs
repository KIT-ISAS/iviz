/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [DataContract]
    public sealed class ConstraintEvalResult : IDeserializable<ConstraintEvalResult>, IMessage
    {
        // This message contains result from constraint evaluation
        // result specifies the result of constraint evaluation 
        // (true indicates state satisfies constraint, false indicates state violates constraint)
        // if false, distance specifies a measure of the distance of the state from the constraint
        // if true, distance is set to zero
        [DataMember (Name = "result")] public bool Result;
        [DataMember (Name = "distance")] public double Distance;
    
        /// Constructor for empty message.
        public ConstraintEvalResult()
        {
        }
        
        /// Explicit constructor.
        public ConstraintEvalResult(bool Result, double Distance)
        {
            this.Result = Result;
            this.Distance = Distance;
        }
        
        /// Constructor with buffer.
        public ConstraintEvalResult(ref ReadBuffer b)
        {
            b.Deserialize(out Result);
            b.Deserialize(out Distance);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ConstraintEvalResult(ref b);
        
        public ConstraintEvalResult RosDeserialize(ref ReadBuffer b) => new ConstraintEvalResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Result);
            b.Serialize(Distance);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 9;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "moveit_msgs/ConstraintEvalResult";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "093643083d24f6488cb5a868bd47c090";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE22QQQ7CMAwE73mFpV5A6hHxEj5ggkMtpTGK3R54PU5bKBKcItuzq910cBlYYSRVvBNE" +
                "KYZcFCrplA1SlbEt1aqvDWjGPKGxlNC9GX1Q5MSkYAO9l5L+y8B1B6sTAZcbRzSXqfkD6nddbHZhDwmz" +
                "/rIzS17GHT26MaeV7+HGDpZIX+HQS6JOlVq2lvTDbPNqvTRu4269OrfQX8b+aUoGJvCkKuEqkrfuIWVB" +
                "O58+bAgvrgPjJWYBAAA=";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
