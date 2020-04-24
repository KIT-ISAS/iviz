namespace Iviz.Msgs.geometry_msgs
{
    public sealed class TwistStamped : IMessage
    {
        // A twist with reference coordinate frame and timestamp
        public std_msgs.Header header;
        public Twist twist;
    
        /// <summary> Constructor for empty message. </summary>
        public TwistStamped()
        {
            header = new std_msgs.Header();
            twist = new Twist();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            twist.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            twist.Serialize(ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 48;
            size += header.GetLength();
            return size;
        }
    
        public IMessage Create() => new TwistStamped();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TwistStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "98d34b0043a2093cf9d9345ab6eef12e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
