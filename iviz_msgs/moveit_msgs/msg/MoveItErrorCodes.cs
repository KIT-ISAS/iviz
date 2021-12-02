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
        internal MoveItErrorCodes(ref Buffer b)
        {
            Val = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MoveItErrorCodes(ref b);
        
        MoveItErrorCodes IDeserializable<MoveItErrorCodes>.RosDeserialize(ref Buffer b) => new MoveItErrorCodes(ref b);
    
        public void RosSerialize(ref Buffer b)
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
                "H4sIAAAAAAAACn2ST4vbMBDF7/4UhsDeDE2yf9qDD4qjZNXIM1lJDuxpcItpTbNOa6eBfvuOghS8ptQH" +
                "G+v9NBq9eW13Xi7SS31Mkll6ujR9fTymX5rv9aU99Ul7VW1VFNLafB7+N0Lpysj8k3+SsLjXAkDBlrwq" +
                "13kWaQUHodWaSnQKgTyXZ4sgjhYpgMLJNa1eScJBGYRSgqPiWcBW5tkybCsQnEF9O+s+rFcgVlqSQxIv" +
                "lTKSrASLhrioyLOHQDlV8hFYuTx7jN0bKcs9n5xnT96Jn8e669ruW3qX/mi75q0+t1+HtG9+/W6Gc9r0" +
                "/akfojtOGEf8dpKvQAVqrSxfih348A/koFDz19JeuGemwTojFDjL/DyauUWhp8UWY+1/VZZjcCTFTX42" +
                "98lkOluD1Z5AlOzy/GEqTiox8jhBDK4wXDHP5k8TVSvYxeIfJxquPsvCRZXzNEuHP8O5eXtv88YwQNwA" +
                "2A2a8jp6H8JsEYN2M4vjIoudzyLn4cCcDwWD0cFRr/591aJpITAKNnjT2KzZOAbv+gIktSOLuvJJ5ojy" +
                "EP8C5qgPc1UDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
