/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Scene")]
    public sealed class Scene : IMessage
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
        internal Scene(Buffer b)
        {
            Name = b.DeserializeString();
            Filename = b.DeserializeString();
            Includes = b.DeserializeArray<Include>();
            for (int i = 0; i < this.Includes.Length; i++)
            {
                Includes[i] = new Include(b);
            }
            Lights = b.DeserializeArray<Light>();
            for (int i = 0; i < this.Lights.Length; i++)
            {
                Lights[i] = new Light(b);
            }
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Scene(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
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
                for (int i = 0; i < Includes.Length; i++)
                {
                    size += Includes[i].RosMessageLength;
                }
                for (int i = 0; i < Lights.Length; i++)
                {
                    size += Lights[i].RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Scene";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4996a8817cb9c422e369fada25addda7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71STUvDQBA9d37FQK+Cn4gIHkRFArUtfl1EynQzSVY3u2Vno9Vf7ybdRL1bc3mTmTfD" +
                "e48d4zkqV68MB0ZRbBkBJHhtS7RU82g0xmlEdAWGKlF6QqEN96SZ16W2ZIYmZFaZJuenZ9SbSiJtoiW0" +
                "t2qXsxEMDpfcz3OY6LIKccG0KKMf/E0H4OyPP7i5uz5F/aY/F7WUsptEwxhvuWDPVnErkizyOrBvDZII" +
                "hyGkxmtshT5ETBltCDcUCesjXDnpAppH/M1IF1akXqnsOFnR3atIkDYRxcArrnknrmmJQeVaUWDprqTF" +
                "Vp8weVUBbj+gZAsK4ygcHjztHz9jjWP07h1renH+HzR0zwSg0Tac4HyWTe/xDPfS/2V2e3Vxn82m55PY" +
                "3U/du/msJR38etxpFj5WDEvnDCqSsJCKcvcucOFMdJPromiEe7/oyZYMj6yC84dx6mOlnR3m2lr2i0gy" +
                "3zuuCUPv57MA2H5WnYtktMcy4TIhbV9GCmyIZD1UH0P1CfAFixokHY8EAAA=";
                
    }
}
