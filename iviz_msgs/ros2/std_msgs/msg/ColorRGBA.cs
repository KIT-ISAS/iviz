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
    public struct ColorRGBA : IMessageRos2, IDeserializable<ColorRGBA>
    {
        [DataMember (Name = "r")] public float R;
        [DataMember (Name = "g")] public float G;
        [DataMember (Name = "b")] public float B;
        [DataMember (Name = "a")] public float A;
    
        /// Explicit constructor.
        public ColorRGBA(float R, float G, float B, float A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColorRGBA(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ColorRGBA RosDeserialize(ref ReadBuffer2 b) => new ColorRGBA(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 16;
        
        public readonly void GetRosMessageLength(ref int c) => WriteBuffer2.Advance(ref c, this);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/ColorRGBA";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
