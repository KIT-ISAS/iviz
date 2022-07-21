/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexColors : IDeserializableCommon<MeshVertexColors>, IMessageCommon
    {
        // Mesh Attribute Message
        [DataMember (Name = "vertex_colors")] public StdMsgs.ColorRGBA[] VertexColors;
    
        public MeshVertexColors()
        {
            VertexColors = System.Array.Empty<StdMsgs.ColorRGBA>();
        }
        
        public MeshVertexColors(StdMsgs.ColorRGBA[] VertexColors)
        {
            this.VertexColors = VertexColors;
        }
        
        public MeshVertexColors(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out VertexColors);
        }
        
        public MeshVertexColors(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out VertexColors);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexColors(ref b);
        
        public MeshVertexColors RosDeserialize(ref ReadBuffer b) => new MeshVertexColors(ref b);
        
        public MeshVertexColors RosDeserialize(ref ReadBuffer2 b) => new MeshVertexColors(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(VertexColors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(VertexColors);
        }
        
        public void RosValidate()
        {
            if (VertexColors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 4 + 16 * VertexColors.Length;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, VertexColors);
        }
    
        public const string MessageType = "mesh_msgs/MeshVertexColors";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2af51ba6de42b829b6f716360dfdf4d9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O5iktS4nOL04v1nfNz8ouC3J0co2MVylKLSlIr" +
                "4pNBQsVcXLZUBly+we5WCpg2c6Xl5CeWGBspFMFZ6XBWEpyVyMUFAF0TsDnPAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
