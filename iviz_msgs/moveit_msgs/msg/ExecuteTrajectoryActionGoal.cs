/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/ExecuteTrajectoryActionGoal")]
    public sealed class ExecuteTrajectoryActionGoal : IDeserializable<ExecuteTrajectoryActionGoal>, IActionGoal<ExecuteTrajectoryGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ExecuteTrajectoryGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ExecuteTrajectoryActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ExecuteTrajectoryGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ExecuteTrajectoryActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ExecuteTrajectoryGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ExecuteTrajectoryActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ExecuteTrajectoryGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionGoal(ref b);
        }
        
        ExecuteTrajectoryActionGoal IDeserializable<ExecuteTrajectoryActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new ExecuteTrajectoryActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "moveit_msgs/ExecuteTrajectoryActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "36f350977c67bc94e8cd408452bad0f0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8VXW2vcRhR+F/Q/DPghdlmvISl5MPSh4CR2ITRtTF+CWWalI+3E0owyM/Jm++v7nTO6" +
                "rNZ2U2jtGMNKmnM/37nMJemCvNrIT6bzaJytzXrVhCqcvXO6vrpQFX5WpsjefKW8i3Tt9WfKo/M7PpfT" +
                "7Of/+S97//HduQqxSIZcJvOO1MeobaF9oRqKutBRq9LBelNtyJ/WdEc1mHTTUqHkNO5aCkswXm9MUPiv" +
                "yJLXdb1TXQBRdCp3TdNZk+tIKpqGZvzgNFZp1WofTd7V2oPe+cJYJi+9boil4z/Ql45sTurq4hw0NnCs" +
                "DAzaQULuSQdjKxyqrDM2vnrJDNnR9dad4pUq5GBUruJGRzaWvraeAtupwzl0/JicW0I2gkPQUgR1LN9W" +
                "eA0nCkpgArUu36hjWP5hFzfOQiCpO+2NXtfEgnNEAFJfMNOLkz3JVkRbbd0gPkmcdPwbsXaUyz6dbpCz" +
                "mr0PXYUAgrD17s4UIF3vREheG7JRAXhe+13GXElldvSWYwwicElG8KtDcLlBAgq1NXGThehZumSDcfpE" +
                "aHywOARavbEqbFxXF3hxnsQvcQS53G4MEiJOcLmorQ7KM2ACnGAAXUm+BZIIiba9MiTZ3wEa2w1ZZaKC" +
                "oxQYtMAFNW1UCDi4WWZIqNkSVI+i1ZpKtkWrnHzUyBxbtB/f3n5TDDlBeGHejpWMcVYlUbHW+S0sK8AB" +
                "UHZ1RA2GoCuSJKjQUm5KkycHewvCspfOBZIIYFTThQjLFKoOVMshf5y5J0pd4+7IxJS3B7tYlv3h1i5O" +
                "HxHq4fE5jDrQnk3a0/mvDl1iz7rP/L6aqH64x/EeGTIXv7095Gz4+6pw5eq+jCdy9BvOZJezGZTQ8Omm" +
                "99GiqEN2wPKBX0HS8m/4TnaLEQD2G821PUVYjBrhji6O8oB7rQuGazp8WiiMBVRWxCledJ5TjaEkhzc3" +
                "kOjm1FSiiOONjDrpKnsodVxIlCAtneSXut4rtTtdd1zS6AEm9evA7RQTDBbpIF8kzkrizEQHXi7hepaV" +
                "tdPx9U8S8t6wvW+TO3sfZ27tfU/eZEWXjqQjrUrvmhU6Eg6eKZmP1EffDxMWx56MMKeY9uPqYAXgtigH" +
                "nkr0Xx6Y0mIfSNjgdjhAPWcO7DzvMQlTZFyJXiz1eop67fO0J+uYGHkJbjwW+cwGKG2E/2TAZqLQtUOT" +
                "PTBnawAXTPi6K0jGjvfo/VA2pvlsSu7ZPKVHaeJsehgJsGqyFYZB/2mUtgexhQRrxpTQOJDxfFcifA5N" +
                "EbbMHu4Pj2Tz+/SJfzJmyMlhWnMEf4DYXiLVcdcy+l4ryDvJKnJYfAc91wMVPBw5Qj/SUfEsc83Dv0/h" +
                "bq8xDNB03lRGoDbF+1DN1oQ4r/J7Kuys3P+bnjnKnr1RPBLj4Q4x1mgYaqnP1JrilrCmxa271yCksZbY" +
                "2RAZnWPlyf4UTLxK/LU4mP3egcFb9tW71AOex8nemAdcZOzw2YH9alxZsRBjpyDNnclNnGAsjCdZYpcJ" +
                "K7yILniJLRziYR2XQqNvIZJwX5B9s23rEf11n3THLMe0rJaLtAYLFe+LcjuT+xy2ToZXcdAARabqnVuo" +
                "WL5M/U5sTsqQQt5n+2ifLNVVqXauwwYMH/Dg+2ukjNnBLrnuROcWPBx6EfOASqmP+7Gx2MU1pvMwBdXX" +
                "8Wk3Pv31LKmeMPZQti3X6Th/Zjnnty8TQDnI33RoeNo+U61yAxncGu7OYep+c3/W3t3yrcoKxAIun5Zw" +
                "O+XppG0lV32+9YflWKs9yfTe02XZ31ylYCpGEQAA";
                
    }
}
