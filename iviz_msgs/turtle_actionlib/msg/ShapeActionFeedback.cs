/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [Preserve, DataContract (Name = "turtle_actionlib/ShapeActionFeedback")]
    public sealed class ShapeActionFeedback : IDeserializable<ShapeActionFeedback>, IActionFeedback<ShapeFeedback>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "feedback")] public ShapeFeedback Feedback { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ShapeActionFeedback()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Feedback = ShapeFeedback.Singleton;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ShapeActionFeedback(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeFeedback Feedback)
        {
            this.Header = Header;
            this.Status = Status;
            this.Feedback = Feedback;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ShapeActionFeedback(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Feedback = ShapeFeedback.Singleton;
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ShapeActionFeedback(ref b);
        }
        
        ShapeActionFeedback IDeserializable<ShapeActionFeedback>.RosDeserialize(ref Buffer b)
        {
            return new ShapeActionFeedback(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Status.RosSerialize(ref b);
            Feedback.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Status is null) throw new System.NullReferenceException(nameof(Status));
            Status.RosValidate();
            if (Feedback is null) throw new System.NullReferenceException(nameof(Feedback));
            Feedback.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Status.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "turtle_actionlib/ShapeActionFeedback";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "aae20e09065c3809e8a8e87c4c8953fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71W23LbNhB951dgRg+xO7XSJr2kntGDKimOOk7isdS+ekByRaIFQRUXyfr7ngUpinKs" +
                "Rg9JNLJ1A84enD272Hckc7KijC+JzLyqjVbpQ+UK9/KmlnrhpQ9OuPiSLEq5prdEeSqzf8SqfZOMvvAj" +
                "eb+4uUbMvOHxrmE3ECBjcmlzUZGXufRSrGqQV0VJ9krThjQTrdaUi/ir363JDbFxWSon8CzIkJVa70Rw" +
                "WORrkdVVFYzKpCfhVUVH+7FTGSHFWlqvsqClxfra5srw8pWVFTE6no7+DWQyEvPpNdYYR1nwCoR2QMgs" +
                "SadMgR9FEpTxr1/xhmSw3NZX+EgFUtAFF76UnsnS49qSY57SXSPGd83hhsCGOIQouRMX8bsHfHSXAkFA" +
                "gdZ1VooLML/b+bI2ACSxkVbJVBMDZ1AAqC9404vLHrKJ0Eaaeg/fIB5inANrOlw+01WJnGk+vQsFBMTC" +
                "ta03KsfSdBdBMq3IeAHfWWl3Ce9qQiaDt6wxFmFXzAhepXN1ppCAXGyVLxPnLaPHbDyoPPlKbjxZGwm/" +
                "RWYLvHB8TvCbfcE0H+5mH6bzDzdi/xiJH/CfbUlxmyilEzvybMiUWJ+sSXwrUBMbObcb1EGDOZ4s53/N" +
                "RA/zx2NMzkiwFsrChCmxRmcB393PZu/vlrNpB/zqGNhSRrA2bImUwx78DdzvvJArDycrz6e3nCB6jHVg" +
                "ikT8z2OAP5gkqtAYDlW51sQIyrs9CoheLMlWqD7NrcDTZUt58edkMptNe5RfH1PeAllmpSKm7ULGKqwC" +
                "94HnhDgVZvz7x/uDLhzmp2fCpHU8eh6iLQ/cn42UB/qsNOwKV6MMVlLpYOkUvfvZH7NJj99I/PwpPUt/" +
                "U+ZPOCAWVB38U7t8/3mOKWUSPTVidsEC+qSXYModAp1amY3UKj91gNZ5XaWMxC/fwHmd9UztYxEezNcl" +
                "r1N4Mr69PVTySPx6LsGUcFXRswzPURc5+TRbx6TNStmKLzW+Pny/C0QmlB8dom+TN1/gEOfJzKY4Kr8m" +
                "AF8bJzxx+3Gx7EONxG8RcGz2YrS3B5BEjqwxCDUiyE4CRhk2U4CDwXUedUvPqD3H2DWrzZJuFY6PypHm" +
                "SetMBmOt622cR3ghSsFy3XaXFci0FxXXmOiNVrwlpzQUBcvYLvL06JNveJXNp0njgGYEaUVyntPN54l3" +
                "MiTdlgqzRbyPey0luoNynoXmcXQJ7R3zVCfsJ8P+wSnJsUAYcahaI1daYzdjuiZ5W0LoDnpvPViSLLeU" +
                "yKg/KrT80V3a8QKtGPR2x1nYj6zsRuzAfBW0xzjpnCyoSY1bU6ZWKtsXQ2Tghi06z3rNApCqQiwK9DmF" +
                "VcN98ngI+Uqp88F6TQ9dBl8eTeQI+x+by3R90QsAAA==";
                
    }
}
