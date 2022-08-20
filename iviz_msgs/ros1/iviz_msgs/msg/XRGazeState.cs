/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRGazeState : IDeserializable<XRGazeState>, IMessage
    {
        [DataMember (Name = "is_valid")] public bool IsValid;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "transform")] public GeometryMsgs.Transform Transform;
    
        public XRGazeState()
        {
        }
        
        public XRGazeState(bool IsValid, in StdMsgs.Header Header, in GeometryMsgs.Transform Transform)
        {
            this.IsValid = IsValid;
            this.Header = Header;
            this.Transform = Transform;
        }
        
        public XRGazeState(ref ReadBuffer b)
        {
            b.Deserialize(out IsValid);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Transform);
        }
        
        public XRGazeState(ref ReadBuffer2 b)
        {
            b.Deserialize(out IsValid);
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Transform);
        }
        
        public XRGazeState RosDeserialize(ref ReadBuffer b) => new XRGazeState(ref b);
        
        public XRGazeState RosDeserialize(ref ReadBuffer2 b) => new XRGazeState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(in Transform);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(in Transform);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 57 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c += 1; // IsValid
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 56; // Transform
            return c;
        }
    
        public const string MessageType = "iviz_msgs/XRGazeState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "b062fdd884f10dff560e6f7d4400606b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwW4TMRC9+ytGyqEtSoNoEYdK3BDQA1KhFReEosl6smvhtbe2N+ny9Tx7k00LRXCA" +
                "RpF21jvvzcybGa+8t2TicsPWaPVeWEugpjxULb6VFIZlG+v4/Cawi2sfWkp7Syn1+h//1IfrdxcUkx6D" +
                "jgmpGV0ndpqDJmTEmhMTEqDG1I2EUysbsQBx24mm8jUNncQFgDeNiSiQanES2NqB+gin5Knybds7U3ES" +
                "SqaVB3ggjSOmjkMyVW85wN8HbVx2XwduJbPjH+W2F1cJXb65gI+LUvXJIKEBDFUQjsbV+EiqNy6dn2UA" +
                "zejLJx9ffFWzm60/xbnU0H3KglLDKWctd12QmBPmeIFgz8YqFwgClQThdKTjcrbEazwhREMu0vmqoWOU" +
                "cDWkxjsQCm04GF5ZycQVpADrUQYdndxjdoXasfN7+pHxEONvaN3Em2s6bdA8m2WIfQ0l4dgFvzEarquh" +
                "kFTWiEtkzSpwGFRGjSHV7G0WG05AldbgyTH6yqATmrYmNSqmkNlLW5aY4/80lr9ZiP2UBcnNQhmxlDSt" +
                "Ca0kbUWg1tb/MkUxz9k6CMrtuMJQqc9SJR/OR7zlZLxTH3sAgoNJwafx7EmK3CXzSIlMm/Ltp/zzSlyW" +
                "2fUOK9AKo63YtgkJoDYBUNSwAKsEgUgyJ5NIe+jhfAJHy99AKRikjOauAxnf1yQfA3Isi3oxp20DfYtX" +
                "HoSyv2XjTUXB1EYfujGBmXbFzSmtzzBI1o45j8HQQpDs1T5Z0OWaBt/TNhcEI+wuGo/2TnmVPUjez/Mt" +
                "s6N4KOiVx7ZDlhi5xsq4mHDFoetr6zm9ekl3kzVM1vcnafVhxh7rtiMf8oqO8j3oeX67PQxoFvmPBe2t" +
                "rVI/AFE3cPiDBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
