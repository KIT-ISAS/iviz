/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.UniqueIdentifierMsgs
{
    [DataContract]
    public sealed class UUID : IDeserializable<UUID>, IMessageRos2
    {
        // A universally unique identifier (UUID).
        //
        //  http://en.wikipedia.org/wiki/Universally_unique_identifier
        //  http://tools.ietf.org/html/rfc4122.html
        [DataMember (Name = "uuid")] public byte[/*16*/] Uuid;
    
        /// Constructor for empty message.
        public UUID()
        {
            Uuid = new byte[16];
        }
        
        /// Explicit constructor.
        public UUID(byte[] Uuid)
        {
            this.Uuid = Uuid;
        }
        
        /// Constructor with buffer.
        public UUID(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(16, out Uuid);
        }
        
        public UUID RosDeserialize(ref ReadBuffer2 b) => new UUID(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Uuid, 16);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (Uuid.Length != 16) BuiltIns.ThrowInvalidSizeForFixedArray(Uuid.Length, 16);
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 16;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Uuid, 16);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "unique_identifier_msgs/UUID";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
