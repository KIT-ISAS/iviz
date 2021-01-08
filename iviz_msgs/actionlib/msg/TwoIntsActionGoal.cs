/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionGoal")]
    public sealed class TwoIntsActionGoal : IDeserializable<TwoIntsActionGoal>, IActionGoal<TwoIntsGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TwoIntsGoal Goal { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionGoal()
        {
            Header = new StdMsgs.Header();
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TwoIntsGoal();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionGoal(StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TwoIntsGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TwoIntsGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionGoal(ref b);
        }
        
        TwoIntsActionGoal IDeserializable<TwoIntsActionGoal>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionGoal(ref b);
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
                int size = 16;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "684a2db55d6ffb8046fb9d6764ce0860";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWrbQBC9C/QPAzkkKdiBtvRg6C008aFQSO5mtDuWlkq76s7Kqv++b1d2nEAPPTTG" +
                "Zllp5s2b92b8KGwlUleOik1ywfeu2Q3a6t1D4H57Ty2OnbPV8xy2Pml+Wp7V1df//Kmr708PG9JkFwKP" +
                "hVZdXdFTYm85WhokseXEtA+g7dpO4qqXg/TI4mEUS+VtOo6i65z53DklfFvxErnvjzQpolIgE4Zh8s5w" +
                "EkpukDcAOdV5Yho5JmemniMSQrTO5/h95EEKfv6p/JrEG6Ht/QZRXsVMyYHUERgmCqvzLV4ieHI+ffqY" +
                "M5AIRVe4SwsLXhhQ6jhlxvJ7jKKZLOsml/mw9LgGPEQSFLJKN+XZDle9JdQBCxmD6egG9H8cUxc8EIUO" +
                "HB03vWRkAx0Ae52Trm9fQ2fqG/Lswxl/gbwU+RdcfwHOba06mNdnCXRqoSMixxgOziK2ORYU0zvxiTB6" +
                "keOxrnLaUhQg37LYCENe8QYnqwbj4ISl2aWurjTFXKD4gmGtq3ebzr8uyTJpJ8qkXZh6i0uImfcyXgRX" +
                "587BmdJJ3iCaWSnm4VF0UsZpW6wvIwpp2J/Kwe54wJTMnXhyidCtaB5ijIgMYyIon9Mzqi4TNAuKv4BT" +
                "I9gYkCAjMTE8zJzeCn1uwtmzP4r4mWFRuChOexHbsPkJdtD5CjV06hM2U5VbKX6QjmLc3pmlzRMLXZ/g" +
                "y84sEWA2TJpAj7CLCIMKJy8XF9/fx7tXf2x1VWEhv3wmPp1NXf0BUQAJ8CQFAAA=";
                
    }
}
