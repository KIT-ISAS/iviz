/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRHandState : IDeserializable<XRHandState>, IMessage
    {
        [DataMember (Name = "is_valid")] public bool IsValid;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "palm")] public GeometryMsgs.Transform Palm;
        [DataMember (Name = "thumb")] public GeometryMsgs.Transform[] Thumb;
        [DataMember (Name = "index")] public GeometryMsgs.Transform[] Index;
        [DataMember (Name = "middle")] public GeometryMsgs.Transform[] Middle;
        [DataMember (Name = "ring")] public GeometryMsgs.Transform[] Ring;
        [DataMember (Name = "little")] public GeometryMsgs.Transform[] Little;
    
        public XRHandState()
        {
            Thumb = System.Array.Empty<GeometryMsgs.Transform>();
            Index = System.Array.Empty<GeometryMsgs.Transform>();
            Middle = System.Array.Empty<GeometryMsgs.Transform>();
            Ring = System.Array.Empty<GeometryMsgs.Transform>();
            Little = System.Array.Empty<GeometryMsgs.Transform>();
        }
        
        public XRHandState(ref ReadBuffer b)
        {
            b.Deserialize(out IsValid);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Palm);
            b.DeserializeStructArray(out Thumb);
            b.DeserializeStructArray(out Index);
            b.DeserializeStructArray(out Middle);
            b.DeserializeStructArray(out Ring);
            b.DeserializeStructArray(out Little);
        }
        
        public XRHandState(ref ReadBuffer2 b)
        {
            b.Deserialize(out IsValid);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Palm);
            b.DeserializeStructArray(out Thumb);
            b.DeserializeStructArray(out Index);
            b.DeserializeStructArray(out Middle);
            b.DeserializeStructArray(out Ring);
            b.DeserializeStructArray(out Little);
        }
        
        public XRHandState RosDeserialize(ref ReadBuffer b) => new XRHandState(ref b);
        
        public XRHandState RosDeserialize(ref ReadBuffer2 b) => new XRHandState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(in Palm);
            b.SerializeStructArray(Thumb);
            b.SerializeStructArray(Index);
            b.SerializeStructArray(Middle);
            b.SerializeStructArray(Ring);
            b.SerializeStructArray(Little);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(in Palm);
            b.SerializeStructArray(Thumb);
            b.SerializeStructArray(Index);
            b.SerializeStructArray(Middle);
            b.SerializeStructArray(Ring);
            b.SerializeStructArray(Little);
        }
        
        public void RosValidate()
        {
            if (Thumb is null) BuiltIns.ThrowNullReference();
            if (Index is null) BuiltIns.ThrowNullReference();
            if (Middle is null) BuiltIns.ThrowNullReference();
            if (Ring is null) BuiltIns.ThrowNullReference();
            if (Little is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 77;
                size += Header.RosMessageLength;
                size += 56 * Thumb.Length;
                size += 56 * Index.Length;
                size += 56 * Middle.Length;
                size += 56 * Ring.Length;
                size += 56 * Little.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // IsValid
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 56; // Palm
            c += 4; // Thumb length
            c = WriteBuffer2.Align8(c);
            c += 56 * Thumb.Length;
            c += 4; // Index length
            c = WriteBuffer2.Align8(c);
            c += 56 * Index.Length;
            c += 4; // Middle length
            c = WriteBuffer2.Align8(c);
            c += 56 * Middle.Length;
            c += 4; // Ring length
            c = WriteBuffer2.Align8(c);
            c += 56 * Ring.Length;
            c += 4; // Little length
            c = WriteBuffer2.Align8(c);
            c += 56 * Little.Length;
            return c;
        }
    
        public const string MessageType = "iviz_msgs/XRHandState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "7e63e355743ca3360c1e27ce5a4ea185";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwW7bMAy96ysI5NB2SDOsHXoosNuwrYcB3VrsUhQBYzG2MFlyJTmp9/V7shOn3dps" +
                "h61BAMsm3yP5SGrhvSUT5yu2RqtPwloCVf1DleJrSaGb17GMr68Du7j0oaaGbf2c8eaWUtXWiz1247Tc" +
                "77HXRmsrexyCceUeszUpAa/Uu3/8U5+vPp5TTHqIOYilJnSV2GkOmpAQa05MyIQqU1YSjq2sxALEdSOa" +
                "emvqGokzAK8rEyE+leIksLUdtRFOyVPh67p1puAklEwtj/BAGkeMPoRkitZygL8P2rjsvgxcS2bHP8pd" +
                "K64Qunh/Dh8XpWiTQUIdGIogHCEljKRa49LpSQbQhG6++vjmVk2u1/4Y36XETIxZoL+cctZy3wSJOWGO" +
                "5wj2aqhyhiBQSRBORzrsv83xGo8I0ZCLNL6o6BAlXHap8g6EQisOhhdWMnEBKcB6kEEHRw+YXU/t2Pkt" +
                "/cC4i/E3tG7kzTUdV2iezTLEtoSScGyCXxkN10XXkxTWiEuYq0Xg0KmMGkKqyYcsNpyA6luDJ8foC4NO" +
                "aFqbVKmY8rwObZljx/7TWD6zD9spC5KbhTJiX1Ial3khaS0Ctdb+tymKec6WQVBuwwWGSn2TIvlwOuAt" +
                "J+Od+tICEByOFHwavr1IkZtkniiRadXbfsk/r8RFP7veYQVqYbQV2zYiAdQmAIoaZmCVIBBJpmQSaQ89" +
                "nE/gqPk7KAWDlNHcNCDjh5rkz4AcyqycTWldQd/eKw9Cv7/9xpsCV1lp9K4bI5hpU9yU0vIEg2TtkPMQ" +
                "DC0EyVbtoxldLKnzLa1zQTiEzUXj0d4xr34PkvfTfMtsKB4Leumx7ZAlRi6xMi4mXHHo+tJ6Tmdv6X48" +
                "dePpx4u0ejdjT3XbkQ95RQf5HvU8v93tBjSL/MeCtqe1Uj8B/XZ3hB8HAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
