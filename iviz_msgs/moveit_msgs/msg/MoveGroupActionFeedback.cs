/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupActionFeedback")]
    public sealed class MoveGroupActionFeedback : IDeserializable<MoveGroupActionFeedback>, IActionFeedback<MoveGroupFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public MoveGroupFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new MoveGroupFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new MoveGroupFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionFeedback(ref b);
        }
        
        MoveGroupActionFeedback IDeserializable<MoveGroupActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71W23LbNhB951dgRg+xO7XaJr2kntGDKimOMnbisdW+ekBwRaIlQRUXyfr7ngUpirKt" +
                "Rg9JNLJ1A84enD272PckM7KiiC+JVF7XptTpQ+Vy98NVLct7L31wwsWX5KZe05Wtw+odUZZK9Y9Ytm+S" +
                "0Rd+JDf3V5eImzVc3jcMBwKETCZtJiryMpNeimWNA+i8IHtR0ppKJlutKBPxV79dkRti46LQTuCZkyEr" +
                "y3IrgsMiXwtVV1UwWklPwuuKDvZjpzZCipW0XqtQSov1tc204eVLKytidDwd/RvIKBLz6SXWGEcqeA1C" +
                "WyAoS9Jpk+NHkQRt/JvXvCEZLDb1BT5SjjR0wYUvpGey9Liy5JindJeI8V1zuCGwIQ4hSubEWfzuAR/d" +
                "uUAQUKBVrQpxBua3W1/UBoAk1tJqmZbEwAoKAPUVb3p13kM2EdpIU+/gG8R9jFNgTYfLZ7ookLOST+9C" +
                "DgGxcGXrtc6wNN1GEFVqMl7Ae1babcK7mpDJ4B1rjEXYFTOCV+lcrTQSkImN9kXivGX0mI0HnSVfyY1H" +
                "6yPht8hsjheOzwl+uyua5sPt7ON0/vFK7B4j8SP+sy0pbhOFdGJLng2ZEuujmsS3AjWxkXO7Rh00mOPJ" +
                "Yv7XTPQwfzrE5IwEa6EsTJgSa3QS8O3dbHZzu5hNO+DXh8CWFMHasCVSDnvwN3C/80IuPZysPZ/ecoLo" +
                "MdaByRPxP48B/mCSqEJjOFTlqiRG0N7tUED0bEG2QvWV3Ao8nbeU7/+cTGazaY/ym0PKGyBLVWhi2i4o" +
                "VmEZuA+8JMSxMOM/Pt3tdeEwP78QJq3j0bMQbbnn/mKkLNBnpWFXuBplsJS6DJaO0bubfZhNevxG4pfn" +
                "9Cz9TcofcUAsqDr4p3b5/vMcU1ISPTVidsEC+qSXYModAp1am7UsdXbsAK3zukoZiV+/gfM665naxyLc" +
                "m69LXqfwZHx9va/kkfjtVIIp4aqiFxmeoi5y8jxbh6TNUtuKLzW+Pny/C0QmlB0com+Tt1/gEKfJzKY4" +
                "KL8mAF8bRzxx/el+0Ycaid8j4NjsxGhvDyCJDFljEGpEkJ0EjDJspgAHg5dZ1C09ofYcY9esNku60Tg+" +
                "KkeaJ60zGYzLst7EeYQXohQs1213WYFMe1FxjYneeMVbMkpDnrOM7SJPjz75hlfZfJo0DmhGkFYk5znd" +
                "fJ54J0PSTaExW8T7uNdSojso41loHkeX0N4xT3XCfjLsH5ySHAuEEYeqFXJVltjNmK5J3oYQuoPeWQ+W" +
                "JMstJTLqjwotf3SXdrxAKwa97WEWdiMruxE7MF+F0mOcdE7m1KTGrUjppVa7YogM3LBF51mvWQBSVYhF" +
                "gT6nsWq4Sx4PIV8pdRWsqH2Tt2cDOaK2DHj6oOQ/+cPt1eELAAA=";
                
    }
}
