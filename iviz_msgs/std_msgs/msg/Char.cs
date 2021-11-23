/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Char : IDeserializable<Char>, IMessage
    {
        [DataMember (Name = "data")] public sbyte Data;
    
        /// Constructor for empty message.
        public Char()
        {
        }
        
        /// Explicit constructor.
        public Char(sbyte Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        internal Char(ref Buffer b)
        {
            Data = b.Deserialize<sbyte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Char(ref b);
        
        Char IDeserializable<Char>.RosDeserialize(ref Buffer b) => new Char(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 1;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "std_msgs/Char";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "1bf77f25acecdedba0e224b162199717";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vOSCxSSEksSeQCADeiGH4KAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
