/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Vector2i")]
    public sealed class Vector2i : IDeserializable<Vector2i>, IMessage
    {
        [DataMember (Name = "x")] public int X { get; set; }
        [DataMember (Name = "y")] public int Y { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Vector2i()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Vector2i(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Vector2i(ref Buffer b)
        {
            X = b.Deserialize<int>();
            Y = b.Deserialize<int>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Vector2i(ref b);
        }
        
        Vector2i IDeserializable<Vector2i>.RosDeserialize(ref Buffer b)
        {
            return new Vector2i(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector2i";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bd7b43fd41d4c47bf5c703cc7d016709";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE8vMKzE2UqjgygTTlVxcAN81niARAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
