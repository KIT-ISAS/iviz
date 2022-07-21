/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TwoIntsAction : IDeserializableCommon<TwoIntsAction>, IMessageCommon,
		IAction<TwoIntsActionGoal, TwoIntsActionFeedback, TwoIntsActionResult>
    {
        [DataMember (Name = "action_goal")] public TwoIntsActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TwoIntsActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TwoIntsActionFeedback ActionFeedback { get; set; }
    
        public TwoIntsAction()
        {
            ActionGoal = new TwoIntsActionGoal();
            ActionResult = new TwoIntsActionResult();
            ActionFeedback = new TwoIntsActionFeedback();
        }
        
        public TwoIntsAction(TwoIntsActionGoal ActionGoal, TwoIntsActionResult ActionResult, TwoIntsActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        public TwoIntsAction(ref ReadBuffer b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        public TwoIntsAction(ref ReadBuffer2 b)
        {
            ActionGoal = new TwoIntsActionGoal(ref b);
            ActionResult = new TwoIntsActionResult(ref b);
            ActionFeedback = new TwoIntsActionFeedback(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TwoIntsAction(ref b);
        
        public TwoIntsAction RosDeserialize(ref ReadBuffer b) => new TwoIntsAction(ref b);
        
        public TwoIntsAction RosDeserialize(ref ReadBuffer2 b) => new TwoIntsAction(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) BuiltIns.ThrowNullReference();
            ActionGoal.RosValidate();
            if (ActionResult is null) BuiltIns.ThrowNullReference();
            ActionResult.RosValidate();
            if (ActionFeedback is null) BuiltIns.ThrowNullReference();
            ActionFeedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += ActionGoal.RosMessageLength;
                size += ActionResult.RosMessageLength;
                size += ActionFeedback.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            ActionGoal.AddRos2MessageLength(ref c);
            ActionResult.AddRos2MessageLength(ref c);
            ActionFeedback.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "actionlib/TwoIntsAction";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6d1aa538c4bd6183a2dfb7fcac41ee50";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71X227bRhB951cM4IfYRe00l6apAT2osuIqcBJDVvtSFMaSHJHbkkt1d2lZf98zy4sp" +
                "x66FIpYgm6K4e+bMzJmZ1WJdzYx348TrypxXqiAVPl5n+Bwthk/n7OrCd89tuNte8YE5jVXyd7dm2d5H" +
                "o2/8ij5dnZ+2Vgodv1zc9yL6lVXKlvJwifqV16XL3EtZMTsjcfFap50Pwfvg9vPQdT5tzDfcogO68sqk" +
                "yqZUslep8oqWFTjrLGd7XPANF9ikyhWnFJ76zYrdCTYucu0I74wNW1UUG6odFvmKkqosa6MT5Zm8Lnlr" +
                "P3ZqQ4pWynqd1IWyWF/ZVBtZvrSqZEHH2/E/NZuEaXZ2ijXGcVJ7DUIbICSWldMmw0OKam38m9eygQ7o" +
                "j3nlXv0ZHSCix/ieM6SgZ0E+V15Y8+0K6hHCyp3C2HeNlycwgigxzKWODsN317h1RwRr4MKrKsnpEC5c" +
                "bnxeGQAy3SirVVywACcIBVBfyKYXRwNkE6CNMlUH3yDe2dgF1vS44tNxjuQVEgZXZ4gkFq5sdaNTLI03" +
                "ASQpNBtP0J1VdhPJrsZkdPBBgo1F2BVSg6tyrko0MpHSWvs8ct4KekiLyPS5q2hYG0FjLVlyeVUXKW4q" +
                "y8Gv4Ahyuc41EhKckLqhtXJkRTkOToiSZiHfQZsIiTKtMSTZ3kAa65wNaU9wlJ2oF7rgcoUeUxTYLZiu" +
                "Uc2aYbqHppiXwkVRwtYrZE4YDePb8tdplxOEF/Q2YqSPMy37fmVS7GhaGorROZVxSAK5FSd6qZPGwZaB" +
                "O2nRpVKaBSBV1s6DGaH8sOqky59kbl8NMLS+CJX37i2p9hrvt/s2U+Lp/ove52sn6cKla8HtiGlnyzPz" +
                "vs8lujcWpLO97wg2N5fTz2ezz+fUvUb0A/43WgsCyVEBG/YiMygB2kuaRtc2hC35t5jjyWL2+5QGmK+2" +
                "MaUD1daik6D7xiya2gn4cj6dfrpcTM964NfbwJYTRk9PpY4U2mGvb1JLj+ShMuG9lYLj2zAATBbRf7wO" +
                "8IdSClFoGizG0apgQdDedSggerhgW2LsFDIDPR+1lK9+m0ym07MB5TfblKXDqCTXLLRdnUgUlrUMwIcC" +
                "8ZiZ8S9f5ndxETNvHzATV8H1tA5lfMf9QUtpzU+GRlThKvSopdJFjf71CL359ON0MuA3oh+/pmf5L078" +
                "IwoIvauq/X25fP80x5gThYYdMHtjNQ4I0mfDRMQRRZsbVaC5PuJAq7y+Ukb0bg/K66VnKh+K8E58ffL6" +
                "CE/GFxd3lTyin3Yl2I6ehxjuEl3k5OtsbZM2S21LOc3J4PPDLhCYcLrlxFAm77+BE7uFWUSxVX6NATkm" +
                "PaKJiy9XiyHUiH4OgOP+UNCeloBEKbImINwEQfUhEJST5vjbnkokbvEOtecEu5JoS0jXGu4/cCTBwWFc" +
                "FNU6HMRlIUrBbh8aFLWDPZwPBqNMtqQc11kmYWwXeb71+5v/7fBtBr+ry/2O/u7n3/8c/v2vx33/bOx5" +
                "R/8CFXGe+g4PAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
