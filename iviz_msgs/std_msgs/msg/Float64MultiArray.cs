using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Float64MultiArray")]
    public sealed class Float64MultiArray : IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        [DataMember (Name = "layout")] public MultiArrayLayout Layout { get; set; } // specification of data layout
        [DataMember (Name = "data")] public double[] Data { get; set; } // array of data
    
        /// <summary> Constructor for empty message. </summary>
        public Float64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Float64MultiArray(MultiArrayLayout Layout, double[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Float64MultiArray(Buffer b)
        {
            Layout = new MultiArrayLayout(b);
            Data = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Float64MultiArray(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Layout.RosSerialize(b);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Layout is null) throw new System.NullReferenceException();
            Layout.RosValidate();
            if (Data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Layout.RosMessageLength;
                size += 8 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Float64MultiArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4b7d974086d4060e7db4613a7e6c3ba4";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UXWvbMBR996+4JC9tlmb5KGUt9CEw2EsLgw3GCKGo1nWsRJaCJDfrfv2O/J12j2PG" +
                "YFn365yjezWmr5qFZ9LWHkgECjnTY6mDWjsnXh/Eqy0DFey92DFJzpRRQVlDmXXJmKRNy4JNENUeXqE1" +
                "FTFcxHA/S5J3yUg33/oZkz9yqjKVNkkykiKIxivJtBXh5nqzbf1rK/XhVaU2LEmS+3/8JI/fvtyRD/Kp" +
                "8Dv/8S0fqPAdmvWkoVKqhWNPgnZs2Km0tl5JBa08SArdoxZIcBQuqLREVM0uvB55RvS59Ucqx2SdZMeS" +
                "MmcLQmV2VFgfAQRLypjm/0zzLgUERHnIte7kak10dPbIQMA+KZUJq2WF4slmmefBOR2FlMrsiDXHM/ex" +
                "XYDFhF58pE9TNIt1nnxuSy1p/fBj/fMbPTOdnAqBDaASsBf+HIQPTklGBmFk2xIgW/G8irwGvplykeeY" +
                "8PbCX6jpfnq4pPsKzGbI4UMMfqpLbBbbiTrfWW4ne+wctsk4UgAWgBBOTml1leYC0mq6uZ7/uv40J1XE" +
                "STipkIMIsGF8XoAztdo6apw9spwq9qDdcxH+riqAypv5dqbFM/IC7ihntcvDqDd59ZspmlBxsFuhxe5q" +
                "AjSTiOaebpeLm/mc6MLYwI1nIyYpT/sSylXpoHaF/bJJuBgiOCkZ8lFv6QCg0GD3DAC+i9tla14O0zU6" +
                "jHpbl3A12OvSVbK8P0nHGaOT0N7xWoqSO3ua0h4L6F0WZlp1yyH+1xVn/3H+u9lKIhEMRsMfo1KvoPhO" +
                "vaDju85t56tRI15+zdG8caSLOCW4BqjEhesvu8BashhYr/5S4w8LDR691AUAAA==";
                
    }
}
