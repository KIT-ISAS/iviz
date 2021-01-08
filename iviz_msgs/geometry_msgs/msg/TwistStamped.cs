/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/TwistStamped")]
    public sealed class TwistStamped : IDeserializable<TwistStamped>, IMessage
    {
        // A twist with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "twist")] public Twist Twist { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwistStamped()
        {
            Header = new StdMsgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwistStamped(StdMsgs.Header Header, in Twist Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwistStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Twist = new Twist(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwistStamped(ref b);
        }
        
        TwistStamped IDeserializable<TwistStamped>.RosDeserialize(ref Buffer b)
        {
            return new TwistStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 48;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrbQBC9C/QPAzkkKY4KSenB0EOhtM2hEEjoNYylsbRE2lV3R3HUr++ble0k9NJD" +
                "W2MjWZp5897Mmz2hj6Q7l5R2TjuKspUovhaqQ4iN86xC28iDEPuG1A2SlIexLL4KNxKpy5eyuMsYGaks" +
                "yuLDX/6UxbfbL2tK2twPqU1vl+plcUK3CmIcGxpEuWFl2gbQcm0n8aKXR+kpM5aG8ludR0mVZd51LhG+" +
                "rXiJ3PczTQlRGqB9GCbvahN/lHwAsFTniWnkqK6eeo6/NSvj2y/Jjym38/rTGlE+ST2pA6kZGHUUTs63" +
                "eIngyXm9urQMJN7twgX+S4sWHxmQdqzGWJ7GKMnIclpbmTeLxgrwaJKgUJPoLD+7x990TqgDFjKGuqMz" +
                "0L+ZtQseiEKPHB1vejHkGn0A7KklnZ6/hDbqa/LswwF/gXwu8ie4/hnYZF10GF5vLUhTiz4icozh0TWI" +
                "3cwZpe6deKXebSLHuSwsbSkKkM/ZmWqDzLPBlVMKtcMkmuzoskgarUCey71r/qE7WwkwYZwXi+aNOPrs" +
                "MLJEGD4IqhkApARiRkYfNzE8iMdD+M9pgmAv6IhtHfs2u8wMZ9b9LrWGeEX7mBcP9pH/TeO+8FFlFFOJ" +
                "eWEOEGovX8us8lZcZxcHjy0YhDFdaD6mIrNxEbku+AqwOI+w0bJCV6gJaKAPua0DPwBU4ChL53EEGtY7" +
                "sk89W7I9Rs6ZVG21ol2H7uYoc8OyxnnzXU3RtQ6Lb6koNRyzmfYCV6TbS/ip7xfWSzXY01Bi0JxxXtH1" +
                "luYw0c404Sbuj5xAG5DcM8sroSGs7Lg5YLxu602ADdCalLjF+vikOO4w+LLY9oH1/Tt6er7FThxuf5bF" +
                "L+aXiO7QBQAA";
                
    }
}
