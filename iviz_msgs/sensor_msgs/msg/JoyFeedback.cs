/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/JoyFeedback")]
    public sealed class JoyFeedback : IDeserializable<JoyFeedback>, IMessage
    {
        // Declare of the type of feedback
        public const byte TYPE_LED = 0;
        public const byte TYPE_RUMBLE = 1;
        public const byte TYPE_BUZZER = 2;
        [DataMember (Name = "type")] public byte Type { get; set; }
        // This will hold an id number for each type of each feedback.
        // Example, the first led would be id=0, the second would be id=1
        [DataMember (Name = "id")] public byte Id { get; set; }
        // Intensity of the feedback, from 0.0 to 1.0, inclusive.  If device is
        // actually binary, driver should treat 0<=x<0.5 as off, 0.5<=x<=1 as on.
        [DataMember (Name = "intensity")] public float Intensity { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JoyFeedback()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public JoyFeedback(byte Type, byte Id, float Intensity)
        {
            this.Type = Type;
            this.Id = Id;
            this.Intensity = Intensity;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public JoyFeedback(ref Buffer b)
        {
            Type = b.Deserialize<byte>();
            Id = b.Deserialize<byte>();
            Intensity = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JoyFeedback(ref b);
        }
        
        JoyFeedback IDeserializable<JoyFeedback>.RosDeserialize(ref Buffer b)
        {
            return new JoyFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
            b.Serialize(Id);
            b.Serialize(Intensity);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 6;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/JoyFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f4dcd73460360d98f36e55ee7f2e46f1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClWPQWvDIBiG78L+wwu9hpB0FHZoLqU5FDoYpT2sl2H0k8iMFjVt8+9nsmZ0nvT59Hlf" +
                "F9iSMNwTnEJsCXG4THtFJBsuvlmvbXzD8fOj/trXW6RVoXimh9P7Zl8nWj7Tzel8rg+JLtkDj2bGFji2" +
                "OuCmjUHrjAS30BK27xryUM6DuGj/atB4mLvk6XV9593FUDaVVdqHCEMSN9cnV0PJVRW/w0DC2f+TuaGW" +
                "Y5GdjWSDjsP89zkng/KuQ5EXiA5lnoTaCtMHfaUc2ClIumqRlCFpuIg9N2ZAoy33Qwbp0z2P0E7J0ROP" +
                "KNbVfV3kK/CQ0lSW5KsRVeVEbM6UcTy+LlPSoxVjL+wHXvepM58BAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
