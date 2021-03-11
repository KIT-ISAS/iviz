/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PickupActionFeedback")]
    public sealed class PickupActionFeedback : IDeserializable<PickupActionFeedback>, IActionFeedback<PickupFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public PickupFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PickupActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new PickupFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PickupActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PickupFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PickupActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new PickupFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PickupActionFeedback(ref b);
        }
        
        PickupActionFeedback IDeserializable<PickupActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new PickupActionFeedback(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PickupActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXXMaNxR93xn+g2Z4iN2J3TbpR+oZHigQh46TMDbtq0crXXbV7GqppAXz73uull0g" +
                "hgkPSRhsvqRzj8499+q+I6nJiTy+JFIFU9nCpI+lz/yPt5UsHoIMtRc+viQzoz7Vy7dEOpXqk1hs3/SS" +
                "wVd+9JL3D7c3CKsbKu8iwV7SFyBktXRalBSklkGKRYUDmCwnd1XQigomWy5Ji/hr2CzJX2PjPDde4JmR" +
                "JSeLYiNqj0WhEqoqy9oaJQOJYEo62I+dxgopltIFo+pCOqyvnDaWly+cLInR8fT0X01WkZiOb7DGelJ1" +
                "MCC0AYJyJL2xGX4USW1seP2KNyT9+bq6wkfKkIYuuAi5DEyWnpaOPPOU/gYxfmgOdw1sqEOIor24iN89" +
                "4qO/FAgCCrSsVC4uwHy2CXllAUhiJZ2RaUEMrKAAUF/wpheXe8hM+0ZYaasWvkHcxTgH1na4fKarHDkr" +
                "+PS+ziAgFi5dtTIaS9NNBFGFIRsEvOek2yS8qwmZ9N+yxliEXTEjeJXeV8ogAVqsTcgTHxyjx2w8Gp18" +
                "M0OerJBewu+R3AwvTIFz/Katm+bDbPJhPP1wK9rHQPyE/+xMittELr3YUGBPpsQSqSb3W42a4Ei7W6FY" +
                "G8zhaD79ZyL2MH8+xOSk1M5BXPgwJZbpLODZ/WTyfjafjDvgV4fAjhTB3XAmsg6H8DcoAB+EXASY2QQ+" +
                "veMc0VMsBZslO6LPH338wSdRhcZzKMxlQYxggm9RQPRiTq5EARbcDQJdbik//D0aTSbjPcqvDymvgSxV" +
                "btAlNKyoWIVFza3gmBCnwgz//Hi/04XD/HIkTFrFo+s6OnPH/WgkXdMXpWFX+AqVsJCmqB2donc/+Wsy" +
                "2uM3EL8+p+foX1LM7ygdrqmqDp/b5eWXOaakJNpqxOyC1WiVQYIpNwk0a2NXsjD61AG2zusqZSB++w7O" +
                "66xnqxCLcGe+LnmdwqPh3d2ukgfi93MJpoTbio4yPEdd5OR5tg5J24VxJd9rfIN0aYitmZmQPjjEvk3e" +
                "fIVDnCczm+Kg/JoAfHOc8MTdx4f5PtRA/BEBh7YVY3uBAEloZI1BqBFBdhIwynUzCHgYvNBRt/SM2vOM" +
                "XbHaLOna4PioHMQ6bJ1Jf1gU1TqOJLwQpYA31e6+ApntXcU1JvYmLN6iKa2zjGXcLgr0FJLveptNxzxk" +
                "sQmaQWSrkw+ccT5SvJmh6jo3mDDirbzXVaJBSPNENI0DTJyxjkiF/WTZQjgoedYIgw6VS6SrKLCbMX2T" +
                "vzUhdAfdug+uJMddJTLaHxi2/NFgtkOGx+K1RKPbT0Q7u7IhsQNTVl0EDJXey4wzjOz4JSmzMKqth8jA" +
                "s4EYnSe+ZgFIlXWsC7Q6g1XXbf6w6ttlr4QfTWhSdziY95KkZcADCPWS/wEpV9bx6AsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
