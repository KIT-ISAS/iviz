/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshTexture")]
    public sealed class MeshTexture : IDeserializable<MeshTexture>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "texture_index")] public uint TextureIndex { get; set; }
        [DataMember (Name = "image")] public SensorMsgs.Image Image { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshTexture()
        {
            Uuid = "";
            Image = new SensorMsgs.Image();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshTexture(string Uuid, uint TextureIndex, SensorMsgs.Image Image)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
            this.Image = Image;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshTexture(ref Buffer b)
        {
            Uuid = b.DeserializeString();
            TextureIndex = b.Deserialize<uint>();
            Image = new SensorMsgs.Image(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshTexture(ref b);
        }
        
        MeshTexture IDeserializable<MeshTexture>.RosDeserialize(ref Buffer b)
        {
            return new MeshTexture(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
            Image.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (Image is null) throw new System.NullReferenceException(nameof(Image));
            Image.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += Image.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshTexture";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "831d05ad895f7916c0c27143f387dfa0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVTW8bNxC9C9B/GMCH2IklB80lMFC0RdO0OgQokNyKQqCWo122XHLND33k1/cNqV1Z" +
                "dd3k0C4syUty3ny8N8Mr+sCxox9SCmaTE8trVC3PZxErrqWcjZ7PsnHpzTeU+JBy4LVxmg84wi76sO5j" +
                "G+9WPazIyPd8Np99+x8/89mHjz/f0xOP89kVfepMpL7GTY13SRkXSTnKrvH9ELDDegztiq5f39LrG4KN" +
                "SpT8sLC8TbALjgP57XRQ0viFlcZqV39OzxWdlpOB16T6gWLns9W0YVLNQzbRJONd2X+EONpfPBPYNqge" +
                "ldWPsPyQTKNs3RKgBr9BPYvkg2mNk4PV4ilSwy7VLL8A9eowWg8e3KNOlDomOOgSwYe8/Htar46XCNrv" +
                "3Vdafr60xAfePQ1WuVKGL0Os6qGpph0HUOP05eoJ6sdSipXb+mfxRnWpGH1jVIKe9iZ150hEd1trmvQs" +
                "hBzdcKd2xgcRX0YPbY1jdNfUXx2X+p6NKnZdvgUEFGviLbncbyqNwe/jZL43GjE9MS/L/2jdeJt7B4Da" +
                "RkyWW6hkp2zmSFsEymghLXNAoYBgb2ssdBWauwK9HrfjshkGAUHhjz7TXlXNoDucVkGbz6gdOd7TaawA" +
                "u1dI6Q8wLHbBx0WOHOL31sQUl9Hn0DBOtbx0nAp3aH4tbc29MpaG4AcfS2gFeAxlKdmcvEzRjwX5aVxA" +
                "8oM5sI20WFDTKefYgmXlsHmLRkJHlv8iIn+GUuFU/cmoSfB9oVdCF+TqPUq9jGts1nz3eHD9vXTdqIC3" +
                "oGe9MS3yNMizUhgBjS+tkqJp77uJ85h4uIjpfbZWZAEuXQs5IIjNMfFJJW9/+71CPbJQTcpgHXwEcyi7" +
                "NW/xfV3wXxad3fyfgz3pWpw6DUUSH0/aAS1JlZhFkB1agQNm9g6EleGLXiy76ThwXE4XAv5QLDS2tUeC" +
                "tND7HoLv++wwCnHTTdN7BBBTVEvRoALGZbYqwABaME7Ol7FR8OUT+SGDQqbVu3tp/shNTgZBHYXzwKpI" +
                "c/WOzkzxAww/7f0C79xe3B+n3iQ+jNeVivfi5mXNcQl4uf3gSBdSsLbGa7wBUxIFD77p6Brh/3pMndw9" +
                "0ONOBaM2aFggY/pbwL4Qoxdg8gwtod+TU86P+BXy7ORrcAXlBCxpLdBS2koJYm5RR5xEw+6MxtnNsaA0" +
                "1uA2QtNsggrH+azclsUpQN6XC+ysfLmtL2fv1OTjOBd1/gVtTQ/yzwgAAA==";
                
    }
}
