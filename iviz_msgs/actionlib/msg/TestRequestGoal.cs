/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestRequestGoal")]
    public sealed class TestRequestGoal : IDeserializable<TestRequestGoal>, IGoal<TestRequestActionGoal>
    {
        public const int TERMINATE_SUCCESS = 0;
        public const int TERMINATE_ABORTED = 1;
        public const int TERMINATE_REJECTED = 2;
        public const int TERMINATE_LOSE = 3;
        public const int TERMINATE_DROP = 4;
        public const int TERMINATE_EXCEPTION = 5;
        [DataMember (Name = "terminate_status")] public int TerminateStatus;
        [DataMember (Name = "ignore_cancel")] public bool IgnoreCancel; // If true, ignores requests to cancel
        [DataMember (Name = "result_text")] public string ResultText;
        [DataMember (Name = "the_result")] public int TheResult; // Desired value for the_result in the Result
        [DataMember (Name = "is_simple_client")] public bool IsSimpleClient;
        [DataMember (Name = "delay_accept")] public duration DelayAccept; // Delays accepting the goal by this amount of time
        [DataMember (Name = "delay_terminate")] public duration DelayTerminate; // Delays terminating for this amount of time
        [DataMember (Name = "pause_status")] public duration PauseStatus; // Pauses the status messages for this amount of time
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestGoal()
        {
            ResultText = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestRequestGoal(int TerminateStatus, bool IgnoreCancel, string ResultText, int TheResult, bool IsSimpleClient, duration DelayAccept, duration DelayTerminate, duration PauseStatus)
        {
            this.TerminateStatus = TerminateStatus;
            this.IgnoreCancel = IgnoreCancel;
            this.ResultText = ResultText;
            this.TheResult = TheResult;
            this.IsSimpleClient = IsSimpleClient;
            this.DelayAccept = DelayAccept;
            this.DelayTerminate = DelayTerminate;
            this.PauseStatus = PauseStatus;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal TestRequestGoal(ref Buffer b)
        {
            TerminateStatus = b.Deserialize<int>();
            IgnoreCancel = b.Deserialize<bool>();
            ResultText = b.DeserializeString();
            TheResult = b.Deserialize<int>();
            IsSimpleClient = b.Deserialize<bool>();
            DelayAccept = b.Deserialize<duration>();
            DelayTerminate = b.Deserialize<duration>();
            PauseStatus = b.Deserialize<duration>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestRequestGoal(ref b);
        }
        
        TestRequestGoal IDeserializable<TestRequestGoal>.RosDeserialize(ref Buffer b)
        {
            return new TestRequestGoal(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
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
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (ResultText is null) throw new System.NullReferenceException(nameof(ResultText));
        }
    
        public int RosMessageLength => 38 + BuiltIns.GetStringSize(ResultText);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "db5d00ba98302d6c6dd3737e9a03ceea";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE2XOwQqCQBSF4b1P4SOU1nIWpndRlIpO0G6Y9GID4ygzV6i3r8gMmu3/ceAoQ3EUcqhO" +
                "+zzhIOpzmkJds1Wg/iTZFRWHjK09qeAA6Zsij45FDSz2clYVJdt4GS4plHxf5Gw7G6HtlZGEwpGkyQXX" +
                "YdCh6sxgUTTSNKgDR1aZLrToJk2C8E7f8Q3Fp84zJ5zqR/1aaoWGgnayktRgwha1fAjZNDh6dbnwg1FO" +
                "bnn0BHB1ZNxCAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
