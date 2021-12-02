/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public JoyFeedback()
        {
        }
        
        /// Explicit constructor.
        public JoyFeedback(byte Type, byte Id, float Intensity)
        {
            this.Type = Type;
            this.Id = Id;
            this.Intensity = Intensity;
        }
        
        /// Constructor with buffer.
        internal JoyFeedback(ref Buffer b)
        {
            Type = b.Deserialize<byte>();
            Id = b.Deserialize<byte>();
            Intensity = b.Deserialize<float>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new JoyFeedback(ref b);
        
        JoyFeedback IDeserializable<JoyFeedback>.RosDeserialize(ref Buffer b) => new JoyFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Type);
            b.Serialize(Id);
            b.Serialize(Intensity);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 6;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/JoyFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f4dcd73460360d98f36e55ee7f2e46f1";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAClWPQWvCMBiG7/kVL3gtpXUIO9iL2IPgYIge5mWkyRcaTBNJUrX/fmlnh8speb7ked8s" +
                "sCVhuCc4hdgS4nCd9opINlxcWK9tfMfx67P+3tdbpFWheKWH08dmXydavtLN6XyuD4ku2ROPZsYWOLY6" +
                "4K6NQeuMBLfQErbvGvJQzoO4aP9q0HiYu+Tpdf3g3dVQNpVV2ocIQxJ31ydXQ8lVFb/DQMLZ/5O5oZZj" +
                "kZ2NZIOOw/z3OSeD8q5DkReIDmWehNoK0wd9oxzYKUi6aZGUIWm4iD03ZkCjLfdDBunTPY/QTsnRE48o" +
                "1tVjXeQr8JDSVJbkqxFV5URszpRxPL4tU9KzFWPsB+Je5DueAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
