/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestAction")]
    public sealed class TestRequestAction : IDeserializable<TestRequestAction>,
		IAction<TestRequestActionGoal, TestRequestActionFeedback, TestRequestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestRequestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestRequestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestRequestActionFeedback ActionFeedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestAction()
        {
            ActionGoal = new TestRequestActionGoal();
            ActionResult = new TestRequestActionResult();
            ActionFeedback = new TestRequestActionFeedback();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestAction(TestRequestActionGoal ActionGoal, TestRequestActionResult ActionResult, TestRequestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestRequestAction(ref Buffer b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestAction(ref b);
        }
        
        TestRequestAction IDeserializable<TestRequestAction>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestAction(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            ActionGoal.RosSerialize(ref b);
            ActionResult.RosSerialize(ref b);
            ActionFeedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ActionGoal is null) throw new System.NullReferenceException(nameof(ActionGoal));
            ActionGoal.RosValidate();
            if (ActionResult is null) throw new System.NullReferenceException(nameof(ActionResult));
            ActionResult.RosValidate();
            if (ActionFeedback is null) throw new System.NullReferenceException(nameof(ActionFeedback));
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
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestAction";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "dc44b1f4045dbf0d1db54423b3b86b30";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVXW2/iRhR+t8R/GCkPm1RNdjfZbbeReKDESalyQeBWfUODfbCnNTadGYfw7/udGdtA" +
                "IAqqdrOIxJc588137oeIjB3RvxUuvdiqsrgpZS6ku52kuA+i5xIjMlVuGxntnnalromSqYz/aeRm9XMn" +
                "6H7lTye4G99c1ufkavp+r06d4DeSCWmRuUvQik/mJjXvWWRwJVjjiUo21XEG4fffjLmxiSfhGXaCIzG2" +
                "skikTsScrEyklWJWgrpKM9KnOT1Sjl1yvqBEuFW7WpA5w8YoU0bgm1JBWub5SlQGQrYUcTmfV4WKpSVh" +
                "1Zy29mOnKoQUC6mtiqtcasiXOlEFi8+0nBOj42vYKkVMYnB1CZnCUFxZBUIrIMSapFFFikURVKqwF+e8" +
                "ITiKluUpHimFA9rDhc2kZbL0tEAYMU9pLnHGD165M2DDOoRTEiOO3bsJHs2JwCGgQIsyzsQxmA9XNisL" +
                "AJJ4lFrJaU4MHMMCQH3Hm96dbCAz7UtRyKJs4D3i+oxDYIsWl3U6zeCznLU3VQoDQnChy0eVQHS6ciBx" +
                "rqiwAlGnpV4FvMsfGRxds40hhF3OI7hKY8pYwQGJWCqbBcZqRnfe4CD99qm0mRsclhF08K4zWVnlCR5K" +
                "zax9SAm4c5kp+MTpwUkjltII7TOJEo6hgXO5i0pYRRb1afCzfkR0LDMqhLICupLhuEVo0HyBepPn2M2Y" +
                "xgfOknB0Cy2mhBQBBRGTthLOY0abJq75q6RxCywMevBMuTa1aOoUmCXY4csb0tAYmZLzgzALitVMxV7B" +
                "moE5q9E5R7wASM0rY8FMIPEgdda4EFLB2xZCXwJ9Rkbh6G5w34vCyfiPfj8cj7sfdlZ6vz6MovCq+3Fn" +
                "ZRT+HvZ56Xxn6fZhHHYvdl5fjR6G3U87r8O/+uEwGjzcdz/Xa5b03FWcCTxmKxNMyzIXKi3g2EkskaB5" +
                "Y0Hvl4mlJ9tszqhpRn6bmRg1X+TY6bIuSCotXaQllMvVRMYxLXbethTWCwuJYK0ZfY/u5fvtAf1r7CiK" +
                "2nYbOHXH9tZ5y6rhGXWCZ92VO8OXhqd/GIb3V4P7G9F8uuID/vuMdWmWoY6sCHWh5HxCBse+Y9SVdauI" +
                "1Ji9fjT4MxQbmB+3MbmUV1ojONC9psRxdRDwcBSGd0PkQAt8vg2sKSb0RPQz4cO2qRJCzhBhXN+gveay" +
                "RU+ugRZpsCa6+znCHwqSs4LvVGjniG1GUNY0KCB6HPkIznmGsHRSU3aZHl5tUL7Ypsx1WsaZwmyBsl4h" +
                "OYyZVTxA7DPES8fUZWPD5J/2HDMtnerIMDb5mvvek5KKXjUNR4UpUelnUuUVusAL9Jratab3eZeepr8p" +
                "dh1lHx3uAGVln4fLj69znFLMlcRhtodVqDXcrdxogRFPFY8yR4t6QYE68tpM6Yqf3iDy2tArSuuScB18" +
                "rfNaC/d7t7frTO6Knw8lWDfwfQwPsS58suutbdLFTOk5T8M8PrRucAMdM6FkS4nNMPnyFZQ4zMwcFFvp" +
                "5w/gefOFmEDXjTahuuIXB9hrR6t67AQSupzvcOSNIFsTMAqPMLitZzu22/SA3DOMXbK12aRLBfX3DHYY" +
                "v3p5Xi7dDxkWRCro7dFLwmauIrgpa6Oj8ZaEplWashlrIdf633iKalrxaxOHV/l7DAvX7c/s/z8uNBjt" +
                "LPy2iqxV6AT/AZzMU6efEAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
