/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.Tf2Msgs
{
    [DataContract]
    public sealed class TF2Error : IDeserializable<TF2Error>, IMessageRos2
    {
        public const byte NO_ERROR = 0;
        public const byte LOOKUP_ERROR = 1;
        public const byte CONNECTIVITY_ERROR = 2;
        public const byte EXTRAPOLATION_ERROR = 3;
        public const byte INVALID_ARGUMENT_ERROR = 4;
        public const byte TIMEOUT_ERROR = 5;
        public const byte TRANSFORM_ERROR = 6;
        [DataMember (Name = "error")] public byte Error;
        [DataMember (Name = "error_string")] public string ErrorString;
    
        /// Constructor for empty message.
        public TF2Error()
        {
            ErrorString = "";
        }
        
        /// Explicit constructor.
        public TF2Error(byte Error, string ErrorString)
        {
            this.Error = Error;
            this.ErrorString = ErrorString;
        }
        
        /// Constructor with buffer.
        public TF2Error(ref ReadBuffer2 b)
        {
            b.Deserialize(out Error);
            b.DeserializeString(out ErrorString);
        }
        
        public TF2Error RosDeserialize(ref ReadBuffer2 b) => new TF2Error(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Error);
            b.Serialize(ErrorString);
        }
        
        public void RosValidate()
        {
            if (ErrorString is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, Error);
            WriteBuffer2.Advance(ref c, ErrorString);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/TF2Error";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
