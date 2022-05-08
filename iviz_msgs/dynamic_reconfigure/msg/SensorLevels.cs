/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class SensorLevels : IDeserializable<SensorLevels>, IMessage
    {
        // This message is deprecated, please use driver_base/SensorLevels instead.
        /// <summary> Parameters that need a sensor to be stopped completely when changed </summary>
        public const byte RECONFIGURE_CLOSE = 3;
        /// <summary> Parameters that need a sensor to stop streaming when changed </summary>
        public const byte RECONFIGURE_STOP = 1;
        /// <summary> Parameters that can be changed while a sensor is streaming </summary>
        public const byte RECONFIGURE_RUNNING = 0;
    
        /// Constructor for empty message.
        public SensorLevels()
        {
        }
        
        /// Constructor with buffer.
        public SensorLevels(ref ReadBuffer b)
        {
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => Singleton;
        
        public SensorLevels RosDeserialize(ref ReadBuffer b) => Singleton;
        
        static SensorLevels? singleton;
        public static SensorLevels Singleton => singleton ??= new SensorLevels();
    
        public void RosSerialize(ref WriteBuffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/SensorLevels";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6322637bee96d5489db6e2127c47602c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE42QsWrDQAyGdz/FD1lL29A5U3BDINjBTuYg+37sA/tsTteUvH2UUtqhgXSQkAT6PqEF" +
                "Dr1XjFSVjrDScY5sJdE9YR4oSnxYuOjPjKfG+peaQae445mDwgdNFPecZc0lEVW+Lov37eZY5af1rqxz" +
                "rPAGLLCXKCMToyL1khBIB4F+sZAmNISmaZ5t3E6jqROHCz57BrS9hI7ur6E+lHsTLP8luNEtRcroQ/eA" +
                "XB2LYltsDP56h91KuN37vW0oP/DXZV/80WTZFQaWm/hjAQAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
