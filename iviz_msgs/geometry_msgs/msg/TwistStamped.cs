/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwistStamped : IDeserializable<TwistStamped>, IMessage
    {
        // A twist with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "twist")] public Twist Twist;
    
        /// Constructor for empty message.
        public TwistStamped()
        {
        }
        
        /// Explicit constructor.
        public TwistStamped(in StdMsgs.Header Header, in Twist Twist)
        {
            this.Header = Header;
            this.Twist = Twist;
        }
        
        /// Constructor with buffer.
        internal TwistStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Twist);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwistStamped(ref b);
        
        TwistStamped IDeserializable<TwistStamped>.RosDeserialize(ref Buffer b) => new TwistStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Twist);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/TwistStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTWvbQBC9768Y8CFOcVRISg+BHgqlrQ+FQEKvYawdS0ukXXV3FEf99X27spWEXnpo" +
                "awTWx8ybeW/e7Io+kh5cUjo4bSnKXqL4WqgOIVrnWYX2kXsh9pbU9ZKU+8F8FbYSqS1/5q4gFBxjPvzl" +
                "n/l2++Waktr7PjXp7VzZrOhW0RJHS70oW1amfUBHrmklXnTyKB2VXsVS+arTIKlC4l3rEuFqxEvkrpto" +
                "TAjSANJ9P3pXZ9YL11M+Mp0npoGjunrsOP4mUkbHleTHWETcfrpGjE9Sj+rQ0ASEOgon5xt8JDM6r1eX" +
                "OcGs7g7hAo/SQNelOGnLmpuVpyFKyn1yukaNNzO5CtgQR1DFJlqXd/d4TOeEImhBhlC3tEbnN5O2wQNQ" +
                "6JGj410nGbiGAkA9y0ln5y+QfYH27MMJfkZ8rvEnsH7BzZwuWsysy+zT2EBABA4xPDqL0N1UQOrOiVfq" +
                "3C5ynEzOmkua1ediRM3jKxPBP6cUaocB2GJgkzRm9DKNe2f/lRsbCXBdnGZLFvufjHUaVCIMHL1pHjoa" +
                "EtAYGPrtYngQj5cwnNMEpl4gRd4v9k3xVbYYrPpdag3xio4hz8/HuP/D7lj1xC9K5ocRQXxQzN9eE6zy" +
                "CmyLaYOH5XthzBNkl0wkWheR6oKvgIojB6srG8hBNkA5H7KcPT8AUuCgnM3DADCscWSfOs65VBSktVRN" +
                "taFDC1VLVHZA2dey4a6m6Bpn50wU6pdkpiO5Den+Eg7qurnnuRjsCJAYtCScV7Td0xRGOmRCuInHgyXQ" +
                "Tpa+ygJoCJt8qhwhXgt6EzB7yJISN9gVnxRHWmXMvgus79/R03I3LXc/zS8OwyLFqgUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
