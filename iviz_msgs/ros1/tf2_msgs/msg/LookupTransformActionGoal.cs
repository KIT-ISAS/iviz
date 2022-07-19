/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [DataContract]
    public sealed class LookupTransformActionGoal : IDeserializableRos1<LookupTransformActionGoal>, IDeserializableRos2<LookupTransformActionGoal>, IMessageRos1, IMessageRos2, IActionGoal<LookupTransformGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public LookupTransformGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public LookupTransformActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new LookupTransformGoal();
        }
        
        /// Explicit constructor.
        public LookupTransformActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, LookupTransformGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        public LookupTransformActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        /// Constructor with buffer.
        public LookupTransformActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new LookupTransformActionGoal(ref b);
        
        public LookupTransformActionGoal RosDeserialize(ref ReadBuffer b) => new LookupTransformActionGoal(ref b);
        
        public LookupTransformActionGoal RosDeserialize(ref ReadBuffer2 b) => new LookupTransformActionGoal(ref b);
    
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
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            GoalId.AddRos2MessageLength(ref c);
            Goal.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "tf2_msgs/LookupTransformActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "f2e7bcdb75c847978d0351a13e699da5";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
