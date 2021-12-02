/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TwoIntsActionGoal : IDeserializable<TwoIntsActionGoal>, IActionGoal<TwoIntsGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TwoIntsGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public TwoIntsActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TwoIntsGoal();
        }
        
        /// Explicit constructor.
        public TwoIntsActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TwoIntsGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal TwoIntsActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TwoIntsGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TwoIntsActionGoal(ref b);
        
        TwoIntsActionGoal IDeserializable<TwoIntsActionGoal>.RosDeserialize(ref Buffer b) => new TwoIntsActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 16 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "684a2db55d6ffb8046fb9d6764ce0860";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWrbQBC96ysGfEhSsANt6cHQW2jiQ6GQ3M1odywtlXbVnZVV/33frmTHhR56SIxg" +
                "WWnmzZt5b/wkbCVSW46KTXLBd67e99ro/WPgbvdADY69s9XLFHY+aX5b3lVf3/hXfX9+3JImO5d/mkmt" +
                "6Dmxtxwt9ZLYcmI6BHB2TStx3clROiRxP4il8jWdBtENEl9ap4SnES+Ru+5EoyIoBTKh70fvDCeh5Hr5" +
                "Kx+ZzhPTwDE5M3YcER+idT6HHyL3ktHxqPwaxRuh3cMWMV7FjMmB0AkIJgqr8w0+UjU6nz59zAnVCoNc" +
                "4yoNJn8pTqnllMnK7yGKZp6sW9T4MDe3ATaGI6hilW7Luz2uekcoAgoyBNPSLZj/OKU2eAAKHTk6rjvJ" +
                "wAYTAOpNTrq5u0LOtLfk2Ycz/Iz4WuN/YP0FN/e0bqFZl7vXscEAETjEcHQWofWpgJjOiU8Eu0WOpypn" +
                "zSWr1bc8YwQhqyiCk1WDcRDA0uRSW2mKGb2okd35Tm7850oUay1kSdswdhaXEDPl2U8ELafWQZDSRF4X" +
                "mlgpZsMomsgG2hW9iyUxEvZLMYgcj7DG1IonlwiNimbTwhfSD4kwcGRnTJ1dMwlKX6CpFuwHKJCRmBjK" +
                "ZUbX8134O3vWBOMFPcgSXudMBxFbs/kJZhYZMOXYJeygKjdSRCAdxLiDM3ODCwPdLOh5QeYAkOpHTWBG" +
                "2DpEbc76ZeXeW7r7q3+uqsLmfflMvJx19QcXCzfiAwUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
