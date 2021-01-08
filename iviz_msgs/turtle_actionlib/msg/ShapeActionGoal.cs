/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionGoal")]
    public sealed class ShapeActionGoal : IDeserializable<ShapeActionGoal>, IActionGoal<ShapeGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ShapeGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ShapeGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ShapeGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ShapeGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionGoal(ref b);
        }
        
        ShapeActionGoal IDeserializable<ShapeActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionGoal(ref b);
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
                int size = 8;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWobMRC9L+w/DOSQpGAH2puht9DEh0LBuZuxNN4V0Upbjdau/75PWttpoIceGmMj" +
                "tDvz5s17M34WtpKor0fDJrsYvNttB+304SmyXz9Sh2PrbLPpeZTyrD5pm6//+dM23zdPK9Js5/LPlVTb" +
                "3NAmc7CcLA2S2XJm2keQdl0vaeHlIB5ZPIxiqb7Np1F0WTJfeqeEbydBEnt/okkRlSOZOAxTcIazUHaD" +
                "vAMoqS4Q08gpOzN5TkiIybpQ4veJB6n45afyc5JghNaPK0QFFTNlB1InYJgkrC50eIngyYX85XPJQOLL" +
                "MS5wlw4GXBlQ7jkXxvJrTKKFLOuqlPk097gEPEQSFLJKd/XZFle9J9QBCxmj6ekO9H+cch8DEIUOnBzv" +
                "vBRkAx0Ae1uSbu//hC7UVxQ4xAv+DPlW5F9wwxtwaWvRwzxfJNCpg46IHFM8OIvY3amiGO8kZMLgJU6n" +
                "tilpc1GAfCtiIwx51RucrBqNgxOWji73baM5lQLVF4xq23zYdP51ReZJO1Mm7ePkLS4xFd7zeBFcPfYO" +
                "ztROygbRkZVSGR5FJ3Wc1tX6OqKQhsO5HOxOB0zJsZdALhO6FS1DjBGRYcwE5Ut6QdV5go6C4ldw2gk2" +
                "BiTISMoMDwun90JfmnD24g+EBkdYFN8Up72I3bF5BTvofIMaOvmMzVTlTqofpKMYt3dmbvPMQpdn+Loz" +
                "cwSYDZNm0CPsIsKgwtnL2cUP8zFPKXvZXu18uP67tU0zb6nYTrTZ+8jllti6SdvmN9By33IyBQAA";
                
    }
}
