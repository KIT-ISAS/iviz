/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [DataContract]
    public sealed class TestActionFeedback : IDeserializable<TestActionFeedback>, IHasSerializer<TestActionFeedback>, IMessage, IActionFeedback<TestFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public TestFeedback Feedback { get; set; }
    
        public TestActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = new TestFeedback();
        }
        
        public TestActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        public TestActionFeedback(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new TestFeedback(ref b);
        }
        
        public TestActionFeedback(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = new TestFeedback(ref b);
        }
        
        public TestActionFeedback RosDeserialize(ref ReadBuffer b) => new TestActionFeedback(ref b);
        
        public TestActionFeedback RosDeserialize(ref ReadBuffer2 b) => new TestActionFeedback(ref b);
    
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
    
        public int RosMessageLength => 4 + Header.RosMessageLength + Status.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = Status.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Feedback
            return c;
        }
    
        public const string MessageType = "actionlib/TestActionFeedback";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "6d3d0bf7fb3dda24779c010a9f3eb7cb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYnVppkn6kntFBlRRHGSfx2GovnY4HBFYkWhJUAVCy/n3fghRF" +
                "OVajQxKNbH0Bbx/evl3sW5KanMjjSyJVMJUtTHpf+sw/v6pkcRdkqL3w8SVZkA9viHQq1T9i2b5JRl/4" +
                "kby/u7pESN3QeNuQGwhwsVo6LUoKUssgxbICd5Pl5C4KWlPBPMsVaRF/DdsV+SE2LnLjBZ4ZWXKyKLai" +
                "9lgUKqGqsqytUTKQCKakg/3YaayQYiVdMKoupMP6ymljefnSyZIYHU9P/9ZkFYn59BJrrCdVBwNCWyAo" +
                "R9Ibm+FHkdTGhlcveYMYiD9vK//ir2Sw2FQX+J4ypKJjIUIuA7Omh5Ujz4Slv0Sw75pTDhEEKhHCaS/O" +
                "4nf3+OjPBaKBC60qlYszHOFmG/LKApDEWjoj04IYWEEKoD7jTc/Oe8g2Qltpqx18g7iPcQqs7XD5TBc5" +
                "klewDL7OoCQWrly1NhpL020EUYUhGwT856TbJryrCZkM3rDYWIRdMTV4ld5XyiATWmxMyBMfHKPHtNwb" +
                "nXwlWx6tkYTfIsUZXjg+Z/r1rnCaDzezD9P5hyuxe4zED/jP/qS4TeTSiy0FdmZKrI9qEt8K1MRGzt0a" +
                "BdFgjieL+R8z0cN8cYjJGamdg7JwY0qs0UnAN7ez2fubxWzaAb88BHakCB6HLZFy2IO/QRn4IOQywMkm" +
                "8OkdJ4geYkHYLBH/8xjgDyaJKjSGQ3muCmIEE/wOBUTPFuRKlGHBPSHQeUv57vfJZDab9ii/OqS8AbJU" +
                "uSGm7WvFKixrbghPCXEszPi3j7d7XTjMj0+ESat4dF1HW+65PxlJ1/RZadgVvkIZLKUpakfH6N3O3s0m" +
                "PX4j8dOn9Bz9TSoccUAsqKoOj+3y/ec5pqQkmmvE7ILVaJhBgil3CLRsY9eyMPrYAVrndZUyEj9/A+d1" +
                "1rNViEW4N1+XvE7hyfj6el/JI/HLqQRTwp1FTzI8RV3k5NNsHZK2S+NKvt34+gj9LhCZkD44RN8mr7/A" +
                "IU6TmU1xUH5NAL42jnji+uPdog81Er9GwLHdidHeHkASGlljEGpEkJ0EjDJsxgEPgxc66paeUHuesStW" +
                "myXdGBwflSPto9aZDMZFUW3iYMILUQqO67a7rECmvai4xkRvxOItmtI6y1jGdlGgh5B8w6tsPk0aBzQj" +
                "SCuSD5xuPk+8kyHpJjeYLeJ93Gsp0R2keSiax9Glbu+YxzphP1n2D05JngXCiEPlCrkqCuxmTN8kb0MI" +
                "3UHvrAdLkuOWEhn1R4WWP7pLO16gFYPe9jALu9mV3YgdmK/qImCu9F5m1KTGr0iZpVG7YogM/LBF56Gv" +
                "WQBSZR2LAn3OYNVwlzweQr526p73R/KkGS67wTz5D/hPNOTfCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<TestActionFeedback> CreateSerializer() => new Serializer();
        public Deserializer<TestActionFeedback> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<TestActionFeedback>
        {
            public override void RosSerialize(TestActionFeedback msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(TestActionFeedback msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(TestActionFeedback msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(TestActionFeedback msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<TestActionFeedback>
        {
            public override void RosDeserialize(ref ReadBuffer b, out TestActionFeedback msg) => msg = new TestActionFeedback(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out TestActionFeedback msg) => msg = new TestActionFeedback(ref b);
        }
    }
}
