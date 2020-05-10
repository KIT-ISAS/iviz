using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class JoyFeedback : IMessage
    {
        // Declare of the type of feedback
        public const byte TYPE_LED = 0;
        public const byte TYPE_RUMBLE = 1;
        public const byte TYPE_BUZZER = 2;
        
        public byte type { get; set; }
        
        // This will hold an id number for each type of each feedback.
        // Example, the first led would be id=0, the second would be id=1
        public byte id { get; set; }
        
        // Intensity of the feedback, from 0.0 to 1.0, inclusive.  If device is
        // actually binary, driver should treat 0<=x<0.5 as off, 0.5<=x<=1 as on.
        public float intensity { get; set; }
        
    
        /// <summary> Constructor for empty message. </summary>
        public JoyFeedback()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public JoyFeedback(byte type, byte id, float intensity)
        {
            this.type = type;
            this.id = id;
            this.intensity = intensity;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal JoyFeedback(Buffer b)
        {
            this.type = b.Deserialize<byte>();
            this.id = b.Deserialize<byte>();
            this.intensity = b.Deserialize<float>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new JoyFeedback(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.type);
            b.Serialize(this.id);
            b.Serialize(this.intensity);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 6;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/JoyFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "f4dcd73460360d98f36e55ee7f2e46f1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1WPQWvCMBiG7/kVL3gtpTqEHexF1oPgYIge5mWkyRcaFhNJUrX/fp+dHS6n5PmS530z" +
                "wxspJyMhGOSOkIfzuDdEupXqW/TW51fsPz+ar23zBl41qme6O7yvtw3T+TNdH47HZsd0IR74bhZihn1n" +
                "E67WOXTBaUgPq+H7U0sRJkSQVN1fjfEwdSn5dXOTp7OjYixrbEwZjjSuoWdXS+yqq99hIhX8/8nU0Op7" +
                "kY3P5JPNw/T3KaeAieGEqqyQA+YlC61Xrk/2QiWwMdB0sYqViTVS5V46N6C1XsahgI58LyJ1Y3KOJDOq" +
                "VX1bVeUSMnGaKVi+vKN6PhJfCuOCzC8LTnq0EkL8AOJe5DueAQAA";
                
    }
}
