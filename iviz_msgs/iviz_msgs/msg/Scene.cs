/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Scene")]
    public sealed class Scene : IDeserializable<Scene>, IMessage
    {
        // A complete scene 
        [DataMember (Name = "name")] public string Name { get; set; } // Name of the scene
        [DataMember (Name = "filename")] public string Filename { get; set; } // Original filename
        [DataMember (Name = "includes")] public Include[] Includes { get; set; } // List of models to be included
        [DataMember (Name = "lights")] public Light[] Lights { get; set; } // List of lights
    
        /// <summary> Constructor for empty message. </summary>
        public Scene()
        {
            Name = "";
            Filename = "";
            Includes = System.Array.Empty<Include>();
            Lights = System.Array.Empty<Light>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Scene(string Name, string Filename, Include[] Includes, Light[] Lights)
        {
            this.Name = Name;
            this.Filename = Filename;
            this.Includes = Includes;
            this.Lights = Lights;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Scene(ref Buffer b)
        {
            Name = b.DeserializeString();
            Filename = b.DeserializeString();
            Includes = b.DeserializeArray<Include>();
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(ref b);
            }
            Lights = b.DeserializeArray<Light>();
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new Light(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Scene(ref b);
        }
        
        Scene IDeserializable<Scene>.RosDeserialize(ref Buffer b)
        {
            return new Scene(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Filename);
            b.SerializeArray(Includes, 0);
            b.SerializeArray(Lights, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Filename is null) throw new System.NullReferenceException(nameof(Filename));
            if (Includes is null) throw new System.NullReferenceException(nameof(Includes));
            for (int i = 0; i < Includes.Length; i++)
            {
                if (Includes[i] is null) throw new System.NullReferenceException($"{nameof(Includes)}[{i}]");
                Includes[i].RosValidate();
            }
            if (Lights is null) throw new System.NullReferenceException(nameof(Lights));
            for (int i = 0; i < Lights.Length; i++)
            {
                if (Lights[i] is null) throw new System.NullReferenceException($"{nameof(Lights)}[{i}]");
                Lights[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Filename);
                foreach (var i in Includes)
                {
                    size += i.RosMessageLength;
                }
                foreach (var i in Lights)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Scene";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a45481618525051c846ff1ff1f5e7329";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTW/bMAw9R7+CQK4DtibDMAzooWiHwUCaBG22S1EEsk3b3GTJEOUm7a+fZEtuu12b" +
                "+sIPPZJPz5LmcAGFaTuFDoEL1AhCsLOka9CyxdlsDmtvwVTgmghJgIoUJtDGUk1aqikpMl2ovsS7e6DR" +
                "Yw9bEbvQqzUlKgZnIMe0XooV1Y3zBSpYnr3Ajxkhzt/4E9e3P74BPdDTvuWaP0bSYg43WKFFXWAgKTXg" +
                "0aENG5TM6CaReksQiP70Nmo0Aq6lBxw/Q2d4EGjr7X8ItOQ7ttFJPTtZ/JH1UJVVw4RGMshRNP8LGmzx" +
                "g29E7KUrqfDlPPSNhYExo7RFI+D0ksWNikoZ6ZaLu7Mv99DCHKw5+J39NvZ9OLxScDiAl0b54bLNCbWL" +
                "UUlV1XNaw5aY6QHFzv/d3mJajuHpeQ80RE/afYVk62jzaOXpaaT9TufPNcPw5QIOVD4HDYZrmKK860aK" +
                "/saW0r0Dz+F9EFGX7SZb7+AcPsX4Krv5frnLNuuLlc+exeztdhNAi1evWlxzjx2K3BgFhWS350aW5sD/" +
                "nJR4rMFKXaP4hYUzdhluNTkyekqUZL0XMqmAtEa791XquYnp3ZQTp9crkpvGHyfvcfKehPgLWKgZowUG" +
                "AAA=";
                
    }
}
