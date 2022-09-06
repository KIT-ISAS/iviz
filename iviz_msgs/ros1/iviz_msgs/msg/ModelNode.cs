/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelNode : IDeserializable<ModelNode>, IHasSerializer<ModelNode>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "parent")] public int Parent;
        [DataMember (Name = "transform")] public Matrix4 Transform;
        [DataMember (Name = "meshes")] public int[] Meshes;
    
        public ModelNode()
        {
            Name = "";
            Transform = new Matrix4();
            Meshes = EmptyArray<int>.Value;
        }
        
        public ModelNode(string Name, int Parent, Matrix4 Transform, int[] Meshes)
        {
            this.Name = Name;
            this.Parent = Parent;
            this.Transform = Transform;
            this.Meshes = Meshes;
        }
        
        public ModelNode(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Parent);
            Transform = new Matrix4(ref b);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<int>.Value
                    : new int[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Meshes = array;
            }
        }
        
        public ModelNode(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.Deserialize(out Parent);
            Transform = new Matrix4(ref b);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<int>.Value
                    : new int[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 4);
                }
                Meshes = array;
            }
        }
        
        public ModelNode RosDeserialize(ref ReadBuffer b) => new ModelNode(ref b);
        
        public ModelNode RosDeserialize(ref ReadBuffer2 b) => new ModelNode(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Parent);
            Transform.RosSerialize(ref b);
            b.SerializeStructArray(Meshes);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Parent);
            Transform.RosSerialize(ref b);
            b.SerializeStructArray(Meshes);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Transform is null) BuiltIns.ThrowNullReference();
            Transform.RosValidate();
            if (Meshes is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 76;
                size += WriteBuffer.GetStringSize(Name);
                size += 4 * Meshes.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size += 4; // Parent
            size += 64; // Transform
            size += 4; // Meshes.Length
            size += 4 * Meshes.Length;
            return size;
        }
    
        public const string MessageType = "iviz_msgs/ModelNode";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f9d8cac4a2655a1f6069aef339ab3144";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5crMKzE2UihILErNK+HyTQTKVJgolBQl5hWn5RflQqSjYxVyU4sz" +
                "Uou5uGypDLh8g92tFDLLMqvic4vTi/WhLuBKy8lPBNlsaAa0W0FZoSi/XCE3MSu/SIGLCwAqJvLSvwAA" +
                "AA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ModelNode> CreateSerializer() => new Serializer();
        public Deserializer<ModelNode> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ModelNode>
        {
            public override void RosSerialize(ModelNode msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ModelNode msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ModelNode msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ModelNode msg) => msg.Ros2MessageLength;
            public override void RosValidate(ModelNode msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ModelNode>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ModelNode msg) => msg = new ModelNode(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ModelNode msg) => msg = new ModelNode(ref b);
        }
    }
}
