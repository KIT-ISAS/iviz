/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsActionGoal : IDeserializable<TwoIntsActionGoal>, IMessage, IActionGoal<TwoIntsGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TwoIntsGoal Goal { get; set; }
    
        public TwoIntsActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TwoIntsGoal();
        }
        
        public TwoIntsActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TwoIntsGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public TwoIntsActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TwoIntsGoal(ref b);
        }
        
        public TwoIntsActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TwoIntsGoal(ref b);
        }
        
        public TwoIntsActionGoal RosDeserialize(ref ReadBuffer b) => new TwoIntsActionGoal(ref b);
        
        public TwoIntsActionGoal RosDeserialize(ref ReadBuffer2 b) => new TwoIntsActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference();
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength + GoalId.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = GoalId.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 16; // Goal
            return c;
        }
    
        public const string MessageType = "actionlib/TwoIntsActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "684a2db55d6ffb8046fb9d6764ce0860";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUTYvbMBC9+1cM5LC7hWTpBz0Eelu6m0OhdPdWyjKWJraoLbkaOan/fZ9kJ02hhx66" +
                "wSAsz7x5M+9NHoStRGrLUbFJLvjO1c+9Nnp7H7jb3VGD49nZ6ukYdj5pvi131Yf//Ks+Pd5vSZOdyz/M" +
                "pFb0mNhbjpZ6SWw5Me0DOLumlbju5CAdkrgfxFL5mqZBdIPEp9Yp4WnES+Sum2hUBKVAJvT96J3hJJRc" +
                "L3/kI9N5Yho4JmfGjiPiQ7TO5/B95F4yOh6VH6N4I7S72yLGq5gxORCagGCisDrf4CNVo/Pp7ZucQCv6" +
                "+iXo62/VChNd414aSHBmQanllFnLzyGKZsKsWxR7NXe5QRFMSVDOKl2Xu2e86g2hGrjIEExL12jh85Ta" +
                "4AEodODouO4kAxuMAqhXOenq5gLZF2jPPpzgZ8TfNf4F1p9xc0/rFuJ1eQw6NpgkAocYDs4itJ4KiOmc" +
                "+ETwXeQ4VTlrLlmtPuZhIwhZRRqcrBqMgxKWji61laaY0Yss2aYvZMu/7kbx2EKWtA1jZ/ESopS+SiPQ" +
                "8tg6CFKayHtDR1aK2TmKJrKTdkXv4k2MhP1SDCLHA6xxbMWTS4RGRbN74Qvph0QYOLIzps6uOQpKn6Gp" +
                "ln3mwmQkJoZymdHlfBf+zp40wXhBb8pFznOmvYit2XwHM4sMmHLsEpZRlRspIpAOYtzembnBhYFuFvS8" +
                "KXMASPWjJjAjrB+iNif9snIvLd3txV9YVWHz3r8jXs66+gWtcZ9xDAUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
