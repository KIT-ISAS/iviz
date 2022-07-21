/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestRequestActionGoal : IDeserializableCommon<TestRequestActionGoal>, IMessageCommon, IActionGoal<TestRequestGoal>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "goal_id")] public ActionlibMsgs.GoalID GoalId { get; set; }
        [DataMember (Name = "goal")] public TestRequestGoal Goal { get; set; }
    
        public TestRequestActionGoal()
        {
            GoalId = new ActionlibMsgs.GoalID();
            Goal = new TestRequestGoal();
        }
        
        public TestRequestActionGoal(in StdMsgs.Header Header, ActionlibMsgs.GoalID GoalId, TestRequestGoal Goal)
        {
            this.Header = Header;
            this.GoalId = GoalId;
            this.Goal = Goal;
        }
        
        public TestRequestActionGoal(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestRequestGoal(ref b);
        }
        
        public TestRequestActionGoal(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            GoalId = new ActionlibMsgs.GoalID(ref b);
            Goal = new TestRequestGoal(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TestRequestActionGoal(ref b);
        
        public TestRequestActionGoal RosDeserialize(ref ReadBuffer b) => new TestRequestActionGoal(ref b);
        
        public TestRequestActionGoal RosDeserialize(ref ReadBuffer2 b) => new TestRequestActionGoal(ref b);
    
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
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += GoalId.RosMessageLength;
                size += Goal.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            GoalId.AddRos2MessageLength(ref c);
            Goal.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "actionlib/TestRequestActionGoal";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "1889556d3fef88f821c7cb004e4251f3";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUTW/bMAy9+1cQyKHtgKZru10K5NAlXpth/UCSAQOGwWAkxhYmS54kJ/W/HyUnabfs" +
                "sMNqGBAsko+PfKRvCSU5qNKRoQjKGq2WRe1Lf3ZjUU8nUPJRKJktyIcZ/Wz5iJZ0n43+85PdzW+uwAfZ" +
                "U7jtiQ1gHtBIdBJqCigxIKws81ZlRe5U05o0B2HdkIRkDV1DfsiBi0p54LckQw617qD17BQsCFvXrVEC" +
                "A0FQNf0Wz5HKAEKDLijRanTsb51UJrqvHNYU0fn1sSVGEEwnV+xjPIk2KCbUMYJwhF6Zko2QtcqEy4sY" +
                "AAP4NrP+/Hs2WGzsKd9TyTLsWUCoMETW9NQ48pEw+itO9qavcshJuEvE6aSH43RX8Kc/Ac7GXKixooJj" +
                "LuGxC5U1DEiwRqdwqSkCC24Fox7FoKOTF8gmQRs0dgffIz7n+BdYs8eNNZ1WLJ6ObfBtyZ1kx8bZtZLs" +
                "uuwSiNCKTACePYeuy2JUnzIbfIzNZieOStLwid5boVgJCRsVqswHF9GTLHFUX2ks/7ofaca2ZMFXttWS" +
                "P6yjVFcqhLXcVIoFSUXEvYENenD9MpGMkzRNeqfZ5Jag2SZjkd2aR2NTkQEVgAslH6eX54LqJgA3nKMj" +
                "pu+nZkOceg8NS1pFLgiCXEBWLjJ62d8tfyV3mnB7mV4Xk+z7DCsiuUTxg5lJjuChbHXgZfQeS0oigG9I" +
                "qJUSfYFbBn64RY+b0jswqbr1gZkBrx97DXf6ReVeW7qzP35jWb+Ui3x2N72/XuTF/Mt4nM/no7cHlusP" +
                "D7NFPhmdH1hm+ad8HE0XB6bPD/N8dHlwPZk9PI7eHVznX8f542L6cD96v7UFcnX66RQsV2h9trRWgyoN" +
                "q1oI5NXUu/b1ohSBnsIuuKKiv92G+cKrutEcmfYtk63DNGaSNHYFCkHNwe2ewrOhQZ7UHaNfNHZXJEUG" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
