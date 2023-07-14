/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexColors : IHasSerializer<MeshVertexColors>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "vertex_colors")] public StdMsgs.ColorRGBA[] VertexColors;
    
        public MeshVertexColors()
        {
            VertexColors = EmptyArray<StdMsgs.ColorRGBA>.Value;
        }
        
        public MeshVertexColors(StdMsgs.ColorRGBA[] VertexColors)
        {
            this.VertexColors = VertexColors;
        }
        
        public MeshVertexColors(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                StdMsgs.ColorRGBA[] array;
                if (n == 0) array = EmptyArray<StdMsgs.ColorRGBA>.Value;
                else
                {
                    array = new StdMsgs.ColorRGBA[n];
                    b.DeserializeStructArray(array);
                }
                VertexColors = array;
            }
        }
        
        public MeshVertexColors(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                StdMsgs.ColorRGBA[] array;
                if (n == 0) array = EmptyArray<StdMsgs.ColorRGBA>.Value;
                else
                {
                    array = new StdMsgs.ColorRGBA[n];
                    b.DeserializeStructArray(array);
                }
                VertexColors = array;
            }
        }
        
        public MeshVertexColors RosDeserialize(ref ReadBuffer b) => new MeshVertexColors(ref b);
        
        public MeshVertexColors RosDeserialize(ref ReadBuffer2 b) => new MeshVertexColors(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(VertexColors.Length);
            b.SerializeStructArray(VertexColors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(VertexColors.Length);
            b.SerializeStructArray(VertexColors);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(VertexColors, nameof(VertexColors));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 16 * VertexColors.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // VertexColors.Length
            size += 16 * VertexColors.Length;
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshVertexColors";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "2af51ba6de42b829b6f716360dfdf4d9";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE1NW8E0tzlBwLCkpykwqLUkFcYsT01O5iktS4nOL04v1nfNz8ouC3J0co2MVylKLSlIr" +
                "4pNBQsVcXLZUBly+we5WCpg2c6Xl5CeWGBspFMFZ6XBWEpyVyMUFAF0TsDnPAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshVertexColors> CreateSerializer() => new Serializer();
        public Deserializer<MeshVertexColors> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshVertexColors>
        {
            public override void RosSerialize(MeshVertexColors msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshVertexColors msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshVertexColors msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshVertexColors msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshVertexColors msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshVertexColors>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshVertexColors msg) => msg = new MeshVertexColors(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshVertexColors msg) => msg = new MeshVertexColors(ref b);
        }
    }
}
