/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshGeometryStamped : IHasSerializer<MeshGeometryStamped>, IMessage
    {
        // Mesh Geometry Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_geometry")] public MeshMsgs.MeshGeometry MeshGeometry;
    
        public MeshGeometryStamped()
        {
            Uuid = "";
            MeshGeometry = new MeshMsgs.MeshGeometry();
        }
        
        public MeshGeometryStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshGeometry MeshGeometry)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshGeometry = MeshGeometry;
        }
        
        public MeshGeometryStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            MeshGeometry = new MeshMsgs.MeshGeometry(ref b);
        }
        
        public MeshGeometryStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Uuid = b.DeserializeString();
            MeshGeometry = new MeshMsgs.MeshGeometry(ref b);
        }
        
        public MeshGeometryStamped RosDeserialize(ref ReadBuffer b) => new MeshGeometryStamped(ref b);
        
        public MeshGeometryStamped RosDeserialize(ref ReadBuffer2 b) => new MeshGeometryStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshGeometry.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Uuid);
            MeshGeometry.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Uuid, nameof(Uuid));
            BuiltIns.ThrowIfNull(MeshGeometry, nameof(MeshGeometry));
            MeshGeometry.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += MeshGeometry.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            size = MeshGeometry.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshGeometryStamped";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2d62dc21b3d9b8f528e4ee7f76a77fb7";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTWvbQBC961cM6OCk4JQ2pQdDb6apD4HQ+GaCGWtH0sJqV91dOVZ/fd9KtmKCKT3U" +
                "QiBp9d6bjzeT06OEmh7ENRJ9n74CV5KFqLZNqMLHH8JKPNXDA8de24q6TqusAXHEJIlJYTiujl9Z9u0/" +
                "X9nj88OC3qWX5fQc2Sr2CvEjK45MpUPauqrFz43sxYDETSuKhr+xbyXcgbiudSDclVjxbExPXQAoOipc" +
                "03RWFxyFokZd53wwtSWmln3URWfYA++80jbBS8+NJHXcQX51Yguh1XIBjA1SdFEjoR4KhRcOqaWrJWWd" +
                "tvH+cyJQTpufLnx6yfL1q5vjXCq4MGVBseaYspZD62EZsuKwQLAPY5V3CIIuCcKpQDfD2Raf4ZYQDblI" +
                "64qablDCUx9rZyEotGeveWckCRdoBVRniTS7PVO2g7Rl607yo+JbjH+RtZNuqmlewzyT2hC6Cp0EsPVu" +
                "rxWgu34QKYwWG8nonWfMVWKNIbP8e2o2QGAN1uDJIbhCwwlFrzrWp7kdbNlidq80lpdXAkVeXrLTloyU" +
                "JwebNy+0lzRREv7yWw5b63zDJpxt4RpdtpWRlVWJDmjJSeZKtV7I7rRNGIvI2obBuNYFHTVGwZVpXRIu" +
                "bU7pBQa2yDArjeP49Qsdprd+evt9fave9Q1FLKXU9izpeETMwps5465u7idD9JGe/QFf4q+DUwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshGeometryStamped> CreateSerializer() => new Serializer();
        public Deserializer<MeshGeometryStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshGeometryStamped>
        {
            public override void RosSerialize(MeshGeometryStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshGeometryStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshGeometryStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshGeometryStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshGeometryStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshGeometryStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshGeometryStamped msg) => msg = new MeshGeometryStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshGeometryStamped msg) => msg = new MeshGeometryStamped(ref b);
        }
    }
}
