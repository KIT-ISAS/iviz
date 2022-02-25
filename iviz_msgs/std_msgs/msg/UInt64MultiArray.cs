/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class UInt64MultiArray : IDeserializable<UInt64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public ulong[] Data;
    
        /// Constructor for empty message.
        public UInt64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<ulong>();
        }
        
        /// Explicit constructor.
        public UInt64MultiArray(MultiArrayLayout Layout, ulong[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public UInt64MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<ulong>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new UInt64MultiArray(ref b);
        
        public UInt64MultiArray RosDeserialize(ref ReadBuffer b) => new UInt64MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 8 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/UInt64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6088f127afb1d6c72927aa1247e945af";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
