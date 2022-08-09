/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class TexCoords : IDeserializable<TexCoords>, IMessage
    {
        [DataMember (Name = "coords")] public Vector3f[] Coords;
    
        public TexCoords()
        {
            Coords = System.Array.Empty<Vector3f>();
        }
        
        public TexCoords(Vector3f[] Coords)
        {
            this.Coords = Coords;
        }
        
        public TexCoords(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Coords);
        }
        
        public TexCoords(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Coords);
        }
        
        public TexCoords RosDeserialize(ref ReadBuffer b) => new TexCoords(ref b);
        
        public TexCoords RosDeserialize(ref ReadBuffer2 b) => new TexCoords(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Coords);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Coords);
        }
        
        public void RosValidate()
        {
            if (Coords is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 12 * Coords.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = WriteBuffer2.Align4(c);
            c += 4;  // Coords length
            c += 12 * Coords.Length;
            return c;
        }
    
        public const string MessageType = "iviz_msgs/TexCoords";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8b5f64b5842ecf35ee87d85a0105c1ad";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEwtLTS7JLzJOi45VSM7PL0op5uKypTLg8g12t1LILMusis8tTi/WD4NayZWWk59YYmyk" +
                "UAFnVcJZVVxcANOhs6WbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
