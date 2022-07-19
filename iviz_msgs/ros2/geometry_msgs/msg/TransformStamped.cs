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
    public struct TransformStamped : IMessageRos2, IDeserializableRos2<TransformStamped>
    {
        // This expresses a transform from coordinate frame header.frame_id
        // to the coordinate frame child_frame_id at the time of header.stamp
        //
        // This message is mostly used by the
        // <a href="https://index.ros.org/p/tf2/">tf2</a> package.
        // See its documentation for more information.
        //
        // The child_frame_id is necessary in addition to the frame_id
        // in the Header to communicate the full reference for the transform
        // in a self contained message.
        // The frame id in the header is used as the reference frame of this transform.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The frame id of the child frame to which this transform points.
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        // Translation and rotation in 3-dimensions of child_frame_id from header.frame_id.
        [DataMember (Name = "transform")] public Transform Transform;
    
        /// Explicit constructor.
        public TransformStamped(in StdMsgs.Header Header, string ChildFrameId, in Transform Transform)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Transform = Transform;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TransformStamped(ref ReadBuffer2 b)
        {
            Deserialize(ref b, out this);
        }
        
        public static void Deserialize(ref ReadBuffer2 b, out TransformStamped h)
        {
            StdMsgs.Header.Deserialize(ref b, out h.Header);
            b.DeserializeString(out h.ChildFrameId);
            b.Deserialize(out h.Transform);
        }
        
        public readonly TransformStamped RosDeserialize(ref ReadBuffer2 b) => new TransformStamped(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId ?? "");
            b.Serialize(in Transform);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public readonly void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, ChildFrameId);
            WriteBuffer2.AddLength(ref c, Transform);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/TransformStamped";
    
        public readonly string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
