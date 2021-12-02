/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Header : IMessage, System.IEquatable<Header>, IDeserializable<Header>
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
        internal Header(ref Buffer b)
        {
            Deserialize(ref b, out this);
        }
        
        internal static void Deserialize(ref Buffer b, out Header h)
        {
            h.Seq = b.Deserialize<uint>();
            h.Stamp = b.Deserialize<time>();
            h.FrameId = b.DeserializeString();
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new Header(ref b);
        
        readonly Header IDeserializable<Header>.RosDeserialize(ref Buffer b) => new Header(ref b);
        
        public override readonly int GetHashCode() => (Seq, Stamp, FrameId).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Header s && Equals(s);
        
        public readonly bool Equals(Header o) => (Seq, Stamp, FrameId) == (o.Seq, o.Stamp, o.FrameId);
        
        public static bool operator==(in Header a, in Header b) => a.Equals(b);
        
        public static bool operator!=(in Header a, in Header b) => !a.Equals(b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Seq);
            b.Serialize(Stamp);
            b.Serialize(FrameId ?? string.Empty);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 16 + BuiltIns.GetStringSize(FrameId);
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Header";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACo2RT2vDMAzF7/4Ugh7aDdrDdst5DHYbrPei2moscOxMVtrl209OGN1uA0Nw/N7v6c8G" +
                "PhRzQAkwkGJARbgUgch9JNknulKCqjiMFGB51XmkenAbOEauYKenTIIpzTBVE2kBX4ZhyuxRCZQH+uM3" +
                "J2dAGFGU/ZRQTF8kcG7yi+BAjW6n0udE2RO8vXSmyZX8pGwFzUbwQlg59/YIbuKsz0/N4DbHW9nblXqS" +
                "ezhoRG3F0tcoVFudWDvLeFybOxi7M7+lhAq75d/JrvUBLMRKoLH4CDur/H3WWLIBCa4ojOdEDextAkbd" +
                "NtP24Re5ld1Bxlx+8CvxnvEfbKOs3NbTPtrOUuu+Tr0N0ISjlCsHk57nBeITU1ZIfBaU2TXXGuk2r23G" +
                "JjLXshH7Yq3Fsy0gwI01uqrS6Ms2Thyc+wYCaw8zKAIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static implicit operator Header((uint seqId, string frameId) p) => new Header(p.seqId, time.Now(), p.frameId);
        public static implicit operator Header((uint seqId, time stamp, string frameId) p) => new Header(p.seqId, p.stamp, p.frameId);
        public static implicit operator Header(string frameId) => new Header(0, time.Now(), frameId);
    }
}
