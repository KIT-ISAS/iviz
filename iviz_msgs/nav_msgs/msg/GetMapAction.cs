/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class GetMapAction : IDeserializable<GetMapAction>,
		IAction<GetMapActionGoal, GetMapActionFeedback, GetMapActionResult>
    {
        [DataMember (Name = "action_goal")] public GetMapActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public GetMapActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public GetMapActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public GetMapAction()
        {
            ActionGoal = new GetMapActionGoal();
            ActionResult = new GetMapActionResult();
            ActionFeedback = new GetMapActionFeedback();
        }
        
        /// Explicit constructor.
        public GetMapAction(GetMapActionGoal ActionGoal, GetMapActionResult ActionResult, GetMapActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        internal GetMapAction(ref Buffer b)
        {
            ActionGoal = new GetMapActionGoal(ref b);
            ActionResult = new GetMapActionResult(ref b);
            ActionFeedback = new GetMapActionFeedback(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new GetMapAction(ref b);
        
        GetMapAction IDeserializable<GetMapAction>.RosDeserialize(ref Buffer b) => new GetMapAction(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "nav_msgs/GetMapAction";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e611ad23fbf237c031b7536416dc7cd7";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71Y31PbOBB+91+hGR4KN0lKf1yv1xkecpBy3EDLAb0XhmEUexOrtaVUkgm5v/6+lWwn" +
                "pqFlbppkAont1e63u9+uVjkmfyZnw9Qro4+NLIQMX2+n+J4crzy8IFcVvnlsw1VH4D1RNpbpl0ZkUl8n" +
                "Bz/5lZxdHr8TWt7dlm7qnh8/8CD5k2RGVuThI4loCjWupSFxciTYvVuV1Q4Ex4PHm8HqfBatR2jJjrj0" +
                "UmfSZqIkLzPppZgYQFbTnGy/oDsqsEiWM8pEeOoXM3IDLLzKlRN4T0mTlUWxEJWDkDciNWVZaZVKT8Kr" +
                "kjrrsVJpIcVMWq/SqpAW8sZmSrP4xMqSWDvejr5WpFMSJ0fvIKMdpZVXALSAhtSSdEpP8VAkldL+1Ute" +
                "kOxczU0flzRF4FvjwufSM1i6n4EwjFO6d7DxS3RuAN0IDsFK5sRuuHeLS7cnYAQQaGbSXOwC+fnC50ZD" +
                "IYk7aZUcF8SKU0QAWp/xomd7K5p1UK2lNo36qHFp4ylqdauXfernyFnB3rtqigBCcGbNncogOl4EJWmh" +
                "SHsBtllpFwmviiaTnfccYwhhVcgIPqVzJlVIQCbmyueJ85a1h2wwOTfExrUVEahVgxUuN1WR4cJYCn4F" +
                "R5DLea6QkOAEl4uYSycsE8bBCSbQSch3oCRCInVtDEm2d6DGPCctlBdwlByTFrygcoa2UhRYzTpdZM2c" +
                "YLpVLcY0YSxSpGS9ROYY0Wp8a/wqa3KC8ALego20cRaTtkfpDCtiF0MNOienFJIg3IxSNVFpdLBG4Aa1" +
                "di6QKABQZeU8kAlUHaQGTf44c9tpeqHdbbO/xk3gxx0W7c1XjlODj7rJ1htIvXNsBvSjUJIHfZ9719sG" +
                "X7w4H304OvlwLJrXgdjH/0irwIUcZF+QZ0Yh6aBZGntaXfsdptc6h4dXJ/+MxIrOF12d3Gwqa9E00F/H" +
                "xPR5kuLzi9Ho7PxqdNQqftlVbCkldO2MS0ai87VUFnLikTsUIby3XFt0H1q8nibiO68d/KFqQhRiL8WG" +
                "MyuINSjvGi0AuntFtsTGUvAu52mvhnz56fBwNDpagfyqC5mbiUxzRQzbVSlHYVLxFrcuEI+ZGf7x8WIZ" +
                "Fzbzeo2ZsQmuZ1Wo2CX2tZayin4YGmaFM2hHE6mKCq3qEXgXo79Ghyv4DsSv38Kz9JlS/wgDQpsylX9I" +
                "l96PMY4plejNQWdrrMIIwC01bH4YQpS+kwX66CMO1MxrK+VAvNkC81rqaeNDES7J1yavjfDh8PR0WckH" +
                "4renAqx3mXUInxJd5OTbbHVB64myJc9rvMf51S4QkFDWcWKVJm9/ghNPCzOTolN+0QBPRI9w4vTj5dWq" +
                "qgPxe1A4bPf/ejCCJpEha6yEYhBkGwLWMogDbj2AcNzGT6g9x7oNR5tDOldwf830gRlhWBRmHkZtFkQp" +
                "2O58IEW9h4dRYGUn4yUZjavplMNYC3m699va6uudt737MU2rGfKyOLao1VLONg2jY7A5iFjiwR7R52nr" +
                "ZR/7a42mx8eNOC0Serpg/qxKc/jRucZyrArlF8JMoNI0JgZJd8QQSN0ZDkpHzUGJl7PPCEx7X+mJacZA" +
                "PAtTdoBhzbxfys9YhgMP2V6sizbLu/u9/b2BEK2D0LGExnOq5HKKpwQrNQbF6/3ei/39Gyz6pL9oM9dM" +
                "3/6LQcLVcH0TTG+cFyu+N+nIDYpmjBNaGqJhSxnoj92ubk1pLi1qgqzCTIugh5vdzNYRfDjxx5hyaygM" +
                "UpLF2Rv3bvn6Nozjy9gjzaaIO+p1+ZyTf5NMIMjnxeUzLDhjpSpDGq5Zyt00p8pwsxbICedi/1Ai3q2N" +
                "GqtQmY1HDOG67Am8rcw4T82xOeSQZNGfG4tYzXA8rBdBUSBpoEOTbigaJFPCvu7tIob9PCwJ5jaU4W/t" +
                "AdtwWT0xqUAd0APpBIcoHEtkSr2wLxoeHuNzFQmArg7IzdqBSM4NgtgKJH9X6H5WB71LuU1R+KGDgNIw" +
                "GDskzyJ1g2jwwxeJK4bccTdy6s1rcd9+W7Tf/t0O/GXo1jXFTjy74Pnq6zLuXK7oe9/3qPk23+qpr/ll" +
                "7/+d+9rfBbf7g2ALOvkPurN/zeMUAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
