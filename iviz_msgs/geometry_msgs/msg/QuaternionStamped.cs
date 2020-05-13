using System.Runtime.Serialization;

namespace Iviz.Msgs.geometry_msgs
{
    [DataContract]
    public sealed class QuaternionStamped : IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        
        [DataMember] public std_msgs.Header header { get; set; }
        [DataMember] public Quaternion quaternion { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public QuaternionStamped()
        {
            header = new std_msgs.Header();
        }
        
        /// <summary> Explicit constructor. </summary>
        public QuaternionStamped(std_msgs.Header header, Quaternion quaternion)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.quaternion = quaternion;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal QuaternionStamped(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.quaternion = new Quaternion(b);
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new QuaternionStamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.Serialize(this.quaternion);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 32;
                size += header.RosMessageLength;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/QuaternionStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VTTYvcMAy9+1cI5rC7hZlCW3oY6K3041Bo2b0PmlhJDImdlZWZTX99nz1MdqEUemiD" +
                "wXIsPT09yRt66EMmlUklS7RMHClpgMkWUqRzsB7XrajERqhJSX2IbEKt8ihw92RhlGw8Tjvnvgh7Uerr" +
                "5n7M8NRYgB5X07kP//hz3+4/7ymbP4y5y68vHNyG7g30WD2NYuzZmNoEbqHrRbeDnGSgyls81VtbJsk7" +
                "BFZRsDqJojwMC80ZTpYgwDjOMTRFgbXuazwiQySmidVCMw+svwlW0LGyPM5V0K8f9/CJWZrZAggtQGhU" +
                "OIfY4ZLcHKK9fVMC3ObhnLY4SgeF1+RkPVshK0+liYUn5z1yvLoUtwM2xBFk8Zlu678DjvmOkAQUZEpN" +
                "T7dg/n2xHq2yXujEGvg4SAFuoABQb0rQzd0L5FihI8d0hb8gPuf4G9i44paatj16NpTq89xBQDhOmk7B" +
                "w/W4VJBmKPNJQzgq6+JK1CWl23yqQ2mlfbUj2Dnn1AQ0wNdhdtm0oNduHIL/X9PYScLU6XIZyeeHcJ2u" +
                "Pz85KNaqoKSJoWV4+XTK/I54Ze2Q2N6/o6fVWlbr52qdnfsFl6snG+ADAAA=";
                
    }
}
