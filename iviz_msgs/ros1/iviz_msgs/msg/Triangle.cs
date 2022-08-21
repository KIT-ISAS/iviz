/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Triangle : IMessage, IDeserializable<Triangle>
    {
        [DataMember (Name = "a")] public uint A;
        [DataMember (Name = "b")] public uint B;
        [DataMember (Name = "c")] public uint C;
    
        public Triangle(uint A, uint B, uint C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
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
    
        public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 12;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "iviz_msgs/Triangle";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7fbd9596e2fe5bfb3fb6622c0cdf3da9";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCsxNlJI5CqFMJJgjGQuLgA3MPMeHAAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}