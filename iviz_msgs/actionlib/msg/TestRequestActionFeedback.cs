/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestActionFeedback : IDeserializable<TestRequestActionFeedback>, IActionFeedback<TestRequestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestRequestFeedback Feedback { get; set; }
    
        /// Constructor for empty message.
        public TestRequestActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TestRequestFeedback.Singleton;
        }
        
        /// Explicit constructor.
        public TestRequestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// Constructor with buffer.
        public TestRequestActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TestRequestFeedback.Singleton;
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestRequestActionFeedback(ref b);
        
        public TestRequestActionFeedback RosDeserialize(ref ReadBuffer b) => new TestRequestActionFeedback(ref b);
    
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
    
        public int RosMessageLength => 0 + Header.RosMessageLength + Status.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestRequestActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WXXPaRhR916/YGR5id2rSJv1IPcMDBeLQcRKPTfvqWa0u0rbSiu4HmH/fc1dCCAca" +
                "HpIw2AJ799yz5557974jmZEVRXwkUnldm1Knj5XL3cubWpYPXvrghIuPZEHO39O/AY+3RFkq1T9i2X5I" +
                "Rl/4lbx/uLlG5Kxh867hOBCgZDJpM1GRl5n0UixrHEHnBdmrktZUMt1qRZmI//XbFbkhNi4K7QTeORmy" +
                "siy3Ijgs8rVQdVUFo5X0JLyu6GA/dmojpFhJ67UKpbRYX9tMG16+tLIiRsfbsTRGkZhPr7HGOFLBaxDa" +
                "AkFZkk6bHP8USdDGv37FG5LBYlNf4SvlSEQXXPhCeiZLTytLjnlKd40Y3zWHGwIb4hCiZE5cxL894qu7" +
                "FAgCCrSqVSEuwPxu64vaAJDEWlot05IYWEEBoL7gTS8ue8gmQhtp6h18g7iPcQ6s6XD5TFcFclby6V3I" +
                "ISAWrmy91hmWptsIokpNxgu4z0q7TXhXEzIZvGWNsQi7YkbwlM7VSiMBmdhoXyTOW0aP2XjUWfKV3Hiy" +
                "QhL+iMzmeHB8TvCbXdk0X+5mH6bzDzdi9xqJH/CbbUlxmyikE1vybMiUWB/VJL4VqImNnNs16qDBHE8W" +
                "879moof54yEmZyRYC2VhwpRYo7OA7+5ns/d3i9m0A351CGxJEawNWyLlsAf/JTYGIZceTtaeT285QfQU" +
                "68Dkifif1wA/MElUoTEcqnJVEiNo73YoIHqxIFuh+kpuBZ4uW8oPf04ms9m0R/n1IeUNkKUqNDFtFxSr" +
                "sAzcB44JcSrM+PeP93tdOMxPR8KkdTx6FqIt99yPRsoCfVYadoWrUQZLqctg6RS9+9kfs0mP30j8/Ck9" +
                "S3+T8iccEAuqDv65Xb7/PMeUlERPjZhdsIA+6SWYcodAp9ZmLUudnTpA67yuUkbil2/gvM56pvaxCPfm" +
                "65LXKTwZ397uK3kkfj2XYEq4qugow3PURU4+zdYhabPUtuJLja8P3+8CkQllB4fo2+TNFzjEeTKzKQ7K" +
                "rwnA18YJT9x+fFj0oUbitwg4Njsx2tsDSCJD1hiEGhFkJwGjDJspwMHgZRZ1S8+oPcfYNavNkm40jo/K" +
                "keZZ60wG47KsN3Ee4YUoBct1211WINNeVFxjojdg8ZaM0pDnLGO7yNOTT77hVTafJo0DmhGkFcl5Tjef" +
                "J97JkHRTaMwW8T7utZToDsp4FprH0SW0d8xznbCfDPsHpyTHAmHEoWqFXJUldjOma5K3IYTuoHfWgyXJ" +
                "ckuJjPqjQssf3aUdL9CKQW97mIXdyMpuxA7MV6H0GCedkzk1qXErUnqp1a4YIgM3bNF51msWgFQVYlGg" +
                "z2msGu6Sx0PI107dyyMDeZL8B69nW1jVCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
