/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/GetMapActionGoal")]
    public sealed class GetMapActionGoal : IDeserializable<GetMapActionGoal>, IActionGoal<GetMapGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public GetMapGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public GetMapActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = GetMapGoal.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public GetMapActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, GetMapGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public GetMapActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = GetMapGoal.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new GetMapActionGoal(ref b);
        }
        
        GetMapActionGoal IDeserializable<GetMapActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new GetMapActionGoal(ref b);
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
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4b30be6cd12b9e72826df56b481f40e0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWrbQBC9C/QPAzkkKSSF9mboLdTJIVBI7mG8O5aWSLvqzsqu/75vVkrSQA89NMZG" +
                "SJp58+a9t74V9pKpr5eGXQkpDmH3NGqnn7eJh7sb6nB5Cr7ZSrnnyR7WR23z7T9/2ub+YbshLX6Zf1tZ" +
                "tc0ZPRSOnrOnUQp7Lkz7BNah6yVfDXKQAV08TuKpvi2nSfTaOh/7oIRvJ1EyD8OJZkVVSeTSOM4xOC5C" +
                "JYzyDsBaQySmiXMJbh44oyFlH6LV7zOPUvHtp/JzluiE7m42qIoqbi4BpE7AcFlYQ+zwEsVziOXrF+tA" +
                "4+MxXeFeOjjwyoBKz8UYy68pixpZ1o2N+bTseA14iCQY5JUu6rMn3OolYQ5YyJRcTxeg/+NU+hSBKHTg" +
                "HHg3iCE76ADYc2s6v/wT2qhvKHJML/gL5NuQf8GNb8C21lUP8waTQOcOOqJyyukQPGp3p4rihiCxEJKX" +
                "OZ/axtqWoQD5bmKjDH3VG1xZNbkAJzwdQ+nbRku2AdUXZLVtPiydfz0jS9JWyqR9mgePm5SN9xIvgqvH" +
                "PsCZuomdIDqyUrbwKDapcbqr1teIQhqO6zjYnQ9IybGXSKEQthW1ECMiMk6FoLy1G6ouCToKhr+C005w" +
                "YkCCnOTC8NA4vRe6XZcI/sUfCA2OsCi9KU57Eb9j9wx20PkMM3QeCk6mKndS/SCdxIV9cMuaKwu9XuGt" +
                "a60As3HWAnqEs4gyqLB6ubj4YT5GPqwOvv6t2bTfECkUQBEFAAA=";
                
    }
}
