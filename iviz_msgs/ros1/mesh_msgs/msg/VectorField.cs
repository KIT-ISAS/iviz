/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class VectorField : IDeserializable<VectorField>, IHasSerializer<VectorField>, IMessage
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
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 24);
                }
                Positions = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Vector3>.Value
                    : new GeometryMsgs.Vector3[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 24);
                }
                Vectors = array;
            }
        }
        
        public VectorField(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point>.Value
                    : new GeometryMsgs.Point[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 24);
                }
                Positions = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Vector3>.Value
                    : new GeometryMsgs.Vector3[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 24);
                }
                Vectors = array;
            }
        }
        
        public VectorField RosDeserialize(ref ReadBuffer b) => new VectorField(ref b);
        
        public VectorField RosDeserialize(ref ReadBuffer2 b) => new VectorField(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Vectors);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Positions);
            b.SerializeStructArray(Vectors);
        }
        
        public void RosValidate()
        {
            if (Positions is null) BuiltIns.ThrowNullReference();
            if (Vectors is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + 24 * Positions.Length + 24 * Vectors.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Positions length
            c = WriteBuffer2.Align8(c);
            c += 24 * Positions.Length;
            c += 4; // Vectors length
            c = WriteBuffer2.Align8(c);
            c += 24 * Vectors.Length;
            return c;
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
            public override int Ros2MessageLength(VectorField msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<VectorField>
        {
            public override void RosDeserialize(ref ReadBuffer b, out VectorField msg) => msg = new VectorField(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out VectorField msg) => msg = new VectorField(ref b);
        }
    }
}
