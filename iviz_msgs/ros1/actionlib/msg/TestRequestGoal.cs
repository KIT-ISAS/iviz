/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestGoal : IDeserializableRos1<TestRequestGoal>, IMessageRos1, IGoal<TestRequestActionGoal>
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
    
        /// Constructor for empty message.
        public TestRequestGoal()
        {
            ResultText = "";
        }
        
        /// Constructor with buffer.
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
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new TestRequestGoal(ref b);
        
        public TestRequestGoal RosDeserialize(ref ReadBuffer b) => new TestRequestGoal(ref b);
    
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
        
        public void RosValidate()
        {
            if (ResultText is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 38 + WriteBuffer.GetStringSize(ResultText);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "actionlib/TestRequestGoal";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "db5d00ba98302d6c6dd3737e9a03ceea";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE2XOwQqCQBSF4b1P4SOU1nIWpndRlIpO0G6Y9GID4ygzV6i3r8gMmu3/ceAoQ3EUcqhO" +
                "+zzhIOpzmkJds1Wg/iTZFRWHjK09qeAA6Zsij45FDSz2clYVJdt4GS4plHxf5Gw7G6HtlZGEwpGkyQXX" +
                "YdCh6sxgUTTSNKgDR1aZLrToJk2C8E7f8Q3Fp84zJ5zqR/1aaoWGgnayktRgwha1fAjZNDh6dbnwg1FO" +
                "bnn0BHB1ZNxCAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
