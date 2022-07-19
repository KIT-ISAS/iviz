/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class String : IDeserializableRos1<String>, IDeserializableRos2<String>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public string Data;
    
        /// Constructor for empty message.
        public String()
        {
            Data = "";
        }
        
        /// Explicit constructor.
        public String(string Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public String(ref ReadBuffer b)
        {
            b.DeserializeString(out Data);
        }
        
        /// Constructor with buffer.
        public String(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new String(ref b);
        
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
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/String";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1dISSxJ5OICADpmzaUNAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
