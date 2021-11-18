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
        internal ExecuteTrajectoryActionGoal(ref Buffer b)
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
                "H4sIAAAAAAAACsVYS2/cNhC+C9j/QMCH2MV6DSRFDgZ6KOA8XCBo2hi9BMaCK40kxhKpkNRutr8+35B6" +
                "rbxuCrS2DQOSyJnhPL55cN+TzMiKMjwSmXpldKU269oV7uKdkdX1lSjwWKssefON0tbTjZVfKPXG7nk/" +
                "7C6SX/7nv0Xy4dO7S+F8FlV5HxRcJCfik5c6kzYTNXmZSS9FbmCAKkqy5xVtqQKXrBvKRNj1+4bcCow3" +
                "pXIC/wVpsrKq9qJ1IPJGpKauW61S6Ul4VdMBPziVFlI00nqVtpW0oDc2U5rJcytrYun4d/S1JZ2SuL66" +
                "BI127C4FhfaQkFqSTukCmyJplfavXjJDcnKzM+f4pAJhGA4XvpSelaVvjSXHekp3iTN+isatIBveIZyS" +
                "OXEa1tb4dGcCh0AFakxailNo/nHvS6MhkMRWWiU3FbHgFB6A1BfM9OJsIpnVvhRaatOLjxLHM/6NWD3I" +
                "ZZvOS8SsYutdW8CBIGys2aoMpJt9EJJWirQXwJ6Vdp8wVzwyOXnLPgYRuEJE8JTOmVQhAJnYKV8mzluW" +
                "HqLBUH00QB7NEIblDWyIoXOlaasMH8ay1hFSAuHclQoxCXZw0oiddMIyZhzsYAxdh5AHVMIrUnenIc52" +
                "C3TsStJCeQFbyTFuAQ2qGy/gc3CzTBeBsyMcPYgWG0KKQAWRkvUSwWONpi7u9FdZHxZ4GOohMmZ0tciJ" +
                "so1M76BZBg7gsq080tA5WVCIg3ANpSpXaTSw08CtOumcI5EAStWt89BMIPFAtepDCKrHi15ttqR8DN3R" +
                "arZIkj/NxvhxFd7uX59Gr9n5i2RUIBL8ZlAuJgp+4e/1RM05wweESV39/nbOWPP6OjP5+p6IR7P0B8Ys" +
                "ksOGFEHx+bYzUiO9XTLj+cifIGn46Z5N9aAGV4I3krN8dHPQawA+SjoSBRY2xinObvd5KdAjkGMeu/iQ" +
                "aUoVOlTYvL2FRHNITTnS2d+GvhfqywSshlOKIrRDTfm1qiZJt5UVMlJIVAMuA1yzuLainUEjlCNeCa4W" +
                "wdVMNDNzBduTJK+M9K9/Dl7vFJusjeZMFg/MmqxHa5KsjVuhNq1za+o1ahM2niyeD6RJX9wjIocCDU9H" +
                "t3btazYScI0MG5ZyFGNuoKHeHolZb7mbYZ+DB3bu/+iM0TkmR2EOeXuOvO1CNZF1Sgy+iDhuk7ynHQ6t" +
                "A/9ZD89IISuDijtTZ6eAGHT8qs3YCmDFohHgsCHSF2N8Lw6jehLbT9khKWCrIl2gM3RLg7QJypbBWQdM" +
                "EZA9Gfd7HrDm6AzCVsnxKvFAOJ+rWvyTOkPVmEc2hf97lE1iKU5bTIhGvBYQeJYUZDAL9wfd9FQwcuBw" +
                "XYtH3rNM1AjZZ+l+Uh56dBqrCqQ+9BhdPj9mp5w/zPV7R/AAM8Ljv51zCLRnKBcPeDmWB+TakKkRxmOw" +
                "NuR3hMnN78y9MhEqbI4xDs6RKaag5K+Ai1eRvwo2Jn+0YLCazbUmVoKnsrNT55iVjCDenJkghkEWczKG" +
                "DJJcoszICcZMWbDCjDAZ2jCeLnm0zQxcoo2HjFreQSThIhGm0KaphhyIbuFlsJzSqlgt43AcqHiKDNe2" +
                "cNHDLMogy2aVMMgUnXVL4fOXsfAFneNhiCJPuZ3Dz1biOhd702Iuhg14sd39MrTcXq9wD/LGLLlLdCIO" +
                "PRoyfpialcaELtGp+44ovg1v++Ht7yeK9gi0owFHpuLq0feig7Dz19cRpuznH9mU9G+7J0tariWDZf3V" +
                "2o2V8NCkjTV3fOPSAWgOd1NNuLxys5K6CL8E8I8C+HGhT9qOZPzu6GDgdzcNxeBpEQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
