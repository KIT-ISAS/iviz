/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class TexCoords : IDeserializableRos1<TexCoords>, IDeserializableRos2<TexCoords>, IMessageRos1, IMessageRos2
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
        public TexCoords(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Coords);
        }
        
        /// Constructor with buffer.
        public TexCoords(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Coords);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new TexCoords(ref b);
        
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
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Coords);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/TexCoords";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "8b5f64b5842ecf35ee87d85a0105c1ad";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEwtLTS7JLzJOi45VSM7PL0op5uKypTLg8g12t1LILMusis8tTi/WD4NayZWWk59YYmyk" +
                "UAFnVcJZVVxcANOhs6WbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
