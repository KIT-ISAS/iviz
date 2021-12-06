/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MoveItErrorCodes : IDeserializable<MoveItErrorCodes>, IMessage
    {
        [DataMember (Name = "val")] public int Val;
        // overall behavior
        public const int SUCCESS = 1;
        public const int FAILURE = 99999;
        public const int PLANNING_FAILED = -1;
        public const int INVALID_MOTION_PLAN = -2;
        public const int MOTION_PLAN_INVALIDATED_BY_ENVIRONMENT_CHANGE = -3;
        public const int CONTROL_FAILED = -4;
        public const int UNABLE_TO_AQUIRE_SENSOR_DATA = -5;
        public const int TIMED_OUT = -6;
        public const int PREEMPTED = -7;
        // planning & kinematics request errors
        public const int START_STATE_IN_COLLISION = -10;
        public const int START_STATE_VIOLATES_PATH_CONSTRAINTS = -11;
        public const int GOAL_IN_COLLISION = -12;
        public const int GOAL_VIOLATES_PATH_CONSTRAINTS = -13;
        public const int GOAL_CONSTRAINTS_VIOLATED = -14;
        public const int INVALID_GROUP_NAME = -15;
        public const int INVALID_GOAL_CONSTRAINTS = -16;
        public const int INVALID_ROBOT_STATE = -17;
        public const int INVALID_LINK_NAME = -18;
        public const int INVALID_OBJECT_NAME = -19;
        // system errors
        public const int FRAME_TRANSFORM_FAILURE = -21;
        public const int COLLISION_CHECKING_UNAVAILABLE = -22;
        public const int ROBOT_STATE_STALE = -23;
        public const int SENSOR_INFO_STALE = -24;
        // kinematics errors
        public const int NO_IK_SOLUTION = -31;
    
        /// Constructor for empty message.
        public MoveItErrorCodes()
        {
        }
        
        /// Explicit constructor.
        public MoveItErrorCodes(int Val)
        {
            this.Val = Val;
        }
        
        /// Constructor with buffer.
        internal MoveItErrorCodes(ref ReadBuffer b)
        {
            Val = b.Deserialize<int>();
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MoveItErrorCodes(ref b);
        
        public MoveItErrorCodes RosDeserialize(ref ReadBuffer b) => new MoveItErrorCodes(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Val);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 4;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveItErrorCodes";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aa336b18d80531f66439810112c0a43e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE32ST4vbMBDF7/4UhsLeDM2f3e0efFAcJatGnslKcmBPg1tMa5p1WjsN9Nt3FKTgNaU+" +
                "yFjvp/HozWu782KeXupjknxIT5emr4/H9Evzvb60pz5pr6qtikJam8/C90YoXRmZP/knCZt7LQAUbMmr" +
                "cp1nkVZwEFqtqUSnEMhzeTYP4miTAiicXNPqlSQclEEoJTgqngVsZZ4twrECwRnUt38tw34FYqUlOSTx" +
                "UikjyUqwaIiLijy7D5RTJf8CK5dnD7F7I2W5d77Wo3fi57Huurb7lt6lP9queavP7dch7Ztfv5vhnDZ9" +
                "f+qH6I4TxhGvTvIVqECtleVLsQMf/4EcFGp+W9oL98w0WGeEAmeZn0Uztyj0tNh8rP2vymIMjqR4yM9m" +
                "mUymszVY7QlEyS7P7qfipBIjDxPE4ArDFVl9nKhawS4W/zTRcPVZFi6qT9794c9wbt7e27wxDBA3AHaD" +
                "pqQYwmw+u4UimMVxkcXOZ5HzcGDOh4LB6OCoV79etWhaCIyCDd60pe9pFIN3fQGS2pFFXbnrnBY8xL/m" +
                "qA9zVQMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
