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
        internal PlaceActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new PlaceFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new PlaceActionFeedback(ref b);
        
        PlaceActionFeedback IDeserializable<PlaceActionFeedback>.RosDeserialize(ref Buffer b) => new PlaceActionFeedback(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "moveit_msgs/PlaceActionFeedback";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "12232ef97486c7962f264c105aae2958";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WXXMaNxR931+hGR5id2q3Tdok9QwPFIhDxkkYm/bVo5Uuu2q1Wippwfz7nquFBWqY" +
                "8JCEweZLOvfo3HOv7nuSmrwo00smVTS1syZ/rEIRfrqtpX2IMjZBhPSSTa1U9I5I51L9I+abN1n/Kz+y" +
                "jw+3N4ipWx7vW3Y9ATJOS69FRVFqGaWY1yBvipL8laUlWSZaLUiL9GtcLyhcY+OsNEHgWZAjL61diyZg" +
                "UayFqquqcUbJSCKaig72Y6dxQoqF9NGoxkqP9bXXxvHyuZcVMTqegf5tyCkSk9EN1rhAqokGhNZAUJ5k" +
                "MK7AjyJrjIuvXvKGrDdb1Vf4SAVS0AUXsZSRydLTwlNgnjLcIMYP7eGugQ1xCFF0EBfpu0d8DJcCQUCB" +
                "FrUqxQWYT9exrB0ASSylNzK3xMAKCgD1BW96cbmHzLRvhJOu3sK3iLsY58C6DpfPdFUiZ5ZPH5oCAmLh" +
                "wtdLo7E0XycQZQ25KOA7L/06411tyKz3jjXGIuxKGcGrDKFWBgnQYmVimYXoGT1l49Ho7Bu58WRtZPwW" +
                "mS3wwvE5wW+3BdN+mI4/jSafbsX20Rc/4z/bktI2Ucog1hTZkDmxPqpN/EagNjZy7peogxZzMJxN/hqL" +
                "PcxfDjE5I433UBYmzIk1Ogt4ej8ef5zOxqMO+OUhsCdFsDZsiZTDHvwN3B+ikPMIJ5vIp/ecIHpKdeCK" +
                "bEf0+aOHP5gkqdAaDlW5sMQIJoYtCohezMhXqD7LrSDS5Ybyw5/D4Xg82qP86pDyCshSlQYtQsOHilWY" +
                "N9wHjglxKszgj8/3O104zK9HwuR1Orpuki133I9G0g19URp2RahRBnNpbOPpFL378YfxcI9fX/z2nJ6n" +
                "v0kxv6N0uKDqJv7fLj9+mWNOSqKnJswuWIM+GSWYcodApzZuKa3Rpw6wcV5XKX3x+js4r7Oeq2Mqwp35" +
                "uuR1Cg8Hd3e7Su6LN+cSzAlXFR1leI66yMnzbB2SdnPjK77U+Pro0pD6MjMhfXCIfZu8/QqHOE9mNsVB" +
                "+bUB+No44Ym7zw+zfai++D0BDtxWjM3tASShkTUGoVYE2UnAKNftFBBgcKuTbvkZtRcYu2a1WdKVwfFR" +
                "OYh12Dqz3sDaepXmEV6IUsCbendZgczmouIaE3ujFW/RlDdFwTJuFkV6itl3vMomozQlbe7drUghcrr5" +
                "POlOhqSr0mC2SPfxXktJ7iDNs9AkjS5pujqiE/aTY//glBRYIIw4VC2QK2uxmzFDm7wVIXQHvbUeLEme" +
                "W0pitD8qbPijuyza8QKtGPTQ5fazsB1Z2Y3YgfmqsRHjZAiy4PQiNWFBysyN2hZDYhDYPYzOs167AKSq" +
                "JhUF+pzBqutt8ngI+Uapq2BFE9u8HQzjiLiJzpMHZf8BUbqn59kLAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
