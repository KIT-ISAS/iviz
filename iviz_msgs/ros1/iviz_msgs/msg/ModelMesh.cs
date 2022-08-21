/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelMesh : IDeserializable<ModelMesh>, IMessage
    {
        [DataMember(Name = "name")] public string Name;
        [DataMember(Name = "vertices")] public GeometryMsgs.Point32[] Vertices;
        [DataMember(Name = "normals")] public GeometryMsgs.Point32[] Normals;
        [DataMember(Name = "tangents")] public GeometryMsgs.Point32[] Tangents;
        [DataMember(Name = "bi_tangents")] public GeometryMsgs.Point32[] BiTangents;
        [DataMember(Name = "tex_coords")] public ModelTexCoords[] TexCoords;
        [DataMember(Name = "color_channels")] public ModelColorChannel[] ColorChannels;
        [DataMember(Name = "faces")] public Triangle[] Faces;
        [DataMember(Name = "material_index")] public uint MaterialIndex;

        public ModelMesh()
        {
            Name = "";
            Vertices = System.Array.Empty<GeometryMsgs.Point32>();
            Normals = System.Array.Empty<GeometryMsgs.Point32>();
            Tangents = System.Array.Empty<GeometryMsgs.Point32>();
            BiTangents = System.Array.Empty<GeometryMsgs.Point32>();
            TexCoords = System.Array.Empty<ModelTexCoords>();
            ColorChannels = System.Array.Empty<ModelColorChannel>();
            Faces = System.Array.Empty<Triangle>();
        }

        public ModelMesh(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);

            unsafe
            {
                int n = b.DeserializeArrayLength();
                Vertices = n == 0
                    ? System.Array.Empty<GeometryMsgs.Point32>()
                    : new GeometryMsgs.Point32[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(System.Runtime.CompilerServices.Unsafe.AsPointer(ref Vertices[0]), n * 16);
                }
            }

            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out Normals);
            b.DeserializeStructArray(out Tangents);
            b.DeserializeStructArray(out BiTangents);
            {
                int n = b.DeserializeArrayLength();
                TexCoords = n == 0
                    ? System.Array.Empty<ModelTexCoords>()
                    : new ModelTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    TexCoords[i] = new ModelTexCoords(ref b);
                }
            }
            {
                int n = b.DeserializeArrayLength();
                ColorChannels = n == 0
                    ? System.Array.Empty<ModelColorChannel>()
                    : new ModelColorChannel[n];
                for (int i = 0; i < n; i++)
                {
                    ColorChannels[i] = new ModelColorChannel(ref b);
                }
            }
            b.DeserializeStructArray(out Faces);
            b.Deserialize(out MaterialIndex);
        }

        public ModelMesh(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out Normals);
            b.DeserializeStructArray(out Tangents);
            b.DeserializeStructArray(out BiTangents);
            {
                int n = b.DeserializeArrayLength();
                TexCoords = n == 0
                    ? System.Array.Empty<ModelTexCoords>()
                    : new ModelTexCoords[n];
                for (int i = 0; i < n; i++)
                {
                    TexCoords[i] = new ModelTexCoords(ref b);
                }
            }
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                ColorChannels = n == 0
                    ? System.Array.Empty<ModelColorChannel>()
                    : new ModelColorChannel[n];
                for (int i = 0; i < n; i++)
                {
                    ColorChannels[i] = new ModelColorChannel(ref b);
                }
            }
            b.Align4();
            b.DeserializeStructArray(out Faces);
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
                size += WriteBuffer.GetArraySize(TexCoords);
                size += WriteBuffer.GetArraySize(ColorChannels);
                size += 12 * Faces.Length;
                return size;
            }
        }

        public int Ros2MessageLength => AddRos2MessageLength(0);

        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Name);
            c = WriteBuffer2.Align4(c);
            c += 4; // Vertices length
            c += 12 * Vertices.Length;
            c += 4; // Normals length
            c += 12 * Normals.Length;
            c += 4; // Tangents length
            c += 12 * Tangents.Length;
            c += 4; // BiTangents length
            c += 12 * BiTangents.Length;
            c += 4; // TexCoords.Length
            foreach (var t in TexCoords)
            {
                c = t.AddRos2MessageLength(c);
            }

            c = WriteBuffer2.Align4(c);
            c += 4; // ColorChannels.Length
            foreach (var t in ColorChannels)
            {
                c = t.AddRos2MessageLength(c);
            }

            c = WriteBuffer2.Align4(c);
            c += 4; // Faces length
            c += 12 * Faces.Length;
            c += 4; // MaterialIndex
            return c;
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
    }
}