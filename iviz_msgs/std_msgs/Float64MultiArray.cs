
namespace Iviz.Msgs.std_msgs
{
    public sealed class Float64MultiArray : IMessage 
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        
        public MultiArrayLayout layout; // specification of data layout
        public double[] data; // array of data
        

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Float64MultiArray";

        public IMessage Create() => new Float64MultiArray();

        public int GetLength()
        {
            int size = 4;
            size += layout.GetLength();
            size += 8 * data.Length;
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public Float64MultiArray()
        {
            layout = new MultiArrayLayout();
            data = new double[0];
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

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "4b7d974086d4060e7db4613a7e6c3ba4";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
