/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelMesh : IHasSerializer<ModelMesh>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "vertices")] public GeometryMsgs.Point32[] Vertices;
        [DataMember (Name = "normals")] public GeometryMsgs.Point32[] Normals;
        [DataMember (Name = "tangents")] public GeometryMsgs.Point32[] Tangents;
        [DataMember (Name = "bi_tangents")] public GeometryMsgs.Point32[] BiTangents;
        [DataMember (Name = "tex_coords")] public ModelTexCoords[] TexCoords;
        [DataMember (Name = "color_channels")] public ModelColorChannel[] ColorChannels;
        [DataMember (Name = "faces")] public Triangle[] Faces;
        [DataMember (Name = "material_index")] public uint MaterialIndex;
    
        public ModelMesh()
        {
            Name = "";
            Vertices = EmptyArray<GeometryMsgs.Point32>.Value;
            Normals = EmptyArray<GeometryMsgs.Point32>.Value;
            Tangents = EmptyArray<GeometryMsgs.Point32>.Value;
            BiTangents = EmptyArray<GeometryMsgs.Point32>.Value;
            TexCoords = EmptyArray<ModelTexCoords>.Value;
            ColorChannels = EmptyArray<ModelColorChannel>.Value;
            Faces = EmptyArray<Triangle>.Value;
        }
        
        public ModelMesh(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Vertices = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Normals = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Tangents = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                BiTangents = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ModelTexCoords>.Value
                    : new ModelTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ModelTexCoords(ref b);
                }
                TexCoords = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ModelColorChannel>.Value
                    : new ModelColorChannel[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ModelColorChannel(ref b);
                }
                ColorChannels = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Triangle>.Value
                    : new Triangle[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Faces = array;
            }
            b.Deserialize(out MaterialIndex);
        }
        
        public ModelMesh(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Vertices = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Normals = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Tangents = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Point32>.Value
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                BiTangents = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ModelTexCoords>.Value
                    : new ModelTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ModelTexCoords(ref b);
                }
                TexCoords = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<ModelColorChannel>.Value
                    : new ModelColorChannel[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new ModelColorChannel(ref b);
                }
                ColorChannels = array;
            }
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<Triangle>.Value
                    : new Triangle[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Faces = array;
            }
            b.Deserialize(out MaterialIndex);
        }
        
        public ModelMesh RosDeserialize(ref ReadBuffer b) => new ModelMesh(ref b);
        
        public ModelMesh RosDeserialize(ref ReadBuffer2 b) => new ModelMesh(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(Normals);
            b.SerializeStructArray(Tangents);
            b.SerializeStructArray(BiTangents);
            b.Serialize(TexCoords.Length);
            foreach (var t in TexCoords)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(ColorChannels.Length);
            foreach (var t in ColorChannels)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(Faces);
            b.Serialize(MaterialIndex);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.SerializeStructArray(Vertices);
            b.SerializeStructArray(Normals);
            b.SerializeStructArray(Tangents);
            b.SerializeStructArray(BiTangents);
            b.Serialize(TexCoords.Length);
            foreach (var t in TexCoords)
            {
                t.RosSerialize(ref b);
            }
            b.Serialize(ColorChannels.Length);
            foreach (var t in ColorChannels)
            {
                t.RosSerialize(ref b);
            }
            b.SerializeStructArray(Faces);
            b.Serialize(MaterialIndex);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Vertices is null) BuiltIns.ThrowNullReference();
            if (Normals is null) BuiltIns.ThrowNullReference();
            if (Tangents is null) BuiltIns.ThrowNullReference();
            if (BiTangents is null) BuiltIns.ThrowNullReference();
            if (TexCoords is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < TexCoords.Length; i++)
            {
                if (TexCoords[i] is null) BuiltIns.ThrowNullReference(nameof(TexCoords), i);
                TexCoords[i].RosValidate();
            }
            if (ColorChannels is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                if (ColorChannels[i] is null) BuiltIns.ThrowNullReference(nameof(ColorChannels), i);
                ColorChannels[i].RosValidate();
            }
            if (Faces is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 36;
                size += WriteBuffer.GetStringSize(Name);
                size += 12 * Vertices.Length;
                size += 12 * Normals.Length;
                size += 12 * Tangents.Length;
                size += 12 * BiTangents.Length;
                foreach (var msg in TexCoords) size += msg.RosMessageLength;
                foreach (var msg in ColorChannels) size += msg.RosMessageLength;
                size += 12 * Faces.Length;
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
            size += 4; // Vertices.Length
            size += 12 * Vertices.Length;
            size += 4; // Normals.Length
            size += 12 * Normals.Length;
            size += 4; // Tangents.Length
            size += 12 * Tangents.Length;
            size += 4; // BiTangents.Length
            size += 12 * BiTangents.Length;
            size += 4; // TexCoords.Length
            foreach (var msg in TexCoords) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // ColorChannels.Length
            foreach (var msg in ColorChannels) size = msg.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Faces.Length
            size += 12 * Faces.Length;
            size += 4; // MaterialIndex
            return size;
        }
    
        public const string MessageType = "iviz_msgs/ModelMesh";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a5cea1d5d993b3cd934f9a3c7f16378c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71TTWvcQAy9z68Q9NJASaG9hEBPeyg9BArdWylG9mht0ZmRGc0mu/n10djjpYXurYkv" +
                "I1nS09OXlsxphISR3EgSqeRzF3XUj9+FU/n86ecveKRceCC96pAkRwzX7QXTSKlcd+i5u/g8iKewp9NO" +
                "JHut0XTqhkVZbTsJkncTpkTBzENVu2HV1e0zG1Igsxywkj4uSSBiITOFjpOnk3Nf/vPnHn58vYd/Fuje" +
                "wX5iNaapICeFMhHMolxYEsgB0DTzBE5wyESgszF//8RlAmPec9HqNWcaWC3k5tYQv5m7gv2SGMmThyJw" +
                "VIIlJzxNlMnmVtMo94EMWwuhr0CN1i2A4WzkGlLyuLCyPwY4Z4lSarA1T2bK2HPgcl5Ct8hIqjhSDfGk" +
                "PKaVTMHfBMcZgpnXiiqrBGo5bOUsOkgrrPJRwAKSBvoAqLUTtUkDWkVLgxbOuyBHX3O7QxCsUz1dpPNF" +
                "en6l2fIjP69z/XtFr251W9q3ofPnVbhFaRxMegMOLeNybXeQ2zu2t28vvj6R7f63u8dN6DdhcO4F7pRb" +
                "1vYEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ModelMesh> CreateSerializer() => new Serializer();
        public Deserializer<ModelMesh> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ModelMesh>
        {
            public override void RosSerialize(ModelMesh msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ModelMesh msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ModelMesh msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ModelMesh msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ModelMesh msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ModelMesh>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ModelMesh msg) => msg = new ModelMesh(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ModelMesh msg) => msg = new ModelMesh(ref b);
        }
    }
}
