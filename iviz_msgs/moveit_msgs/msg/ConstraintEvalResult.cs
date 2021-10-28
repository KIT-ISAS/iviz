/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ConstraintEvalResult")]
    public sealed class ConstraintEvalResult : IDeserializable<ConstraintEvalResult>, IMessage
    {
        // This message contains result from constraint evaluation
        // result specifies the result of constraint evaluation 
        // (true indicates state satisfies constraint, false indicates state violates constraint)
        // if false, distance specifies a measure of the distance of the state from the constraint
        // if true, distance is set to zero
        [DataMember (Name = "result")] public bool Result;
        [DataMember (Name = "distance")] public double Distance;
    
        /// <summary> Constructor for empty message. </summary>
        public ConstraintEvalResult()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public ConstraintEvalResult(bool Result, double Distance)
        {
            this.Result = Result;
            this.Distance = Distance;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal ConstraintEvalResult(ref Buffer b)
        {
            Result = b.Deserialize<bool>();
            Distance = b.Deserialize<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ConstraintEvalResult(ref b);
        }
        
        ConstraintEvalResult IDeserializable<ConstraintEvalResult>.RosDeserialize(ref Buffer b)
        {
            return new ConstraintEvalResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Result);
            b.Serialize(Distance);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 9;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/ConstraintEvalResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "093643083d24f6488cb5a868bd47c090";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE22QQQ7CMAwE73mFpV5A6hHxEj5ggkMtpTGK3R54PU5bKBKcItuzq910cBlYYSRVvBNE" +
                "KYZcFCrplA1SlbEt1aqvDWjGPKGxlNC9GX1Q5MSkYAO9l5L+y8B1B6sTAZcbRzSXqfkD6nddbHZhDwmz" +
                "/rIzS17GHT26MaeV7+HGDpZIX+HQS6JOlVq2lvTDbPNqvTRu4269OrfQX8b+aUoGJvCkKuEqkrfuIWVB" +
                "O58+bAgvrgPjJWYBAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
