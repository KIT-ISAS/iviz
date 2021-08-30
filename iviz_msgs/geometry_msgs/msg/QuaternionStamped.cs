/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/QuaternionStamped")]
    public sealed class QuaternionStamped : IDeserializable<QuaternionStamped>, IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "quaternion")] public Quaternion Quaternion;
    
        /// <summary> Constructor for empty message. </summary>
        public QuaternionStamped()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public QuaternionStamped(in StdMsgs.Header Header, in Quaternion Quaternion)
        {
            this.Header = Header;
            this.Quaternion = Quaternion;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public QuaternionStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Quaternion = new Quaternion(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new QuaternionStamped(ref b);
        }
        
        QuaternionStamped IDeserializable<QuaternionStamped>.RosDeserialize(ref Buffer b)
        {
            return new QuaternionStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Quaternion.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get {
                int size = 32;
                size += Header.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
