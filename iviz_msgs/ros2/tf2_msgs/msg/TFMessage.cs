/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.Tf2Msgs
{
    [DataContract]
    public sealed class TFMessage : IDeserializable<TFMessage>, IMessageRos2
    {
        [DataMember (Name = "transforms")] public GeometryMsgs.TransformStamped[] Transforms;
    
        /// Constructor for empty message.
        public TFMessage()
        {
            Transforms = System.Array.Empty<GeometryMsgs.TransformStamped>();
        }
        
        /// Explicit constructor.
        public TFMessage(GeometryMsgs.TransformStamped[] Transforms)
        {
            this.Transforms = Transforms;
        }
        
        /// Constructor with buffer.
        public TFMessage(ref ReadBuffer2 b)
        {
            b.DeserializeArray(out Transforms);
            for (int i = 0; i < Transforms.Length; i++)
            {
                GeometryMsgs.TransformStamped.Deserialize(ref b, out Transforms[i]);
            }
        }
        
        public TFMessage RosDeserialize(ref ReadBuffer2 b) => new TFMessage(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeArray(Transforms);
        }
        
        public void RosValidate()
        {
            if (Transforms is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Transforms);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/TFMessage";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
