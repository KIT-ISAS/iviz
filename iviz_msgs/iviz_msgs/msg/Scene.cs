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
        public Scene(ref Buffer b)
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
                "H4sIAAAAAAAAE71UUWvbMBB+jiD/4SCvg63pGGPQh9KNYUiT0GZ7KSUo9tm+TZaMJDdpf/1OthSnbI9L" +
                "7QedPn93/u7zyTO4htw0rUKP4HLUCEI4b0lXoGWDk8kMlryCKcHXkZIIJSlMpJWlirRUR1BkOlddgQ+P" +
                "QEPkmLYg50OtxhSoHHgDO0zPC7GgqvacoMLqJif8ARHi6j9f4vb++xegJ3rZNq5y76NoMYM7LNGizjGI" +
                "lBrw4NGGBqVz6I8mdZYgCP3Ba/RoINxKJhw+Qmtcb9Ca178YaIkrNjFINVuZ/5ZVn5WV/Rtq6UAOpvEn" +
                "qLHBd1yIHFtXUM7prq8bE4Nih9LmtYDzWxYbFaUy0l/OHy4+PUIDM7Bmz539MvZtNLxyMAzgVNwYZezl" +
                "HGSzI9R+BAoqy86dMLAh5+iJkQ1/585iosTt9PwtRCmiI+0/g53GoErBLgXyDcTEtseB9HX/drZqT8W4" +
                "qTGcy7Tbte2gkY9wIb08v87+hyGSM+tVttzAFXxIwNfs7tvNJlstrxcMXyT4fr0KtPmUM18NzPDYP7cc" +
                "74xRkEvnt66Whdm7f4xPHHmwUle8/Ym5Z0IZzjx5MvoEKshy2GMpi7RGu+VUdVLKdH4Ew312D5PEo4TD" +
                "qOZ5DF9Yyh85YZZsLgYAAA==";
                
    }
}
