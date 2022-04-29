/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ExecuteTrajectoryActionFeedback : IDeserializable<ExecuteTrajectoryActionFeedback>, IActionFeedback<ExecuteTrajectoryFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ExecuteTrajectoryFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public ExecuteTrajectoryActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new ExecuteTrajectoryFeedback();
        }
        
        /// Explicit constructor.
        public ExecuteTrajectoryActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ExecuteTrajectoryFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public ExecuteTrajectoryActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new ExecuteTrajectoryFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ExecuteTrajectoryActionFeedback(ref b);
        
        public ExecuteTrajectoryActionFeedback RosDeserialize(ref ReadBuffer b) => new ExecuteTrajectoryActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Feedback is null) BuiltIns.ThrowNullReference();
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
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71W23LbNhB951dgxg+xO7XaJr2kntGDKjGOMk7isdW+ekBiRaIlQRUXyfr7ngUpinKs" +
                "Rg9JNLJ1A84enD272LckFVlRxpdE5l43ptLZQ+0K98N1I6t7L31wwsWXJH2kPHhaWPk35b6x2zdEKpP5" +
                "P2LZvUnGX/iRvL+/vkJ81XJ62zI9EyBmlLRK1OSlkl6KZYOD6KIke1nRmiomXa9Iifir367IjbBxUWon" +
                "8CzIkJVVtRXBYZFvRN7UdTA6l56E1zUd7MdObYQUK2m9zkMlLdY3VmnDy5dW1sToeDr6N5DJScxnV1hj" +
                "HGumQWgLhNySdNoU+FEkQRv/6iVvSM4Wm+YSH6lAOvrgwpfSM1l6XFlyzFO6K8T4rj3cCNgQhxBFOXEe" +
                "v3vAR3chEAQUaNXkpTgH89utLxsDQBJrabXMKmLgHAoA9QVvenExQDYR2kjT7OBbxH2MU2BNj8tnuiyR" +
                "s4pP70IBAbFwZZu1VliabSNIXmkyXsCDVtptwrvakMnZG9YYi7ArZgSv0rkm10iAEhvty8R5y+gxGw9a" +
                "JV/JjUfrJOG3yGyBF47PCX69K572w236YTb/cC12j7H4Ef/ZlhS3iVI6sSXPhsyI9cnbxHcCtbGRc7tG" +
                "HbSYk+li/lcqBpg/HWJyRoK1UBYmzIg1Ogn49i5N398u0lkP/PIQ2FJOsDZsiZTDHvwN3O+8kEsPJ2vP" +
                "p7ecIIq9A6ET8T+PM/zBJFGF1nCoylVFjKC926GA6PmCbI3qq7gVeLroKN//OZ2m6WxA+dUh5Q2QZV5q" +
                "Ytou5KzCMnAfeE6IY2Emf3y82+vCYX5+JkzWxKOrEG255/5sJBXos9KwK1yDMlhKXQVLx+jdpe/S6YDf" +
                "WPzyKT1L3MWPOCAWVBP8U7t8/3mOGeUSPTVi9sEC+qSXYModAp1am7WstDp2gM55faWMxa/fwHm99Uzj" +
                "YxHuzdcnr1d4Orm52VfyWPx2KsGMcFXRswxPURc5+TRbh6TNUtuaLzW+PvywC0QmpA4OMbTJ6y9wiNNk" +
                "ZlMclF8bgK+NI564+Xi/GEKNxe8RcGJ2YnS3B5CEQtYYhFoRZC8Bo4zaKcDB4JWKumUn1J5j7IbVZkk3" +
                "GsdH5UjzpHUmZ5OqajZxHuGFKAXLddtfViDTXVRcY2IwZvEWRVkoCpaxW+Tp0Sff8Cqbz5LWAe0I0onk" +
                "PKebzxPvZEi6KTVmi3gfD1pKdAcpnoXmcXQJ3R3zVCfsJ8P+wSnJsUAYcaheIVdVhd2M6drkbQihe+id" +
                "9WBJstxSIqPhqNDxR3fpxgu0YtDbHmZhN7KyG7ED81WoPMZJ52RBbWrcinK91PmuGCIDN+rQedZrF4BU" +
                "HWJRoM9prBrtksdDyFdKXQ0rat/m7ehgjugdE55CKPkP5b96TfELAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
