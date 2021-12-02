/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ShapeActionFeedback : IDeserializable<ShapeActionFeedback>, IActionFeedback<ShapeFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ShapeFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public ShapeActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ShapeFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public ShapeActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        internal ShapeActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ShapeActionFeedback(ref b);
        
        ShapeActionFeedback IDeserializable<ShapeActionFeedback>.RosDeserialize(ref Buffer b) => new ShapeActionFeedback(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTXPbNhC981dgRofYndhpkzZNPaODKimOOk7isdRePSCxItGCoAqAkvXv+xYUKaqW" +
                "Jjok0cjWF/D24e3bxX4gqciJIr4kMgu6skanj6XP/avbSpp5kKH2wseXZF7IFb0nUqnM/hHL3Ztk+JUf" +
                "ycf57Q1iqobHh4bdQICMVdIpUVKQSgYplhXI67wgd2VoTYaJlitSIv4ativy19i4KLQXeOZkyUljtqL2" +
                "WBQqkVVlWVudyUAi6JIO9mOntkKKlXRBZ7WRDusrp7Tl5UsnS2J0PD39W5PNSMwmN1hjPWV10CC0BULm" +
                "SHptc/woklrb8OY1b0gGi011hY+UIwVdcBEKGZgsPa0ceeYp/Q1i/NAc7hrYEIcQRXlxEb97xEd/KRAE" +
                "FGhVZYW4APP7bSgqC0ASa+m0TA0xcAYFgPqCN7247CEz7Rthpa1a+AZxH+McWNvh8pmuCuTM8Ol9nUNA" +
                "LFy5aq0VlqbbCJIZTTYI+M5Jt014VxMyGbxnjbEIu2JG8Cq9rzKNBCix0aFIfHCMHrPxqFXyjdx4sjYS" +
                "fovM5njh+Jzgd23BNB/up58ms0+3on0MxY/4z7akuE0U0ostBTZkSqxP1iR+J1ATGzl3a9RBgzkaL2Z/" +
                "TUUP86dDTM5I7RyUhQlTYo3OAr5/mE4/3i+mkw749SGwo4xgbdgSKYc9+Bu43wchlwFO1oFP7zhB9BTr" +
                "wObJnujzxwB/MElUoTEcqnJliBF08C0KiF4syJWoPsOtINDljvL8z/F4Op30KL85pLwBsswKjRah4MOM" +
                "VVjW3AeOCXEqzOj3zw97XTjMz0fCpFU8uqqjLffcj0ZSNX1RGnaFr1AGS6lN7egUvYfpH9Nxj99Q/PKc" +
                "nqO/KWN+R+lwQVV1+L9dXn6ZY0qZRE+NmF2wGn0ySDDlDoFOre1aGq1OHWDnvK5ShuLtd3BeZz1bhViE" +
                "e/N1yesUHo/u7vaVPBS/nkswJVxVdJThOeoiJ8+zdUjaLrUr+VLj66NLQ+zLzITUwSH6Nnn3FQ5xnsxs" +
                "ioPyawLwtXHCE3ef54s+1FD8FgFHthVjd3sASShkjUGoEUF2EjDKdTMFeBjcqKhbekbtecauWG2WdKNx" +
                "fFQOYh22zmQwMqbaxHmEF6IU8KbaX1Ygs7uouMZEb7TiLYrSOs9Zxt2iQE8h+Y5X2WwSp6TdvduK5AOn" +
                "m88T72RIuik0Zot4H/daSnQHKZ6FZnF0idPVEZ2wnyz7B6ckzwJhxKFyhVwZg92M6ZvkbQihO+jWerAk" +
                "OW4pkVF/VNjx16odL9CKQQ9drp+FdmRlN2IH5qvaBIyT3suc04vU+BVleqmzthgiA8/uYXSe9ZoFIFXW" +
                "sSjQ5zRWXbfJ4yHkG6Uu1C4Yeuwy+OpgIkfY/wCby3R90QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
