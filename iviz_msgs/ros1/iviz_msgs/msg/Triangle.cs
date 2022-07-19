/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle : IMessageRos1, IMessageRos2, IDeserializableRos1<Triangle>, IDeserializableRos2<Triangle>
    {
        [DataMember (Name = "a")] public uint A;
        [DataMember (Name = "b")] public uint B;
        [DataMember (Name = "c")] public uint C;
    
        /// Explicit constructor.
        public Triangle(uint A, uint B, uint C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        readonly ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Triangle(ref b);
        
        public readonly Triangle RosDeserialize(ref ReadBuffer b) => new Triangle(ref b);
        
        public readonly Triangle RosDeserialize(ref ReadBuffer2 b) => new Triangle(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 12;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly void AddRos2MessageLength(ref int c) => WriteBuffer2.AddLength(ref c, this);
        
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/Triangle";
    
        public readonly string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "7fbd9596e2fe5bfb3fb6622c0cdf3da9";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlJI5CqFMJJgjGQuLgA3MPMeHAAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
