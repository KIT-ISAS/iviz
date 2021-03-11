/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceActionFeedback")]
    public sealed class PlaceActionFeedback : IDeserializable<PlaceActionFeedback>, IActionFeedback<PlaceFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public PlaceFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new PlaceFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PlaceFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PlaceActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new PlaceFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceActionFeedback(ref b);
        }
        
        PlaceActionFeedback IDeserializable<PlaceActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new PlaceActionFeedback(ref b);
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
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW8bNxC9L6D/QECH2EHttknTpgZ0UCXFceAkgq32anDJ0S5bLlcluZb17/uGK62k" +
                "WkJ0SCLI1hf55vHNm+G8J6nJizK9ZFJFUztr8ocqFOHH61ra+yhjE0RIL9nUSkXviHQu1T9ivn7TywZf" +
                "+dHLPt5fXyGqbpm8T/x6WV+Aj9PSa1FRlFpGKeY1+JuiJH9h6ZEsc60WpEX6Na4WFC6xcVaaIPAsyJGX" +
                "1q5EE7Ao1kLVVdU4o2QkEU1Fe/ux0zghxUL6aFRjpcf62mvjePncy4oYHc9A/zbkFImb8RXWuECqiQaE" +
                "VkBQnmQwrsCPImuMi69f8YasP1vWF/hIBbLQBRexlJHJ0tPCU2CeMlwhxsv2cJfAhjqEKDqIs/TdAz6G" +
                "c4EgoECLWpXiDMynq1jWDoAkHqU3MrfEwAoKAPUFb3pxvoPMtK+Ek67ewLeI2xinwLoOl890USJnlk8f" +
                "mgICYuHC149GY2m+SiDKGnJRwHpe+lXGu9qQWf8da4xF2JUyglcZQq0MEqDF0sQyC9EzesrGg9HZNzPk" +
                "0QLpZfweyS3wwhQ4x283ZdN+mE4+jW8+XYvNYyB+wn92JqVtopRBrCiyJ3NiiVSb+7VGbXCk3T+iVlvM" +
                "4Wh289dE7GD+vI/JSWm8h7jwYU4s00nA07vJ5ON0Nhl3wK/2gT0pgrvhTGQdDuFvUAAhCjmPMLOJfHrP" +
                "OaKnVAquyLZEnz/6+INPkgqt51CYC0uMYGLYoIDo2Yx8hQK03A0ina8p3/85Gk0m4x3Kr/cpL4EsVWnQ" +
                "JTSsqFiFecOt4JAQx8IM//h8t9WFw/xyIExep6PrJjlzy/1gJN3QF6VhV4QalTCXxjaejtG7m3yYjHb4" +
                "DcSb5/Q8/U2K+R2kwzVVN/H/dvnhyxxzUhJtNWF2wRq0yijBlJsEmrVxj9IafewAa+d1lTIQv34H53XW" +
                "c3VMRbg1X5e8TuHR8PZ2W8kD8dupBHPCbUUHGZ6iLnLyPFv7pN3c+IrvNb5BujSk1sxMSO8dYtcmb7/C" +
                "IU6TmU2xV35tAL45jnji9vP9bBdqIH5PgEO3EWN9gQBJaGSNQagVQXYSMMplOwgEGNzqpFt+Qu0Fxq5Z" +
                "bZZ0aXB8VA5i7bfOrD+0tl6mkYQXohTwpt7eVyCzvqu4xsTOgMVbNOVNUbCM60WRnmL2XW+zmzEPWWyC" +
                "dhBZ6xQiZ5yPlG5mqLosDSaMdCvvdJVkENI8Ed2kASbNWAekwn5ybCEclAJrhEGHqgXSZS12M2Zo87ck" +
                "hO6gN+6DK8lzV0mMdgeGNX80mPWQEbB4KdHodhOxmV3ZkNiBKauxEUNlCLLgDCM7YUHKzI3a1ENiENhA" +
                "jM4TX7sApKom1QVancGqy03+sOrbZa+CH01sU7c3l/eybEOA5w/qZf8BppV6vOYLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
