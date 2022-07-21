/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeActionGoal : IDeserializableCommon<ShapeActionGoal>, IMessageCommon, IActionGoal<ShapeGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public ShapeGoal Goal { get; set; }
    
        public ShapeActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new ShapeGoal();
        }
        
        public ShapeActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, ShapeGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public ShapeActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ShapeGoal(ref b);
        }
        
        public ShapeActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new ShapeGoal(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new ShapeActionGoal(ref b);
        
        public ShapeActionGoal RosDeserialize(ref ReadBuffer b) => new ShapeActionGoal(ref b);
        
        public ShapeActionGoal RosDeserialize(ref ReadBuffer2 b) => new ShapeActionGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            GoalId.RosSerialize(ref b);
            Goal.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (GoalId is null) BuiltIns.ThrowNullReference();
            GoalId.RosValidate();
            if (Goal is null) BuiltIns.ThrowNullReference();
            Goal.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + GoalId.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            GoalId.AddRos2MessageLength(ref c);
            Goal.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "turtle_actionlib/ShapeActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dbfccd187f2ec9c593916447ffd6cc77";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUwWobMRC96ysGfEhSsEPbW6C30CSHQmlyK8WMpfGuqFbaarR29+/7pHXcFHrooTEL" +
                "i7Qzb97Me+N7YSeZ+vYybItPMfjddtBOr+8Sh4db6vDaemceex6l3rUb8+E//8ynx7sb0uKW4vcLpRU9" +
                "Fo6Os6NBCjsuTPsExr7rJa+DHCQgiYdRHLWvZR5FN0h86r0Snk6iZA5hpkkRVBLZNAxT9JaLUPGD/JGP" +
                "TB+JaeRcvJ0CZ8Sn7Hys4fvMg1R0PCo/JolW6OH2BjFRxU7Fg9AMBJuF1ccOH8lMPpb372oCrejrl6Rv" +
                "v5nV0zGtcS8dBDizoNJzqazl55hFK2HWGxR7s3S5QRFMSVDOKV22uy2OekWoBi4yJtvTJVr4PJc+RQAK" +
                "HTh73gWpwBajAOpFTbq4eoEcG3TkmJ7hF8TfNf4FNp5xa0/rHuKFOgadOkwSgWNOB+8QupsbiA1eYiG4" +
                "LnOeTc1aSprVxzpsBCGrSYM3qybroYSjoy+90ZIrepOlmvSVbPnXzWgeO5El7dMUHA4pS+urNQItj72H" +
                "IK2Jujd0ZKVcnaNoojrpoendvImRcDwVg8j5AGsce4nkC6FR0epe+EKGsRAGjuyKqYtrjoLSZ2jayb5y" +
                "YbKSC0O5yujlfE/8vXvWBOMFvbkWOc+Z9iJux/Y7mDlkwJRTKFhGVe6kiUA6ivV7b5cGTwx0c0Kvm7IE" +
                "gNQwaQEzwvohavOsX1XulaQrUy5BtmcFr8//Y8YsaymuEzX7kLieMjs/qfkFSRBq0hoFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
