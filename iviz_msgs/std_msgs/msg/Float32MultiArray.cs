/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Float32MultiArray : IDeserializable<Float32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public float[] Data; // array of data
    
        /// Constructor for empty message.
        public Float32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<float>();
        }
        
        /// Explicit constructor.
        public Float32MultiArray(MultiArrayLayout Layout, float[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Float32MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Float32MultiArray(ref b);
        
        Float32MultiArray IDeserializable<Float32MultiArray>.RosDeserialize(ref Buffer b) => new Float32MultiArray(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException(nameof(Layout));
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 4 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Float32MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6a40e0ffa6a17a503ac3f8616991b1f6";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9NlmT5KGUt5CEw2EsLgw5GCaGo1nWsRLaCJDfrfv2O/J12j2PG" +
                "YPl+nnOkqyF91ywckzbmSMKTT5keCu3Vxlrxdi/eTOEpY+fEnklyonLllckpMTYakjRxkXHuRWnDK7Sm" +
                "LKSLkO5mUfShGOn6Wz1DcieOVaLiukhCUnhRR0WJNsKvlttdE19522dIZacmLYqi9T9+oofHb3fkvHzO" +
                "3N59fs8HKvyAZh1pqBRrYdmRoD3nbFVceadSQSsHkkJ3qAUKnIT1Ki6QVbHzbyeeEX1t4lHKMhkr2bKk" +
                "xJqM0JktZcYFAN6QyvP6/0LztgQERHvItWnlalx0subEQMAuKlQOtUsUzyZJHPf26SSkVPmeWHPYc4Dy" +
                "AUvuO/FRPo5xWIx15FJTaEmb+5+bp0d6YTpb5T3ngErAnrlLEM5bJRkVRC6bIwGyJc9p4NWLTZQNPIeE" +
                "txP+Sk0Ok+OI1iWYbZ/Dp5D8XLXYLnZjdWlZ7sYHWI67aBgoAAtACCsntJrGqYC0mm6u57+uv8xJZWES" +
                "zsqnIAJsGJ9X4IyNNpbqYIcq55I9aHdchLsrG6Dzdr6bafGCuoA7SFntUz/oXE79Zmi+JnTsWUu0sK7G" +
                "QDMOaNZ0u1zczOdEV7nxXEfWYpJydCigXFkOapfYR3XBRR/BWUmfDjpPCwCNetYLAPgubpeNe9kvV+sw" +
                "6HxtwVXP1pYrZfm4k5YTxknC8Q7XUpDcmvOEDlhA7yLLJ+VpOYb/quPsP85/O1tRIILBqPljVKoVFN+r" +
                "V5z49uQ281WrES6/emveBdJVmBJcA1TgwnWjNrGSLCRWq7/0+AM6tuzE1AUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
