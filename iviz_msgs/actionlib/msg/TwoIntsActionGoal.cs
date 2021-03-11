/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionGoal")]
    public sealed class TwoIntsActionGoal : IDeserializable<TwoIntsActionGoal>, IActionGoal<TwoIntsGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TwoIntsGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TwoIntsGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TwoIntsGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionGoal(ref Buffer b)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
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
                "H4sIAAAAAAAACr1UwYrbMBC9C/IPAznsbiFZaEsPgd6W7uZQKGzuYSJNbFFbcjVyXP99n+wku4Ueetg2" +
                "GITsmTdv5r3Jk7CTRPV0GLbZx9D4w77VSu8fIzfbB6pw7L0zuyFuQ9bydnq3MJ/f+LcwX58fN6TZzQSe" +
                "JloLs6TnzMFxctRKZseZ6RhB21e1pFUjJ2mQxW0njqaveexE10jc1V4JTyVBEjfNSL0iKEeysW374C1n" +
                "oexb+S0fmT4QU8cpe9s3nBAfk/OhhB8Tt1LQ8aj86CVYoe3DBjFBxfbZg9AIBJuE1YcKH8n0PuQP70uC" +
                "WWKWK1ylwvCvxSnXnAtZ+dkl0cKTdYMa7+bm1sDGdARVnNLt9G6Pq94RioCCdNHWdAvm38ZcxwBAoRMn" +
                "z4dGCrDFBIB6U5Ju7l4hF9obChziBX5GfKnxN7Dhilt6WtXQrCnda19hgAjsUjx5h9DDOIHYxkvIBMcl" +
                "TqMpWXNJs/xSZowgZE2K4GTVaD0EcDT4XBvNqaBPahSD/jND/nEvii136GGWTuvYNw6XmArr2VIEOYfa" +
                "Q5Opj7I0NLBSKp5R9FE8tJ0kn1yJqXA4V4PO6QR3DLUE8pnQq2jxLawhbZcJM0d2wdTZOIOg9BWaDoIV" +
                "AQWykjJDvMLo9YjP/L27yKIIHhjKxJdR01HEHdh+BzOHDPiybzLWUJUrmXQg7cT6o7dzg2cGuj6jlx2Z" +
                "A0Cq7TWDGWHxELW+SIio/6De/at/sIUx2L9PH4nP52FhfgHUaK/1DQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
