/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract (Name = "actionlib/TwoIntsActionGoal")]
    public sealed class TwoIntsActionGoal : IDeserializable<TwoIntsActionGoal>, IActionGoal<TwoIntsGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TwoIntsGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TwoIntsGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TwoIntsGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TwoIntsActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TwoIntsGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionGoal(ref b);
        }
        
        TwoIntsActionGoal IDeserializable<TwoIntsActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "684a2db55d6ffb8046fb9d6764ce0860";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwYrbMBC9+ysGctjdwmahLT0Eelu6m0OhsHsPY2lii9qSq5Hj+u/7JDtpCj300A0G" +
                "IXvmzZt5b/IsbCVSW46KTXLBd64+9Nrow1Pgbv9IDY6Ds9XrFPY+aX5b3lWf//Ov+vrytCNNdin/vJDa" +
                "0Etibzla6iWx5cR0DODsmlbifScn6ZDE/SCWytc0D6JbJL62TglPI14id91MoyIoBTKh70fvDCeh5Hr5" +
                "Ix+ZzhPTwDE5M3YcER+idT6HHyP3ktHxqPwYxRuh/eMOMV7FjMmB0AwEE4XV+QYfqRqdTx/e54Rqg0He" +
                "4yoNJn8pTqnllMnKzyGKZp6sO9R4tzS3BTaGI6hilW7LuwOuekcoAgoyBNPSLZh/m1MbPACFThwd151k" +
                "YIMJAPUmJ93cXSH7Au3ZhzP8gvi7xr/A+gtu7um+hWZd7l7HBgNE4BDDyVmE1nMBMZ0Tnwh2ixznKmct" +
                "JavNlzxjBCGrKIKTVYNxEMDS5FJbaYoZvaiR3flGbvzrShRrrWRJ2zB2FpcQpfRVGoGWU+sgSGkirwtN" +
                "rBSzYRRNZAPti97FkhgJ+7UYRI4nWGNqxZNLhEZFs2nhC+mHRBg4sjOmLq6ZBKUv0FTLMXNhMhITQ7nM" +
                "6Hq+K39nz5pgvKA35yKXOdNRxNZsvoOZRQZMOXYJO6jKjRQRSAcx7ujM0uDKQLcrel6QJQCk+lETmBG2" +
                "DlHbs35ZubeW7uHqn6vC4n36SLyedfULZGkgFAIFAAA=";
                
    }
}
