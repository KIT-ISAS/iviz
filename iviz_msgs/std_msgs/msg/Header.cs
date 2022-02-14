/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Header : IMessage, IDeserializable<Header>, System.IEquatable<Header>
    {
        // Standard metadata for higher-level stamped data types.
        // This is generally used to communicate timestamped data 
        // in a particular coordinate frame.
        // 
        // sequence ID: consecutively increasing ID 
        [DataMember (Name = "seq")] public uint Seq;
        //Two-integer timestamp that is expressed as:
        // * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')
        // * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')
        // time-handling sugar is provided by the client library
        [DataMember (Name = "stamp")] public time Stamp;
        //Frame this data is associated with
        [DataMember (Name = "frame_id")] public string FrameId;
    
        /// Explicit constructor.
        public Header(uint Seq, time Stamp, string FrameId)
        {
            this.Seq = Seq;
            this.Stamp = Stamp;
            this.FrameId = FrameId;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Header(ref ReadBuffer b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer b, out Header h)
        {
            h.Seq = b.Deserialize<uint>();
            h.Stamp = b.Deserialize<time>();
            h.FrameId = b.DeserializeString();
        }
        
        readonly ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Header(ref b);
        
        public readonly Header RosDeserialize(ref ReadBuffer b) => new Header(ref b);
        
        public readonly override int GetHashCode() => (Seq, Stamp, FrameId).GetHashCode();
        public readonly override bool Equals(object? o) => o is Header s && Equals(s);
        public readonly bool Equals(Header o) => (Seq, Stamp, FrameId) == (o.Seq, o.Stamp, o.FrameId);
        public static bool operator==(in Header a, in Header b) => a.Equals(b);
        public static bool operator!=(in Header a, in Header b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Seq);
            b.Serialize(Stamp);
            b.Serialize(FrameId ?? "");
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 16 + BuiltIns.GetStringSize(FrameId);
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Header";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE42RT2vDMAzF7/4UghzaDtrDdut5DHYbrPei2moscOxMVtrl209O2brdBobg+L3f058O" +
                "3hVzQAkwkGJARTgXgch9JNkmulCCqjiMFGB51XmkunMdHCJXsNNTJsGUZpiqibSAL8MwZfaoBMoD/fGb" +
                "kzMgjCjKfkoopi8SODf5WXCgRrdT6WOi7Alen/emyZX8pGwFzUbwQlg59/YIbuKsT4/N4LrDtWztSj3J" +
                "PRw0orZi6XMUqq1OrHvLeLg1tzP23vyWEiqsl39Hu9YNWIiVQGPxEdZW+dussWQDElxQGE+JGtjbBIy6" +
                "aqbV5hc5L+iMuXzjb8R7xn+w+YfbetpG21lq3deptwGacJRy4WDS07xAfGLKColPgjK75rpFuu6lzdhE" +
                "5lo2Yl+stXi2BQS4skZXVRp92caRg3NfAmsPMygCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static implicit operator Header((uint seqId, string frameId) p) => new(p.seqId, time.Now(), p.frameId);
        public static implicit operator Header((uint seqId, time stamp, string frameId) p) => new(p.seqId, p.stamp, p.frameId);
        public static implicit operator Header(string frameId) => new(0, time.Now(), frameId);
    }
}
