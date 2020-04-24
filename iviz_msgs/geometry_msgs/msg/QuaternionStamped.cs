namespace Iviz.Msgs.geometry_msgs
{
    public sealed class QuaternionStamped : IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        
        public std_msgs.Header header;
        public Quaternion quaternion;
    
        /// <summary> Constructor for empty message. </summary>
        public QuaternionStamped()
        {
            header = new std_msgs.Header();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            quaternion.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            quaternion.Serialize(ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 32;
            size += header.GetLength();
            return size;
        }
    
        public IMessage Create() => new QuaternionStamped();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "geometry_msgs/QuaternionStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
