/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = "tf2_msgs/LookupTransformActionGoal")]
    public sealed class LookupTransformActionGoal : IDeserializable<LookupTransformActionGoal>, IActionGoal<LookupTransformGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public LookupTransformGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public LookupTransformActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new LookupTransformGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, LookupTransformGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public LookupTransformActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionGoal(ref b);
        }
        
        LookupTransformActionGoal IDeserializable<LookupTransformActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new LookupTransformActionGoal(ref b);
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
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f2e7bcdb75c847978d0351a13e699da5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwWobMRC971cM5JCkYBfSW6C30MTQQiG+m7E03hXZlbYayc7+fZ+0azeFHHpozIKQ" +
                "NPPmzbwnPwlbidTVpWGTXPC92+8GbfXzY+B+80Atlp2zzfcQXvK4jez1EOJQbutd8/U//5ofz4/3pMnO" +
                "NJ5mclf0nNhbjpYGSWw5MYEHda7tJK56OUqPJB5GsVRv0zSKrpG47ZwSvla8RO77ibIiKAUyYRiyd4aT" +
                "UHKD/JWPTOeJaeSYnMk9R8SHaJ0v4YfIgxR0fCq/sngjtHm4R4xXMTk5EJqAYKKwOt/ikprsfPpyVxKa" +
                "q+0prLCVFgpcilPqOBWy8jpG0cKT9R41Ps3NrYGN4QiqWKWberbDVm8JRUBBxmA6ugHzn1Pqggeg0JGj" +
                "430vBdhgAkC9LknXt2+QfYX27MMZfkb8U+NfYP0Ft/S06qBZX7rX3GKACBxjODqL0P1UQUzvxCeC7SLH" +
                "qSlZc8nm6luZMYKQVRXByqrBOAhg6eRS12iKBb2qUVz6QW5892lUay1kSbuQe4tNiFL7qo1Ay1PnIEht" +
                "ojwXOrFSLIZRNFEMtKl6V0tiJOyXYhA5HmGNUyeeXCI0KlpMC1/IMCbCwJFdMHV2zUlQ+gJNezkULkxG" +
                "YmIoVxi9ne/C39mzJhgv6E2lyGXOdBCxezYvYGaRAVPmPuENqnIrVQTSUYw7ODM3uDDQ9YJeHsgcAFJD" +
                "1gRmhFeHqPVZv6LcB0mXDnezaO/8gTXn+oljK2lXbXQ+05CjkeVsHtt8Uudoc+QqU9mFnMC/xixINeZs" +
                "TvcqdsFpmn0IPbE9Mt4Wuv4NE08qNYMFAAA=";
                
    }
}
