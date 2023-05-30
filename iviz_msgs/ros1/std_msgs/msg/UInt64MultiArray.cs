/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class UInt64MultiArray : IHasSerializer<UInt64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public ulong[] Data;
    
        public UInt64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<ulong>.Value;
        }
        
        public UInt64MultiArray(MultiArrayLayout Layout, ulong[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public UInt64MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                int n = b.DeserializeArrayLength();
                ulong[] array;
                if (n == 0) array = EmptyArray<ulong>.Value;
                else
                {
                    array = new ulong[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public UInt64MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                ulong[] array;
                if (n == 0) array = EmptyArray<ulong>.Value;
                else
                {
                    array = new ulong[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public UInt64MultiArray RosDeserialize(ref ReadBuffer b) => new UInt64MultiArray(ref b);
        
        public UInt64MultiArray RosDeserialize(ref ReadBuffer2 b) => new UInt64MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.Serialize(Data.Length);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Layout.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Data.Length);
            b.Align8();
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Layout, nameof(Layout));
            Layout.RosValidate();
            BuiltIns.ThrowIfNull(Data, nameof(Data));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 8 * Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Layout.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Data.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Data.Length;
            return size;
        }
    
        public const string MessageType = "std_msgs/UInt64MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6088f127afb1d6c72927aa1247e945af";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
    
        public Serializer<UInt64MultiArray> CreateSerializer() => new Serializer();
        public Deserializer<UInt64MultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<UInt64MultiArray>
        {
            public override void RosSerialize(UInt64MultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(UInt64MultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(UInt64MultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(UInt64MultiArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(UInt64MultiArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<UInt64MultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out UInt64MultiArray msg) => msg = new UInt64MultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out UInt64MultiArray msg) => msg = new UInt64MultiArray(ref b);
        }
    }
}
