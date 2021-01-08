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
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new LookupTransformGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public LookupTransformActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, LookupTransformGoal Goal)
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
                "H4sIAAAAAAAACrVUwWobMRC9C/YfBnJIUogL6c3QW2gSaKGQ3M1YGu+K7EpbjdaO/75P2nXSQA49JMZm" +
                "LWnmzZt5T3sn7CRRVx+GbfYx9H67GbTVr7eR+/sbavHYeGd+xvg0jY+Jg+5iGsppPWvM9w/+NObXw+2a" +
                "NLuZyF2l15gzesgcHCdHg2R2nJlAhTrfdpKuetlLjyweRnFUT/NxFF2VzMfOK+HbSpDEfX+kSRGVI9k4" +
                "DFPwlrNQ9oO8ASipPhDTyCl7O/WckBCT86HE7xIPUvHLT+XPJMEK3d+sERVU7JQ9SB2BYZOw+tDiEMGT" +
                "D/nbdclA4uMhXmEtLaR4YUC541wYy/OYRAtZ1nUp82XucQV4DElQyCld1L0NlnpJqAMWMkbb0QXo/z7m" +
                "LgYgCu05ed72UpAt5gDY85J0fvkvdKG+psAhnvBnyNci/4MbXoFLW1cdxOvLCHRqMUdEjinuvUPs9lhR" +
                "bO8lZIIFE6djY0raXBQgP8qwEYa8qg2erBqthxKODj53jdGcSoGqC0zbmE9z57uXZXbaQpm0i1PvsIip" +
                "8J7tRVD10HkoUzspN4gOrJSKeRSdVDvdV+mrRTEaDks5yJ32cMmhk0A+E7oVLSaGRWQYM2HyJb2g6uyg" +
                "g6D4CzhtBTcGJMhKygwNC6e3gz414d1JHwwaHCFRfJ047UTclu0T2GHOZ6ihU59xM1W5laoH6SjW77yd" +
                "21xY6GqBr3dmjgCzYdIMeoS7iLDVi5a+qvhpOubd9azgO++3xpxIZE6t5E311WlP45SsLHvzAOed8t+4" +
                "KXGVrKzilI2ZYxakGnNyq38Wt+AYs42xJ3Z7xoVz6PsvxHB70qQFAAA=";
                
    }
}
