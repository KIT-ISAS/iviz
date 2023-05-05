/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class VectorField : IHasSerializer<VectorField>, IMessage
    {
        [DataMember (Name = "positions")] public GeometryMsgs.Point[] Positions;
        [DataMember (Name = "vectors")] public GeometryMsgs.Vector3[] Vectors;
    
        public VectorField()
        {
            Positions = EmptyArray<GeometryMsgs.Point>.Value;
            Vectors = EmptyArray<GeometryMsgs.Vector3>.Value;
        }
        
        public VectorField(GeometryMsgs.Point[] Positions, GeometryMsgs.Vector3[] Vectors)
        {
            this.Positions = Positions;
            this.Vectors = Vectors;
        }
        
        public VectorField(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.DeserializeStructArray(array);
                }
                Positions = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Vector3[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Vector3>.Value;
                else
                {
                    array = new GeometryMsgs.Vector3[n];
                    b.DeserializeStructArray(array);
                }
                Vectors = array;
            }
        }
        
        public VectorField(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Point[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Point>.Value;
                else
                {
                    array = new GeometryMsgs.Point[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Positions = array;
            }
            {
                int n = b.DeserializeArrayLength();
                GeometryMsgs.Vector3[] array;
                if (n == 0) array = EmptyArray<GeometryMsgs.Vector3>.Value;
                else
                {
                    array = new GeometryMsgs.Vector3[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Vectors = array;
            }
        }
        
        public VectorField RosDeserialize(ref ReadBuffer b) => new VectorField(ref b);
        
        public VectorField RosDeserialize(ref ReadBuffer2 b) => new VectorField(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Positions.Length);
            b.SerializeStructArray(Positions);
            b.Serialize(Vectors.Length);
            b.SerializeStructArray(Vectors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Positions.Length);
            b.Align8();
            b.SerializeStructArray(Positions);
            b.Serialize(Vectors.Length);
            b.Align8();
            b.SerializeStructArray(Vectors);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Positions, nameof(Positions));
            BuiltIns.ThrowIfNull(Vectors, nameof(Vectors));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += 24 * Positions.Length;
                size += 24 * Vectors.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Positions.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * Positions.Length;
            size += 4; // Vectors.Length
            size = WriteBuffer2.Align8(size);
            size += 24 * Vectors.Length;
            return size;
        }
    
        public const string MessageType = "mesh_msgs/VectorField";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "9da8d62df10048ede4a91e419a35679d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71SwUrFQAy871cEvCiUCioeBM/yDoKgeBGRvDbdt9gmZZPns369afusiIIXsad0d2Z2" +
                "ZnYjSUeWh6dOox7fSGJ7eIReNFkS1hC/7N9TZZJPHfEyTRrC5R9/4fr26gLid1vhAO42SaESNkysYBta" +
                "jII0gP7nOEgMTSYC7bGi0LSCdn4Gr8s0LNPb/9jft/YRIFOfSYlN3fLc41fPJTh05UEUhNsBOkKPZfLJ" +
                "dGKdslM9eumqlKmRTAUkg1pIgWXsq8NnlyRWGtnY9y6GYBlZW5xq82WnHFIZywJ2G+IZlTg60BUiMeVU" +
                "QU4x1TPTD+oWMsI+XAHWnMAute3seT7Mr8hFsthEOCph1cAgW9iNgXzIUKPhKLSmxReu29GvFLAdjU8S" +
                "P7wHr0UVI3l3aoR1GX6563c9vu0P6wIAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<VectorField> CreateSerializer() => new Serializer();
        public Deserializer<VectorField> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<VectorField>
        {
            public override void RosSerialize(VectorField msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(VectorField msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(VectorField msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(VectorField msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(VectorField msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<VectorField>
        {
            public override void RosDeserialize(ref ReadBuffer b, out VectorField msg) => msg = new VectorField(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out VectorField msg) => msg = new VectorField(ref b);
        }
    }
}
