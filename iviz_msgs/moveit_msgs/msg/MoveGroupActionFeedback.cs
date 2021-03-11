/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = "moveit_msgs/MoveGroupActionFeedback")]
    public sealed class MoveGroupActionFeedback : IDeserializable<MoveGroupActionFeedback>, IActionFeedback<MoveGroupFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public MoveGroupFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MoveGroupActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new MoveGroupFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MoveGroupActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, MoveGroupFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MoveGroupActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new MoveGroupFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionFeedback(ref b);
        }
        
        MoveGroupActionFeedback IDeserializable<MoveGroupActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new MoveGroupActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                size += Feedback.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "moveit_msgs/MoveGroupActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W23LbNhB954z+ATN6iN2p3TbpJfWMHlRZcZSxE4+t9tUDEisSLQmyAChZf9+z4EVU" +
                "LU30kEQjWzfg7MGes4t9T1KRFVl4iWTidWlyHT8VLnU/3JQyf/TS10648BLdlWu6sWVdvSNSsUz+Eav2" +
                "zSiafOHHKLp7vLlCZNWweR84jqKxACejpFWiIC+V9FKsSpxBpxnZi5zWlDPfoiIlwq9+W5G7xMZlpp3A" +
                "MyVDVub5VtQOi3wpkrIoaqMT6Ul4XdDefuzURkhRSet1UufSYn1plTa8fGVlQYyOp6N/azIJicX1FdYY" +
                "R0ntNQhtgZBYkk6bFD+KqNbGv3nNG6LxclNe4COlUKIPLnwmPZOl58qSY57SXSHGd83hLoGN7BCiKCfO" +
                "wndP+OjOBYKAAlVlkokzML/f+qw0ACSxllbLOCcGTpABoL7iTa/OB8hM+0oYacoOvkHcxTgF1vS4fKaL" +
                "DJrlfHpXp0ggFla2XGuFpfE2gCS5JuMF7Gel3Ua8qwkZjd9xjrEIu4IieJXOlYmGAEpstM8i5y2jBzWe" +
                "tIq+miGPFsko4vcQN8ULU2CN33al03y4n3+8Xny8Ed1jIn7Ef3YmhW0ik05sybMnY+IUJY32bY6a4JDd" +
                "rlGvDeZ0tlz8NRcDzJ/2MVmU2lokFz6MidN0EvD9w3x+d7+cX/fAr/eBLSUEd8OZUB0O4W9QAM4LufIw" +
                "s/Z8essa0XMoBZNGO6IvH2P8wSchC43nUJhVToygvetQQPRsSbZAAebcDTydt5Qf/5zN5vPrAeU3+5Q3" +
                "QJZJptElFKyYcBZWNbeCQ4k4Fmb6x6eHXV44zM8HwsRlOLqqgzN33A9GUjV9NjXsCleiElZS57WlY/Qe" +
                "5h/mswG/ifjlJT1Lf1PC/A7S4Zoqa/9/u3z/eY4xJRJtNWD2wWq0Si/BlJsEmrU2a5lrdewArfP6SpmI" +
                "X7+B83rrmdKHItyZrxevz/Bsenu7q+SJ+O1UgjHhtqKDDE/JLjR5qdY+abPStuB7jW+QXobQmpkJqb1D" +
                "DG3y9gsc4rQ0syn2yq8JwDfHEU/cfnpcDqEm4vcAODVdMtoLBEhCQTUGoSYJsk8Bo1w2g4CDwXMV8haf" +
                "UHuOsTH/4I5GfjYax0flINZ+64zG0zwvN2Ek4YUoBbwpd/cVyLR3FdeYGAxZvEVRXKcpp7Fd5OnZR9/0" +
                "Nltc85DFJmgGkTZPzrPifKRwMyOrm0xjwgi38qCrBIOQ4oloEQaYMGMdSBX2k2EL4aDkOEcYdKioIFee" +
                "Yzdjuka/DSF0D925D64ky10lMBoODC1/NJh2yEA3Bj00uqEQ3ezKhsQOTFl17jFUOidTVhjquIoSvdJJ" +
                "Vw+BgWMDMTpPfM0CkCrqUBdodRqrLjv9sOrrqVfAj9o30r2YzUdR1JHgGYRG0X8cRfbW7gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
