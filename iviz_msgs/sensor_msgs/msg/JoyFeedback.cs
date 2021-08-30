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
        [DataMember (Name = "type")] public byte Type;
        // This will hold an id number for each type of each feedback.
        // Example, the first led would be id=0, the second would be id=1
        [DataMember (Name = "id")] public byte Id;
        // Intensity of the feedback, from 0.0 to 1.0, inclusive.  If device is
        // actually binary, driver should treat 0<=x<0.5 as off, 0.5<=x<=1 as on.
        [DataMember (Name = "intensity")] public float Intensity;
    
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
                "H4sIAAAAAAAAE1WPQWvCMBiG7/kVL3gtpTqEHexF1oPgYIge5mWkyRcaFhNJUrX/fp+dHS6n5PmS530z" +
                "wxspJyMhGOSOkIfzuDdEupXqW/TW51fsPz+ar23zBl41qme6O7yvtw3T+TNdH47HZsd0IR74bhZihn1n" +
                "E67WOXTBaUgPq+H7U0sRJkSQVN1fjfEwdSn5dXOTp7OjYixrbEwZjjSuoWdXS+yqq99hIhX8/8nU0Op7" +
                "kY3P5JPNw/T3KaeAieGEqqyQA+YlC61Xrk/2QiWwMdB0sYqViTVS5V46N6C1XsahgI58LyJ1Y3KOJDOq" +
                "VX1bVeUSMnGaKVi+vKN6PhJfCuOCzC8LTnq0EkL8AOJe5DueAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
