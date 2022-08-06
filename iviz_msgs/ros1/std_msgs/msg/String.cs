/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class String : IDeserializable<String>, IMessage
    {
        [DataMember (Name = "data")] public string Data;
    
        public String()
        {
            Data = "";
        }
        
        public String(string Data)
        {
            this.Data = Data;
        }
        
        public String(ref ReadBuffer b)
        {
            b.DeserializeString(out Data);
        }
        
        public String(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Data);
        }
        
        public String RosDeserialize(ref ReadBuffer b) => new String(ref b);
        
        public String RosDeserialize(ref ReadBuffer2 b) => new String(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + WriteBuffer.GetStringSize(Data);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.AddLength(c, Data);
            return c;
        }
    
        public const string MessageType = "std_msgs/String";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1dISSxJ5OICADpmzaUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
