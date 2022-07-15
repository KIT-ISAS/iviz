/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class SetParametersResult : IDeserializable<SetParametersResult>, IMessageRos2
    {
        // A true value of the same index indicates that the parameter was set
        // successfully. A false value indicates the change was rejected.
        [DataMember (Name = "successful")] public bool Successful;
        // Reason why the setting was either successful or a failure. This should only be
        // used for logging and user interfaces.
        [DataMember (Name = "reason")] public string Reason;
    
        /// Constructor for empty message.
        public SetParametersResult()
        {
            Reason = "";
        }
        
        /// Explicit constructor.
        public SetParametersResult(bool Successful, string Reason)
        {
            this.Successful = Successful;
            this.Reason = Reason;
        }
        
        /// Constructor with buffer.
        public SetParametersResult(ref ReadBuffer2 b)
        {
            b.Deserialize(out Successful);
            b.DeserializeString(out Reason);
        }
        
        public SetParametersResult RosDeserialize(ref ReadBuffer2 b) => new SetParametersResult(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Successful);
            b.Serialize(Reason);
        }
        
        public void RosValidate()
        {
            if (Reason is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Successful);
            WriteBuffer2.Advance(ref c, Reason);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/SetParametersResult";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
