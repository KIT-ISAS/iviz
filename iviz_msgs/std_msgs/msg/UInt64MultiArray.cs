using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    public sealed class UInt64MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public ulong[] data; // array of data
        
    
        /// <summary> Constructor for empty message. </summary>
        public UInt64MultiArray()
        {
            layout = new MultiArrayLayout();
            data = System.Array.Empty<ulong>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            layout.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out data, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            layout.Serialize(ref ptr, end);
            BuiltIns.Serialize(data, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += layout.RosMessageLength;
                size += 8 * data.Length;
                return size;
            }
        }
    
        public IMessage Create() => new UInt64MultiArray();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "std_msgs/UInt64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "6088f127afb1d6c72927aa1247e945af";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsxJaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6XrBwTIUxexKefM70WBVeLa0Vbw/izVSeSnZObJkkZ0orr4ymzNhkSNKkVcnai2jD" +
                "K4qCyhAuQribJMmHZFQ03/oZkjtwqjKVNkkyksKLZldSKe1vrldrap/opS48VjqFJUly/4+f5PHp2x05" +
                "Lzel27rP7/lAhR/QrCMNldJCWHYkaMuarUpr75VU0MqBpCg61AIJDsJ6lVaIqtn5twNPiL6e9iOVZTJW" +
                "smVJmTUloTJbKo0LALwhpXXzf6Z5mwIKojzkWrZynVx0sObAQMAuyr2YRxQbk2WOe+d0EFIqvSUuOJy5" +
                "C+0CLNp34iN9mqJZjHXkclMVkpYPP5fPT/TCdLTKe9aASsBeunMQzlslGRmElqeWANnI8yrw6u3NlA08" +
                "h4S3E/5CjXfj/SXdRzCrPodPIXhTl1jN1iN1bpmvRztY9utkGCgAC0AIK8e0uEpzAWkLurme/rr+MiVV" +
                "hkk4Kp+DCLBhfF6BMzWFsdRsdshyjOxBu+Mi3F0sgMqr6XpSiBfkBdxBzmqb+0Hncuo3U3ChYs8a0cK6" +
                "GAHNKKC5p9v57GY6JbrQxnOzsxGTlKNdBeViOqgdsV82CWd9BEclfT7oPC0AFOpZzwDgO7udn9zzfrpG" +
                "h0HnaxMuerY2XZTl40lazhidhPYO11KQ3JrjmHZYQO+q1OPYLfvwX1ec/Mf5b2crCUQwGA1/jEq9guJb" +
                "9YqObzv3NF+NGuHya47m3Ua6CFOCa4AqXLjusg2sJQuB9eovNf4AefzhwNQFAAA=";
                
    }
}
