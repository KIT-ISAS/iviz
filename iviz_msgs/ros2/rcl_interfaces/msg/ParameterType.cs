/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RclInterfaces
{
    [DataContract]
    public sealed class ParameterType : IDeserializable<ParameterType>, IMessage
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
    
        public ParameterType()
        {
        }
        
        public ParameterType(ref ReadBuffer b)
        {
        }
        
        public ParameterType(ref ReadBuffer2 b)
        {
        }
        
        public ParameterType RosDeserialize(ref ReadBuffer b) => Singleton;
        
        public ParameterType RosDeserialize(ref ReadBuffer2 b) => Singleton;
        
        static ParameterType? singleton;
        public static ParameterType Singleton => singleton ??= new ParameterType();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => 0;
        
        public int AddRos2MessageLength(int c) => c;
    
        public const string MessageType = "rcl_interfaces/ParameterType";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "607fc9419cfd453b9ade36cf9de88ce8";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE3XOXWvCMBQG4Pv8igPeDtmcbu4iFxWDCNqWGAWvSmiPJtAvmtON/ftlXXcVC4FAnpPz" +
                "vjNQBh0CfbfoIG+6Dl3b1AVQA2QQPnXZezWawDpw6K96gFR3ukLC7jJMVOicvuOcsRls8ab7kv7+PsGX" +
                "sbkBW7Wl9RFk/B5/6oZA/47YAtr/XXPW25rWkEYyOgolZBYnKjsJxZ9ZQJskOfCX4HkfK7ETki8C2Sbn" +
                "zUHw1wBOSu7jHV+GEVclskjK6MpXD/NHfJtqMfr7RJeR1xONRv5g7AeqfHV4pwEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
