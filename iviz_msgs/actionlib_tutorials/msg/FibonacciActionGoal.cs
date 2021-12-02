/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class FibonacciActionGoal : IDeserializable<FibonacciActionGoal>, IActionGoal<FibonacciGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public FibonacciGoal Goal { get; set; }
    
        /// Constructor for empty message.
        public FibonacciActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new FibonacciGoal();
        }
        
        /// Explicit constructor.
        public FibonacciActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, FibonacciGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        /// Constructor with buffer.
        internal FibonacciActionGoal(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new FibonacciGoal(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new FibonacciActionGoal(ref b);
        
        FibonacciActionGoal IDeserializable<FibonacciActionGoal>.RosDeserialize(ref Buffer b) => new FibonacciActionGoal(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) throw new System.NullReferenceException(nameof(GoalId));
            GoalId.RosValidate();
            if (Goal is null) throw new System.NullReferenceException(nameof(Goal));
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + GoalId.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "actionlib_tutorials/FibonacciActionGoal";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "006871c7fa1d0e3d5fe2226bf17b2a94";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUwWrbQBC96ysGckhSsAPtzdBbSOJDoZDczXh3LA2RdtWdlVX9fd9KtpNADz00QiBW" +
                "mnnzZt4bPQl7SdTMj4pd1hha3e86q+3uMXK7vacaj5366kH3MbBzWt7Pb6vv//mqfjw/bsiyXwg8LbSu" +
                "6Dlz8Jw8dZLZc2Y6RLDWupG0auUoLZK468XT/DVPvdgaiS+NGuGuJUjitp1oMATlSC523RDUcRbK2smH" +
                "fGRqIKaeU1Y3tJwQH5PXUMIPiTsp6LhNfg0SnND2foOYYOKGrCA0AcElYdNQ4yNVg4b87WtJqK5exrjC" +
                "UWrM/lKccsO5kJXffRIrPNk2qPFlaW4NbAxHUMUb3czvdjjaLaEIKEgfXUM3YP5zyk0MABQ6clLet1KA" +
                "HSYA1OuSdH37DrnQ3lDgEM/wC+JbjX+BDRfc0tOqgWZt6d6GGgNEYJ/iUT1C99MM4lqVkAmGS5ymqmQt" +
                "JaurhzJjBCFrVgRPNotOIYCnUXNTWU4FfVaj+POT3PjXpZitdSJL1sSh9TjEVCgvfiJoOTYKQeYmyrrQ" +
                "yEapGMbQRDHQdtZ7tiRGwuFUDCKnI6wxNhJIM6FRsWJa+EK6PhMGjuyCaYtrRkHpCzTtBfsBCuQkZYZy" +
                "hdH7+Z74qz9rYggeGbLEtznTQcTv2b2CmUcGTDm0GTtoxrXMIpD14vSgbmnwxMDWJ/SyIEsASHWDZTAj" +
                "bB2i1mf9inKfLl0eII5iXHcf/mJVtSwlVhv/mT+gtZZWDQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
