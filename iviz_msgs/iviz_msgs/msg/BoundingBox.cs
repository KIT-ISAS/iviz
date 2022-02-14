/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class BoundingBox : IDeserializable<BoundingBox>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "center")] public GeometryMsgs.Pose Center;
        [DataMember (Name = "size")] public GeometryMsgs.Vector3 Size;
    
        /// Constructor for empty message.
        public BoundingBox()
        {
        }
        
        /// Explicit constructor.
        public BoundingBox(in StdMsgs.Header Header, in GeometryMsgs.Pose Center, in GeometryMsgs.Vector3 Size)
        {
            this.Header = Header;
            this.Center = Center;
            this.Size = Size;
        }
        
        /// Constructor with buffer.
        public BoundingBox(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Center);
            b.Deserialize(out Size);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new BoundingBox(ref b);
        
        public BoundingBox RosDeserialize(ref ReadBuffer b) => new BoundingBox(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Center);
            b.Serialize(in Size);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 80 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/BoundingBox";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "b62f495883fa58652affe5e9c85a03e9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VTWvcMBC9+1cM5JBN2biQlB4CPRRKmz0UUhJ6DbPS2Ba1JUeSd+P8+j7Ju94uCaSH" +
                "NovBH5r39N7MaPZaWIunJt+KWlwn0Y/3XajD+xsXhJTY+Gzlp6jo/CUF8yRF8ekf/4rvt9+uKEQ9bXY9" +
                "aTuh28hWs9cEJaw5MlUO0k3diD9vZSMtQNz1oimvxrGXUAJ415hAuGqx4rltRxoCgqIj5bpusEZxFIqm" +
                "kyM8kMYSU88+GjW07BHvvDY2hVeeO0nsuII8DGKV0OrLFWJsEDVEA0EjGJQXDsbWWKRiMDZeXiRAcXK3" +
                "ded4lRoFmDen2HBMYuWx9xKSTg5X2OPdZK4EN5Ij2EUHWuRv93gNZ6hGkiC9Uw0toPxmjI2zIBTasDe8" +
                "biURK2QArKcJdHr2B7PN1Jat29NPjIc9/obWzrzJ03mDmrXJfRhqJBCBvXcboxG6HjOJag16jFqz9uzH" +
                "IqGmLYuTrynHCAIqVwR3DsEpgwJo2prYFCH6xJ6rcW/0/+rG5ycDBj+Tl1QkyOdokBNXUZ8ODbJUeYGN" +
                "npUsU5elz3q3bnIs8kLOmz22pOLGoRvmgOLHAJfeZt5D3FsZhJT9yUEvRDY25GrN+uEFRyNLPrJbVK3j" +
                "+PEDPc5P4/z09DbyD6nbe5gLhQ46yuex+PT2cMg75ktXFq842j9t38bbbva+ZIw2ee3YUpkG1CqPFGcx" +
                "kDphlAyzb0YCqI0HNLfhHaapwDj61kTSTgJZl3qh41+gFJzvhOa+BxmGrGcb2imV+AzIQsq6XNK2ETtF" +
                "pfOZp2mev0aRN7XREzJleAYz7cwtKVYXON9tO2meNkP7gcS7qXBnJa0qGt1A22QID3439h2tZdaVx1N0" +
                "bplm/o7ihV5HWkLgOjVAiPjDebXqvwHrxRxTOQcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
