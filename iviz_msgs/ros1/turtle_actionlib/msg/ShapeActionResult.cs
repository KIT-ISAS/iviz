/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.TurtleActionlib
{
    [DataContract]
    public sealed class ShapeActionResult : IDeserializable<ShapeActionResult>, IHasSerializer<ShapeActionResult>, IMessage, IActionResult<ShapeResult>
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "status")] public ActionlibMsgs.GoalStatus Status { get; set; }
        [DataMember (Name = "result")] public ShapeResult Result { get; set; }
    
        public ShapeActionResult()
        {
            Status = new ActionlibMsgs.GoalStatus();
            Result = new ShapeResult();
        }
        
        public ShapeActionResult(in StdMsgs.Header Header, ActionlibMsgs.GoalStatus Status, ShapeResult Result)
        {
            this.Header = Header;
            this.Status = Status;
            this.Result = Result;
        }
        
        public ShapeActionResult(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ShapeResult(ref b);
        }
        
        public ShapeActionResult(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Status = new ActionlibMsgs.GoalStatus(ref b);
            Result = new ShapeResult(ref b);
        }
        
        public ShapeActionResult RosDeserialize(ref ReadBuffer b) => new ShapeActionResult(ref b);
        
        public ShapeActionResult RosDeserialize(ref ReadBuffer2 b) => new ShapeActionResult(ref b);
    
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
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Status.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 8; // Result
            return size;
        }
    
        public const string MessageType = "turtle_actionlib/ShapeActionResult";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c8d13d5d140f1047a2e4d3bf5c045822";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTXPbNhC981dgRofYndppkjZNPaODa6uOOk7isdVeOh0PSK5ItCDA4sOy/n3eghRN" +
                "OVajQxKNbNoS8Pbh7dvFviVZkhN1emSyCMoarfLbxlf++YWV+ibIEL3w6ZHd1LKla/JRB+HSI5t+4Vf2" +
                "7ubiBPHKjsPbjtlEgIgppStFQ0GWMkixtCCuqprckaY70kyyaakU6duwbskfY+OiVl7gXZEhJ7Vei+ix" +
                "KFhR2KaJRhUykAiqoa392KmMkKKVLqgiaumw3rpSGV6+dLIhRsfb03+RTEFifn6CNcZTEYMCoTUQCkfS" +
                "K1PhS5FFZcKrl7xBTMRf19a/+DubLFb2CJ9ThTwMLESoZWDWdN9CaCYs/QmCfded8hhBoBIhXOnFQfrs" +
                "Fv/6Q4Fo4EKtLWpxgCNcrUNtDQBJ3EmnZK6JgQtIAdRnvOnZ4QjZJGgjjd3Ad4gPMfaBNQMun+moRvI0" +
                "y+BjBSWxsHX2TpVYmq8TSKEVmSBgPifdOuNdXchs8huLjUXYlVKDp/TeFgqZKMVKhTrzwTF6SsutKrOv" +
                "ZMudBZLxn0hxhQfH50y/2VRN98/V7P35/P2F2Lym4gf8Zn9S2iZq6cWaAjszJ9an6BLfC9TFRs7dHQqi" +
                "wzw9W8z/nIkR5ottTM5IdA7Kwo05sUZ7AV9dz2bvrhaz8wH45Tawo4LgcdgSKYc9+BOUgQ9CLgOcrAKf" +
                "3nGC6D4VhKky8T+vCX5gkqRCZziUZ6uJEVTwGxQQPViQa1CGmntCoMOe8s0fZ2ez2fmI8qttyisgy6JW" +
                "xLR9LFiFZeSG8JQQu8Kc/vrh+kEXDvPjE2Fym45exmTLB+5PRiojfVYadoW3KIOlVDo62kXvevb77GzE" +
                "byp++pSeo3+oCDsckArKxvDYLt9/nmNOhURzTZhDsIiGGSSYcodAy1bmTmpV7jpA77yhUqbi9Tdw3mA9" +
                "Y0MqwgfzDckbFD47vbx8qOSp+HlfgjnhzqInGe6jLnLyaba2SZulcg3fbnx9hHEXSEyo3DrE2CZvvsAh" +
                "9pOZTbFVfl0AvjZ2eOLyw81iDDUVvyTAU7MRo789gCRKZI1BqBNBDhIwynE3DngYXJdJt3yP2vOMbVlt" +
                "lnSlcHxUjjSPWmc2OdXartJgwgtRCo7rdrisQKa/qLjGxGi+4i0l5bGqWMZ+UaD7kH3Dq2x+nnUO6EaQ" +
                "XiQfON18nnQnQ9JVrTBbpPt41FKSO6jkoWieRpfY3zGPdcJ+MuwfnJI8C4QRh5oWudIauxnTd8lbEUIP" +
                "0BvrwZLkuKUkRuNRoeeP7tKPF2jFoLfezsKSqMxl8S+7ETu6QRZzpfeyoi41vqVCLVWxKYbEwB/36Dz0" +
                "dQtAqompKNDnFFYdb5LHQ8hXSl2ILmi6HTL4fDSWZ9lSW8lTJs+UTll3K02lafhYtha5bLKPmNIuo/oL" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ShapeActionResult> CreateSerializer() => new Serializer();
        public Deserializer<ShapeActionResult> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ShapeActionResult>
        {
            public override void RosSerialize(ShapeActionResult msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ShapeActionResult msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ShapeActionResult msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ShapeActionResult msg) => msg.Ros2MessageLength;
            public override void RosValidate(ShapeActionResult msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<ShapeActionResult>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ShapeActionResult msg) => msg = new ShapeActionResult(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ShapeActionResult msg) => msg = new ShapeActionResult(ref b);
        }
    }
}
