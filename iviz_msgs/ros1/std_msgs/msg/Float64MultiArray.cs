/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Float64MultiArray : IHasSerializer<Float64MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public double[] Data;
    
        public Float64MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<double>.Value;
        }
        
        public Float64MultiArray(MultiArrayLayout Layout, double[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public Float64MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public Float64MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public Float64MultiArray RosDeserialize(ref ReadBuffer b) => new Float64MultiArray(ref b);
        
        public Float64MultiArray RosDeserialize(ref ReadBuffer2 b) => new Float64MultiArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Layout.RosSerialize(ref b);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Layout is null) BuiltIns.ThrowNullReference();
            Layout.RosValidate();
            if (Data is null) BuiltIns.ThrowNullReference();
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
    
        public const string MessageType = "std_msgs/Float64MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "4b7d974086d4060e7db4613a7e6c3ba4";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Float64MultiArray> CreateSerializer() => new Serializer();
        public Deserializer<Float64MultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Float64MultiArray>
        {
            public override void RosSerialize(Float64MultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Float64MultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Float64MultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Float64MultiArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Float64MultiArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Float64MultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Float64MultiArray msg) => msg = new Float64MultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Float64MultiArray msg) => msg = new Float64MultiArray(ref b);
        }
    }
}
