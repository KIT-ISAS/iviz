/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class ByteMultiArray : IDeserializable<ByteMultiArray>, IMessage
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
            Data = System.Array.Empty<byte>();
        }
        
        public ByteMultiArray(MultiArrayLayout Layout, byte[] Data)
        {
            this.Layout = Layout;
            this.Data = Data;
        }
        
        public ByteMultiArray(ref ReadBuffer b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        public ByteMultiArray(ref ReadBuffer2 b)
        {
            Layout = new MultiArrayLayout(ref b);
            b.DeserializeStructArray(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ByteMultiArray(ref b);
        
        public ByteMultiArray RosDeserialize(ref ReadBuffer b) => new ByteMultiArray(ref b);
        
        public ByteMultiArray RosDeserialize(ref ReadBuffer2 b) => new ByteMultiArray(ref b);
    
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
    
        public int RosMessageLength => 4 + Layout.RosMessageLength + Data.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Layout.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        public const string MessageType = "std_msgs/ByteMultiArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "70ea476cbcfd65ac2f68f3cda1e891fe";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
