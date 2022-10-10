/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PoseArray : IHasSerializer<PoseArray>, IMessage
    {
        // An array of poses with a header for global reference.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public Pose[] Poses;
    
        public PoseArray()
        {
            Poses = EmptyArray<Pose>.Value;
        }
        
        public PoseArray(in StdMsgs.Header Header, Pose[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        public PoseArray(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                int n = b.DeserializeArrayLength();
                Pose[] array;
                if (n == 0) array = EmptyArray<Pose>.Value;
                else
                {
                    array = new Pose[n];
                    b.DeserializeStructArray(array);
                }
                Poses = array;
            }
        }
        
        public PoseArray(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Pose[] array;
                if (n == 0) array = EmptyArray<Pose>.Value;
                else
                {
                    array = new Pose[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Poses = array;
            }
        }
        
        public PoseArray RosDeserialize(ref ReadBuffer b) => new PoseArray(ref b);
        
        public PoseArray RosDeserialize(ref ReadBuffer2 b) => new PoseArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Poses.Length);
            b.SerializeStructArray(Poses);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Poses.Length);
            b.Align8();
            b.SerializeStructArray(Poses);
        }
        
        public void RosValidate()
        {
            if (Poses is null) BuiltIns.ThrowNullReference(nameof(Poses));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += 56 * Poses.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Poses.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Poses.Length;
            return size;
        }
    
        public const string MessageType = "geometry_msgs/PoseArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "916c28c5764443f268b296bb671b9d97";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71Uy27bMBC88ysW8CFJ0bjoAz0Y6CFA0cehQNrkFgTGWlxLBChSWVJ21K/vUIrl+lL0" +
                "0MYQYIqcnZ2dXWpBV4FYlQeKW+pikkR7lxtiaoStKG2jUu3jhj2pbEUlVLI05st0OoGMuUbk3f1EYMyH" +
                "f/wz324+ryhlu25TnV5Nuc2CbjIHy2qplcyWM49qG1c3opdeduIRxG0nlsbTPHSSlgi8bVwiPLUEUfZ+" +
                "oD4BlCNVsW374CrOQtm1chKPSAe7qGPNruo9K/BRrQsFvlVupbDjSfLQF6fo68cVMCFJ1WcHQQMYKhVO" +
                "LtQ4JNO7kN++KQG0oLsfMb2+N4vbfbzEvtSweFZBueFcVMtjp5KKYE4rJHsxVblEErgkSGcTnY97a7ym" +
                "C0I2aJEuVg2do4TrITcxgFBox+p446UQV7ACrGcl6OziN+YwUgcO8UA/MR5z/A1tmHlLTZcNmueLDamv" +
                "4SSAncads4BuhpGk8k5CJu82yjqYEjWlNItPxWyAEDW2Bv+cUqwcOmHHETYpa2Ef27J29n+NZS0R46fD" +
                "NJvlIqDAK1yW0iTI5+zgydPtKvOzVUEZHVfysoxb2bZP527EwheK6g6xS8L1wjTMAPO9R5UaRt4j7rkK" +
                "hJTDFcIsZHYhjd2a9aMW3JFR8km5Zusj5/fv6HFeDfPq5/PIP1p3qGFuFCboxM9T8eXt4eg7PjQtPoN/" +
                "ruiw2hvzCzR+fYtpBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<PoseArray> CreateSerializer() => new Serializer();
        public Deserializer<PoseArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PoseArray>
        {
            public override void RosSerialize(PoseArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PoseArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PoseArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PoseArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(PoseArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<PoseArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PoseArray msg) => msg = new PoseArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PoseArray msg) => msg = new PoseArray(ref b);
        }
    }
}
