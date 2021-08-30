/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Char")]
    public sealed class Char : IDeserializable<Char>, IMessage
    {
        [DataMember (Name = "data")] public sbyte Data;
    
        /// <summary> Constructor for empty message. </summary>
        public Char()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Char(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Char(ref Buffer b)
        {
            Data = b.Deserialize<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Char(ref b);
        }
        
        Char IDeserializable<Char>.RosDeserialize(ref Buffer b)
        {
            return new Char(ref b);
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
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Char";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
