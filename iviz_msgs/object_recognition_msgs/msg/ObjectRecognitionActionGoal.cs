/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = "object_recognition_msgs/ObjectRecognitionActionGoal")]
    public sealed class ObjectRecognitionActionGoal : IDeserializable<ObjectRecognitionActionGoal>, IActionGoal<ObjectRecognitionGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ObjectRecognitionGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectRecognitionActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ObjectRecognitionGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectRecognitionActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ObjectRecognitionGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectRecognitionActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ObjectRecognitionGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionGoal(ref b);
        }
        
        ObjectRecognitionActionGoal IDeserializable<ObjectRecognitionActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new ObjectRecognitionActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "195eff91387a5f42dbd13be53431366b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTWvbQBC9C/wfBnJIUrAL7c3QW2iSQ2lpcivFjHfH0rSrXXVnZdf/vm8l56OQQw+N" +
                "EQh5Z968mfdmb4S9ZOqmV8OuaIpBt5veWnt7nTjcXlGL10Z983n7Q1z5Ki61UWtgPZ9OF82H//xbNJ/u" +
                "rtdkxc9UbiaCi+aM7gpHz9lTL4U9F6ZdQgPadpKXQfYSkMX9IJ6m03IcxFZIvO/UCE8rUTKHcKTREFQS" +
                "udT3Y1THRahoL3/lI1MjMQ2ci7oxcEZ8yl5jDd9l7qWi4zH5NUp0QrdXa8REEzcWBaEjEFwWNo0tDqkZ" +
                "NZb372pCc3Z/SEt8SgsZHotT6bhUsvJ7yGKVJ9saNd7Mza2AjelAiuiNLqb/Nvi0S0IRUJAhuY4uwPzL" +
                "sXQpAlBoz1l5G6QCO0wAqOc16fzyGXKlvabIMT3Az4hPNf4FNj7i1p6WHTQLtXsbWwwQgUNOe/UI3R4n" +
                "EBdUYiF4L3M+NjVrLtmcfawzRhCyJkXwZrPkFAJ4OmjpGiu5ok9qVKu+miFf3JBqy3v0MEtnXRqDx0fK" +
                "lfVsKYKch06hydRHXRo6sFGunjH0UT10O0k+uRJT4XiqBp3zHu44dBJJC6FXsepbWEP6oRBmjuyKabNx" +
                "DoLSj9C0FawIKJCTXBjiVUbPR3zir/5BFkwY9KBMeho17UT8lt1PMPPIgC/HULCGZtzKpAPZIE536uYG" +
                "TwxsdUKvOzIHgFQ/WgEzwuIhavUgIaJeT7003WCb/HSFzTK+eLMtmmabUqh6bHLSZhcSY2u/faedhiJ5" +
                "E7TXYovmD0I1rpBEBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
