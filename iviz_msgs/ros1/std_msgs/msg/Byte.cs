/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Byte : IDeserializable<Byte>, IMessage
    {
        [DataMember (Name = "data")] public byte Data;
    
        public Byte()
        {
        }
        
        public Byte(byte Data)
        {
            this.Data = Data;
        }
        
        public Byte(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Byte(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        public Byte RosDeserialize(ref ReadBuffer b) => new Byte(ref b);
        
        public Byte RosDeserialize(ref ReadBuffer2 b) => new Byte(ref b);
    
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
        }
    
        public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        public const string MessageType = "std_msgs/Byte";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ad736a2e8818154c487bb80fe42ce43b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE0uqLElVSEksSeTiAgAksd8TCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
