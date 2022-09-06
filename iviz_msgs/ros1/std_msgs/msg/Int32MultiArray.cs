/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Int32MultiArray : IDeserializable<Int32MultiArray>, IHasSerializer<Int32MultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public int[] Data;
    
        public Int32MultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<int>.Value;
        }
        
        public Int32MultiArray(MultiArrayLayout Layout, int[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public Int32MultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<int>.Value
                    : new int[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Data = array;
            }
        }
        
        public Int32MultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<int>.Value
                    : new int[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Data = array;
            }
        }
        
        public Int32MultiArray RosDeserialize(ref ReadBuffer b) => new Int32MultiArray(ref b);
        
        public Int32MultiArray RosDeserialize(ref ReadBuffer2 b) => new Int32MultiArray(ref b);
    
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
                size += 4 * Data.Length;
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
            size += 4 * Data.Length;
            return size;
        }
    
        public const string MessageType = "std_msgs/Int32MultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1d99f79f8b325b44fee908053e9c945b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJQrXOsRLaCJDfr/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6rlk4Jm3MnoQnnzM9VtqrpbXi7UG8mcpTwc6JLZPkTJXKK1NSZmwyJGnSquDSi2jD" +
                "K7SmIoSLEO4mSfIhGenmWz9DcgdOVabSJklGUnjR7EpU6Rfz1Zq6J3qpC4+VTmFJktz/4yd5fPp2R87L" +
                "TeG27vN7PlDhBzTrSEOlVAvLjgRtuWSr0tp7JRW0ciApdIdaIMFBWK/SClE1O/924AnR19N+pLJMxkq2" +
                "LCmzpiBUZkuFcQGAN6TKsvk/07xNAQlRHnItW7lOLjpYc2AgYJdUUe+IYmOyzHHvnA5CSlVuiTWHM3eh" +
                "XYCl9J34SJ+maBZjHbncVFrS8uHn8vmJXpiOVnnPJaASsBfuHITzVklGBlHKU0uAbOR5FXj19mbKBp5D" +
                "wtsJf6HGu/H+ku4jmFWfw6cQvKlLrGbrkTq3zNejHSz7dTIMFIAFIISVY1pcpbmAtJpurqe/rr9MSRVh" +
                "Eo7K5yACbBifV+BMjTaWms0OWY6RPWh3XIS7iwVQeTVdT7R4QV7AHeSstrkfdC6nfjMFFyr2rBEtrIsR" +
                "0IwCmnu6nc9uplOii9J4bnY2YpJytKugXEwHtSP2yybhrI/gqKTPB52nBYBCPesZAHxnt/OTe95P1+gw" +
                "6HxtwkXP1qaLsnw8ScsZo5PQ3uFaCpJbcxzTDgvoXRXlOHbLPvzXFSf/cf7b2UoCEQxGwx+jUq+g+Fa9" +
                "ouPbzj3NV6NGuPyao3m3kS7ClOAaoAoXrrtsA2vJQmC9+kuNP6p4ENnUBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Int32MultiArray> CreateSerializer() => new Serializer();
        public Deserializer<Int32MultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Int32MultiArray>
        {
            public override void RosSerialize(Int32MultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Int32MultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Int32MultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Int32MultiArray msg) => msg.Ros2MessageLength;
            public override void RosValidate(Int32MultiArray msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<Int32MultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Int32MultiArray msg) => msg = new Int32MultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Int32MultiArray msg) => msg = new Int32MultiArray(ref b);
        }
    }
}
