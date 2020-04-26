using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class JoyFeedback : IMessage
    {
        // Declare of the type of feedback
        public const byte TYPE_LED = 0;
        public const byte TYPE_RUMBLE = 1;
        public const byte TYPE_BUZZER = 2;
        
        public byte type;
        
        // This will hold an id number for each type of each feedback.
        // Example, the first led would be id=0, the second would be id=1
        public byte id;
        
        // Intensity of the feedback, from 0.0 to 1.0, inclusive.  If device is
        // actually binary, driver should treat 0<=x<0.5 as off, 0.5<=x<=1 as on.
        public float intensity;
        
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out type, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
            BuiltIns.Deserialize(out intensity, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(type, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
            BuiltIns.Serialize(intensity, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 6;
    
        public IMessage Create() => new JoyFeedback();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "sensor_msgs/JoyFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "f4dcd73460360d98f36e55ee7f2e46f1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE1WPQWvCMBiG7/kVL3gtpTqEHexF1oPgYIge5mWkyRcaFhNJUrX/fp+dHS6n5PmS530z" +
                "wxspJyMhGOSOkIfzuDdEupXqW/TW51fsPz+ar23zBl41qme6O7yvtw3T+TNdH47HZsd0IR74bhZihn1n" +
                "E67WOXTBaUgPq+H7U0sRJkSQVN1fjfEwdSn5dXOTp7OjYixrbEwZjjSuoWdXS+yqq99hIhX8/8nU0Op7" +
                "kY3P5JPNw/T3KaeAieGEqqyQA+YlC61Xrk/2QiWwMdB0sYqViTVS5V46N6C1XsahgI58LyJ1Y3KOJDOq" +
                "VX1bVeUSMnGaKVi+vKN6PhJfCuOCzC8LTnq0EkL8AOJe5DueAQAA";
                
    }
}
