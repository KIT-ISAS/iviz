/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.ActionlibTutorials
{
    [DataContract]
    public sealed class AveragingActionResult : IDeserializable<AveragingActionResult>, IMessage, IActionResult<AveragingResult>
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
            if (Status is null) BuiltIns.ThrowNullReference();
            Status.RosValidate();
            if (Result is null) BuiltIns.ThrowNullReference();
            Result.RosValidate();
        }
    
        public int RosMessageLength => 8 + Header.RosMessageLength + Status.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Status.AddRos2MessageLength(ref c);
            Result.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "actionlib_tutorials/AveragingActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
