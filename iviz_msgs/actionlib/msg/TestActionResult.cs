/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TestActionResult")]
    public sealed class TestActionResult : IDeserializable<TestActionResult>, IActionResult<TestResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TestResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TestActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TestResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TestActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TestResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TestActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TestResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TestActionResult(ref b);
        }
        
        TestActionResult IDeserializable<TestActionResult>.RosDeserialize(ref Buffer b)
        {
            return new(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Result is null) throw new System.NullReferenceException(nameof(Result));
            Result.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TestActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d669e3a63aa986c667ea7b0f46ce85e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WTXPbNhC981dgxofYnVppk36kntFBlVRHGSfx2GqvHpBYkWhJUMWHZP37vgUoSnKs" +
                "RockGtu0JODtw9u3i31LUpEVVXxksvC6NbXOHxpXupfXrazvvfTBCRcf2ZycvyMXai9sfGTDL/zK3t9f" +
                "XyGcShTeJmJnAjyMklaJhrxU0kuxaMFblxXZy5pWVDPHZklKxG/9ZklugI3zSjuBn5IMWVnXGxEcFvlW" +
                "FG3TBKML6Ul43dDBfuzURkixlNbrItTSYn1rlTa8fGFlQ4yOH0f/BjIFidnkCmuMoyJ4DUIbIBSWpNOm" +
                "xJciC9r41694Q3Y2X7eXeEsl1O+DC19Jz2TpcQl9mad0V4jxXTrcANgQhxBFOXEeP3vAW3chEAQUaNkW" +
                "lTgH89uNr1oDQBIrabXMa2LgAgoA9QVvenGxh2witJGm3cInxF2MU2BNj8tnuqyQs5pP70IJAbFwaduV" +
                "VliabyJIUWsyXsByVtpNxrtSyOzsD9YYi7ArZgRP6VxbaCRAibX2Vea8ZfSYjQetsq/kxqNlkfG/yGyJ" +
                "B8fnBL/Z1kp6czv9MJl9uBbb11D8gL9sS4rbRCWd2JBnQ+bE+hQp8Z1AKTZybleog4Q5Gs9nf03FHuaP" +
                "h5ickWAtlIUJc2KNTgK+vZtO39/Op5Me+NUhsKWCYG3YEimHPfgTuN95IRceTtaeT285QfQY68CUmfif" +
                "1xl+YZKoQjIcqnJZEyNo77YoIHo+J9ug+mpuBZ4uOsr3f47H0+lkj/LrQ8prIMui0sS0XShYhUXgPvCc" +
                "EMfCjH7/eLfThcP89EyYvI1HVyHacsf92Ugq0GelYVe4FmWwkLoOlo7Ru5u+m473+A3Fz5/Ss/Q3Ff6I" +
                "A2JBtcE/tcv3n+eYUyHRUyNmHyygT3oJptwh0Km1Wclaq2MH6JzXV8pQ/PINnNdbz7Q+FuHOfH3yeoXH" +
                "o5ubXSUPxa+nEswJVxU9y/AUdZGTT7N1SNostG34UuPrw+93gciE1MEh9m3y5gsc4jSZ2RQH5ZcC8LVx" +
                "xBM3H+/n+1BD8VsEHJmtGN3tASShkDUGoSSC7CVglEGaAhwMXquoW35C7TnGblltlnStcXxUjjRPWmd2" +
                "Nqrrdh3nEV6IUrBct/1lBTLdRcU1JvamKt6iKA9lyTJ2izw9+uwbXmWzSZYckEaQTiTnOd18nngnQ9J1" +
                "pTFbxPt4r6VEd5DiWWgWR5fQ3TFPdcJ+MuwfnJIcC4QRh5olclXX2M2YLiVvTQjdQ2+tB0uS5ZYSGe2P" +
                "Ch1/dJduvEArBr3NYRYWRCqXxT/sRuxI8yvGSedkSSk1bkmFXuhiWwyRgRt06DzrpQUg1YRYFOhzGqsG" +
                "2+TxEPK1U/dyN4VnaaLsZvH/AE+90CzNCwAA";
                
    }
}
