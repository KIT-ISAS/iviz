/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Header : IMessage, IHasSerializer<Header>
    {
        // Standard metadata for higher-level stamped data types.
        // This is generally used to communicate timestamped data 
        // in a particular coordinate frame.
        // 
        // sequence ID: consecutively increasing ID 
        /// <summary> [Ros1] </summary>
        [DataMember (Name = "seq")] public uint Seq;
        //Two-integer timestamp that is expressed as:
        // * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')
        // * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')
        // time-handling sugar is provided by the client library
        [DataMember (Name = "stamp")] public time Stamp;
        //Frame this data is associated with
        [DataMember (Name = "frame_id")] public string FrameId;
    
        public Header(uint Seq, time Stamp, string FrameId)
        {
            this.Seq = Seq;
            this.Stamp = Stamp;
            this.FrameId = FrameId;
        }
        
        public Header(ref ReadBuffer b)
        {
            b.Deserialize(out Seq);
            b.Deserialize(out Stamp);
            FrameId = b.DeserializeString();
        }
        
        public Header(ref ReadBuffer2 b)
        {
            Seq = default;
            b.Align4();
            b.Deserialize(out Stamp);
            FrameId = b.DeserializeString();
        }
        
        public readonly Header RosDeserialize(ref ReadBuffer b) => new Header(ref b);
        
        public readonly Header RosDeserialize(ref ReadBuffer2 b) => new Header(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Seq);
            b.Serialize(Stamp);
            b.Serialize(FrameId ?? "");
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Stamp);
            b.Serialize(FrameId ?? "");
        }
        
        public void RosValidate()
        {
            FrameId ??= "";
        }
    
        [IgnoreDataMember]
        public readonly int RosMessageLength
        {
            get
            {
                int size = 16;
                size += WriteBuffer.GetStringSize(FrameId);
                return size;
            }
        }
        
        [IgnoreDataMember] public readonly int Ros2MessageLength => AddRos2MessageLength(0);
        
        public readonly int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 8; // Stamp
            size = WriteBuffer2.AddLength(size, FrameId);
            return size;
        }
    
        public const string MessageType = "std_msgs/Header";
    
        [IgnoreDataMember] public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        [IgnoreDataMember] public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE42RT0sDMRDF7/kUA3toK7Sg3noWwZtobyJlmkx3B7LJOplt3W/vZItWb0Jg2eS935s/" +
                "DbwqpoASoCfFgIpwzAIdtx3JOtKJIhTFfqAA86tOA5WNa2DXcQE7LSUSjHGCsZhIM/jc92Nij0qg3NMf" +
                "vzk5AcKAouzHiGL6LIFTlR8Fe6p0O4U+Rkqe4Olha5pUyI/KVtBkBC+EhVNrj+BGTnp/Vw3QwNtLLrfv" +
                "rtmd89ruqSW5VgHaodaq6XMQKrVgLFsLu7l0ubGQrYEsLhRYznd7+y0rsDSrhYbsO1haC8+TdjkZkOCE" +
                "wniIVMHeRmHURTUtVr/IaUYnTPkbfyFeM/6DTT/c2tO6s+XFOoYytjZJEw6STxxMephmiI9MSSHyQVAm" +
                "V12XSNc81mGbyFzzauyLpWTPtokAZ9bOFZVKn9ey5+DcFwKP/KIxAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static implicit operator Header((uint seqId, string frameId) p) => new(p.seqId, time.Now(), p.frameId);
        public static implicit operator Header((uint seqId, time stamp, string frameId) p) => new(p.seqId, p.stamp, p.frameId);
        public static implicit operator Header(string frameId) => new(0, time.Now(), frameId);
    
        public Serializer<Header> CreateSerializer() => new Serializer();
        public Deserializer<Header> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Header>
        {
            public override void RosSerialize(Header msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Header msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Header msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Header msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Header msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Header>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Header msg) => msg = new Header(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Header msg) => msg = new Header(ref b);
        }
    }
}
