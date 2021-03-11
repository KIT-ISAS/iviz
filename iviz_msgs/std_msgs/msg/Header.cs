/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Header")]
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
        [DataMember (Name = "frame_id")] public string? FrameId;
    
        /// <summary> Explicit constructor. </summary>
        public Header(uint Seq, time Stamp, string FrameId)
        {
            this.Seq = Seq;
            this.Stamp = Stamp;
            this.FrameId = FrameId;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Header(ref Buffer b)
        {
            Seq = b.Deserialize<uint>();
            Stamp = b.Deserialize<time>();
            FrameId = b.DeserializeString();
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Header(ref b);
        }
        
        readonly Header IDeserializable<Header>.RosDeserialize(ref Buffer b)
        {
            return new Header(ref b);
        }
        
        public override readonly int GetHashCode() => (Seq, Stamp, FrameId).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Header s && Equals(s);
        
        public readonly bool Equals(Header o) => (Seq, Stamp, FrameId) == (o.Seq, o.Stamp, o.FrameId);
        
        public static bool operator==(in Header a, in Header b) => a.Equals(b);
        
        public static bool operator!=(in Header a, in Header b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(Seq);
            b.Serialize(Stamp);
            b.Serialize(FrameId ?? string.Empty);
        }
        
        public readonly void Dispose()
        {
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(FrameId ?? string.Empty);
                return size;
            }
        }
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Header";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACo2RT2vDMAzF74F9B0EObQftYbvlPAa7DdZ7UW01Fjh2Jivt8u0nJ4xut4EhOH7v9/Sn" +
                "hQ/F5FE8DKToUREuWSBwH0j2ka4UoSgOI3lYXnUeqRyaFo6BC9jpKZFgjDNMxUSaweVhmBI7VALlgf74" +
                "zckJEEYUZTdFFNNn8Zyq/CI4UKXbKfQ5UXIEby+daVIhNylbQbMRnBAWTr09QjNx0uenamja4y3v7Uo9" +
                "yT0cNKDWYulrFCq1TiydZTyuzR2M3ZnfUnyB7fLvZNeyAwtxBDRmF2Brlb/PGnIyIMEVhfEcqYKdTcCo" +
                "m2ra7H6Ra9kdJEz5B78S7xn/wVbKyq097YPtLNbuy9TbAE04Sr6yN+l5XiAuMiWFyGdBmZvqWiOb9rXO" +
                "2ETmWjZiXywlO7YFeLixhqaoVPqyjRP75qH5BhP+q5MpAgAA";
                
        public override string ToString() => Extensions.ToString(this);
        /// Custom iviz code
        public static implicit operator Header((uint seqId, string frameId) p) => new Header(p.seqId, time.Now(), p.frameId);
        public static implicit operator Header((uint seqId, time stamp, string frameId) p) => new Header(p.seqId, p.stamp, p.frameId);
        public static implicit operator Header(string frameId) => new Header(0, time.Now(), frameId);
    }
}
