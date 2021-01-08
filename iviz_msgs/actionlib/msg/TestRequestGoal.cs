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
        [DataMember (Name = "terminate_status")] public int TerminateStatus { get; set; }
        [DataMember (Name = "ignore_cancel")] public bool IgnoreCancel { get; set; } // If true, ignores requests to cancel
        [DataMember (Name = "result_text")] public string ResultText { get; set; }
        [DataMember (Name = "the_result")] public int TheResult { get; set; } // Desired value for the_result in the Result
        [DataMember (Name = "is_simple_client")] public bool IsSimpleClient { get; set; }
        [DataMember (Name = "delay_accept")] public duration DelayAccept { get; set; } // Delays accepting the goal by this amount of time
        [DataMember (Name = "delay_terminate")] public duration DelayTerminate { get; set; } // Delays terminating for this amount of time
        [DataMember (Name = "pause_status")] public duration PauseStatus { get; set; } // Pauses the status messages for this amount of time
    
        /// <summary> Constructor for empty message. </summary>
        public TestRequestGoal()
        {
            ResultText = "";
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
        public TestRequestGoal(ref Buffer b)
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
        
        public void RosValidate()
        {
            if (ResultText is null) throw new System.NullReferenceException(nameof(ResultText));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 38;
                size += BuiltIns.UTF8.GetByteCount(ResultText);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestRequestGoal";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "db5d00ba98302d6c6dd3737e9a03ceea";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACmXOSwrCMBSF4XnAPXQJPocZaLyDirYlqeAsxPaigZiW5BZ09yq+wEz/jwPHeppNsxrk" +
                "Li+WNWi1FwKU4mNm/2S5KmUNaz5JRMIGxJOmCW1LBXyW5LUsKz5PMhwEVHVeFnzxNsJwsd4Q6kiGhsiO" +
                "Xecye/JdQN0Y36BjkYL1pyxgHBxpwit9xmfUr/qeRR3tpXePpbPoibVDMGQ7n7XozE2bpsE+qd8LP+jN" +
                "ED+PRuwOHYVPLUMBAAA=";
                
    }
}
