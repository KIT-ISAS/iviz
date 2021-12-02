/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeActionGoal : IDeserializable<ShapeActionGoal>, IActionGoal<ShapeGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ShapeGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public ShapeActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ShapeGoal();
        }
        
        /// Explicit constructor.
        public ShapeActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ShapeGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal ShapeActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ShapeGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeActionGoal(ref b);
        
        ShapeActionGoal IDeserializable<ShapeActionGoal>.RosDeserialize(ref Buffer b) => new ShapeActionGoal(ref b);
    
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
    
        public int RosMessageLength => 8 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTW/bMAy961cQ6KHtgKbAdguwW9GPw4AB7T1gJMYWJkueSCfLv9+TnaYdsMMOa2DA" +
                "kEw+PvI95lE4SKV+fjn2FktOcbsZtNPbh8Lp6Y46vDYxuOeeR2l38437+p9/7tvzw5rUwlL8caF0Qc/G" +
                "OXANNIhxYGPaFTCOXS/1JsleEpJ4GCXQ/NWOo+gKiS99VMLTSZbKKR1pUgRZIV+GYcrRswlZHOSPfGTG" +
                "TEwjV4t+SlwRX2qIuYXvKg/S0PGo/Jwke6GnuzVisoqfLILQEQi+CmvMHT6Sm2K2L59bgrt4OZQbHKXD" +
                "3M/FyXq2RlZ+jVW08WRdo8anpbkVsDEcQZWgdDXfbXDUa0IRUJCx+J6uwPz70fqSASi05xp5m6QBe0wA" +
                "qJct6fL6HXKjvabMubzCL4hvNf4FNp9xW083PTRLrXudOgwQgWMt+xgQuj3OID5FyUYwW+V6dC1rKeku" +
                "7tuMEYSsWRG8WbX4CAECHaL1Tq029FmN5s0PcuNfF2K21oksaV+mFHAotVFe/ETQ8tBHCDI30daFDqxU" +
                "m2EUTTQDPc16z5bESDifikHkuoc1Dr1kikZoVLSZFr6QYTTCwJHdMHVxzUFQ+gxNW8F+gAJ5qcZQrjF6" +
                "P98T/xheNcF4QQ+ylLc5004kbNn/ALOADJhySoYdVOVOZhFIR/FxF/3S4ImBrk7obUGWAJAaJjUwI2wd" +
                "olav+jXlPkg6m6ol2ZwVvD3/fTm3bKOETtTtUuF2qhzipO43wkW4iREFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
