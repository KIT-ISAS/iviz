/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Float64MultiArray : IDeserializable<Float64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout; // specification of data layout
        [DataMember (Name = "data")] public double[] Data; // array of data
    
        /// Constructor for empty message.
        public Float64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<double>();
        }
        
        /// Explicit constructor.
        public Float64MultiArray(MultiArrayLayout Layout, double[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Float64MultiArray(ref Buffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            Data = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Float64MultiArray(ref b);
        
        Float64MultiArray IDeserializable<Float64MultiArray>.RosDeserialize(ref Buffer b) => new Float64MultiArray(ref b);
    
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + 8 * Data.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Float64MultiArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "4b7d974086d4060e7db4613a7e6c3ba4";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UXWvbMBR996+4JC9plmT5KGUt5CEw2EsLgw5GCaGo1nWsRLaCJDfrfv2O/J12j2PG" +
                "YPl+nnOkqyF91ywckzbmSMKTT5keCu3Vxlrxdi/eTOEpY+fEnklyonLllckpMTYakjRxkXHuRWnDK7Sm" +
                "LKSLkO5mUfShGOn6Wz1DcieOVaLiukhCUnhRR0WJNsLfXG93TXzlbZ8hlZ2atCiK1v/4iR4ev92R8/I5" +
                "c3v3+T0fqPADmnWkoVKshWVHgvacs1Vx5Z1KBa0cSArdoRYocBLWq7hAVsXOv514RvS1iUcpy2SsZMuS" +
                "EmsyQme2lBkXAHhDKs/r/wvN2xIQEO0h16aVq3HRyZoTAwG7qFC5Xy1LFM8mSRz39ukkpFT5nlhz2HOA" +
                "8gFL7jvxUT6OcViMdeRSU2hJm/ufm6dHemE6W+U954BKwJ65SxDOWyUZFUQumyMBsiXPaeDVi02UDTyH" +
                "hLcTfqQmh8nxitYlmG2fw6eQ/Fy12C52Y3VpWe7GB1iOu2gYKAALQAgrJ7SaxqmAtJpurue/rr/MSWVh" +
                "Es7KpyACbBifV+CMjTaW6mCHKueSPWh3XIS7Kxug83a+m2nxgrqAO0hZ7VM/6FxO/WZoviZ07FlLtLCu" +
                "xkAzDmjWdLtc3MznRKPceK4jazFJOToUUK4sB7VL7Fd1wUUfwVlJnw46TwsAjXrWCwD4Lm6XjXvZL1fr" +
                "MOh8bcFVz9aWK2X5uJOWE8ZJwvEO11KQ3JrzhA5YQO8iyyflaTmG/6rj7D/OfztbUSCCwaj5Y1SqFRTf" +
                "q1ec+PbkNvNVqxEuv3pr3gXSKEwJrgEqcOG6qzaxkiwkVqu/9PgDCw0evdQFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
