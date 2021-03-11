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
            Name = string.Empty;
            Filename = string.Empty;
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
        
        public void Dispose()
        {
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
        [Preserve] public const string RosMd5Sum = "5e11da295d41bbfbd413d6274556c4a9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/bOBA9R4D/A4FcC+w2SbvdBXpQJDomqi9IcrpBUQi0TNls9QWRStL++h1Sokwj" +
                "14V9MKk3w+GbR87wGrmo7Jq+ZpIhUbKWIccRcuDtAbW0YVdX1yiCEXUVksfZxThUvGbGKR74gbe0XkCH" +
                "tGU97tm374hPMwFuARdSxWq6PasFkh3aMWPfOwE/HCUsqNUoriz/CXFWzuf/+bdywuzhH8Sf+e+iEQfx" +
                "x0x75VyjlFVsYG3JFE/aIvYq2aBypEIwueg0DhwprlsYZ5kmh5CCw+sd6juhNUpgfOPBBg4Rm3liYva0" +
                "/EkPehWp9A5HKhCddINTOLKGvYNAXIB6e17CcqHjzgsVY8HoUB4ddAnV5lRXTlV3VN7efHv/8Ttq0DUa" +
                "uhdI7kc3XIqGlnHljLyVn9B9gCO/8PHa3QY5+oz+PMNd3yc5ecRgeH926x2vq7vh9gbRZsdZK5fvPa+q" +
                "UZzsrOFC8Gdm0kYd6M/lr+V7NzZ9IUpaQ+gFFEfe8pYJ8RYpgAVrD/K4mAZW1ayUkCOEndjvoML2hboK" +
                "Tg53chxUkclpdpEamdM3Kg/zeDD85pFegsusgOGSPyW4iOIIW6etMZ+s19tsOmsLzhLsbQM3BfzGxt3w" +
                "nuBI3ZlbG8YhybLpytzZ+AaTh43y/nDOIw3dIAP449meGxKRCGfK8JdtiBPXI/kTwJ/OqWdJ4Ho4nAj9" +
                "bdsCtW/oJiqvs3xTvA6wl5M4UqaznLfRlyj+qvEbZzZAiIRED8U6jcNi+2ipZyxZssGprZ8xeE8BiXxs" +
                "S2hM9/G/loIGhWQiW0GDn3h9MLTipAihdEkSPFmUAIXatagAkG3v89T1cosFoD55JD62OCjPMI7zzRzh" +
                "zsLJQ4R1V7AZfE3dpFB/1v4a8wI31LLbYEjSNLaV0KiPPTfQJE79HUocHKDAoX+z19lb/urZPG1o36um" +
                "MTmNz8XktzQW3QMqWsrOlF/Xs4FK3rXz98tAe90livEN8nyJ0tTP+coImcREX18jok/S6X5qaYyKWRJP" +
                "R3jWjy11dl1Xo5IKWYgj3Xcv4k1zXjonbQ/MeYTmCeZKvcJcq7Mgez6ozgqQWcLblg0FrKutlj7CmzJj" +
                "ziVkM/yWxxSdjv30svwGKv8BM2DSgL4JAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
