/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestGoal : IDeserializable<TestRequestGoal>, IMessage, IGoal<TestRequestActionGoal>
    {
        public const int TERMINATE_SUCCESS = 0;
        public const int TERMINATE_ABORTED = 1;
        public const int TERMINATE_REJECTED = 2;
        public const int TERMINATE_LOSE = 3;
        public const int TERMINATE_DROP = 4;
        public const int TERMINATE_EXCEPTION = 5;
        [DataMember (Name = "terminate_status")] public int TerminateStatus;
        /// <summary> If true, ignores requests to cancel </summary>
        [DataMember (Name = "ignore_cancel")] public bool IgnoreCancel;
        [DataMember (Name = "result_text")] public string ResultText;
        /// <summary> Desired value for the_result in the Result </summary>
        [DataMember (Name = "the_result")] public int TheResult;
        [DataMember (Name = "is_simple_client")] public bool IsSimpleClient;
        /// <summary> Delays accepting the goal by this amount of time </summary>
        [DataMember (Name = "delay_accept")] public duration DelayAccept;
        /// <summary> Delays terminating for this amount of time </summary>
        [DataMember (Name = "delay_terminate")] public duration DelayTerminate;
        /// <summary> Pauses the status messages for this amount of time </summary>
        [DataMember (Name = "pause_status")] public duration PauseStatus;
    
        public TestRequestGoal()
        {
            ResultText = "";
        }
        
        public TestRequestGoal(ref ReadBuffer b)
        {
            b.Deserialize(out TerminateStatus);
            b.Deserialize(out IgnoreCancel);
            b.DeserializeString(out ResultText);
            b.Deserialize(out TheResult);
            b.Deserialize(out IsSimpleClient);
            b.Deserialize(out DelayAccept);
            b.Deserialize(out DelayTerminate);
            b.Deserialize(out PauseStatus);
        }
        
        public TestRequestGoal(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out TerminateStatus);
            b.Deserialize(out IgnoreCancel);
            b.Align4();
            b.DeserializeString(out ResultText);
            b.Align4();
            b.Deserialize(out TheResult);
            b.Deserialize(out IsSimpleClient);
            b.Align4();
            b.Deserialize(out DelayAccept);
            b.Deserialize(out DelayTerminate);
            b.Deserialize(out PauseStatus);
        }
        
        public TestRequestGoal RosDeserialize(ref ReadBuffer b) => new TestRequestGoal(ref b);
        
        public TestRequestGoal RosDeserialize(ref ReadBuffer2 b) => new TestRequestGoal(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(TerminateStatus);
            b.Serialize(IgnoreCancel);
            b.Serialize(ResultText);
            b.Serialize(TheResult);
            b.Serialize(IsSimpleClient);
            b.Serialize(DelayAccept);
            b.Serialize(DelayTerminate);
            b.Serialize(PauseStatus);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(TerminateStatus);
            b.Serialize(IgnoreCancel);
            b.Serialize(ResultText);
            b.Serialize(TheResult);
            b.Serialize(IsSimpleClient);
            b.Serialize(DelayAccept);
            b.Serialize(DelayTerminate);
            b.Serialize(PauseStatus);
        }
        
        public void RosValidate()
        {
            if (ResultText is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 38 + WriteBuffer.GetStringSize(ResultText);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // TerminateStatus
            c += 1; // IgnoreCancel
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, ResultText);
            c = WriteBuffer2.Align4(c);
            c += 4; // TheResult
            c += 1; // IsSimpleClient
            c = WriteBuffer2.Align4(c);
            c += 8; // DelayAccept
            c += 8; // DelayTerminate
            c += 8; // PauseStatus
            return c;
        }
    
        public const string MessageType = "actionlib/TestRequestGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "db5d00ba98302d6c6dd3737e9a03ceea";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE2XOwQqCQBSF4b1P4SOU1nIWpndRlIpO0G6Y9GID4ygzV6i3r8gMmu3/ceAoQ3EUcqhO" +
                "+zzhIOpzmkJds1Wg/iTZFRWHjK09qeAA6Zsij45FDSz2clYVJdt4GS4plHxf5Gw7G6HtlZGEwpGkyQXX" +
                "YdCh6sxgUTTSNKgDR1aZLrToJk2C8E7f8Q3Fp84zJ5zqR/1aaoWGgnayktRgwha1fAjZNDh6dbnwg1FO" +
                "bnn0BHB1ZNxCAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
