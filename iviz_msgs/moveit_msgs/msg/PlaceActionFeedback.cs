/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MoveitMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class PlaceActionFeedback : IDeserializable<PlaceActionFeedback>, IActionFeedback<PlaceFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public PlaceFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public PlaceActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new PlaceFeedback();
        }
        
        /// Explicit constructor.
        public PlaceActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, PlaceFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public PlaceActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new PlaceFeedback(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new PlaceActionFeedback(ref b);
        
        public PlaceActionFeedback RosDeserialize(ref ReadBuffer b) => new PlaceActionFeedback(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVptk36kntFBlRRHGSfR2GqvHhBckWhBUAVAyfr3fQtSFOVY" +
                "jQ5JNLL1Bbx9ePt2sW9JZuREEV8SqYKurNHpQ+lz/8NNJc19kKH2wseXZGGkojdEWSrVP2LVvklGX/iR" +
                "vL+/uUbMrOHxtmE3ECBjM+kyUVKQmQxSrCqQ13lB7srQhgwTLdeUifhr2K3JD7FxWWgv8MzJkpPG7ETt" +
                "sShUQlVlWVutZCARdElH+7FTWyHFWrqgVW2kw/rKZdry8pWTJTE6np7+rckqEvPpNdZYT6oOGoR2QFCO" +
                "pNc2x48iqbUNr17yhmSw3FZX+Eg5UtAFF6GQgcnS49qRZ57SXyPGd83hhsCGOIQomRcX8bsHfPSXAkFA" +
                "gdaVKsQFmC92oagsAElspNMyNcTACgoA9QVvenHZQ7YR2kpb7eEbxEOMc2Bth8tnuiqQM8On93UOAbFw" +
                "7aqNzrA03UUQZTTZIOA7J90u4V1NyGTwhjXGIuyKGcGr9L5SGgnIxFaHIvHBMXrMxoPOkq/kxpO1kfBb" +
                "ZDbHC8fnBL/eF0zzYTH7MJ1/uBH7x0j8iP9sS4rbRCG92FFgQ6bE+qgm8a1ATWzk3G1QBw3meLKc/zUT" +
                "PcyfjjE5I7VzUBYmTIk1Ogt4cTebvV8sZ9MO+OUxsCNFsDZsiZTDHvwN3O+DkKsAJ+vAp3ecIHqMdWDz" +
                "RPzPY4A/mCSq0BgOVbk2xAg6+D0KiF4syZWoPsOtINBlS/n+z8lkNpv2KL86prwFslSFJqbta8UqrGru" +
                "A88JcSrM+I+PdwddOMzPz4RJq3j0rI62PHB/NlJW02elYVf4CmWwktrUjk7Ru5u9m016/Ebil0/pOfqb" +
                "VDjhgFhQVR2e2uX7z3NMSUn01IjZBavRJ4MEU+4Q6NTabqTR2akDtM7rKmUkfv0GzuusZ6sQi/Bgvi55" +
                "ncKT8e3toZJH4rdzCaaEq4qeZXiOusjJp9k6Jm1X2pV8qfH1EfpdIDKh7OgQfZu8/gKHOE9mNsVR+TUB" +
                "+No44Ynbj/fLPtRI/B4Bx3YvRnt7AElkyBqDUCOC7CRglGEzBXgY3GRRt/SM2vOMXbHaLOlW4/ioHGmf" +
                "tM5kMDam2sZ5hBeiFBzXbXdZgUx7UXGNid5oxVsySus8ZxnbRYEeQ/INr7L5NGkc0IwgrUg+cLr5PPFO" +
                "hqTbQmO2iPdxr6VEd1DGs9A8ji51e8c81Qn7ybJ/cEryLBBGHCrXyJUx2M2YvknelhC6g95bD5Ykxy0l" +
                "MuqPCi1/dJd2vEArBr3dcRb2Iyu7ETswX9UmYJz0XubUpMavSemVVvtiiAz8sEXnWa9ZAFJlHYsCfU5j" +
                "1XCfPB5CvlLqSlhRhyZvR8M4IrbRefKg5D9Ruqfn2QsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
