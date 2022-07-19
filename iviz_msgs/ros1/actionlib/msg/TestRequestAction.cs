/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestAction : IDeserializableRos1<TestRequestAction>, IDeserializableRos2<TestRequestAction>, IMessageRos1, IMessageRos2,
		IAction<TestRequestActionGoal, TestRequestActionFeedback, TestRequestActionResult>
    {
        [DataMember (Name = "action_goal")] public TestRequestActionGoal ActionGoal { get; set; }
        [DataMember (Name = "action_result")] public TestRequestActionResult ActionResult { get; set; }
        [DataMember (Name = "action_feedback")] public TestRequestActionFeedback ActionFeedback { get; set; }
    
        /// Constructor for empty message.
        public TestRequestAction()
        {
            ActionGoal = new TestRequestActionGoal();
            ActionResult = new TestRequestActionResult();
            ActionFeedback = new TestRequestActionFeedback();
        }
        
        /// Explicit constructor.
        public TestRequestAction(TestRequestActionGoal ActionGoal, TestRequestActionResult ActionResult, TestRequestActionFeedback ActionFeedback)
        {
            this.ActionGoal = ActionGoal;
            this.ActionResult = ActionResult;
            this.ActionFeedback = ActionFeedback;
        }
        
        /// Constructor with buffer.
        public TestRequestAction(ref ReadBuffer b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        /// Constructor with buffer.
        public TestRequestAction(ref ReadBuffer2 b)
        {
            ActionGoal = new TestRequestActionGoal(ref b);
            ActionResult = new TestRequestActionResult(ref b);
            ActionFeedback = new TestRequestActionFeedback(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TestRequestAction(ref b);
        
        public TestRequestAction RosDeserialize(ref ReadBuffer b) => new TestRequestAction(ref b);
        
        public TestRequestAction RosDeserialize(ref ReadBuffer2 b) => new TestRequestAction(ref b);
    
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestRequestAction";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "dc44b1f4045dbf0d1db54423b3b86b30";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VX227jNhB911cQyMMmRZPdTXbbbQA/uLY2dZGLYatF3wyKGktsKcolqTj++85QlCzH" +
                "SWOgm9RwIkscHp65jxKwbgZ/13gZCicrfVVxxbj/ucjxd5Q8lpiBrZVrZYy/25f6CpClXPzVyi3DfTT4" +
                "xp/oZn51GU5RMn2/x4Q0in4BnoFhhb9EnfSitLl9TxKTMSN1FzLr6+Kt4c3wOrStyxoKDb/oiM0d1xk3" +
                "GSvB8Yw7zpYV8pZ5AeZUwT0o3MTLFWTMr7rNCuwZbkwKaRl+c9BguFIbVlsUchUTVVnWWgrugDlZws5+" +
                "3Ck142zFjZOiVtygfGUyqUl8aXgJhI5fSybRAthkfIky2oKonURCG0QQBriVOsdFFtVSu4tz2hAdJevq" +
                "FG8hR+t3hzNXcEdk4WGFAUQ8ub3EM75rlDtDbDQO4CmZZcf+2QJv7QnDQ5ACrCpRsGNkPt24otIICOye" +
                "G8lTBQQs0AKI+o42vTvpIWsPrbmuWvgGcXvGIbC6wyWdTgv0mSLtbZ2jAVFwZap7maFouvEgQknQjmHI" +
                "GW42Ee1qjoyOvpKNUQh3eY/glVtbCYkOyNhauiKyzhC69wZF6GsnUT8tfGgFsswWVa0yvKkMeL28IujL" +
                "dSHRIV4JShe25paZJocgowCaeH/7kESTcB0OQyebewyNdQGaScdQUbAUtBgXUK6wzCiFuwnTNlGzBjy6" +
                "g2YpLIkLZwKM4+g5YtS3b+Avs9YnaF6kt6FDOjuzZVeudIY7mqqGOWgtz8E7gdkVCLmUolEwMLBnAZ0S" +
                "pBFAUmVtHTJjmHUoddb6jzz3lvXPV74mF5N4djO5HSbxYv7baBTP54MPeyvDn+9mSTwefNxbmcW/xiNa" +
                "Ot9bur6bx4OLvcfj2d108GnvcfzHKJ4mk7vbweew5sCUvtYs0F2utlFaVYrJXKNXF4JjaqrWfI1TFg4e" +
                "XLu5gLYBNdvswspypXCnz7coqw33YZaB4psFFwJWe087CtuFFcdIbRm9ocf6HfblnjX3/Fig2YMJLTqY" +
                "5pX5P+YTPWqn1A2+tCSbm2l8O57cXrH2M2Af8H+TqD67CiwfG3CUo5hGmLii6RKhmu7UjoA5HCWT32PW" +
                "w/y4i0nluzYGwwI7VgoUUQcBT2dxfDPF6O+Az3eBDQjAPphREfIB2xYHxpcYW1TWUHtD1QoefNPUecT+" +
                "5XOEf1iHvBWa7oQtHKOaEKSzLQoSPU6a2FU0Nzg4CZR9jsfjHuWLXcpUnrkoJBBtWwuywrKmoeEpQzx3" +
                "TCgYPZN/euKYtPKqY26Rybfcnzwpq+FF01BU2AoL/JJLVWPxf4ZeW7W29D7v0zPwJwj3TAT4wl/V7nG4" +
                "fP8yxxQE1RCP2R1WY5WhJuXHCRzrpL7nCjvTMwqEyOsyZcB+eIPI60JPV84n4Tb4Oud1Fh4Nr6+3mTxg" +
                "Px5KMPTtpxgeYl30yb63dknrpTQlTcA0Nbh+FfBMINtRoh8mX76BEoeZmYJiJ/2aA2jGfCYmsN8mfagB" +
                "+8kDDruJKoyaiIT9relt0BiBdyYglLPmlSGMdGS39IDcs4RdkbXJpGuJ6j8xz+HUNVSqWvuXFxLEVDC7" +
                "Exdnoa374arXzmhLBmmd52TGIOSb/psOT6EJvzRoBH3fkNju+/V/mBK6V/T/49284x/9AwWTHxuDEAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
