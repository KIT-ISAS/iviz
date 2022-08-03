/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsActionFeedback : IDeserializable<TwoIntsActionFeedback>, IMessage, IActionFeedback<TwoIntsFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TwoIntsFeedback Feedback { get; set; }
    
        public TwoIntsActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public TwoIntsActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public TwoIntsActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public TwoIntsActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TwoIntsFeedback.Singleton;
        }
        
        public TwoIntsActionFeedback RosDeserialize(ref ReadBuffer b) => new TwoIntsActionFeedback(ref b);
        
        public TwoIntsActionFeedback RosDeserialize(ref ReadBuffer2 b) => new TwoIntsActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Feedback is null) BuiltIns.ThrowNullReference();
            Feedback.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Status.AddRos2MessageLength(ref c);
            Feedback.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "actionlib/TwoIntsActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVppkn6kntFBlRRHGSfx2GovnY4HBFYkWhJUAVCy/n3fghRF" +
                "OVajQxKNbH0Bbx/evl3sW5KanMjjSyJVMJUtTHpf+sw/v6pkcRdkqL3w8SVZbKq5Df4NkU6l+kcs2zfJ" +
                "6As/kvd3V5eIqhsmbxt+AwE6VkunRUlBahmkWFagb7Kc3EVBayqYarkiLeKvYbsiP8TGRW68wDMjS04W" +
                "xVbUHotCJVRVlrU1SgYSwZR0sB87jRVSrKQLRtWFdFhfOW0sL186WRKj4+np35qsIjGfXmKN9aTqYEBo" +
                "CwTlSHpjM/woktrY8OolbxAD8edt5V/8lQyg7AW+pwzZ6FiIkMvArOlh5cgzYekvEey75pRDBIFKhHDa" +
                "i7P43T0++nOBaOBCq0rl4gxHuNmGvLIAJLGWzsi0IAZWkAKoz3jTs/Meso3QVtpqB98g7mOcAms7XD7T" +
                "RY7kFSyDrzMoiYUrV62NxtJ0G0FUYcgGAQs66bYJ72pCJoM3LDYWYVdMDV6l95UyyIQWGxPyxAfH6DEt" +
                "90YnX8mWR8sk4bdIcYYXjs+Zfr2rnebDzezDdP7hSuweI/ED/rM/KW4TufRiS4GdmRLro5rEtwI1sZFz" +
                "t0ZBNJjjyWL+x0z0MF8cYnJGauegLNyYEmt0EvDN7Wz2/mYxm3bALw+BHSmCx2FLpBz24G9QBj4IuQxw" +
                "sgl8escJoodYEDZLxP88BviDSaIKjeFQnquCGMEEv0MB0bMFuRJlWHBPCHTeUr77fTKZzaY9yq8OKW+A" +
                "LFVuiGn7WrEKy5obwlNCHAsz/u3j7V4XDvPjE2HSKh5d19GWe+5PRtI1fVYadoWvUAZLaYra0TF6t7N3" +
                "s0mP30j89Ck9R3+TCkccEAuqqsNju3z/eY4pKYnmGjG7YDUaZpBgyh0CLdvYtSyMPnaA1nldpYzEz9/A" +
                "eZ31bBViEe7N1yWvU3gyvr7eV/JI/HIqwZRwZ9GTDE9RFzn5NFuHpO3SuJJvN74+Qr8LRCakDw7Rt8nr" +
                "L3CI02RmUxyUXxOAr40jnrj+eLfoQ43ErxFwbHditLcHkIRG1hiEGhFkJwGjDJtxwMPghY66pSfUnmfs" +
                "itVmSTcGx0flSPuodSaDcVFUmziY8EKUguO67S4rkGkvKq4x0ZuyeIumtM4ylrFdFOghJN/wKptPk8YB" +
                "zQjSiuQDp5vPE+9kSLrJDWaLeB/3Wkp0B2keiuZxdKnbO+axTthPlv2DU5JngTDiULlCrooCuxnTN8nb" +
                "EEJ30DvrwZLkuKVERv1RoeWP7tKOF2jFoLc9zMJudmU3Ygfmq7oImCu9lxk1qfErUmZp1K4YIgM/bNF5" +
                "6GsWgFRZx6JAnzNYNdwlj4eQr52654+m8iT5D+Nxz6XWCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
