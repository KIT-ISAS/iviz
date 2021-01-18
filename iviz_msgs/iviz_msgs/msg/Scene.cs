/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Scene")]
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
        [Preserve] public const string RosMd5Sum = "965a996af3ac75fb8fe896116fd6f8e5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71US2+bQBA+e3/FKFwrtXGqqqrkQ5RUFZJjW4mbSxRZCwwwLeyinSV28us7wIIb5RqH" +
                "yzz2m9e3s0RwCamtmwo9AqdoEJRi78gUYHSNs1kEK5Fgc/BlgIyAnCocQWtHBRldTU4Vm7RqM3x4BBo0" +
                "FtiS2He5apthxeAtJDieZ2pJRekloOokz/7DDx6lFu/8qZu7Xz+AnuhlV3PBn0PTKoJbzNGhSbFrUhvA" +
                "g0fXDaiZ0U8k6STNFmeRcFij8XwG2slAhbFO5hlBrSPopvktMhA5ZLnRAjh8hcZyz+JG5BsEOpKydVDG" +
                "nI1O/+qij4rzvkKpGfTArNxTiTV+kkTEwm9GqYRznzcEdmMxapeWCk7PaxhU5ZXV/mL+cP7tEWqIwNm9" +
                "TPbHuo/p4RWD/ZZe2cq6iznoOiG5wMnOKM9bPp5jTcz0hGori9DKHQdAME/ffWhEtWT8d3BBFkEmQerT" +
                "NzJOPO2hL/viQtKesqNRYvdmRytpmqFFed6Z9h/QZ/8zUYGXzTpebWEBX4J9Hd/+vNrG69XlUrznwXu3" +
                "WXeg+atfYDjzzw2qxNoKUs1+x6XO7J7f7EtYcHDaFKjuMfVynHcPnDxZc/Rk5ETtXGMIGYNuJ3HVMY1t" +
                "/eRTp+ds7G6qf5i050l7UeofDNfUSTcGAAA=";
                
    }
}
