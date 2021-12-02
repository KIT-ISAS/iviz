/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Tf2Msgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class LookupTransformActionGoal : IDeserializable<LookupTransformActionGoal>, IActionGoal<LookupTransformGoal>
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
        internal LookupTransformActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new LookupTransformGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new LookupTransformActionGoal(ref b);
        
        LookupTransformActionGoal IDeserializable<LookupTransformActionGoal>.RosDeserialize(ref Buffer b) => new LookupTransformActionGoal(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "tf2_msgs/LookupTransformActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "f2e7bcdb75c847978d0351a13e699da5";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTWsbMRC9768YyCFJIS6kN0NvIR/QQiG5m7E03hXRSluNZMf/vk/adZJCDj00ZkFI" +
                "mnnzZt6T74WtJBra0rHJLgbvtptRe/16F9k/3FCPZeNs9yPG5zI9JQ66i2mst+2u+/6ff93Px7s1abYz" +
                "jfuZ3Bk9Zg6Wk6VRMlvOTOBBg+sHSVde9uKRxOMkltptPk6iKyQ+DU4JXy9BEnt/pKIIypFMHMcSnOEs" +
                "lN0of+Uj0wVimjhlZ4rnhPiYrAs1fJd4lIqOT+V3kWCEHm7WiAkqpmQHQkcgmCSsLvS4pK64kL9d14Tu" +
                "7OkQr7CVHgq8Fqc8cK5k5WVKopUn6xo1vszNrYCN4QiqWKWLdrbBVi8JRUBBpmgGugDzX8c8xABAoT0n" +
                "x1svFdhgAkA9r0nnl++QK+01BQ7xBD8jvtX4F9jwilt7uhqgma/da+kxQAROKe6dRej22ECMdxIywXaJ" +
                "07GrWXPJ7uy2zhhByGqKYGXVaBwEsHRweeg0p4re1Kgu/SQ3fvg0mrUWsqRDLN5iE1OlPPuJoOVhcBCk" +
                "NVGfCx1YKVXDKJqoBnpoejdLYiQclmIQOe1hjcMggVwmNCpaTQtfyDhlwsCRXTF1ds1BUPoVmraC9wEK" +
                "ZCRlhnKV0fv5LvydPWmC8YIeZIlvc6adiN2yeQYziwyYsviMN6jKvTQRSCcxbufM3ODCQFcLen0gcwBI" +
                "jUUzmBFeHaJWJ/2qcp8kXd5dz6J98AfWnepnTr3kTbPR6UxjSUaWs3ls80mboy2Jm0x1F0sG/xazILWY" +
                "kzndi9gFp+u2MXpiu2e8LXT9BxNPKjWDBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
