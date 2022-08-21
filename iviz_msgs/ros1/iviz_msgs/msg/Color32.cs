/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Color32 : IMessage, IDeserializable<Color32>
    {
        [DataMember (Name = "r")] public byte R;
        [DataMember (Name = "g")] public byte G;
        [DataMember (Name = "b")] public byte B;
        [DataMember (Name = "a")] public byte A;
    
        public Color32(byte R, byte G, byte B, byte A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color32(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color32(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Color32 RosDeserialize(ref ReadBuffer b) => new Color32(ref b);
        
        public readonly Color32 RosDeserialize(ref ReadBuffer2 b) => new Color32(ref b);
    
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
    
        public const int RosFixedMessageLength = 4;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 4;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => c + Ros2FixedMessageLength;
        
    
        public const string MessageType = "iviz_msgs/Color32";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3a89b17adab5bedef0b554f03235d9b3";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvNzCuxUCjiKgXT6VA6CUoncnEBACHBa7shAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}