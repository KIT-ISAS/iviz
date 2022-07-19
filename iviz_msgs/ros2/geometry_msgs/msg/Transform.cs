/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Transform : IMessageRos2, IDeserializableRos2<Transform>
    {
        // This represents the transform between two coordinate frames in free space.
        [DataMember (Name = "translation")] public Vector3 Translation;
        [DataMember (Name = "rotation")] public Quaternion Rotation;
    
        /// Explicit constructor.
        public Transform(in Vector3 Translation, in Quaternion Rotation)
        {
            this.Translation = Translation;
            this.Rotation = Rotation;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Transform(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Transform RosDeserialize(ref ReadBuffer2 b) => new Transform(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public readonly void AddRosMessageLength(ref int c) => WriteBuffer2.AddLength(ref c, this);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/Transform";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
