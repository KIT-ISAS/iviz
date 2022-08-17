/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class GetMapAction : IDeserializable<GetMapAction>, IMessage,
		IAction<GetMapActionGoal, GetMapActionFeedback, GetMapActionResult>
    {
        [DataMember (Name = "action_goal")] public GetMapActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public GetMapActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public GetMapActionFeedback ActionFeedback { get; set; }
    
        public GetMapAction()
        {
            ActionGoal = new GetMapActionGoal();
            ActionResult = new GetMapActionResult();
            ActionFeedback = new GetMapActionFeedback();
        }
        
        public GetMapAction(GetMapActionGoal ActionGoal, GetMapActionResult ActionResult, GetMapActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public GetMapAction(ref ReadBuffer b)
        {
            ActionGoal = new GetMapActionGoal(ref b);
            ActionResult = new GetMapActionResult(ref b);
            ActionFeedback = new GetMapActionFeedback(ref b);
        }
        
        public GetMapAction(ref ReadBuffer2 b)
        {
            ActionGoal = new GetMapActionGoal(ref b);
            ActionResult = new GetMapActionResult(ref b);
            ActionFeedback = new GetMapActionFeedback(ref b);
        }
        
        public GetMapAction RosDeserialize(ref ReadBuffer b) => new GetMapAction(ref b);
        
        public GetMapAction RosDeserialize(ref ReadBuffer2 b) => new GetMapAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) BuiltIns.ThrowNullReference();
            ActionGoal.RosValidate();
            if (ActionResult is null) BuiltIns.ThrowNullReference();
            ActionResult.RosValidate();
            if (ActionFeedback is null) BuiltIns.ThrowNullReference();
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = ActionGoal.AddRos2MessageLength(c);
            c = ActionResult.AddRos2MessageLength(c);
            c = ActionFeedback.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "nav_msgs/GetMapAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "e611ad23fbf237c031b7536416dc7cd7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71YW1PbRhR+16/YGR4CHduBJE3TzPDggkPphIQC6QvDMGvp2NpE2nV2Vxj31/c7u5Js" +
                "EZMwndgegy3pXL5zP+sT8mdyNky9MvrEyELI8PV2iu/JycrDC3JV4ZvHNlx1CN4RZWOZfmlIJvV1cviT" +
                "X8nZ5clboeXdbemm7vnJAwuSP0lmZEUePpKIplDjmhoUp8eCzbtVWW1AMDxYvBmszmdRe4SW7IhLL3Um" +
                "bSZK8jKTXoqJAWQ1zcn2C7qjAkyynFEmwlO/mJEbgPEqV07gPSVNVhbFQlQORN6I1JRlpVUqPQmvSurw" +
                "g1NpIcVMWq/SqpAW9MZmSjP5xMqSWDrejr5WpFMSp8dvQaMdpZVXALSAhNSSdEpP8VAkldL+5QtmEDvi" +
                "+sK4g5tk52pu+rhPU0SgRSF8Lj2jpvsZMocBS/cWyn6JVg6gBF4iqMuc2A33bnHp9gS0AQvNTJqLXZhw" +
                "vvC50RBI4k5aJccFseAUroDUZ8z0bG9Fsg6itdSmER8lLnU8Raxu5bJN/RzBK9gNrprCkyCcWXOnMpCO" +
                "F0FIWijSXiDtrLSLhLmiymTnHTsbROAKocGndM6kCpHIxFz5PHHesvQQFs7SDaXl2tIIOVaDFS43VZHh" +
                "wlgKdgVDEMt5rhCQYATXjZhLJyxnjoMRnEmnId4hN+ESqWtlCLK9Q2rMc9JCeQFDyXH2Ii+onKG/FAW4" +
                "WaaLWTMnqG5FizFNGIsUKVkvETlGtOrfGr/KmpjAvYC3YCWtn8WkbVY6A0dsZyhG5+SUQhCEm1GqJiqN" +
                "BtYI3KCWzpUSCQCqrJwHMoHyA9WgiR9HbjvdL/S9bTbaOA1+3GrR53zlODT4qLttPUnqEbIZ0I9CSR4M" +
                "AG5ibxp88eJ89OH49MOJaF6HYh//Y1qFXMiR7AvynFEIOtIsjT2trv1Optcyh0dXp/+MxIrMg65MbjaV" +
                "tWgaaLRj4vR5kuDzi9Ho7PxqdNwKftEVbCkltO+MS0ai87WpLOTEI3YoQlhvubboPvR6PU3Ed147+EPV" +
                "BC/EXorJMyuIJSjvGikAuntFtsSEKXjcedqrIV9+OjoajY5XIL/sQuZmItNcEcN2VcpemFQ869Y54jE1" +
                "wz8+Xiz9wmperVEzNsH0rAoVu8S+VlNW0Q9dw1nhDNrRRKqiQqt6BN7F6K/R0Qq+Q/Hrt/AsfabUP5IB" +
                "oU2Zyj9Ml96PMY4plejNQWarrMIuwC01DD9sI0rfyQJ99BED6sxrK+VQvN5C5rWpp40PRbhMvjZ4rYeP" +
                "hu/fLyv5UPz2VID1lFmH8CneRUy+jVYXtJ4oW/LixjPOr3aBgISyjhGrafLmJxjxNDdzUnTKLyrgjeiR" +
                "nHj/8fJqVdSh+D0IHLbzv16MIElkiBoLoegE2bqApQziplsvIOy38RNqz7Fsw95ml84VzF+zfWBHGBaF" +
                "mYedmwlRCra7H0hRz/CwCqxMMmbJaFxNp+zGmsjTvd/WqK8nb3v3Y5pWM8RlcWJRq6WcbRpGR2FzIrHE" +
                "iz28z9vWiz7ma42mx+eOuC0Serrg/FmlZvejc43lWBXKL4SZQKRpVAyS7oohELoznJiOmxMTs7PNcEx7" +
                "X+mJadZAPAtbdoBhzbxfys9gw8mHbC/WRRvl3f3e/t5AiNZAyFhC4z1VcjnFU4KVGovi9X7vYH//Bkyf" +
                "9Bdt5prTt38wSLgarm+C6o3nxYrtTThyg6IZ46iWBm/YUob0x7SrW1OaS4uaIKuw08Lp4WY3srUHH278" +
                "0afcGgqDkGRx98a9W76+Dev40vcIsyniRL0un3Pwb5IJCPnguHwGhjMWqjKE4Zqp3E1zvAw3a4KccED2" +
                "Dyni3VqpsQqV2VjEEK7LnsDbyozj1JyfQwxJFv25sfDVDMfDmgmCQpKGdGjCDUGDZEqY694uotvPA0tQ" +
                "t6EIf6sP2IbL6olBBeqAHkgnOEThWCJT6oW5aHh5jM9VTAB0dUBueAciOTdwYkuQ/F2h+1kd5C7pNpXC" +
                "Dw0ElCaDMSF5F6kbRIMftkhcMeSOuTGnXr8S9+23Rfvt3+3AX7puXVPs+LMLnq++Lv3O5Yq+932Lmm/z" +
                "rZ76mp/4/t+5r/2BcLu/DLagk/8Anb2ZVOwUAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
