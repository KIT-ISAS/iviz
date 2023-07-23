/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingActionResult : IHasSerializer<AveragingActionResult>, IMessage, IActionResult<AveragingResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public AveragingResult Result { get; set; }
    
        public AveragingActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new AveragingResult();
        }
        
        public AveragingActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, AveragingResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public AveragingActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new AveragingResult(ref b);
        }
        
        public AveragingActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new AveragingResult(ref b);
        }
        
        public AveragingActionResult RosDeserialize(ref ReadBuffer b) => new AveragingActionResult(ref b);
        
        public AveragingActionResult RosDeserialize(ref ReadBuffer2 b) => new AveragingActionResult(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            Status.RosValidate();
            Result.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Status.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 8; // Result
            return size;
        }
    
        public const string MessageType = "actionlib_tutorials/AveragingActionResult";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTW8bNxC9768goEPsolaapE1TAzqosuq4cBJDVnspCoO7O9plyyVVfljWv88b7mol" +
                "OVajQxJB9toS+ebxzZvhvCVZkhN1emSyCMoarfK7xlf++aWV+jbIEL3w6ZGN78nJSplqRj7qIFx6ZKMv" +
                "/Mre3V6eI2bZ8njbshsIkDGldKVoKMhSBikWFuRVVZM703RPmok2SypF+jasl+SH2DivlRd4V2RwAK3X" +
                "InosClYUtmmiUYUMJIJqaG8/diojpFhKF1QRtXRYb12pDC9fONkQo+Pt6b9IpiBxdXGONcZTEYMCoTUQ" +
                "CkfSQzR8KbKoTHj1kjeIgfhrZv2Lv7PBfGXP8DlVyEXPQoRaBmZND0sIzYSlP0ew79pTDhEEKhHClV6c" +
                "pM/u8K8/FYgGLrS0RS1OcISbdaitASCJe+mUzDUxcAEpgPqMNz073UE2CdpIYzfwLeI2xjGwpsflM53V" +
                "SJ5mGXysoCQWLp29VyWW5usEUmhFJggY0Em3znhXGzIb/MZiYxF2pdTgKb23hUImSrFSoc58cIye0nKn" +
                "yuwr2fJgkWT8J1Jc4cHxOdNvNpXT/nMzfX9x9f5SbF4j8QN+sz8pbRO19GJNgZ2ZE+tTtInvBGpjI+cO" +
                "hdhhjifzqz+nYgfzxT4mZyQ6B2XhxpxYo6OAb2bT6bub+fSiB365D+yoIHgctkTKYQ/+BGXgg5CLACer" +
                "wKd3nCB6SAVhqkz8z2uAH5gkqdAaDuW51MQIKvgNCoiezMk1KEPNPSHQaUf59o/JZDq92KH8ap/yCsiy" +
                "qBUxbR8LVmERuSE8JcShMONfP8y2unCYH58Ik9t09DImW265PxmpjPRZadgV3qIMFlLp6OgQvdn09+lk" +
                "h99I/PQpPUf/UBEOOCAVlI3hsV2+/zzHnAqJ5pow+2ARDTNIMOUOgZatzL3Uqjx0gM55faWMxOtv4Lze" +
                "esaGVIRb8/XJ6xWejK+vt5U8Ej8fSzAn3Fn0JMNj1EVOPs3WPmmzUK7h242vj7DbBRITKvcOsWuTN1/g" +
                "EMfJzKbYK782AF8bBzxx/eF2vgs1Er8kwLHZiNHdHkASJbLGINSKIHsJGGXYjgMeBtdl0i0/ovY8Y1tW" +
                "myVdKRwflSPNo9aZDcZa21UaTHghSsFx3faXFch0FxXXmNiZsXhLSXmseMDa3GaBHkL2Da+yq4usdUA7" +
                "gnQi+cDp5vOkOxmSrmqF2SLdxzstJbmDSh6KrtLoErs75rFO2E+G/YNTkmeBMOJQs0SutMZuxvRt8laE" +
                "0D30xnqwJDluKYnR7qjQ8Ud36cYLtGLQW+9nYUFU5rL4l92IHe0gi7nSe1lRmxq/pEItVLEphsTADzt0" +
                "HvraBSDVxFQU6HMKq4ab5PEQ8tVTFyKSoyDX80fTeZYttJU8bPJo6ZR1d9JUmvqP5dIipU32EXX1k5AF" +
                "DAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<AveragingActionResult> CreateSerializer() => new Serializer();
        public Deserializer<AveragingActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<AveragingActionResult>
        {
            public override void RosSerialize(AveragingActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(AveragingActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(AveragingActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(AveragingActionResult msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(AveragingActionResult msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<AveragingActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out AveragingActionResult msg) => msg = new AveragingActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out AveragingActionResult msg) => msg = new AveragingActionResult(ref b);
        }
    }
}
