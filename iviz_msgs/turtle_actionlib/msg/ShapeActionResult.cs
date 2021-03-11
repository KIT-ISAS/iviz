/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionResult")]
    public sealed class ShapeActionResult : IDeserializable<ShapeActionResult>, IActionResult<ShapeResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ShapeResult Result { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ShapeResult();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeActionResult(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ShapeResult(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionResult(ref b);
        }
        
        ShapeActionResult IDeserializable<ShapeActionResult>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionResult(ref b);
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
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionResult";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WwW4bNxC9L6B/IOBD7KB22qRpUwM6qLLqKHASw1Z7NbjL0S5bLrkluZL1933DlVZS" +
                "LSM6JBFsryWRbx7fvBnOe5KKvKjSI5NF1M4anT/UoQyvrp0091HGNoiQHtl9JRu6o9CaKHx6DLLhV34N" +
                "so/315eIqDoW7xO3QXYiwMUq6ZWoKUoloxRzB+66rMifG1qQYZ51Q0qkb+OqoXCBjbNKB4Gfkix5acxK" +
                "tAGLohOFq+vW6kJGElHXtLcfO7UVUjTSR120Rnqsd15py8vnXtbE6PgJ9G9LtiAxvbrEGhuoaKMGoRUQ" +
                "Ck8yaFviS5G12sY3r3lDdjJbunO8pRIZ6IOLWMnIZOmxgcTMU4ZLxHjZHe4C2FCHEEUFcZo+e8DbcCYQ" +
                "BBSocUUlTsH8dhUrZwFIYiG9lrkhBi6gAFBf8KYXZzvITPtSWGndBr5D3MY4Btb2uHym8wo5M3z60JYQ" +
                "EAsb7xZaYWm+SiCF0WSjgO289KuMd3Uhs5M/WGMswq6UETxlCK7QSIASSx2rLETP6CkbD1pl38yQzxbH" +
                "IOP/kdwSD6bAOX63KZnuze3k09X007XYvIbiR/xlZ1LaJioZxIoiezInlqjocr/WqAuOtPsF6rTDHI1n" +
                "078mYgfzp31MTkrrPcSFD3NimY4Cvr2bTD7eziZXPfDrfWBPBcHdcCayDofwJyiAEIWcR5hZRz695xzR" +
                "YyoFW2Zbok9fJ/iFT5IKnedQmI0hRtAxbFBA9HRGvkYBGu4Gkc7WlO//HI8nk6sdym/2KS+BLItKo0so" +
                "WLFgFeYtt4JDQjwXZvT757utLhzm5wNhcpeOrtrkzC33g5FUS1+Uhl0RHCphLrVpPT1H727yYTLe4TcU" +
                "b5/S8/Q3FczvIB2uKdfG/9vlhy9zzKmQaKsJsw/WolVGCabcJNCstV1Io9VzB1g7r6+UofjlOzivt551" +
                "MRXh1nx98nqFx6Obm20lD8WvxxLMCbcVHWR4jLrIydNs7ZO2c+1rvtf4BunTkFozMyG1d4hdm7z7Coc4" +
                "TmY2xV75dQH45njGEzef72e7UEPxWwIc2Y0Y6wsESEIhawxCnQiyl4BRLrpBIMDgRiXd8iNqLzC2Y7VZ" +
                "0qXG8VE5iLXfOrOTkTFumUYSXohSwD9ue1+BzPqu4hoTO8MVb1GUt2XJMq4XRXqM2Xe9zaZXPGSxCbpB" +
                "ZK1TiJxxPlK6maHqstKYMNKtvNNVkkFI8UQ0TQNMmrEOSIX9ZNlCOCgF1giDDtUN0mUMdjNm6PK3JITu" +
                "oTfugyvJc1dJjHYHhjV/NJj1kIFuDHqr/UTMiVQui3/YkNjRDbIYKkOQJWcY2QkNFXqui009JAaBDcTo" +
                "PPF1C0CqblNdoNVprLrY5A+rvl32YuujoYc+ia92BvNBls2Nkzxl8nDptfMP0paG+o9l45DOepD9BzLL" +
                "pzr+CwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
