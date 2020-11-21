/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Int16MultiArray")]
    public sealed class Int16MultiArray : IDeserializable<Int16MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public short[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Int16MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<short>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Int16MultiArray(MultiArrayLayout Layout, short[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Int16MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<short>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Int16MultiArray(ref b);
        }
        
        Int16MultiArray IDeserializable<Int16MultiArray>.RosDeserialize(ref Buffer b)
        {
            return new Int16MultiArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 2 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Int16MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d9338d7f523fcb692fae9d0a0e9f067c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBuuSuI7rjxCagA+GQC8pFFIoxZiw0Y6stSWt2V3FTX99364+rKQ9" +
                "li4ykmd2Zt57O7MJfS1YWKZC6wMJRy5n+lIXTq2NEa8P4lXXjkq2VuyYJGeqUk7pijJt4ighqdO65MqJ" +
                "YMQjioJKHy98vJ3GURz9kY+K9t2shOyRU5WptE2TkRROtLviSFVufrPZdtuxgrtfCYViXZwvGUerf7xA" +
                "4/HzHVknn0q7sx/fk/JqfIN4Z/KQKy2EYUuCdlyxUWnjvZIKmllQFcUZOnAndBTGqbRGWMPRvR55SnTf" +
                "BSCXYdJGsmFJmdEloTYbKrUNEJwmVVWt4Z36fRZoCQjQbd3r1rnoaPSRAYJtHNVQfrkISJ50llkeHNlR" +
                "SKmqHXHBvgEAzHk8lRseAyqkKZpHG0s213Uhaf3wff3jkZ6ZTkY5xxXwEhiU9i0O64yS7FOISnYNAs6B" +
                "7pVnN9icKRPYJuR/5yO4UJP95HBJq4BoMyTywYc/NVU28+1YvbUstuM9LIctEgYeAAQgwsgJLa/SXEDk" +
                "gm6uZz+vP81IlX48TsrlYAN8mKkXYE11oQ21m6FnQqegAcifCQl719ZA+c1sOy3EM1ID8yhntcvdaOCz" +
                "6hdD/hWh6tAcQMO8HAPS2ENa0e1ifjObEV1U2nG7s5WVlKV9DQlDPugeCFx2GedDECclXT4auHoMKDU0" +
                "v8GA9/x20fsXw4ytIKOBs8+5HBr7jK1Afx6s4YzRW+h6f3F5/Y0+TWiPD4hfl9UktM/B/2+qTv/v5XDf" +
                "9WgceToYmFYHjFDzBfV36gVz0LdzP3itKv6CbM/p3U668MODO4Jq3MsWB9hFNtL5yObrb1V+A8fl88j9" +
                "BQAA";
                
    }
}
