/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Mesh : IDeserializable<Mesh>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "vertices")] public GeometryMsgs.Point32[] Vertices;
        [DataMember (Name = "normals")] public GeometryMsgs.Point32[] Normals;
        [DataMember (Name = "tangents")] public GeometryMsgs.Point32[] Tangents;
        [DataMember (Name = "bi_tangents")] public GeometryMsgs.Point32[] BiTangents;
        [DataMember (Name = "tex_coords")] public TexCoords[] TexCoords;
        [DataMember (Name = "color_channels")] public ColorChannel[] ColorChannels;
        [DataMember (Name = "faces")] public Triangle[] Faces;
        [DataMember (Name = "material_index")] public uint MaterialIndex;
    
        public Mesh()
        {
            Name = "";
            Vertices = System.Array.Empty<GeometryMsgs.Point32>();
            Normals = System.Array.Empty<GeometryMsgs.Point32>();
            Tangents = System.Array.Empty<GeometryMsgs.Point32>();
            BiTangents = System.Array.Empty<GeometryMsgs.Point32>();
            TexCoords = System.Array.Empty<TexCoords>();
            ColorChannels = System.Array.Empty<ColorChannel>();
            Faces = System.Array.Empty<Triangle>();
        }
        
        public Mesh(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out Normals);
            b.DeserializeStructArray(out Tangents);
            b.DeserializeStructArray(out BiTangents);
            b.DeserializeArray(out TexCoords);
            for (int i = 0; i < TexCoords.Length; i++)
            {
                TexCoords[i] = new TexCoords(ref b);
            }
            b.DeserializeArray(out ColorChannels);
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                ColorChannels[i] = new ColorChannel(ref b);
            }
            b.DeserializeStructArray(out Faces);
            b.Deserialize(out MaterialIndex);
        }
        
        public Mesh(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeStructArray(out Vertices);
            b.DeserializeStructArray(out Normals);
            b.DeserializeStructArray(out Tangents);
            b.DeserializeStructArray(out BiTangents);
            b.DeserializeArray(out TexCoords);
            for (int i = 0; i < TexCoords.Length; i++)
            {
                TexCoords[i] = new TexCoords(ref b);
            }
            b.Align4();
            b.DeserializeArray(out ColorChannels);
            for (int i = 0; i < ColorChannels.Length; i++)
            {
                ColorChannels[i] = new ColorChannel(ref b);
            }
            b.Align4();
            b.DeserializeStructArray(out Faces);
            b.Deserialize(out MaterialIndex);
        }
        
        public Mesh RosDeserialize(ref ReadBuffer b) => new Mesh(ref b);
        
        public Mesh RosDeserialize(ref ReadBuffer2 b) => new Mesh(ref b);
    
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
            get {
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
    
        public const string MessageType = "iviz_msgs/Mesh";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a5cea1d5d993b3cd934f9a3c7f16378c";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71TO2scQQzu51cI0tgQHHAaE0h1RUgRCNidCYt2V7crPDNaRjr7zr8+mn0cKXJdLtuM" +
                "tNInfXqpFc4DZEwUBpJEVk5N0kE//RTO9vn++Re8UjHuSC86ZCkJ42W7YR4o22WHlpuzzxMddyKl1wqk" +
                "Y9PNSthJlLIbMWeKbumq2nSL7qDCjo/klj1Wqoc5NCQ0clNsOPd0DOHrP/7Cj8dvX+CvZYUP8DSyOtNs" +
                "yFnBRoJJlI0lg+wBXXNP4Az7QgQ6OfObN7YRnHnLptVrKtSxOuT2ziN+d3cF/yUpUU89mMBBCeac8DZS" +
                "IZ9WTaPcRvLYaoR9DbTSugPwOBu5NVLucWblfzzgVCSJVbA3TyYq2HJkO83QDZlIFQeqkJ6Uh7yQMXwh" +
                "OEwQ3bxUVFllUM/hi+boKGthlY8CGkju6COg1k7UJnXoFc0Nmjnvohz6mjvso2Cd6vEsnc7S+5Vmy6/8" +
                "vsz1vJgX13hd1asz+fMWlsNY07v0v9L7gtcbe4CyvsP6tuuL1yeyXf127bgJ7SZ0IfwGBH9cG+IEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
