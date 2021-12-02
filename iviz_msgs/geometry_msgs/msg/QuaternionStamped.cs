/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class QuaternionStamped : IDeserializable<QuaternionStamped>, IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "quaternion")] public Quaternion Quaternion;
    
        /// Constructor for empty message.
        public QuaternionStamped()
        {
        }
        
        /// Explicit constructor.
        public QuaternionStamped(in StdMsgs.Header Header, in Quaternion Quaternion)
        {
            this.Header = Header;
            this.Quaternion = Quaternion;
        }
        
        /// Constructor with buffer.
        internal QuaternionStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Quaternion);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new QuaternionStamped(ref b);
        
        QuaternionStamped IDeserializable<QuaternionStamped>.RosDeserialize(ref Buffer b) => new QuaternionStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Quaternion);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 32 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/QuaternionStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVTwYrcMAy9+ysEc9jdwkyhLT0M9Fba3UOhZfc+aGIlMSR2VlZmNv36PiedTKEUemiD" +
                "wXIsPT09yRt6akMmlUElS7RMHClpgMkWUqRzsBbXtajESqhKSX2IbEK1ci9w92Shl2zcDzvn7oW9KLXz" +
                "5r6N8NRYgJ5X07kP//hzXx4/7ymbP/S5ya8XDm5DjwZ6rJ56MfZsTHUCt9C0ottOTtLRzFs8zbc2DZJ3" +
                "7qcoWI1EUe66icYMJ0sQoO/HGKqiwFr3JR6RIRLTwGqhGjvW3wQr6FhZnsdZ0IePe/jELNVoAYQmIFQq" +
                "nENscEluDNHevikBbvN0TlscpYHCa3Kylq2QlZfSxMKT8x45Xi3F7YANcQRZfKbb+d8Bx3xHSAIKMqSq" +
                "pVsw/zpZi1ZZK3RiDXzspABXUACoNyXo5u4X5EJ7T5FjusAviNccfwMbV9xS07ZFz7pSfR4bCAjHQdMp" +
                "eLgepxmk6sp8UheOyjq5ErWkdJtP81BaaZ8vHcHOOacqoAF+HmaXTQv63I1D8P9rGhtJmDqdlpG8PoTL" +
                "dP35yUGxWgUlDQwtcbo+nTK/PV5Z3SW29+/oZbWm1fq+WmfnfgCXqycb4AMAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
