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
            for (int i = 0; i < Includes.Length; i++)
            {
                Includes[i] = new Include(b);
            }
            Lights = b.DeserializeArray<Light>();
            for (int i = 0; i < Lights.Length; i++)
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
                "H4sIAAAAAAAAE71UTW/bMAw9R7+CQK4DtibDMGzooeiGwUCaBG3WS1EEsk3b3GTJEOUm7a+fZEve2l6X" +
                "+sIPPZJPz5LmcAGFaTuFDoEL1AhCsLOka9CyxdlsDmtvwVTgmghJgIoUJtDGUk1aqikpMl2ovsS7e6DR" +
                "Yw9bEbvQqzUlKgZnIMe0XooV1Y3zBSpYnv2DHzNCnP/nT1zd/PgC9EBP+5Zrfh9JizlcY4UWdYGBpNSA" +
                "R4c2bFAyo5tE6i1BIPrT26jRCLiSHnD8CJ3hQaCtt68QaMl3bKOTenay+C3roSqrhgmNZJCjaP4XNNji" +
                "O9+I2EtXUuHLeegbCwNjRmmLRsDpJYsbFZUy0i0Xd2ef7qGFOVhz8Dv7ZezbcHim4HAAL43yw2WbE2oX" +
                "o5Kqque0hi0x0wOKnf+7vcW0HMPT8x5oiJ60+wzJ1tHm0crT00j7nc6fa74O05cLOFDpmhQ0GO5hivKu" +
                "Gzn6K1tK9wZEhwdCRGG2m2y9g3P4EONv2fX3y122WV+sfPYsZm+2mwBaPHvW4pp77FDkxigoJLs9N7I0" +
                "B35xVOK5Bit1jeIWC2fsMlxrcmT0lCjJei9kUgFpjXbvq9TfJqZ3U06cXq9Ibhp/nLzHyXsS4g+K6fFv" +
                "BgYAAA==";
                
    }
}
