/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class ByteMultiArray : IHasSerializer<ByteMultiArray>, IMessage
    {
        // Please look at the MultiArrayLayout message definition for
        // documentation on all multiarrays.
        /// <summary> Specification of data layout </summary>
        [DataMember (Name = "layout")] public MultiArrayLayout Layout;
        /// <summary> Array of data </summary>
        [DataMember (Name = "data")] public byte[] Data;
    
        public ByteMultiArray()
        {
            Layout = new MultiArrayLayout();
            Data = EmptyArray<byte>.Value;
        }
        
        public ByteMultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public ByteMultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                    array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public ByteMultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                byte[] array;
                if (n == 0) array = EmptyArray<byte>.Value;
                else
                {
                    array = new byte[n];
                    b.DeserializeStructArray(array);
                }
                Data = array;
            }
        }
        
        public ByteMultiArray RosDeserialize(ref ReadBuffer b) => new ByteMultiArray(ref b);
        
        public ByteMultiArray RosDeserialize(ref ReadBuffer2 b) => new ByteMultiArray(ref b);
    
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
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            Layout.RosValidate();
            BuiltIns.ThrowIfNull(Data, nameof(Data));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Layout.RosMessageLength;
                size += Data.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Layout.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Data.Length
            size += 1 * Data.Length;
            return size;
        }
    
        public const string MessageType = "std_msgs/ByteMultiArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "70ea476cbcfd65ac2f68f3cda1e891fe";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71U32vbMBB+919xJC9tlmb5Ucpa6ENgsJcWBh2MEkJRrXOsRLaCJDfL/vp9kh3bafc4" +
                "ZgyW73R33/fpTkP6rlk4Jm3MjoQnnzM9VtqrpbXi+CCOpvJUsHNiwyQ5U6XyypSUGZsMSZq0Krj0Itrw" +
                "Cq2pCOEihLtJknxIRrr51s+Q3J5Tlam0SZKRFF40u5LXo+fVmnpP9FIXHiudwpIkuf/HT/L49O2OnJcv" +
                "hdu4z+/5QIUf0KwjDZVSLSw7ErThkq1Ka++VVNDKgaTQHWqBBHthvUorRNXs/HHPE6Kvp/1IZZmMlWxZ" +
                "UmZNQajMlgrjAgBvSJVl83+meZsCGqI85Fq2cp1ctLdmz0DALqlU6RfziOLFZJnj3jnthZSq3BBrDmfu" +
                "QrsAS+k78ZE+TdEsxjpyuam0pOXDz+XzE70yHazynktAJWAv3DkI562SjAyilKeWANnI8yrw6u3NlA08" +
                "h4S3E/5Cjbfj3SXdRzCrPodPIfilLrGarUfq3DJfj7aw7NbJMFAAFoAQVo5pcZXmAtJqurme/rr+MiVV" +
                "hEk4KJ+DCLBhfN6AMzXaWGo2O2Q5RPag3XER7i4WQOXVdD3R4hV5AXeQs9rkftC5nPrNFFyo2LNGtLAu" +
                "RkAzCmju6XY+u5lOiS5K47nZ2YhJytG2gnIxHdSO2C+bhLM+goOSPh90nhYACvWsZwDwnd3OT+55P12j" +
                "w6DztQkXPVubLsry8SQtZ4xOQnuHaylIbs1hTFssoHdVlOPYLbvwX1ec/Mf5b2crCUQwGA1/jEq9guIb" +
                "9YaObzv3NF+NGuHya47m3Ua6CFOCa4AqXLjusg2sJQuB9eovNf4A8nBJftQFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ByteMultiArray> CreateSerializer() => new Serializer();
        public Deserializer<ByteMultiArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ByteMultiArray>
        {
            public override void RosSerialize(ByteMultiArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ByteMultiArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ByteMultiArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ByteMultiArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ByteMultiArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ByteMultiArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ByteMultiArray msg) => msg = new ByteMultiArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ByteMultiArray msg) => msg = new ByteMultiArray(ref b);
        }
    }
}
