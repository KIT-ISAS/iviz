/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibMsgs
{
    [DataContract (Name = "actionlib_msgs/GoalStatus")]
    public sealed class GoalStatus : IDeserializable<GoalStatus>, IMessage
    {
        [DataMember (Name = "goal_id")] public GoalID GoalId { get; set; }
        [DataMember (Name = "status")] public byte Status { get; set; }
        public const byte PENDING = 0; // The goal has yet to be processed by the action server
        public const byte ACTIVE = 1; // The goal is currently being processed by the action server
        public const byte PREEMPTED = 2; // The goal received a cancel request after it started executing
        //   and has since completed its execution (Terminal State)
        public const byte SUCCEEDED = 3; // The goal was achieved successfully by the action server (Terminal State)
        public const byte ABORTED = 4; // The goal was aborted during execution by the action server due
        //    to some failure (Terminal State)
        public const byte REJECTED = 5; // The goal was rejected by the action server without being processed,
        //    because the goal was unattainable or invalid (Terminal State)
        public const byte PREEMPTING = 6; // The goal received a cancel request after it started executing
        //    and has not yet completed execution
        public const byte RECALLING = 7; // The goal received a cancel request before it started executing,
        //    but the action server has not yet confirmed that the goal is canceled
        public const byte RECALLED = 8; // The goal received a cancel request before it started executing
        //    and was successfully cancelled (Terminal State)
        public const byte LOST = 9; // An action client can determine that a goal is LOST. This should not be
        //    sent over the wire by an action server
        //Allow for the user to associate a string with GoalStatus for debugging
        [DataMember (Name = "text")] public string Text { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GoalStatus()
        {
            GoalId = new GoalID();
            Text = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalStatus(GoalID GoalId, byte Status, string Text)
        {
            this.GoalId = GoalId;
            this.Status = Status;
            this.Text = Text;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GoalStatus(ref Buffer b)
        {
            GoalId = new GoalID(ref b);
            Status = b.Deserialize<byte>();
            Text = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GoalStatus(ref b);
        }
        
        GoalStatus IDeserializable<GoalStatus>.RosDeserialize(ref Buffer b)
        {
            return new GoalStatus(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            GoalId.RosSerialize(ref b);
            b.Serialize(Status);
            b.Serialize(Text);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Text is null) throw new System.NullReferenceException(nameof(Text));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 5;
                size += GoalId.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Text);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_msgs/GoalStatus";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d388f9b87b3c471f784434d671988d4a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1U227aQBB9R+IfRspLK1XpvU0r8UDBQlS5KdC+RuvdMd527aV7wcnfd2aDDSSg8hDV" +
                "CGOs2TNnzpyZiRVmOoYF/dxq1e9FXYcz8EGE6Nt/19nleHo5gfYawBu6n8C8xHQQSuHhHgMECznC0lmJ" +
                "3qOC/B4CxQgZtK3Bo1uha0GHo/n0ZwZboG93QbUHGZ3DOph7gtX14kjk65ssu7ieZ+MO+d0uskOJekUo" +
                "AqSoJfKbPxF9AFEEdKADC+ACReAdyhgod7+3ofr0OqGvqFUSwmuCBGmrpUGG0MG3MET1xRxdpWtiMSON" +
                "8WVLevZjNMqy8Rbp97ukG4IWstTIxH2ULEQRDWuzR4uDeYbfrm420nCeD3vy5DZVr6Jj2Tfs96ZSEf+t" +
                "DnvD2wqhENpEhwcJ3mTfs9EWwwF8fErQ4S+UzHAvoUaH0sbw2DSvjmCZoxTRYwLtssVahCCIa24QLPmj" +
                "Xgmj1cES1gbsRmYAn/6HATsH1jakcdx4sOvgRuXR8Px8M9QD+HwsxRwLS/3bx/EohakxT1u2S7sutKsI" +
                "OJTiIbhbCIkKqt0yts1y9gxlHCk1W2NnEB8yEL2Dzji/ms23sQbwJSEO61YPaTRtPIYCRa1jFHzQQXQq" +
                "MMop1UiPnoxuVJIuP2YKPYNbVpxlbTQpQCNEyR5t0n7vZGiMbYBESqE0FPRgQXhvpaaaiI8PaT3wuMGE" +
                "uHGt0acjCvO4WCQt11EB7wLj9nuDZ776vYvZ5Ou6AqPz28ov/GsmNB1THckM1ONq2arlA3eeqwqaNhJp" +
                "25RalvSGFN3aMckoqE4ZZBpYeVJB7RMMmhJr9hLVip51WjrEakldM4aPMyq95z42SMk78NaH5E90vGQS" +
                "p34vMUusUzNSEbRyaJmttKIUgjjS7ttuSIGociF/szn5iEMfTYCK/CkW3Gxqk1+i1IWW7XQkFp7NxPB8" +
                "ah1BzKqYxoTWn6YwUmHdSY7jz192Dz+TOwgAAA==";
                
    }
}
