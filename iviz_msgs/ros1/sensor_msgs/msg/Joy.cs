/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Joy : IDeserializable<Joy>, IMessage
    {
        // Reports the state of a joysticks axes and buttons.
        /// <summary> Timestamp in the header is the time the data is received from the joystick </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        /// <summary> The axes measurements from a joystick </summary>
        [DataMember (Name = "axes")] public float[] Axes;
        /// <summary> The buttons measurements from a joystick </summary>
        [DataMember (Name = "buttons")] public int[] Buttons;
    
        public Joy()
        {
            Axes = System.Array.Empty<float>();
            Buttons = System.Array.Empty<int>();
        }
        
        public Joy(in StdMsgs.Header Header, float[] Axes, int[] Buttons)
        {
            this.Header = Header;
            this.Axes = Axes;
            this.Buttons = Buttons;
        }
        
        public Joy(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Axes);
            b.DeserializeStructArray(out Buttons);
        }
        
        public Joy(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStructArray(out Axes);
            b.DeserializeStructArray(out Buttons);
        }
        
        public Joy RosDeserialize(ref ReadBuffer b) => new Joy(ref b);
        
        public Joy RosDeserialize(ref ReadBuffer2 b) => new Joy(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Axes);
            b.SerializeStructArray(Buttons);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Axes);
            b.SerializeStructArray(Buttons);
        }
        
        public void RosValidate()
        {
            if (Axes is null) BuiltIns.ThrowNullReference();
            if (Buttons is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 4 * Axes.Length;
                size += 4 * Buttons.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Axes);
            WriteBuffer2.AddLength(ref c, Buttons);
        }
    
        public const string MessageType = "sensor_msgs/Joy";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "5a9ea5f83505693b71e785041e67a8bb";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TTWvcMBC961cM+JCksCltbwu9lX4cCiXJLYRlVpq1ldiSq5E38b/vkxxnQw+hhwrh" +
                "j/l4b/Rm1NCVjDFlpdwJaeYsFA/EdB9nzd4+KPGT4BEc7aecY9BL813YSaJueZ1WQ9kPApBhJB8q4nOM" +
                "X/CLu344zlyMSaz4ozg6pDhUz8prDn3k/Onj7d1SwGsShFXbIKxTkkEC6q8Ip7qND0v2c9V/5a/WtyDI" +
                "mM//eZmf19+2kNntBm31/SKkaeg6Q2BODvVkruIcIgT2bSdp08tReqqyQqnqzfMoaERDNx1UxG4lSOK+" +
                "n2lSBOVINg7DFLwtHX1py5qPTDSIaeSEk049J8TH5Hwo4YfEgxR0bJXfkwQr9OPLFjFBxU4ZLQOTDzZB" +
                "Ph9aOMlMVfGSAI1vr6J+uDPNzWPcwC4thuA0HLnjXKqWpzGJloJZtyB7t5zyEiRQSUDnlM6rbYdfvSCw" +
                "oRaMrO3oHEf4NecuLqN25OR530sBtpACqGcl6eziFXKo0IFDXOEXxBPHv8CGF9xypk2H5vVFBp1arsM+" +
                "pnj0DqH7uYLY3mPCqPf7xGk29R5UStN8LWIjCFnrrWDVaD064ejR585oTgW9tmXnnTF/APWAXZq2AwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
