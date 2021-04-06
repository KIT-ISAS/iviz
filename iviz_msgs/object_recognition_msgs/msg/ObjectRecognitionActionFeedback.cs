/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionActionFeedback")]
    public sealed class ObjectRecognitionActionFeedback : IDeserializable<ObjectRecognitionActionFeedback>, IActionFeedback<ObjectRecognitionFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ObjectRecognitionFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ObjectRecognitionFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ObjectRecognitionFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ObjectRecognitionFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionFeedback(ref b);
        }
        
        ObjectRecognitionActionFeedback IDeserializable<ObjectRecognitionActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVptkzZJPaODKimOMk7ssdVePSC4ItGCoAqAkvXv+xakKMqx" +
                "Gh2SaGTrC3j78PbtYt+TzMiJIr4kUgVdWaPTh9Ln/qerSpr7IEPthY8vyU36N6lwR6rKrea174iyVKp/" +
                "xLJ9k4y+8iP5eH91ifhZw+l9w3QgQMxm0mWipCAzGaRYVjiIzgtyF4bWZJh0uaJMxF/DdkV+iI2LQnuB" +
                "Z06WnDRmK2qPRaESqirL2molA4mgSzrYj53aCilW0gWtaiMd1lcu05aXL50sidHx9PRvTVaRmE8vscZ6" +
                "UnXQILQFgnIkvbY5fhRJrW149ZI3JIPFprrAR8qRji64CIUMTJYeV44885T+EjF+aA43BDbEQT5s5sVZ" +
                "/O4BH/25QBBQoFWlCnEG5rfbUFQWgCTW0mmZGmJgBQWA+oI3vTjvIdsIbaWtdvAN4j7GKbC2w+UzXRTI" +
                "meHT+zqHgFi4ctVaZ1iabiOIMppsEPCgk26b8K4mZDJ4xxpjEXbFjOBVel8pjQRkYqNDkfjgGD1m40Fn" +
                "yTdy49E6SfgtMpvjheNzgt/uiqf5cDv7NJ1/uhK7x0j8jP9sS4rbRCG92FJgQ6bE+qgm8a1ATWzk3K1R" +
                "Bw3meLKY/zUTPcxfDjE5I7VzUBYmTIk1Ogn49m42+3i7mE074JeHwI4UwdqwJVIOe/A3cL8PQi4DnKwD" +
                "n95xgugx1oHNE/E/jwH+YJKoQmM4VOXKECPo4HcoIHq2IFei+gy3gkDnLeX7PyeT2Wzao/zqkPIGyFIV" +
                "mpi2rxWrsKy5DzwnxLEw4z9u7va6cJhfnwmTVvHoWR1tuef+bKSspi9Kw67wFcpgKbWpHR2jdzf7MJv0" +
                "+I3Eb5/Tc8Td/IgDYkFVdXhqlx+/zDElJdFTI2YXrEafDBJMuUOgU2u7lkZnxw7QOq+rlJF4/R2c11nP" +
                "ViEW4d58XfI6hSfj6+t9JY/Em1MJpoSrip5leIq6yMnn2TokbZfalXyp8fUR+l0gMqHs4BB9m7z9Coc4" +
                "TWY2xUH5NQH42jjiieub+0UfaiR+j4BjuxOjvT2AJDJkjUGoEUF2EjDKsJkCPAxusqhbekLtecauWG2W" +
                "dKNxfFSOtE9aZzIYG1Nt4jzCC1EKjuu2u6xApr2ouMZEb8ziLRmldZ6zjO2iQI8h+Y5X2XyaNA5oRpBW" +
                "JB843XyeeCdD0k2hMVvE+7jXUqI7KONZaB5Hl7q9Y57qhP1k2T84JXkWCCMOlSvkyhjsZkzfJG9DCN1B" +
                "76wHS5LjlhIZ9UeFlj+6SzteoBWD3vYwC7uRld2IHZivahMwTnovc2pS41ek9FKrXTFEBn7YovOs1ywA" +
                "qbKORYE+p7FquEseDyHfKHVVHMYf3H4ab3J4dEhPkv8AHh9+uu8LAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
