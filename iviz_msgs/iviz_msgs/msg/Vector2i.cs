/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Vector2i : IDeserializable<Vector2i>, IMessage
    {
        [DataMember (Name = "x")] public int X;
        [DataMember (Name = "y")] public int Y;
    
        /// Constructor for empty message.
        public Vector2i()
        {
        }
        
        /// Explicit constructor.
        public Vector2i(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        /// Constructor with buffer.
        internal Vector2i(ref Buffer b)
        {
            X = b.Deserialize<int>();
            Y = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Vector2i(ref b);
        
        Vector2i IDeserializable<Vector2i>.RosDeserialize(ref Buffer b) => new Vector2i(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
        }
        
        public void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector2i";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bd7b43fd41d4c47bf5c703cc7d016709";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsvMKzE2UqjgygTTlVxcAN81niARAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
