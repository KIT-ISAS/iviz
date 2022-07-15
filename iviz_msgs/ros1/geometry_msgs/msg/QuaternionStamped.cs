/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class QuaternionStamped : IDeserializable<QuaternionStamped>, IMessageRos1
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "quaternion")] public Quaternion Quaternion;
    
        /// Constructor for empty message.
        public QuaternionStamped()
        {
        }
        
        /// Explicit constructor.
        public QuaternionStamped(in StdMsgs.Header Header, in Quaternion Quaternion)
        {
            this.Header = Header;
            this.Quaternion = Quaternion;
        }
        
        /// Constructor with buffer.
        public QuaternionStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Quaternion);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new QuaternionStamped(ref b);
        
        public QuaternionStamped RosDeserialize(ref ReadBuffer b) => new QuaternionStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Quaternion);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 32 + Header.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/QuaternionStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTYvcMAy9+1cI5rC7hZlCW3oY6K3041Bo2b0PmlhJDImdlZWZTX99nz1MdqEUemiD" +
                "wXIsPT09yRt66EMmlUklS7RMHClpgMkWUqRzsB7XrajERqhJSX2IbEKt8ihw92RhlGw8Tjvnvgh7Uerr" +
                "5n7M8NRYgB5X07kP//hz3+4/7ymbP4y5y68vHNyG7g30WD2NYuzZmNoEbqHrRbeDnGSgyls81VtbJsk7" +
                "BFZRsDqJojwMC80ZTpYgwDjOMTRFgbXuazwiQySmidVCMw+svwlW0LGyPM5V0K8f9/CJWZrZAggtQGhU" +
                "OIfY4ZLcHKK9fVMC3ObhnLY4SgeF1+RkPVshK0+liYUn5z1yvLoUtwM2xBFk8Zlu678DjvmOkAQUZEpN" +
                "T7dg/n2xHq2yXujEGvg4SAFuoABQb0rQzd0L5FihI8d0hb8gPuf4G9i44paatj16NpTq89xBQDhOmk7B" +
                "w/W4VJBmKPNJQzgq6+JK1CWl23yqQ2mlfbUj2Dnn1AQ0wNdhdtm0oNduHIL/X9PYScLU6XIZyeeHcJ2u" +
                "Pz85KNaqoKSJoWV4+XTK/I54Ze2Q2N6/o6fVWlbr52qdnfsFl6snG+ADAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
