/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class SensorLevels : IDeserializable<SensorLevels>, IMessage
    {
        // This message is deprecated, please use driver_base/SensorLevels instead.
        public const byte RECONFIGURE_CLOSE = 3; // Parameters that need a sensor to be stopped completely when changed
        public const byte RECONFIGURE_STOP = 1; // Parameters that need a sensor to stop streaming when changed
        public const byte RECONFIGURE_RUNNING = 0; // Parameters that can be changed while a sensor is streaming
    
        /// Constructor for empty message.
        public SensorLevels()
        {
        }
        
        /// Constructor with buffer.
        internal SensorLevels(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => Singleton;
        
        SensorLevels IDeserializable<SensorLevels>.RosDeserialize(ref Buffer b) => Singleton;
        
        public static readonly SensorLevels Singleton = new SensorLevels();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/SensorLevels";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "6322637bee96d5489db6e2127c47602c";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACo2QsWrDQAyGdz/FD1lL29A5U3BDINjBTuYg+37sA/tsTteUvH2UUtqhgXSQkAT6PqEF" +
                "Dr1XjFSVjrDScY5sJdE9YR4oSnxYuOjPjKfG+peaQae445mDwgdNFPecZc0lEVW+Lov37eZY5af1rqxz" +
                "rPAGLLCXKCMToyL1khBIB4F+sZAmNISmaZ5t3E6jqROHCz57BrS9hI7ur6E+lHsTLP8luNEtRcroQ/eA" +
                "XB2LYltsDP56h91KuN37vW0oP/DXZV/80WTZFQaWm/hjAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
