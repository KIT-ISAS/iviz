/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = "actionlib_tutorials/AveragingActionGoal")]
    public sealed class AveragingActionGoal : IDeserializable<AveragingActionGoal>, IActionGoal<AveragingGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public AveragingGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public AveragingActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new AveragingGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public AveragingActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, AveragingGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public AveragingActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new AveragingGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new AveragingActionGoal(ref b);
        }
        
        AveragingActionGoal IDeserializable<AveragingActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new AveragingActionGoal(ref b);
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
        [Preserve] public const string RosMessageType = "actionlib_tutorials/AveragingActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWobMRC9L+w/DOSQpGAH2puhh0Jo4kOhkNzNWBrvimqlrUZr13/fJ60d19BDD22N" +
                "jdDuzJs37834WdhKor4eDZvsYvBuuxm004enyH79SB2OjbPNp70k7lzoyvP6tG0+/uVP23x5eVqRZjtT" +
                "eK7E2uaGXjIHy8nSIJktZ6ZdBHHX9ZIWXvbikcXDKJbq23wcRZcl87V3Svh2EtCA90eaFFE5konDMAVn" +
                "OAtlN8gVQEl1gZhGTtmZyXNCQkzWhRK/SzxIxS8/le+TBCO0flwhKqiYKTuQOgLDJGGFbniJ4MmF/OF9" +
                "yUDi6yEucJcOJrwxoNxzLozlx5hEC1nWVSnzbu5xCXiIJChkle7qsw2uek+oAxYyRtPTHeh/PeY+BiAK" +
                "7Tk53nopyAY6APa2JN3e/wpdqK8ocIhn/BnyUuRPcMMFuLS16GGeLxLo1EFHRI4p7p1F7PZYUYx3EjJh" +
                "+BKnY9uUtLkoQD4XsRGGvOoNTlaNxsEJSweX+7bRnEqB6gvGtW3+2XT+dk3mSTtRJu3j5C0uMRXe83gR" +
                "XD30Ds7UTsoG0YGVUhkeRSd1nNbV+jqikIbDqRzsTlhAAEgglwndipYhxojIMGaC8iW9oOo8QQdB8Tdw" +
                "2go2BiTISMoMDwuna6HPTTh79gdCgyMsihfFaSdit2y+gR10vkENnXzGZqpyJ9UP0lGM2zkzt3liocsT" +
                "fN2ZOQLMhkkz6BF2EWFQ4eTl7OJ/8DFPMMpBuIerP7m2aeZlFduJNjsfudwSWzdp2/wEvkCPlj0FAAA=";
                
    }
}
