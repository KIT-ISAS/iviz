/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Actionlib
{
    [Preserve, DataContract (Name = "actionlib/TwoIntsActionResult")]
    public sealed class TwoIntsActionResult : IDeserializable<TwoIntsActionResult>, IActionResult<TwoIntsResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public TwoIntsResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public TwoIntsActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new TwoIntsResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public TwoIntsActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, TwoIntsResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public TwoIntsActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new TwoIntsResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionResult(ref b);
        }
        
        TwoIntsActionResult IDeserializable<TwoIntsActionResult>.RosDeserialize(ref Buffer b)
        {
            return new TwoIntsActionResult(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Result.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                int size = 8;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "actionlib/TwoIntsActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ba7dea8b8cddcae4528ade4ef74b6e7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1W0XIaNxR93xn+QTM8xO7UTpukaeoZHiimDh0n8di0rx6tdNlVq9VSSQvm73uuFhao" +
                "YcJDagZ7wZbOPTr33Kv7kaQmL8r0yKSKpnbW5I9VKMLrm1rahyhjE0RIj2y6rCcuhnsKjY3Cp0cvG3zj" +
                "Vy/79HBzhZi65fExsetlfQE2TkuvRUVRahmlmNVgb4qS/IWlBVlmWs1Ji/TfuJpTuMTGaWmCwLsgR15a" +
                "uxJNwKJYC1VXVeOMkpFENBXt7cdO44QUc+mjUY2VHutrr43j5TMvK2J0vAP905BTJCbXV1jjAqkmGhBa" +
                "AUF5ksG4Av8UWWNcfPuGN2R96HmBr1QgB11wEUsZmSw9zSEx85ThCjG+aw93CWyoQ4iigzhLf3vE13Au" +
                "EAQUaF6rUpyB+d0qlrUDIImF9EbmlhhYQQGgvuJNr853kJn2lXDS1Rv4FnEb4xRY1+HymS5K5Mzy6UNT" +
                "QEAsnPt6YTSW5qsEoqwhFwWM56VfZbyrDZn1f2ONsQi7UkbwlCHUyiABWixNLLMQPaOnbDwanf1vhjxa" +
                "Hr2MPyO5BR5MgXP8YVM07Ze78efryecbsXkNxA/4zc6ktE2UMogVRfZkTiyRanO/1qgNjrT7BSq1xRyO" +
                "ppM/x2IH88d9TE5K4z3EhQ9zYplOAr67H48/3U3H1x3wm31gT4rgbjgTWYdD+C8ogBCFnEWY2UQ+vecc" +
                "0VMqBVdkW6LPX338wCdJhdZzKMy5JUYwMWxQQPRsSr5CAVruBpHO15Qf/hiNxuPrHcpv9ykvgSxVadAl" +
                "NKyoWIVZw63gkBDHwgx//XK/1YXDvDsQJq/T0XWTnLnlfjCSbuir0rArQo1KmEljG0/H6N2Pfx+PdvgN" +
                "xE/P6Xn6ixTzO0iHa6pu4n/t8v3XOeakJNpqwuyCNWiVUYIpNwk0a+MW0hp97ABr53WVMhDvX8B5nfVc" +
                "HVMRbs3XJa9TeDS8vd1W8kD8fCrBnHBb0UGGp6iLnDzP1j5pNzO+4nuNb5AuDak1MxPSe4fYtcmHb3CI" +
                "02RmU+yVXxuAb44jnrj98jDdhRqIXxLg0G3EWF8gQBIaWWMQakWQnQSMctkOAgEGtzrplp9Qe4Gxa1ab" +
                "JV0aHB+Vg1j7rTPrD62tl2kk4YUoBXyot/cVyKzvKq4xsTNe8RZNeVMULON6UaSnmL3obTa55iGLTdAO" +
                "ImudQuSM85HSzQxVl6XBhJFu5Z2ukgxCmieiSRpg0ox1QCrsJ8cWwkEpsEYYdKiaI13WYjdjhjZ/S0Lo" +
                "DnrjPriSPHeVxGh3YFjzN3ozZKAbgx4a3W4iZkQ6l+pvNiR2tIMshsoQZMEZRnbCnJSZGbWph8QgsIEY" +
                "nSe+dgFIVU2qC7Q6g1WXm/xh1Qtk7/XeTN7LUDHv36HAql72LxLm0xXdCwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
