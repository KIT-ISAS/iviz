/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/FibonacciActionGoal")]
    public sealed class FibonacciActionGoal : IDeserializable<FibonacciActionGoal>, IActionGoal<FibonacciGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public FibonacciGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public FibonacciActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new FibonacciGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public FibonacciActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, FibonacciGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public FibonacciActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new FibonacciGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionGoal(ref b);
        }
        
        FibonacciActionGoal IDeserializable<FibonacciActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new FibonacciActionGoal(ref b);
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
                int size = 4;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "006871c7fa1d0e3d5fe2226bf17b2a94";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9C/QfBnJIUnAC7c3QW8jHoVBI7ma8O5aGSrvqzsqu/33fruykgR56aGtk" +
                "Fkkzb968N6NHYS+J+no07LLGMOh2M1pntw+Rh6c76nBs1Df3uo2BndPyvD5tm89/+dc2X54f1mTZLxQe" +
                "K7G2uaDnzMFz8jRKZs+ZaRdBXLte0mqQvQzI4nEST/VtPk5iNyXzpVcjXJ0ESTwMR5oNUTmSi+M4B3Wc" +
                "hbKO8g6gpGogpolTVjcPnJAQk9dQ4neJR6n45W/yfZbghJ7u1ogKJm7OClJHYLgkbBo6vETwrCF/+lgy" +
                "kPhyiCvcSwcTXhlQ7jkXxvJjSmKFLNu6lPmw9HgDeIgkKOSNruqzDW7tmlAHLGSKrqcr0P96zH0MQBTa" +
                "c1LeDlKQHXQA7GVJurz+FbpQX1PgEM/4C+RbkT/BDW/Apa1VD/OGIoHNHXRE5JTiXj1it8eK4gaVkAnD" +
                "lzgd26akLUUBcl/ERhjyqjc42Sw6hROeDpr7trGcSoHqC8a1bf7ZdP52TZZJO1Em6+M8eNzEVHgv40Vw" +
                "9dArnKmdlA2iAxulMjyGTuo4PVXr64hCGg6ncrA77TElh14CaSZ0K1aGGCMi45QJypf0gmrLBB0ExV/B" +
                "aSvYGJAgJykzPCyc3gt9bkL92R8IDY6wKL4pTjsRv2X3Deyg8wVq2DxkbKYZd1L9IJvE6U7d0uaJhd2c" +
                "4OvOLBFgNs6WQY+wiwiDCicvFxf/g495hlEK4W7ffeTaplmWFVtfvkI/AaJNA5ouBQAA";
                
    }
}
