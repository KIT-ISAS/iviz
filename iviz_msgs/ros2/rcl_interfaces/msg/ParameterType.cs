/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.RclInterfaces
{
    [DataContract]
    public sealed class ParameterType : IDeserializable<ParameterType>, IMessageRos2
    {
        // These types correspond to the value that is set in the ParameterValue message.
        // Default value, which implies this is not a valid parameter.
        public const byte PARAMETER_NOT_SET = 0;
        public const byte PARAMETER_BOOL = 1;
        public const byte PARAMETER_INTEGER = 2;
        public const byte PARAMETER_DOUBLE = 3;
        public const byte PARAMETER_STRING = 4;
        public const byte PARAMETER_BYTE_ARRAY = 5;
        public const byte PARAMETER_BOOL_ARRAY = 6;
        public const byte PARAMETER_INTEGER_ARRAY = 7;
        public const byte PARAMETER_DOUBLE_ARRAY = 8;
        public const byte PARAMETER_STRING_ARRAY = 9;
    
        /// Constructor for empty message.
        public ParameterType()
        {
        }
        
        /// Constructor with buffer.
        public ParameterType(ref ReadBuffer2 b)
        {
        }
        
        public ParameterType RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static ParameterType? singleton;
        public static ParameterType Singleton => singleton ??= new ParameterType();
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public void GetRosMessageLength(ref int c) { }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "rcl_interfaces/ParameterType";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
