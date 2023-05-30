/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class VectorFieldStamped : IHasSerializer<VectorFieldStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector_field")] public MeshMsgs.VectorField VectorField;
    
        public VectorFieldStamped()
        {
            VectorField = new MeshMsgs.VectorField();
        }
        
        public VectorFieldStamped(in StdMsgs.Header Header, MeshMsgs.VectorField VectorField)
        {
            this.Header = Header;
            this.VectorField = VectorField;
        }
        
        public VectorFieldStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            VectorField = new MeshMsgs.VectorField(ref b);
        }
        
        public VectorFieldStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            VectorField = new MeshMsgs.VectorField(ref b);
        }
        
        public VectorFieldStamped RosDeserialize(ref ReadBuffer b) => new VectorFieldStamped(ref b);
        
        public VectorFieldStamped RosDeserialize(ref ReadBuffer2 b) => new VectorFieldStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            VectorField.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            VectorField.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(VectorField, nameof(VectorField));
            VectorField.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Header.RosMessageLength;
                size += VectorField.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = VectorField.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/VectorFieldStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3d9fc2de2c0939ad4bbe0890ccb68ce5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTWvcMBC9+1cM7CGbsnFpUnoI9FbS5lAITeglhGXWGtuisuRK8m7cX98neddNykJ7" +
                "aGoMlqV5b958KUS17kITXn8SVuKpzZ+ik9BO+1+lis5faTGKtnm9rtNPUbz/x0/x+fbjJYXngooF3Ua2" +
                "ir2iTiIrjky1g1DdtOLPjGzFAMRdL4ryaRx7CSWAd60OhLcRK56NGWkIMIqOKtd1g9UVR6GoEexTPJDa" +
                "ElPPPupqMOxh77zSNpnXnjtJ7HiDfB/EVkLXHy5hY4NUQ9QQNIKh8sJB2waHVAzaxovzBKAF3X9x4c1D" +
                "sbjbuTPsS4O8zyoothyTannsvYQkmMMlnL2aoizhBFkSuFOBlnlvjd9wSvAGLdK7qqUlQrgZY+ssCIW2" +
                "7DVvjCTiCqkA60kCnZw+YbaZ2rJ1B/qJ8ZePv6G1M2+K6axF8UxKQxgaZBKGvXdbrWC6GTNJZbTYSEZv" +
                "PPuxSKjJZbG4SsmGEVC5NPhyCK7SqISinY5tEaJP7Lksa/1ibXl0IIpGHJrSj9PJjUM17x+od0FHjXb4" +
                "7XxCXsBiGqTwUmKPyDqMA+oaWduQM38QSq5O/Z7sUuvXXlCBnispauM4vntLj/NqnFc//o/8fdYOAXhJ" +
                "Y4GGQSvs8/hcc5km8zqPkLOYxE4YYWHoZySASntAEXoJVvGCG0VWpCMpJ4GsS/nq+BsoBf2c0Nz3IMPt" +
                "4tkGwzlt2AZkKWVTrmjXip2sUj/mayRfPLoirxutJiQcdTOYaR/cimJ9jn42ZtI8OUOJQOJdzIDTkq5r" +
                "Gt1AuxQQFn5/3znayKwrj2N0bpUuuz3FkX5IDR24weTaEHHTlsUfav0To+aXkykGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<VectorFieldStamped> CreateSerializer() => new Serializer();
        public Deserializer<VectorFieldStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<VectorFieldStamped>
        {
            public override void RosSerialize(VectorFieldStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(VectorFieldStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(VectorFieldStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(VectorFieldStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(VectorFieldStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<VectorFieldStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out VectorFieldStamped msg) => msg = new VectorFieldStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out VectorFieldStamped msg) => msg = new VectorFieldStamped(ref b);
        }
    }
}
