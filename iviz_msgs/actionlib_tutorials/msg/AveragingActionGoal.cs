/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract (Name = "actionlib_tutorials/AveragingActionGoal")]
    public sealed class AveragingActionGoal : IDeserializable<AveragingActionGoal>, IActionGoal<AveragingGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public AveragingGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new AveragingGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, AveragingGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal AveragingActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionGoal(ref b);
        }
        
        AveragingActionGoal IDeserializable<AveragingActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionGoal(ref b);
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
                int size = 4;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1561825b734ebd6039851c501e3fb570";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwYrbMBC9+ysGctjdQrLQ3gI9FJbu5lAo7N7DRJrYorLkauSk/vs+yU66Cz300A0G" +
                "I3nmzZv3ZvIkbCVRV18Nm+xi8O6w77XV+8fIfvdALV57Z5svJ0ncutCW+3rbfP7Pv+bb8+OWNNuZwNNM" +
                "a0XPmYPlZKmXzJYz0zGCtWs7SWsvJ/FI4n4QS/VrngbRDRJfOqeEp5UA8t5PNCqCciQT+34MznAWyq6X" +
                "N/nIdIGYBk7ZmdFzQnxM1oUSfkzcS0HHo/JzlGCEdg9bxAQVM2YHQhMQTBJWCIaP1Iwu5E8fS0KzejnH" +
                "NY7SQvtrccod50JWfg1JtPBk3aLGh7m5DbAhjqCKVbqtd3sc9Y5QBBRkiKajWzD/PuUuBgAKnTg5Pngp" +
                "wAYKAPWmJN3cvUIOFTpwiBf4GfFPjX+BDVfc0tO6g2e+dK9jCwEROKR4chahh6mCGO8kZMLAJU5TU7Lm" +
                "ks3qa9EYQciqjuDNqtE4GGDp7HLXaE4FvbpR5vOdpvGvS1FHayFL2sXRWxxiktpXbQRenjsHQ2oTZV3o" +
                "zEqpDIyiiTJAu+p3HUlIwmEpBpMTlg35EshlQqOiZWgxF9IPmSA4sgumzlNzFpS+QtNBjoULk5GUGc4V" +
                "Rq/1Xfg7e/EE8oLeVIpcdaajiD2w+QFmFhkYytFn7KAqt1JNIB3EuKMzc4MLA90s6GVB5gCQ6kfNYEbY" +
                "OkRtLv4V597dujzCHAe57t/8izXNspTQxIs2vwEKcGpoDwUAAA==";
                
    }
}
