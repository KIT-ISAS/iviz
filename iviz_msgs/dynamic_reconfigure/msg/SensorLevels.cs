/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/SensorLevels")]
    public sealed class SensorLevels : IDeserializable<SensorLevels>, IMessage
    {
        // This message is deprecated, please use driver_base/SensorLevels instead.
        public const byte RECONFIGURE_CLOSE = 3; // Parameters that need a sensor to be stopped completely when changed
        public const byte RECONFIGURE_STOP = 1; // Parameters that need a sensor to stop streaming when changed
        public const byte RECONFIGURE_RUNNING = 0; // Parameters that can be changed while a sensor is streaming
    
        /// <summary> Constructor for empty message. </summary>
        public SensorLevels()
        {
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal SensorLevels(ref Buffer b)
        {
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        SensorLevels IDeserializable<SensorLevels>.RosDeserialize(ref Buffer b)
        {
            return Singleton;
        }
        
        public static readonly SensorLevels Singleton = new SensorLevels();
    
        public void RosSerialize(ref Buffer b)
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 0;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/SensorLevels";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6322637bee96d5489db6e2127c47602c";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACo2Qy2rDQAxF94b+w4VsSx903VVwQyDYwU7WQfZc7AF7bEbThPx9lFKaRQrtQkIS6Byh" +
                "BXa9V4xUlY6w0nGObCXRPWIeKEp8Wrjoj4yHxvrnmkGnuOGRg8IHTRT3lGXNORFVviyLj/VqX+WH5aas" +
                "c7zjDVhgK1FGJkZF6iUhkA4C/WIhTWgITdM827idRlMnDmecega0vYSO7t5Q78qtCV7/JbjSLUXK6EP3" +
                "B7naF8W6WBn85Rd2K+F67/e2ofzAm8u++KPJHrIL964NlGQBAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
