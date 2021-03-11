/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/String")]
    public sealed class String : IDeserializable<String>, IMessage
    {
        [DataMember (Name = "data")] public string Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public String()
        {
            Data = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public String(string Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public String(ref Buffer b)
        {
            Data = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new String(ref b);
        }
        
        String IDeserializable<String>.RosDeserialize(ref Buffer b)
        {
            return new String(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += BuiltIns.UTF8.GetByteCount(Data);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/String";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "992ce8a1687cec8c8bd883ec73ca41d1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1dISSxJ5OLlAgCAhD+7DgAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
