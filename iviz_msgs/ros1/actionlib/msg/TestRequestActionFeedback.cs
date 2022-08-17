/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestActionFeedback : IDeserializable<TestRequestActionFeedback>, IMessage, IActionFeedback<TestRequestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestRequestFeedback Feedback { get; set; }
    
        public TestRequestActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = TestRequestFeedback.Singleton;
        }
        
        public TestRequestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestRequestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public TestRequestActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TestRequestFeedback.Singleton;
        }
        
        public TestRequestActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = TestRequestFeedback.Singleton;
        }
        
        public TestRequestActionFeedback RosDeserialize(ref ReadBuffer b) => new TestRequestActionFeedback(ref b);
        
        public TestRequestActionFeedback RosDeserialize(ref ReadBuffer2 b) => new TestRequestActionFeedback(ref b);
    
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = Feedback.AddRos2MessageLength(c);
            return c;
        }
    
        public const string MessageType = "actionlib/TestRequestActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXMaRxC976+YKg6WUhGO7Xw4quJAAMu4ZFuFSC6plGp2ttmdZHeWzAeIf5/Xs8sC" +
                "MsQcbFOSFtDM6zevX/f0W5IZWVHERyKV17UpdfpQudw9v6llee+lD064+Ejm5PyM/g14vCHKUqn+EYv2" +
                "TTL4wq/k/f3NNSJnDZu3DceeACWTSZuJirzMpJdiUeMIOi/IXpW0opLpVkvKRPyv3yzJ9bFxXmgn8JOT" +
                "ISvLciOCwyJfC1VXVTBaSU/C64oO9mOnNkKKpbReq1BKi/W1zbTh5QsrK2J0/DiWxigS0/E11hhHKngN" +
                "QhsgKEvSaZPjnyIJ2vhXL3mD6Ik/Z7V78VfSm6/rK3xPOTLSsRC+kJ5Z0+PSkmPC0l0j2HfNKfsIApUI" +
                "4TInLuJ3D/joLgWigQsta1WICxzhbuOL2gCQxEpaLdOSGFhBCqA+403PLveQTYQ20tRb+AZxF+McWNPh" +
                "8pmuCiSvZBlcyKEkFi5tvdIZlqabCKJKTcYL2NBKu0l4VxMy6b1hsbEIu2Jq8JTO1UojE5lYa18kzltG" +
                "j2l50FnylWx5slQSfosU53hwfM706239NB/uJh/G0w83YvsaiB/wl/1JcZsopBMb8uzMlFgf1SS+FaiJ" +
                "jZzbFQqiwRyO5tM/JmIP88UhJmckWAtl4caUWKOzgO9mk8n7u/lk3AG/PAS2pAgehy2RctiDv4kdQsiF" +
                "h5O159NbThA9xoIweSL+59XDL0wSVWgMh/JclsQI2rstCohezMlWKMOSe4Kny5by/e+j0WQy3qP86pDy" +
                "GshSFZqYtguKVVgEbgjHhDgVZvjbx9lOFw7z45EwaR2PnoVoyx33o5GyQJ+Vhl3hapTBQuoyWDpFbzZ5" +
                "Nxnt8RuInz6lZ+lvUv6EA2JB1cE/tcv3n+eYkpJorhGzCxbQML0EU+4QaNnarGSps1MHaJ3XVcpA/PwN" +
                "nNdZz9Q+FuHOfF3yOoVHw9vbXSUPxC/nEkwJdxYdZXiOusjJp9k6JG0W2lZ8u/H14fe7QGRC2cEh9m3y" +
                "+gsc4jyZ2RQH5dcE4GvjhCduP97P96EG4tcIODRbMdrbA0giQ9YYhBoRZCcBo/SbccDB4GUWdUvPqD3H" +
                "2DWrzZKuNY6PypHmSetMesOyrNdxMOGFKAXLddtdViDTXlRcY2Jv0uItGaUhz1nGdpGnR598w6tsOk4a" +
                "BzQjSCuS85xuPk+8kyHputCYLeJ9vNdSojso46FoGkeX0N4xT3XCfjLsH5ySHAuEEYeqJXJVltjNmK5J" +
                "3poQuoPeWg+WJMstJTLaHxVa/ugu7XiBVgx6m8MsbGdXdiN2YL4Kpcdc6ZzMqUmNW5LSC622xRAZuH6L" +
                "zkNfswCkqhCLAn1OY1V/mzweQr526p4fmcyT5D8GgC4q3gsAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
