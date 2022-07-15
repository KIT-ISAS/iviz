/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.StdMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Header : IMessageRos2, IDeserializable<Header>
    {
        // Standard metadata for higher-level stamped data types.
        // This is generally used to communicate timestamped data
        // in a particular coordinate frame.
        // Two-integer timestamp that is expressed as seconds and nanoseconds.
        [DataMember (Name = "stamp")] public time Stamp;
        // Transform frame with which this data is associated.
        [DataMember (Name = "frame_id")] public string FrameId;
    
        /// Explicit constructor.
        public Header(time Stamp, string FrameId)
        {
            this.Stamp = Stamp;
            this.FrameId = FrameId;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Header(ref ReadBuffer2 b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer2 b, out Header h)
        {
            b.Deserialize(out h.Stamp);
            b.DeserializeString(out h.FrameId);
        }
        
        public readonly Header RosDeserialize(ref ReadBuffer2 b) => new Header(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Stamp);
            b.Serialize(FrameId ?? "");
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Stamp);
            WriteBuffer2.Advance(ref c, FrameId);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Header";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
