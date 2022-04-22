/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ObjectRecognitionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectRecognitionActionGoal : IDeserializable<ObjectRecognitionActionGoal>, IActionGoal<ObjectRecognitionGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ObjectRecognitionGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public ObjectRecognitionActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ObjectRecognitionGoal();
        }
        
        /// Explicit constructor.
        public ObjectRecognitionActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ObjectRecognitionGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        public ObjectRecognitionActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ObjectRecognitionGoal(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectRecognitionActionGoal(ref b);
        
        public ObjectRecognitionActionGoal RosDeserialize(ref ReadBuffer b) => new ObjectRecognitionActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "object_recognition_msgs/ObjectRecognitionActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "195eff91387a5f42dbd13be53431366b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUwWrcMBC9+ysGckhSyBbaW6C30GQPpaXJrZRlLI1ttbLkauTd+u/7JG82KeTQQ7MY" +
                "hFYzb97Me9KdsJVEQ10aNtnF4F27G7XXt7eR/faGeiw7Z5vP7Q8x+auY2AdXAst5PW0+/Odf8+n+9po0" +
                "25XI3UrvjO4zB8vJ0iiZLWemLoK96wdJV1724pHE4ySW6mleJtENEh8Gp4SvlyCJvV9oVgTlSCaO4xyc" +
                "4SyU3Sh/5SPTBWKaOGVnZs8J8TFZF0p4l3iUgo5P5dcswQhtb64RE1TMnB0ILUAwSVhd6HFIzexCfv+u" +
                "JDRnD4d4ha300OBUnPLAuZCV31MSLTxZr1HjzdrcBtgYDnQIVumi/rfDVi8JRUBBpmgGugDzL0seYgCg" +
                "0J6T49ZLATaYAFDPS9L55TPkUKEDh/gIvyI+1fgX2HDCLT1dDdDMl+517jFABE4p7p1FaLtUEOOdhEww" +
                "XuK0NCVrLdmcfSwzRhCyqiJYWTUaBwEsHVweGs2poFc1ik9fyY0vXo5qrSNZ0iHO3mITk9S+aiPQ8jA4" +
                "CFKbKNeFDqyUimEUTRQDbave1ZIYCYdjMYic9rDGYZBALhMaFS2mhS9knDJh4MgumLq65iAofYKmVrrC" +
                "hclIygzlCqPn8z3yd/ZRE4wX9JZS5DRn6kRsy+YnmFlkwJSzz7iDqtxLFYF0EuM6Z9YGjwx0c0QvF2QN" +
                "AKlx1gxmhFuHqM2jfkW5V5Iu1pdrl56erlXDF1+0pmlj9EWMXYqu6Xxk3Ndv36lzPkvaeTe6rM0f4Pin" +
                "EToFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
