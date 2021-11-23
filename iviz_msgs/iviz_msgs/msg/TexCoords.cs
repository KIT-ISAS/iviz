/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class TexCoords : IDeserializable<TexCoords>, IMessage
    {
        [DataMember (Name = "coords")] public Vector3f[] Coords;
    
        /// Constructor for empty message.
        public TexCoords()
        {
            Coords = System.Array.Empty<Vector3f>();
        }
        
        /// Explicit constructor.
        public TexCoords(Vector3f[] Coords)
        {
            this.Coords = Coords;
        }
        
        /// Constructor with buffer.
        internal TexCoords(ref Buffer b)
        {
            Coords = b.DeserializeStructArray<Vector3f>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new TexCoords(ref b);
        
        TexCoords IDeserializable<TexCoords>.RosDeserialize(ref Buffer b) => new TexCoords(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Coords, 0);
        }
        
        public void RosValidate()
        {
            if (Coords is null) throw new System.NullReferenceException(nameof(Coords));
        }
    
        public int RosMessageLength => 4 + 12 * Coords.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/TexCoords";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "8b5f64b5842ecf35ee87d85a0105c1ad";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEwtLTS7JLzJOi45VSM7PL0op5uKypTLg8g12t1LILMusis8tTi/WD4NayZWWk59YYmyk" +
                "UAFnVcJZVVxcANOhs6WbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
