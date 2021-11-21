/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/PlaceLocation")]
    public sealed class PlaceLocation : IDeserializable<PlaceLocation>, IMessage
    {
        // A name for this grasp
        [DataMember (Name = "id")] public string Id;
        // The internal posture of the hand for the grasp
        // positions and efforts are used
        [DataMember (Name = "post_place_posture")] public TrajectoryMsgs.JointTrajectory PostPlacePosture;
        // The position of the end-effector for the grasp relative to a reference frame 
        // (that is always specified elsewhere, not in this message)
        [DataMember (Name = "place_pose")] public GeometryMsgs.PoseStamped PlacePose;
        // The approach motion
        [DataMember (Name = "pre_place_approach")] public GripperTranslation PrePlaceApproach;
        // The retreat motion
        [DataMember (Name = "post_place_retreat")] public GripperTranslation PostPlaceRetreat;
        // an optional list of obstacles that we have semantic information about
        // and that can be touched/pushed/moved in the course of grasping
        [DataMember (Name = "allowed_touch_objects")] public string[] AllowedTouchObjects;
    
        /// <summary> Constructor for empty message. </summary>
        public PlaceLocation()
        {
            Id = string.Empty;
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory();
            PlacePose = new GeometryMsgs.PoseStamped();
            PrePlaceApproach = new GripperTranslation();
            PostPlaceRetreat = new GripperTranslation();
            AllowedTouchObjects = System.Array.Empty<string>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PlaceLocation(string Id, TrajectoryMsgs.JointTrajectory PostPlacePosture, GeometryMsgs.PoseStamped PlacePose, GripperTranslation PrePlaceApproach, GripperTranslation PostPlaceRetreat, string[] AllowedTouchObjects)
        {
            this.Id = Id;
            this.PostPlacePosture = PostPlacePosture;
            this.PlacePose = PlacePose;
            this.PrePlaceApproach = PrePlaceApproach;
            this.PostPlaceRetreat = PostPlaceRetreat;
            this.AllowedTouchObjects = AllowedTouchObjects;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PlaceLocation(ref Buffer b)
        {
            Id = b.DeserializeString();
            PostPlacePosture = new TrajectoryMsgs.JointTrajectory(ref b);
            PlacePose = new GeometryMsgs.PoseStamped(ref b);
            PrePlaceApproach = new GripperTranslation(ref b);
            PostPlaceRetreat = new GripperTranslation(ref b);
            AllowedTouchObjects = b.DeserializeStringArray();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PlaceLocation(ref b);
        }
        
        PlaceLocation IDeserializable<PlaceLocation>.RosDeserialize(ref Buffer b)
        {
            return new PlaceLocation(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            PostPlacePosture.RosSerialize(ref b);
            PlacePose.RosSerialize(ref b);
            PrePlaceApproach.RosSerialize(ref b);
            PostPlaceRetreat.RosSerialize(ref b);
            b.SerializeArray(AllowedTouchObjects, 0);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (PostPlacePosture is null) throw new System.NullReferenceException(nameof(PostPlacePosture));
            PostPlacePosture.RosValidate();
            if (PlacePose is null) throw new System.NullReferenceException(nameof(PlacePose));
            PlacePose.RosValidate();
            if (PrePlaceApproach is null) throw new System.NullReferenceException(nameof(PrePlaceApproach));
            PrePlaceApproach.RosValidate();
            if (PostPlaceRetreat is null) throw new System.NullReferenceException(nameof(PostPlaceRetreat));
            PostPlaceRetreat.RosValidate();
            if (AllowedTouchObjects is null) throw new System.NullReferenceException(nameof(AllowedTouchObjects));
            for (int i = 0; i < AllowedTouchObjects.Length; i++)
            {
                if (AllowedTouchObjects[i] is null) throw new System.NullReferenceException($"{nameof(AllowedTouchObjects)}[{i}]");
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.GetStringSize(Id);
                size += PostPlacePosture.RosMessageLength;
                size += PlacePose.RosMessageLength;
                size += PrePlaceApproach.RosMessageLength;
                size += PostPlaceRetreat.RosMessageLength;
                size += BuiltIns.GetArraySize(AllowedTouchObjects);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceLocation";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "f3dbcaca40fb29ede2af78b3e1831128";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVX32/bNhB+F5D/gYAfmgyOC7TDHgLsIUC3LgMGZGjQlyIwaPFks5FIlaTseH/9vjtK" +
                "sux2y4AtWRDAknh3vO9+fDzO1LVyuiFV+aDSxka1Djq2RUzBurWypihm6m5DyrpEwelatT6mLpDyFeRJ" +
                "bbQzvTL1ujOWscl6FxWvUoX1hGdodZFMkYL+TGXyYb9s4jq+/tXD+t34UbZYtrUuadnvNngxGB52J2cu" +
                "YV70jr1QgWqd7JZU8krjraJArgTQwHBh7zxtdFJArOud3kcVWyptZQkO15F2G8jPlfMQcTkyDcWo13RR" +
                "rMk3lAbvb32kD0k3LTRHp0ePddsGr8uNajw7XrwPtm0pAK2L7CCwtAEqojgID8oBuxCc/BvdQ6h6YdbV" +
                "CFDL68hXbWPicPlVTLqsKQIMTO44dYhOpEa7ZEugRPyabFWvfJfEjsnSJSyuOJRduSHzuu0i/zR+C8wS" +
                "HlKl70KUspD4o3r6Ivp0jwjXfkdmKfpLv+JEx+Ks+PE//jsrfvvw/ko9UV9nxS+kDQW1kZ+Dm59Zbsnt" +
                "EIsTnVt+hUjLv8/oekwm+5xdPEMSUFvO6GBQf0kbnbQU+sauUaGXNW2phlYuP1lN+5biQioIRcsdTY4C" +
                "UrCX7uN+KH3TdM6WOiGnFnCn+tBESrVqdUBddLUOkPfBWMfi0j5sHf+RvnTSUzfvriDjIpUdtxx2sq5E" +
                "MUbmkJt3qugQtbdvWKGY3e38JbPJGhkYN89lBmfpEe0Q2U8dr7DHdxncArYRHcIuJqpz+bbEa7xQ2AQu" +
                "UOvRZOfw/HafNj7X5FYHq1c12CuihOsaVl+x0quLiWV2+wok6PxgPls87PFPzLrRLmO6ZFqsGX3s1ggg" +
                "BNHZW2sgutrnfqktuYT2XAUd9gVr5S2L2c9CUcI5khHmqBh9aZEAo3Y2bQZ+lmwswdL/Vy9JX3CV/sQc" +
                "dxDOjTJyKvIKr5Hw8WT4NFcoFGBKWMWLLkuqUaayeH8Pi/5YOp8i9yPLT/ZCRYOc6JHLjwwX53VdT/h8" +
                "q+uO8vnTk1XkAKOm4ZFmQqTc+3IUomecOoG5APaiqGqv0w/fCw30jk2+HeBMPh7BmnzPaArT5SUpmmUV" +
                "fLNEDWDh2fL5l2cXJ/Fa8QepsMmJedr9+VgYOveETMWAHIAvCCH7HoipA02VQ4qTiP3gbFaBkPMWp+Sc" +
                "uY8/m349jxOMyAc76C4UcHA9DALF7x3gByd2D3Ivh7HvMqF0kFTS1uW6nU5E4Gzx+gjxUHTqcXzaj09/" +
                "vBSCQ/xGGGO6eEqcRvXYf377cog+DymL4glQw9Pu+eDx7GNTBvf1XMYgDVXWMe0wVY0D22FIFZ15PpOB" +
                "sbXlQ9cK6/E4p5KODxFmWIEe0Wg4b/i5tlVi6ueYySCFzHPqk5xIEBiGSBYSopSFqjfMcxkfUlgwNkB9" +
                "Mk1P/DyZcT8KDb4dxtxRczRFEZ/MEVKDwVODP3I2cPr3QstxoVdugH74lueApsPMCkrnmQJnJluueJpg" +
                "ICsCFJYjqOdRn8eGLaGLU6KmxREwbgnTh+1eqNSPY/XNcle9zL9i2sHGVn5fGN23YWVXjvsXXDpTNzLb" +
                "eYfJsCHcN/jIHjWhOBbUgu89QVI8VzYp49FBuITBRqMf+LqCQUvxla5tYey4t/AZKue0WC/mChc4VARL" +
                "SbewFzII46oT7Nr2tXq488g1sUc3V6l6g+RgihCf82a55ILPLHWxUDeV2vtO7RgQHkI/f8s0MvglzZe8" +
                "l0YfqvZrch8ul4hdTMjvUxR3VvwJef8tXroPAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
