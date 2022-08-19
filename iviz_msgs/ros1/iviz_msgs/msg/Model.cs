/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Model : IDeserializable<Model>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "filename")] public string Filename;
        [DataMember (Name = "orientation_hint")] public string OrientationHint;
        [DataMember (Name = "meshes")] public ModelMesh[] Meshes;
        [DataMember (Name = "materials")] public ModelMaterial[] Materials;
        [DataMember (Name = "nodes")] public ModelNode[] Nodes;
    
        public Model()
        {
            Name = "";
            Filename = "";
            OrientationHint = "";
            Meshes = System.Array.Empty<ModelMesh>();
            Materials = System.Array.Empty<ModelMaterial>();
            Nodes = System.Array.Empty<ModelNode>();
        }
        
        public Model(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeString(out OrientationHint);
            b.DeserializeArray(out Meshes);
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new ModelMesh(ref b);
            }
            b.DeserializeArray(out Materials);
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new ModelMaterial(ref b);
            }
            b.DeserializeArray(out Nodes);
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new ModelNode(ref b);
            }
        }
        
        public Model(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Filename);
            b.DeserializeString(out OrientationHint);
            b.DeserializeArray(out Meshes);
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i] = new ModelMesh(ref b);
            }
            b.DeserializeArray(out Materials);
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = new ModelMaterial(ref b);
            }
            b.DeserializeArray(out Nodes);
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new ModelNode(ref b);
            }
        }
        
        public Model RosDeserialize(ref ReadBuffer b) => new Model(ref b);
        
        public Model RosDeserialize(ref ReadBuffer2 b) => new Model(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(OrientationHint);
            b.Serialize(Meshes.Length);
            foreach (var t in Meshes)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Materials.Length);
            foreach (var t in Materials)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Nodes.Length);
            foreach (var t in Nodes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.Serialize(OrientationHint);
            b.Serialize(Meshes.Length);
            foreach (var t in Meshes)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Materials.Length);
            foreach (var t in Materials)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(Nodes.Length);
            foreach (var t in Nodes)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Filename is null) BuiltIns.ThrowNullReference();
            if (OrientationHint is null) BuiltIns.ThrowNullReference();
            if (Meshes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Meshes.Length; i++)
            {
                if (Meshes[i] is null) BuiltIns.ThrowNullReference(nameof(Meshes), i);
                Meshes[i].RosValidate();
            }
            if (Materials is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Materials.Length; i++)
            {
                if (Materials[i] is null) BuiltIns.ThrowNullReference(nameof(Materials), i);
                Materials[i].RosValidate();
            }
            if (Nodes is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i] is null) BuiltIns.ThrowNullReference(nameof(Nodes), i);
                Nodes[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 24;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Filename);
                size += WriteBuffer.GetStringSize(OrientationHint);
                size += WriteBuffer.GetArraySize(Meshes);
                size += WriteBuffer.GetArraySize(Materials);
                size += WriteBuffer.GetArraySize(Nodes);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.AddLength(c, Filename);
            c = WriteBuffer2.AddLength(c, OrientationHint);
            c = WriteBuffer2.Align4(c);
            c += 4; // Meshes.Length
            foreach (var t in Meshes)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Materials.Length
            foreach (var t in Materials)
            {
                c = t.AddRos2MessageLength(c);
            }
            c = WriteBuffer2.Align4(c);
            c += 4; // Nodes.Length
            foreach (var t in Nodes)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/Model";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bd9be904104258bcedbf207e42db2852";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WS4/bNhC+61cQyCUFijw2myeQg1aS10L1giVvuggKgZZom61ECiTt9ebXZ0hRKo10" +
                "b137YHG+GXK+Gc6QlEpQtkMM98ST43hLO+LKXFDCFFaUs3pPmfJS3pIuJXL//S/Uw4dIC2FFBMWdhu3Q" +
                "ajL4A5TBR3re1//556Xl7RdEj/RH3cudfD3zmyIw0ewI74kSj6NNwSGSd1dA6kiEog3wesqAcdHrSJ7S" +
                "K8x2kKCnDTa0nm0Mt4qcAs5FK/VscqobI4y6gHdcBHvMGNGJbLRYN6MsvQqSynadTuYWa9IH42TOd01Z" +
                "S07PleL/DNB7gao9lcAUioQyidSeoIFLqgsG8S3CIIElogxtBSFIDsD85QNVewTMN1RJbTUI0lAJU357" +
                "BSvGYC4RQLzvSUtapDg6SIKMT/SwJ4LAvmk3km46AmtLRXCrF7K0XiEE60zk7EqsNWWs14YFB8F7rvRk" +
                "SB4fiMAb2lH1aKZOM6HAJd4RPQWKl+7YSEbhfwg6DKgD9RiRZsWQBB9QcjC74zYwzUcirBBnDfkdYakz" +
                "oZPUYIjIJMhwDjp+aLVvb9txrHf1NI8e59GPy7TPXKJPVrUt2svQcbvCM4LlAKMLcLAeTbd9QsJ+d/a7" +
                "sV/8/ESm/p/6Hk+DzTRoLnW+2hPHxn6TRFlYh9HCXycV+orenOF+GMZVfBeB4q13dirbzCLcb/QtM8st" +
                "3W6h4WeZ9BRa/UjmPuDQc9Crs7w59EMtG9zp7ptACfcVZdCivyI1sCBsp/azSpBtRxoFccKydmfhJmzr" +
                "HuKdD251EGQ8tvXoUvVvHVta1X0R1VmeRU6iDRbGi8W6HNPswGURBevEXwF+5eJ+ehNHmd6udy4cpXFZ" +
                "jrt17eLLKL5dauv35zxWqZ+UAH8487mMsziLSq346Crywg/i6h7gT+fUyyLxgygdCX12dYn2m/qFjuss" +
                "3lW0SKKgivNMq85iXmd/ZPk3g195VgFLFHF2Wy9WeVqv75zsTZqyWEYrN3+TIrhP4iyM3BROqpv8TyeD" +
                "EwrBZG4GJ/xfXu8nWnlRp9A1cZHcO5QAhbZxqABQrm+qlR9UDgtAw/guDiOHg7ZM87xa2hWuHTy+zaLQ" +
                "4jODbyu/qPWf499gQeKnhcPBgGm8WuVuJgwaRoGfGBJTgw8Yums8lcZnyWitHoepkHs8DLpfR6PD0T5f" +
                "5p427QcPHcWnM9dc0/oOt/KDwINp0PrwC3K8UHPq1+3ZoTaGM2ChTzQ4KAU9XSMlMJNbeEt60w1q387P" +
                "T3JkMKX1+9sP4Bu9QII/wA78zQU8OX4CDIKj+ggMAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
