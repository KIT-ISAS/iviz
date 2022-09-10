/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class EngagementLevel : IHasSerializer<EngagementLevel>, IMessage
    {
        // Discrete engagement levels of a person with respect to the robot
        // It is for instance published on /humans/persons/person_id/engagement_status 
        // to access to the engagement level of a detected human. 
        // It will output one of the five following levels: unknown, disengaged, 
        // engaging, engaged and disengaging.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // unknown: no information is provided about the engagement level 
        public const byte UNKNOWN = 0;
        // disengaged: the human has not looked in the direction of the robot
        public const byte DISENGAGED = 1;
        // engaging: the human has started to look in the direction of the robot
        public const byte ENGAGING = 2;
        // engaged: the human is fully engaged with the robot
        public const byte ENGAGED = 3;
        // disengaging: the human has started to look away from the robot
        public const byte DISENGAGING = 4;
        [DataMember (Name = "level")] public byte Level;
    
        public EngagementLevel()
        {
        }
        
        public EngagementLevel(in StdMsgs.Header Header, byte Level)
        {
            this.Header = Header;
            this.Level = Level;
        }
        
        public EngagementLevel(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Level);
        }
        
        public EngagementLevel(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Level);
        }
        
        public EngagementLevel RosDeserialize(ref ReadBuffer b) => new EngagementLevel(ref b);
        
        public EngagementLevel RosDeserialize(ref ReadBuffer2 b) => new EngagementLevel(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 1;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Level
            return size;
        }
    
        public const string MessageType = "hri_msgs/EngagementLevel";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "98693df082bea7da40fa598b187373d9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61UTW8TMRC9+1eMlENb1Ca0cECRekBKCREiIFrEAaHIWU92re7aiz8S8u95tpM0rYrU" +
                "A9EqTtYz78174/GAJtpXjgMTm1rW3LEJ1PKaW092RZJ6dt4a2ujQkGPfcxUoWAoNk7NLG8SAZoG0p5V1" +
                "pI0P0lRMfVy22jesCLmjJnbS+FGB2q8LrUYPnAskhugJcECXVcXe73meVlYKUyi6CmDI6EMqhWx0i/0Y" +
                "+hhAzSk0Qaz0Gl+2be1Gm3oncEzR3Bu7MeektC8s6jwB5d8IPN9xK5JGHYKwMRQfWSp21JSFBLJ2aGMy" +
                "Fk7Aj04GDf0wp3d2rVXCWaK451WJqE14R9/nn+ZffsyvXwPxoaxxzslSqZEeFEiz9h6Q2uQ9pR38SHw7" +
                "zaU9BXQyu72ZT99PbybXl0f6nqKiCS5ZCuMT+IugM+5sPr2+2gM/rjYdjdi224OV+Sg9i4Lq3hypfkGB" +
                "ciO3tHK2+6fiVNlbsXuZjRZCXP/nj/h8Ox2jOLXofO1H5WxAyS2mQUmnqOMglQwyT0mj64bdRek6FHU9" +
                "FOXdsO3ZD5F418A2PDUbdjK5F32RXdmui0ZXEiMbdMeP8pGJlmFo4ZKuYisd4q1T2qTwlZMdJ3Q8nn9H" +
                "TpM6m4wRYzxXMWBIwKQNLgTp05zMJuVUvrlKCTSgn9+sv/wlBncbe4H3XOPsH6pAD2S+CvhPj6siFSz9" +
                "GGSvisohSOASg055Os3vFvjrzwhsqIV7WzV0Cglft6Gx5fCtpdNy2XICrmAFUE9S0snZEbLJ0EYau4cv" +
                "iA8cL4E1B9yk6aJB89pkg481nDwe4+U2g1StzvOrl066rUhZhVIMPiSzEYSs3Bqs0ntbaRl2MyB8cAk9" +
                "twW3oRB/ARfAP16OBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<EngagementLevel> CreateSerializer() => new Serializer();
        public Deserializer<EngagementLevel> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<EngagementLevel>
        {
            public override void RosSerialize(EngagementLevel msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(EngagementLevel msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(EngagementLevel msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(EngagementLevel msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(EngagementLevel msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<EngagementLevel>
        {
            public override void RosDeserialize(ref ReadBuffer b, out EngagementLevel msg) => msg = new EngagementLevel(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out EngagementLevel msg) => msg = new EngagementLevel(ref b);
        }
    }
}
