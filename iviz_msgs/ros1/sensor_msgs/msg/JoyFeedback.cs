/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
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
    
        public JoyFeedback()
        {
        }
        
        public JoyFeedback(byte Type, byte Id, float Intensity)
        {
            this.Type = Type;
            this.Id = Id;
            this.Intensity = Intensity;
        }
        
        public JoyFeedback(ref ReadBuffer b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out Id);
            b.Deserialize(out Intensity);
        }
        
        public JoyFeedback(ref ReadBuffer2 b)
        {
            b.Deserialize(out Type);
            b.Deserialize(out Id);
            b.Deserialize(out Intensity);
        }
        
        public JoyFeedback RosDeserialize(ref ReadBuffer b) => new JoyFeedback(ref b);
        
        public JoyFeedback RosDeserialize(ref ReadBuffer2 b) => new JoyFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Type);
            b.Serialize(Id);
            b.Serialize(Intensity);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Type);
            b.Serialize(Id);
            b.Serialize(Intensity);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 6;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, Id);
            WriteBuffer2.AddLength(ref c, Intensity);
        }
    
        public const string MessageType = "sensor_msgs/JoyFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f4dcd73460360d98f36e55ee7f2e46f1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1WPQWvCMBiG7/kVL3gtpTqEHexF1oPgYIge5mWkyRcaFhNJUrX/fp+dHS6n5PmS530z" +
                "wxspJyMhGOSOkIfzuDdEupXqW/TW51fsPz+ar23zBl41qme6O7yvtw3T+TNdH47HZsd0IR74bhZihn1n" +
                "E67WOXTBaUgPq+H7U0sRJkSQVN1fjfEwdSn5dXOTp7OjYixrbEwZjjSuoWdXS+yqq99hIhX8/8nU0Op7" +
                "kY3P5JPNw/T3KaeAieGEqqyQA+YlC61Xrk/2QiWwMdB0sYqViTVS5V46N6C1XsahgI58LyJ1Y3KOJDOq" +
                "VX1bVeUSMnGaKVi+vKN6PhJfCuOCzC8LTnq0EkL8AOJe5DueAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
